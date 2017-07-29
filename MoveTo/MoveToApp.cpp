#include "stdafx.h"

#include <algorithm>

#pragma comment(lib,"shlwapi.lib")


using std::wstring;
using std::vector;
using std::find;
using std::set;
#include "../../MyUtility/CommandLineParser.h"
using Ambiesoft::COption;
using Ambiesoft::CCommandLineParser;



#include "ChooseDirDialog.h"
#include "MoveToApp.h"

#define SEC_OPTION		L"option"
#define KEY_DIRS		L"Dirs"

void ErrorExit(LPCWSTR pMessage)
{
	MessageBox(NULL,
		pMessage,
		APPNAME,
		MB_ICONERROR);

	exit(1);
}


//int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
//	_In_opt_ HINSTANCE hPrevInstance,
//	_In_ LPWSTR    lpCmdLine,
//	_In_ int       nCmdShow)
BOOL CMoveToApp::InitInstance()
{
	//UNREFERENCED_PARAMETER(hPrevInstance);
	//UNREFERENCED_PARAMETER(lpCmdLine);
	CWinApp::InitInstance();

	COption opTarget(L"/T", L"/t", 1);
	COption opFile(L"");
	CCommandLineParser cmd;
	cmd.AddOption(&opTarget);
	cmd.AddOption(&opFile);
	cmd.Parse();

	wstring destDir = opTarget.getFirstValue();
	wstring sourcefile = opFile.getFirstValue();
	if (sourcefile.empty())
	{
		ErrorExit(I18N(L"Source file is empty."));
	}
	if (!PathFileExists(sourcefile.c_str()))
	{
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" does not exist."), sourcefile.c_str());
		ErrorExit(msg.c_str());
	}


	wstring dbFile = stdwin32::stdCombinePath(
		stdwin32::stdGetParentDirectory(stdwin32::stdGetModuleFileName()),
		APPNAME L".db");
	vector<wstring> allPrevSave;

	if (!sqlGetPrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allPrevSave, dbFile.c_str()))
	{
		ErrorExit(I18N(L"Failed to load from db."));
	}

	vector<wstring> allSaving;

	if (destDir.empty())
	{
		if (allPrevSave.empty())
		{
			TCHAR szFolder[MAX_PATH];
			if (!browseFolder(NULL, I18N(L"Move to"), szFolder))
				return 0;
			if (!PathIsDirectory(szFolder))
			{
				wstring msg = stdwin32::string_format(I18N(L"\"%s\" is not a folder."), szFolder);
				ErrorExit(msg.c_str());
			}
			destDir = szFolder;
		}
		else
		{
			CChooseDirDialog dlg;
			dlg.m_strSource = sourcefile.c_str();
			set<wstring> dupcheck;
			for each(wstring s in allPrevSave)
			{
				if (dupcheck.find(s) == dupcheck.end())
				{
					dlg.m_arDirs.Add(s.c_str());
					dupcheck.insert(s);
				}
			}
			if(IDOK != dlg.DoModal())
				return FALSE;

			destDir = dlg.m_strDirResult;

			for (int i = 0; i < dlg.m_arDirs.GetSize(); ++i)
			{
				allSaving.push_back(wstring(dlg.m_arDirs[i]));
			}
		}
	}

	if (destDir.empty() || !stdwin32::stdIsFullPath(destDir.c_str()))
	{
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is empty or not full path."), destDir.c_str());
		ErrorExit(msg.c_str());
	}

	if (!PathIsDirectory(destDir.c_str()))
	{
		if (PathFileExists(destDir.c_str()))
		{
			wstring msg = stdwin32::string_format(I18N(L"\"%s\" is a file."), destDir.c_str());
			ErrorExit(msg.c_str());
		}

		wstring msg = stdwin32::string_format(I18N(L"\"%s\" does not exist. Do you want to create a new folder?"), destDir.c_str());
		if (IDYES != MessageBox(NULL,
			msg.c_str(),
			APPNAME,
			MB_ICONQUESTION | MB_YESNO))
		{
			return FALSE;
		}

		CreateDirectory(destDir.c_str(), NULL);
	}

	// final check
	if (!PathIsDirectory(destDir.c_str()))
	{
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is not a folder."), destDir.c_str());
		ErrorExit(msg.c_str());
	}


	destDir = stdwin32::stdAddBackSlash(destDir);
#ifdef _DEBUG
	{
		wstring msg = L"SOURCE: " + sourcefile +L"\r\n";
		msg.append(L"DEST: " + destDir);
		MessageBox(NULL, msg.c_str(), L"DEBUG", MB_OK);
	}
#endif
	if (!SHMoveFile(destDir.c_str(), sourcefile.c_str()))
		return FALSE;


	vector<wstring>::iterator cIter = find(allSaving.begin(), allSaving.end(), destDir);
	if (cIter == allSaving.end())
		allSaving.push_back(destDir);
	
	if (!sqlWritePrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allSaving, dbFile.c_str()))
	{
		ErrorExit(I18N(L"Failed to save to db."));
	}

	return FALSE;
}




CMoveToApp::CMoveToApp()
{
}


CMoveToApp::~CMoveToApp()
{
}


CMoveToApp theApp;

