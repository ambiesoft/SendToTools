#include "stdafx.h"


#pragma comment(lib,"shlwapi.lib")


using std::wstring;
using std::vector;

using Ambiesoft::COption;
using Ambiesoft::CCommandLineParser;

#include "../../MyUtility/browseFolder.h"
#include "../../MyUtility/SHMoveFile.h"
#include "../../MyUtility/sqliteserialize.h"

#include "ChooseDirDialog.h"

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

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

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
		wstring msg = stdwin32::string_formatW(I18N(L"\"%s\" does not exist."), sourcefile.c_str());
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
				wstring msg = stdwin32::string_formatW(I18N(L"\"%s\" is not a folder."), szFolder);
				ErrorExit(msg.c_str());
			}
			mainArg = szFolder;
		}
		else
		{
			CChooseDirDialog dlg;
			dlg.DoModal();
		}
	}

	mainArg = stdwin32::stdAddBackSlash(mainArg);
	if (!SHMoveFile(mainArg.c_str(), sourcefile.c_str()))
		return 1;



	allsave.push_back(mainArg);
	if (!sqlWritePrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allsave, dbFile.c_str()))
	{
		ErrorExit(I18N(L"Failed to save to db."));
	}
	return 0;
}