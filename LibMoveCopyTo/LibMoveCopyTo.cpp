
#include "stdafx.h"

#include "LibMoveCopyTo.h"

#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/GetLastErrorString.h"
#include "ChooseDirDialog.h"

#include "../../lsMisc/DebugNew.h"

#pragma comment(lib,"shlwapi.lib")


using std::wstring;
using std::vector;
using std::find;
using std::set;

using Ambiesoft::ArgCount;
using Ambiesoft::COption;
using Ambiesoft::CCommandLineParser;
using Ambiesoft::sqlGetPrivateProfileStringArray;
using Ambiesoft::sqlWritePrivateProfileStringArray;
using Ambiesoft::SHMoveFile;
using Ambiesoft::GetLastErrorStringW;
using Ambiesoft::GetSHFileOpErrorString;

using stdwin32::stdAddBackSlash;

typedef vector<wstring> STRINGVECTOR;






#define SEC_OPTION L"option"
#define KEY_DIRS L"Dirs"

wstring getHelpString()
{
	wstring ret;
	ret.append(I18N(L"Usage")).append(L":\r\n");
	ret.append(L"/t TARGETDIR SOURCE1 [SOURCE2]...\r\n");

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





extern "C" {
	DllExport int libmain(LPCWSTR pAppName);
}

int libmain(LPCWSTR pAppName)
{
	//UNREFERENCED_PARAMETER(hPrevInstance);
	//UNREFERENCED_PARAMETER(lpCmdLine);
	gAppName = pAppName;

	COption opTarget(L"/T", L"/t", 1);
	COption opFile(L"", Ambiesoft::ArgCount_Infinite);
	CCommandLineParser cmd;
	cmd.AddOption(&opTarget);
	cmd.AddOption(&opFile);
	cmd.Parse();

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
			wstring msg = stdwin32::string_format(I18N(L"\"%s\" does not exist."), it->c_str());
			ShowError(msg.c_str());
			return 1;
		}
	}


	wstring dbFile = stdwin32::stdCombinePath(
		stdwin32::stdGetParentDirectory(stdwin32::stdGetModuleFileName()),
		wstring(gAppName) + L".db");
	vector<wstring> allPrevSave;

	if (!sqlGetPrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allPrevSave, dbFile.c_str()))
	{
		ShowError(I18N(L"Failed to load from db."));
		return 1;
	}

	STRINGVECTOR allSaving;

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
			CChooseDirDialog dlg;
			for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
			{
				dlg.m_strSource += it->c_str();
				dlg.m_strSource += L"\r\n";
			}
			
			set<wstring> dupcheck;
			for each(wstring s in allPrevSave)
			{
				if (dupcheck.find(s) == dupcheck.end())
				{
					dlg.m_arDirs.Add(s.c_str());
					dupcheck.insert(s);
				}
			}
			if (IDOK != dlg.DoModal())
				return 0;

			destDir = dlg.m_strDirResult;

			for (int i = 0; i < dlg.m_arDirs.GetSize(); ++i)
			{
				wstring t(dlg.m_arDirs[i]);
				t=stdAddBackSlash(t);
				
				allSaving.push_back(t);
			}
		}
	}

	if (destDir.empty() || !stdwin32::stdIsFullPath(destDir.c_str()))
	{
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is empty or not full path."), destDir.c_str());
		ShowError(msg.c_str());
		return 1;
	}

	if (!PathIsDirectory(destDir.c_str()))
	{
		if (PathFileExists(destDir.c_str()))
		{
			wstring msg = stdwin32::string_format(I18N(L"\"%s\" is a file."), destDir.c_str());
			ShowError(msg.c_str());
			return 1;
		}

		wstring msg = stdwin32::string_format(I18N(L"\"%s\" does not exist. Do you want to create a new folder?"), destDir.c_str());
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
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is not a folder."), destDir.c_str());
		ShowError(msg.c_str());
		return 1;
	}


	destDir = stdwin32::stdAddBackSlash(destDir);

#ifdef _DEBUG
	bool bShowDebugInfo = true;
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

	int nRet = 0;
	if (!SHMoveFile(destDir.c_str(), sourcefiles, &nRet))
	{
		wstring error = GetSHFileOpErrorString(nRet);
		ShowError(error.c_str());
	}


	vector<wstring>::iterator cIter = find(allSaving.begin(), allSaving.end(), destDir);
	if (cIter == allSaving.end())
		allSaving.push_back(destDir);

	if (!sqlWritePrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allSaving, dbFile.c_str()))
	{
		ShowError(I18N(L"Failed to save to db."));
		return 1;
	}


	return 0;
}















// CLibMoveCopyToApp

BEGIN_MESSAGE_MAP(CLibMoveCopyToApp, CWinApp)
END_MESSAGE_MAP()


// CLibMoveCopyToApp コンストラクション

CLibMoveCopyToApp::CLibMoveCopyToApp()
{
	// TODO: この位置に構築用コードを追加してください。
	// ここに InitInstance 中の重要な初期化処理をすべて記述してください。
}


// 唯一の CLibMoveCopyToApp オブジェクトです。

CLibMoveCopyToApp theApp;


// CLibMoveCopyToApp 初期化

BOOL CLibMoveCopyToApp::InitInstance()
{
	CWinApp::InitInstance();

	return TRUE;
}

