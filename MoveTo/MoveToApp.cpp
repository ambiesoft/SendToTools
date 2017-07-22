#include "stdafx.h"

#include <algorithm>

#pragma comment(lib,"shlwapi.lib")


using std::wstring;
using std::vector;
using std::find;

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

	COption opTarget(L"/T", 1);
	COption opFile(L"");
	CCommandLineParser cmd;
	cmd.AddOption(&opTarget);
	cmd.AddOption(&opFile);
	cmd.Parse();

	wstring mainArg = opTarget.getFirstValue();
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
	vector<wstring> allsave;

	if (!sqlGetPrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allsave, dbFile.c_str()))
	{
		ErrorExit(I18N(L"Failed to load from db."));
	}

	if (!PathIsDirectory(mainArg.c_str()))
	{
		if (allsave.empty())
		{
			TCHAR szFolder[MAX_PATH];
			if (!browseFolder(NULL, I18N(L"Move to"), szFolder))
				return 0;
			if (!PathIsDirectory(szFolder))
			{
				wstring msg = stdwin32::string_format(I18N(L"\"%s\" is not a folder."), szFolder);
				ErrorExit(msg.c_str());
			}
			mainArg = szFolder;
		}
		else
		{
			CChooseDirDialog dlg;
			dlg.m_strSource = sourcefile.c_str();
			for each(wstring s in allsave)
				dlg.m_arDirs.Add(s.c_str());
			if(IDOK != dlg.DoModal())
				return FALSE;

			mainArg = dlg.m_strDirResult;
		}
	}

	if (mainArg.empty() || !stdwin32::stdIsFullPath(mainArg.c_str()))
	{
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is empty or not full path."), mainArg.c_str());
		ErrorExit(msg.c_str());
	}

	if (!PathIsDirectory(mainArg.c_str()))
	{
		if (PathFileExists(mainArg.c_str()))
		{
			wstring msg = stdwin32::string_format(I18N(L"\"%s\" is a file."), mainArg.c_str());
			ErrorExit(msg.c_str());
		}

		wstring msg = stdwin32::string_format(I18N(L"\"%s\" does not exist. Do you want to create a new folder?"), mainArg.c_str());
		if (IDYES != MessageBox(NULL,
			msg.c_str(),
			APPNAME,
			MB_ICONQUESTION | MB_YESNO))
		{
			return FALSE;
		}

		CreateDirectory(mainArg.c_str(), NULL);
	}

	// final check
	if (!PathIsDirectory(mainArg.c_str()))
	{
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is not a folder."), mainArg.c_str());
		ErrorExit(msg.c_str());
	}


	mainArg = stdwin32::stdAddBackSlash(mainArg);
#ifdef _DEBUG
	{
		wstring msg = L"SOURCE: " + sourcefile +L"\r\n";
		msg.append(L"DEST: " + mainArg);
		MessageBox(NULL, msg.c_str(), L"DEBUG", MB_OK);
	}
#endif
	if (!SHMoveFile(mainArg.c_str(), sourcefile.c_str()))
		return FALSE;


	vector< wstring >::iterator cIter = find(allsave.begin(), allsave.end(), mainArg);
	if (cIter == allsave.end())
		allsave.push_back(mainArg);
	
	if (!sqlWritePrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allsave, dbFile.c_str()))
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

