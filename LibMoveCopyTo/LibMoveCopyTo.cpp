//BSD 2-Clause License
//
//Copyright (c) 2017, Ambiesoft
//All rights reserved.
//
//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:
//
//* Redistributions of source code must retain the above copyright notice, this
//  list of conditions and the following disclaimer.
//
//* Redistributions in binary form must reproduce the above copyright notice,
//  this list of conditions and the following disclaimer in the documentation
//  and/or other materials provided with the distribution.
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
//AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
//FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
//DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
//CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
//OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
//OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


#include "stdafx.h"

#include "LibMoveCopyToLib.h"
#include "LibMoveCopyTo.h"

#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/GetLastErrorString.h"
#include "ChooseDirDialog.h"
#include "ReleaseMutex.h"
#include "WaitingDialog.h"

#include "../../lsMisc/DebugNew.h"

#pragma comment(lib,"shlwapi.lib")


//using std::wstring;
//using std::vector;
//using std::find;
//using std::set;
//
//using Ambiesoft::ArgCount;
//using Ambiesoft::COption;
//using Ambiesoft::CCommandLineParser;
//using Ambiesoft::sqlGetPrivateProfileStringArray;
//using Ambiesoft::sqlWritePrivateProfileStringArray;
//using Ambiesoft::sqlGetPrivateProfileInt;
//using Ambiesoft::sqlWritePrivateProfileInt;
//using Ambiesoft::SHMoveFile;
//using Ambiesoft::GetLastErrorStringW;
//using Ambiesoft::GetSHFileOpErrorString;
//using Ambiesoft::I18N;


using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;
using namespace stdwin32;
using namespace std;

typedef vector<wstring> STRINGVECTOR;






#define SEC_OPTION L"option"
#define KEY_DIRS L"Dirs"
#define KEY_PRIORITY L"Priority"

extern CLibMoveCopyToApp theApp;;

wstring getHelpString()
{
	wstring ret;
	
	ret.append(I18N(L"Usage")).append(L":\r\n");
	ret.append(stdGetFileName(stdGetModuleFileName<wchar_t>()));
	ret.append(L" [/t TARGETDIR] [/p priority] [/lang LANG] SOURCE1 [SOURCE2]...\r\n");
	ret.append(L"\r\n");
	ret.append(L"  priority\r\n");
	ret.append(L"    0:Hight, 1:Normal, 2:Low, 3:Background");
	ret.append(L"\r\n");
	ret.append(L"  lang\r\n");
	ret.append(L"    jpn,enu");

	return ret;
}
void ShowError(LPCWSTR pMessage)
{
	wstring message(pMessage);
	message.append(L"\r\n\r\n");
	message.append(getHelpString());

	MessageBox(NULL,
		message.c_str(),
		gAppName,
		MB_ICONERROR);
}
void ShowError(wstring message)
{
	ShowError(message.c_str());
}

void showHelp()
{
	MessageBox(NULL,
		getHelpString().c_str(),
		gAppName,
		MB_ICONINFORMATION);
}


struct WaitingDialogData
{
	HANDLE hFinishEvent_;
	WORD wResult_;
	WaitingDialogData() : hFinishEvent_(nullptr), wResult_(0){}
};

UINT __cdecl MyControllingFunction(LPVOID pParam)
{
	WaitingDialogData* pData = (WaitingDialogData*)pParam;
	CWaitingDialog waitingDlg;
	waitingDlg.m_hWait = pData->hFinishEvent_;

	pData->wResult_ = waitingDlg.DoModal();
	
	return 0;
}
int libmain(LPCWSTR pAppName, HICON hIcon)
{
	//UNREFERENCED_PARAMETER(hPrevInstance);
	//UNREFERENCED_PARAMETER(lpCmdLine);
	
	Ambiesoft::i18nInitLangmap(theApp.m_hInstance, nullptr, L"LibMoveCopyTo");
	

	gAppName = pAppName;
	if (_wcsicmp(gAppName, L"MoveTo") == 0)
		gOperationName = L"Move";
	else if (_wcsicmp(gAppName, L"CopyTo") == 0)
		gOperationName = L"Copy";
	else
	{
		ShowError(stdFormat(I18N(L"Unknown app (%s)"), gAppName));
		return 1;
	}

	
	COption opTarget(L"/T", L"/t", 1);
	COption opFile(L"", Ambiesoft::ArgCount::ArgCount_Infinite);
	wstring lang;
	bool bHelp = false;
	int nPriority = -1;
	CCommandLineParser cmd;
	cmd.AddOption(&opTarget);
	cmd.AddOption(&opFile);
	cmd.AddOption(L"/p", 1, &nPriority);
	cmd.AddOption(L"/lang", 1, &lang);
	cmd.AddOption( L"/h", L"-h" , 0, &bHelp);

	cmd.Parse();

	if (!lang.empty())
		Ambiesoft::i18nInitLangmap(theApp.m_hInstance, lang.c_str(), L"LibMoveCopyTo");

	if (bHelp)
	{
		showHelp();
		return 0;
	}
	wstring destDir = opTarget.getFirstValue();
	STRINGVECTOR sourcefiles;
	for (unsigned int i = 0; i < opFile.getValueCount(); ++i)
	{
		sourcefiles.push_back(opFile.getValue(i));
	}
	if (sourcefiles.empty())
	{
		ShowError(I18N(L"Source file is empty."));
		return 1;
	}

	for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
	{
		if (!PathFileExists(it->c_str()))
		{
			// wstring msg = stdwin32::string_format(I18N(L"\"%s\" does not exist."), it->c_str());
			wstring msg = stdFormat(I18N(L"\"%s\" does not exist."), it->c_str());
			ShowError(msg.c_str());
			return 1;
		}
	}


	wstring dbFile = stdCombinePath(
		stdGetParentDirectory(stdGetModuleFileName<wchar_t>()),
		wstring(gAppName) + L".db");


	STRINGVECTOR allSaving;
	if (!sqlGetPrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allSaving, dbFile.c_str()))
	{
		ShowError(I18N(L"Failed to load from db."));
		return 1;
	}
	
	if (nPriority == -1)
		nPriority = sqlGetPrivateProfileInt(SEC_OPTION, KEY_PRIORITY, -1, dbFile.c_str());
	
	
	if (destDir.empty())
	{
		//if (allPrevSave.empty())
		//{
		//	TCHAR szFolder[MAX_PATH];
		//	if (!browseFolder(NULL, I18N(L"Move to"), szFolder))
		//		return 0;
		//	if (!PathIsDirectory(szFolder))
		//	{
		//		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is not a folder."), szFolder);
		//		ShowError(msg.c_str());
		//		return 1;
		//	}
		//	destDir = szFolder;
		//}
		//else
		{
			CChooseDirDialog dlg(hIcon);
			for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
			{
				dlg.m_strSource += it->c_str();
				dlg.m_strSource += L"\r\n";
			}
			
			set<wstring> dupcheck;
			for each(wstring s in allSaving)
			{
				if (dupcheck.find(s) == dupcheck.end())
				{
					dlg.m_arDirs.Add(s.c_str());
					dupcheck.insert(s);
				}
			}

			dlg.m_nCmbPriority = nPriority;

			if (IDOK != dlg.DoModal())
				return 0;

			
			nPriority = dlg.m_nCmbPriority;
			destDir = dlg.m_strDirResult;
			
			// reset allSaving
			allSaving.clear();
			for (int i = 0; i < dlg.m_arDirs.GetSize(); ++i)
			{
				wstring t(dlg.m_arDirs[i]);
				t = stdAddBackSlash(t);
				allSaving.emplace_back(t);
			}


			// save all
			vector<wstring>::iterator cIter = find(allSaving.begin(), allSaving.end(), destDir);
			if (cIter == allSaving.end())
				allSaving.push_back(destDir);

			bool failed = false;
			
			failed |= !sqlWritePrivateProfileInt(SEC_OPTION, KEY_PRIORITY, nPriority, dbFile.c_str());
			failed |= !sqlWritePrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allSaving, dbFile.c_str());
			if (failed)
				ShowError(I18N(L"Failed to save to db."));

		}
	}

	if (destDir.empty() || !stdIsFullPath(destDir.c_str()))
	{
		wstring msg = stdFormat(I18N(L"\"%s\" is empty or not full path."), destDir.c_str());
		ShowError(msg.c_str());
		return 1;
	}

	if (!PathIsDirectory(destDir.c_str()))
	{
		if (PathFileExists(destDir.c_str()))
		{
			wstring msg = stdFormat(I18N(L"\"%s\" is a file."), destDir.c_str());
			ShowError(msg.c_str());
			return 1;
		}

		wstring msg = stdFormat(I18N(L"\"%s\" does not exist. Do you want to create a new folder?"), destDir.c_str());
		if (IDYES != MessageBox(NULL,
			msg.c_str(),
			gAppName,
			MB_ICONQUESTION | MB_YESNO))
		{
			return 1;
		}

		CreateDirectory(destDir.c_str(), NULL);
	}

	// final check
	if (!PathIsDirectory(destDir.c_str()))
	{
		wstring msg = stdFormat(I18N(L"\"%s\" is not a folder."), destDir.c_str());
		ShowError(msg.c_str());
		return 1;
	}


	destDir = stdwin32::stdAddBackSlash(destDir);

#ifdef _DEBUG
	bool bShowDebugInfo = !true;
#else
	bool bShowDebugInfo = (GetAsyncKeyState(VK_CONTROL) < 0) && (GetAsyncKeyState(VK_SHIFT) < 0);
#endif

	if(bShowDebugInfo)
	{
		wstring msg = L"SOURCE: \r\n";
		for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
		{
			msg += *it;
			msg += L"\r\n";
		}

		msg.append(L"\r\n");
		msg.append(L"DEST: \r\n" + destDir);
		MessageBox(NULL, msg.c_str(), L"DEBUG", MB_OK);
	}

	if (CChooseDirDialog::IsValidPriority(nPriority))
	{
		if (!SetPriorityClass(GetCurrentProcess(), CChooseDirDialog::GetPriorityValue(nPriority)))
		{
			AfxMessageBox(I18N(L"Failed to set priority class."));
		}
	}

	CReleaseMutex mutex(L"LibMoveCopyTo_Mutex", TRUE);
	if (!mutex)
	{
		AfxMessageBox(I18N(L"Failed to create a mutex."));
		return 1;
	}
	if (mutex.WillWait())
	{
		WaitingDialogData wdd;
		CEvent eventDialogWait(FALSE, TRUE);
		wdd.hFinishEvent_ = eventDialogWait;
		// HANDLE eventDialogWait = CreateEvent(NULL, TRUE, FALSE, NULL);
		CWinThread* pThread = AfxBeginThread(MyControllingFunction, &wdd);
		mutex.Wait();    // wait other app
		SetEvent(eventDialogWait);  // let dialog finish
		WaitForSingleObject(*pThread, INFINITE);
		if (wdd.wResult_ == IDCANCEL)
			return 0;
	}
	else
	{
		// AfxMessageBox(L"waiting");
		mutex.Wait();
	}

	int nRet = -1;
	if (_wcsicmp(gAppName, L"MoveTo") == 0)
		nRet = SHMoveFile(destDir.c_str(), sourcefiles);
	else if (_wcsicmp(gAppName,  L"CopyTo") == 0)
		nRet = SHCopyFile(destDir.c_str(), sourcefiles);
	else
	{
		ShowError(stdFormat(I18N(L"Unknown app (%s)"), gAppName));
		return 1;
	}

	if (nRet != 0 && nRet != 1223 /* user cancel*/ )
	{
		wstring error = GetSHFileOpErrorString(nRet);
		error += L"\n";
		error += L"Source:\n";
		for (auto&& s : sourcefiles)
		{
			error += L"  ";
			error += s;
			error += L"\n";
		}
		error += L"Destination:\n";
		error += L"  ";
		error += destDir;
		error += L"\n";
		ShowError(error.c_str());
	}




	 
	return 0;
}















// CLibMoveCopyToApp

BEGIN_MESSAGE_MAP(CLibMoveCopyToApp, CWinApp)
END_MESSAGE_MAP()


// CLibMoveCopyToApp

CLibMoveCopyToApp::CLibMoveCopyToApp()
{
}


CLibMoveCopyToApp theApp;

BOOL CLibMoveCopyToApp::InitInstance()
{
	CWinApp::InitInstance();

	return TRUE;
}

