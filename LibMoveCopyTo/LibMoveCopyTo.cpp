f//BSD 2-Clause License
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
#include "../../lsMisc/RevealFolder.h"
#include "../../lsMisc/GetUnreparsePath.h"
#include "../../lsMisc/GetLocalPathFromNetPath.h"
#include "../../lsMisc/OpenCommon.h"

#include "ChooseDirDialog.h"
#include "ReleaseMutex.h"
#include "WaitingDialog.h"

#include "../../lsMisc/DebugNew.h"

#pragma comment(lib,"shlwapi.lib")

using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;

using namespace std;

typedef vector<wstring> STRINGVECTOR;

#define SEC_OPTION L"option"
#define KEY_DIRS L"Dirs"
#define KEY_PRIORITY L"Priority"
#define KEY_OPEN_AFTER_OPERATION L"OpenAfterOperation"
#define KEY_OPEN_FOLDER_AFTER_OPERATION L"OpenFolderAfterOperation"
extern CLibMoveCopyToApp theApp;;

void ShowError(wstring message)
{
	MessageBox(NULL,
		message.c_str(),
		gAppName,
		MB_ICONERROR);
}
void ShowError(const CCommandLineParser& cmd, wstring message)
{
	message.append(L"\r\n\r\n");
	message.append(cmd.getHelpMessage());

	ShowError(message);
}

void showHelp(const CCommandLineParser& cmd)
{
	MessageBox(NULL,
		cmd.getHelpMessage().c_str(),
		gAppName,
		MB_ICONINFORMATION);
}


struct WaitingDialogData
{
	HANDLE hFinishEvent_;
	WORD wResult_;
	CString strAppName_;
	CString strFrom_;
	CString strTo_;
	bool bStartNow_ = false;
	WaitingDialogData(CString strAppName, CString strFrom, CString strTo) : 
		hFinishEvent_(nullptr), 
		wResult_(0),
		strAppName_(strAppName),
		strFrom_(strFrom),
		strTo_(strTo) {}

	CString appName() const {
		return strAppName_;
	}
	CString from() const {
		return strFrom_;
	}
	CString to() const {
		return strTo_;
	}
};

UINT __cdecl MyControllingFunction(LPVOID pParam)
{
	WaitingDialogData* pData = (WaitingDialogData*)pParam;
	CWaitingDialog waitingDlg;
	waitingDlg.m_hWait = pData->hFinishEvent_;
	waitingDlg.m_strAppName = pData->appName();
	waitingDlg.m_strFrom = pData->from();
	waitingDlg.m_strTo = pData->to();
	pData->wResult_ = waitingDlg.DoModal();
	if (pData->wResult_ == IDC_BUTTON_STARTNOW)
		pData->bStartNow_ = true;
	return 0;
}

static const wchar_t* availableLanguages[] = {
	L"enu",
	L"jpn",
};

void initLanuage()
{
	wstring lang;

	CCommandLineParser cmd;
	cmd.AddOption({ L"/lang" },
		ExactCount::Exact_1,
		&lang,
		ArgEncodingFlags_Default,
		I18N(L"Launguage 'jpn' or 'enu'"));
	cmd.Parse();

	if (lang.empty())
	{
		Ambiesoft::i18nInitLangmap(theApp.m_hInstance, nullptr, L"LibMoveCopyTo");
	}
	else
	{
		bool langOk = false;
		for (auto&& availableLang : availableLanguages)
		{
			if (availableLang == lang)
			{
				langOk = true;
				break;
			}
		}
		if (langOk)
		{
			Ambiesoft::i18nInitLangmap(theApp.m_hInstance, lang.c_str(), L"LibMoveCopyTo");
		}
		else
		{
			Ambiesoft::i18nInitLangmap(theApp.m_hInstance, nullptr, L"LibMoveCopyTo");
		}
	}
}

int libmain(LPCWSTR pAppName, LPCWSTR pButtonText, HICON hIcon)
{
	gAppName = pAppName;

	initLanuage();

	if (_wcsicmp(gAppName, L"MoveTo") == 0)
		;
	else if (_wcsicmp(gAppName, L"CopyTo") == 0)
		;
	else
	{
		ShowError(stdFormat(I18N(L"Unknown app (%s)"), gAppName));
		return 1;
	}

	COption opTarget({ L"/T", L"/t" },
		ArgCount::ArgCount_One,
		ArgEncodingFlags_Default,
		I18N(L"Target directory"));
	COption opFile({ L"" },
		Ambiesoft::ArgCount::ArgCount_OneToInfinite,
		ArgEncodingFlags_Default,
		I18N(L"Source files"));
	wstring lang;
	bool bHelp = false;
	int nPriority = -1;
	bool bOpenAfterOperation = false;
	bool bOpenFolderAfterOperation = false;
	
	CCommandLineParser cmd;
	cmd.setStrict();

	cmd.AddOption(&opTarget);
	cmd.AddOption(&opFile);
	cmd.AddOption({ L"/p" },
		ExactCount::Exact_1,
		&nPriority,
		ArgEncodingFlags_Default,
		I18N(L"Priority 0:Hight, 1:Normal, 2:Low, 3:Background"));
	cmd.AddOption({ L"/lang" },
		ExactCount::Exact_1,
		&lang,
		ArgEncodingFlags_Default,
		I18N(L"Launguage 'jpn' or 'enu'"));
	bool bUnrS = false;
	cmd.AddOption({ L"/unrs" },
		ArgCount::ArgCount_Zero,
		&bUnrS,
		ArgEncodingFlags_Default,
		I18N(L"Resolve source's reparse point before opration"));
	bool bUnNetpath = false;
	cmd.AddOption({ L"/unnet" },
		ArgCount::ArgCount_Zero,
		&bUnNetpath,
		ArgEncodingFlags_Default,
		I18N(L"Resolve source's network path to local path before opration"));
	cmd.AddOption({ L"/openafter" }, 
		ArgCount::ArgCount_Zero, 
		&bOpenAfterOperation,
		ArgEncodingFlags_Default,
		I18N(L"Open files after operation"));
	cmd.AddOption({ L"/openfolderafter" },
		ArgCount::ArgCount_Zero,
		&bOpenFolderAfterOperation,
		ArgEncodingFlags_Default,
		I18N(L"Open folders after operation"));
	cmd.AddOption({ L"/h", L"-h", L"/?"}, ArgCount::ArgCount_Zero, &bHelp);

	cmd.Parse();

	if (cmd.hadUnknownOption())
	{
		ShowError(cmd,
			wstring() + I18N(L"Unknown Option:") + L"\r\n" + cmd.getUnknowOptionStrings());
		return 1;
	}
	
	{
		bool langOk = false;
		for (auto&& availableLang : availableLanguages)
		{
			if (availableLang == lang)
			{
				langOk = true;
				break;
			}
		}
		if (!langOk)
		{
			ShowError(cmd,I18N(L"Unknown Language:") + lang);
			return false;
		}
	}

	if (bHelp)
	{
		showHelp(cmd);
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
		ShowError(cmd, I18N(L"Source file is empty."));
		return 1;
	}

	for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
	{
		if (!PathFileExists(it->c_str()))
		{
			wstring msg = stdFormat(I18N(L"\"%s\" does not exist."), it->c_str());
			ShowError(cmd, msg.c_str());
			return 1;
		}
	}

	const wstring dbFile = stdCombinePath(
		stdGetParentDirectory(stdGetModuleFileName<wchar_t>()),
		wstring(gAppName) + L".db");

	STRINGVECTOR allSaving;
	if (!sqlGetPrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allSaving, dbFile.c_str()))
	{
		ShowError(cmd, I18N(L"Failed to load from db."));
		return 1;
	}

	std::for_each(allSaving.begin(), allSaving.end(), [](wstring &s)
	{
		s = stdAddPathSeparator(s);
	});

	if (nPriority == -1)
		nPriority = sqlGetPrivateProfileInt(SEC_OPTION, KEY_PRIORITY, -1, dbFile.c_str());
	
	if (!bOpenAfterOperation)
		bOpenAfterOperation = !!sqlGetPrivateProfileInt(SEC_OPTION, KEY_OPEN_AFTER_OPERATION, 0, dbFile.c_str());
	if (!bOpenFolderAfterOperation)
		bOpenFolderAfterOperation = !!sqlGetPrivateProfileInt(SEC_OPTION, KEY_OPEN_FOLDER_AFTER_OPERATION, 0, dbFile.c_str());

	if (destDir.empty())
	{
		CChooseDirDialog dlg(pButtonText, hIcon);
		for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
		{
			dlg.m_strSource += it->c_str();
			dlg.m_strSource += L"\r\n";
		}

		set<wstring> dupcheck;
		for each (wstring s in allSaving)
		{
			if (dupcheck.find(s) == dupcheck.end())
			{
				dlg.m_arDirs.Add(s.c_str());
				dupcheck.insert(s);
			}
		}

		dlg.m_nCmbPriority = nPriority;
		dlg.m_bOpenAfterOperation = bOpenAfterOperation ? 1 : 0;
		dlg.m_bOpenFolderAfterOperation = bOpenFolderAfterOperation ? 1 : 0;

		if (IDOK != dlg.DoModal())
			return 0;

		nPriority = dlg.m_nCmbPriority;
		bOpenAfterOperation = !!dlg.m_bOpenAfterOperation;
		bOpenFolderAfterOperation = !!dlg.m_bOpenFolderAfterOperation;
		destDir = dlg.m_strDirResult;

		// reset allSaving
		allSaving.clear();
		for (int i = 0; i < dlg.m_arDirs.GetSize(); ++i)
		{
			wstring t(dlg.m_arDirs[i]);
			t = stdAddPathSeparator(t);
			allSaving.emplace_back(t);
		}

		// save all
		vector<wstring>::iterator cIter = find(allSaving.begin(), allSaving.end(), destDir);
		if (cIter == allSaving.end())
			allSaving.push_back(destDir);

		bool failed = false;

		failed |= !sqlWritePrivateProfileInt(SEC_OPTION, KEY_PRIORITY, nPriority, dbFile.c_str());
		failed |= !sqlWritePrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allSaving, dbFile.c_str());
		failed |= !sqlWritePrivateProfileInt(SEC_OPTION, KEY_OPEN_AFTER_OPERATION, bOpenAfterOperation ? 1:0, dbFile.c_str());
		failed |= !sqlWritePrivateProfileInt(SEC_OPTION, KEY_OPEN_FOLDER_AFTER_OPERATION, bOpenFolderAfterOperation?1:0, dbFile.c_str());
		if (failed)
			ShowError(cmd, I18N(L"Failed to save to db."));
	}

	if (destDir.empty() || !stdIsFullPath(destDir.c_str()))
	{
		wstring msg = stdFormat(I18N(L"\"%s\" is empty or not full path."), destDir.c_str());
		ShowError(cmd, msg.c_str());
		return 1;
	}

	if (!PathIsDirectory(destDir.c_str()))
	{
		if (PathFileExists(destDir.c_str()))
		{
			wstring msg = stdFormat(I18N(L"\"%s\" is a file."), destDir.c_str());
			ShowError(cmd, msg.c_str());
			return 1;
		}

		if (PathIsUNC(destDir.c_str()))
			RevealFolder(destDir.c_str());

		if (!PathIsDirectory(destDir.c_str()))
		{
			wstring msg = stdFormat(I18N(L"\"%s\" does not exist. Do you want to create a new folder?"), destDir.c_str());
			if (IDYES != MessageBox(NULL,
				msg.c_str(),
				gAppName,
				MB_ICONQUESTION | MB_YESNO))
			{
				return 1;
			}

			if (!CreateDirectory(destDir.c_str(), NULL))
			{
				wstring error = GetLastErrorString(GetLastError());
				wstring msg = stdFormat(I18N(L"Failed to create folder '%s'\n%s"), 
					destDir.c_str(),
					error.c_str());
				ShowError(cmd, msg.c_str());
				return 1;
			}
		}
	}

	// final check
	if (!PathIsDirectory(destDir.c_str()))
	{
		wstring msg = stdFormat(I18N(L"\"%s\" is not a folder."), destDir.c_str());
		ShowError(cmd, msg.c_str());
		return 1;
	}

	destDir = stdAddPathSeparator(destDir);

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
		CString strFrom, strTo;
		for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
		{
			strFrom += stdAddDQIfNecessary(*it).c_str();
			strFrom += L" ";
		}
		strFrom = stdTrim(wstring((LPCTSTR)strFrom)).c_str();
		strTo = destDir.c_str();

		WaitingDialogData wdd(pAppName, strFrom, strTo);
		CEvent eventDialogWait(FALSE, TRUE);
		wdd.hFinishEvent_ = eventDialogWait;

		CWinThread* pThread = AfxBeginThread(MyControllingFunction, &wdd);

		HANDLE wh[] = { *pThread, mutex };
		DWORD dwWait = WaitForMultipleObjects(_countof(wh), wh, FALSE, INFINITE);
		if ((WAIT_OBJECT_0 + 0) == dwWait || (WAIT_ABANDONED_0 + 0) == dwWait)
		{ 
			// thread signaled or abandoned
			if (wdd.wResult_ == IDCANCEL)
				return 0;
			else if (wdd.wResult_ == IDC_BUTTON_STARTNOW)
				;  // go through
			else
				ASSERT(false);
		}
		else if ((WAIT_OBJECT_0 + 1) == dwWait || (WAIT_ABANDONED_0 + 1) == dwWait)
		{
			// mutex signaled or abandoned
			SetEvent(eventDialogWait);  // let dialog finish
			WaitForSingleObject(*pThread, INFINITE); // wait dialog
		}
	}
	else
	{
		// AfxMessageBox(L"waiting");
		mutex.Wait();
	}

	if (bUnrS)
		sourcefiles = GetUnreparsePath(sourcefiles);
	if (bUnNetpath)
		sourcefiles = GetLocalPathFromNetPath(sourcefiles);
	int nRet = -1;
	if (_wcsicmp(gAppName, L"MoveTo") == 0)
		nRet = SHMoveFileEx(sourcefiles, destDir.c_str());
	else if (_wcsicmp(gAppName,  L"CopyTo") == 0)
		nRet = SHCopyFileEx(sourcefiles, destDir.c_str());
	else
	{
		ShowError(cmd, stdFormat(I18N(L"Unknown app (%s)"), gAppName));
		return 1;
	}

	if (nRet != 0 && nRet != 1223 /* user cancel*/ )
	{
		wstring error = GetSHFileOpErrorString(nRet);
		error += L"\n\n";
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
		ShowError(cmd, error.c_str());
	}
	else if (nRet == 0)
	{
		if (bOpenAfterOperation || bOpenFolderAfterOperation)
		{
			for (auto&& sourceFile : sourcefiles)
			{
				wstring destFull = stdCombinePath(destDir, stdGetFileName(sourceFile));
				if (bOpenAfterOperation)
					OpenCommon(nullptr, destFull.c_str());
				if (bOpenFolderAfterOperation)
					OpenFolder(nullptr, destFull.c_str());
			}
		}
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
