#include "StdAfx.h"
#include "../../lsMisc/CHandle.h"

#include "resource.h"

using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;
using namespace std;

#define APPNAME L"SwapFilename"

void ErrorExit(const wstring& error, LPCTSTR funcname)
{
	MessageBox(nullptr,
		stdFormat(L"%s\n%s", error.c_str(), funcname).c_str(),
		APPNAME,
		MB_ICONERROR);

	exit(1);
}
void ErrorExit(const wstring& error)
{
	ErrorExit(error, L"");
}

BOOL DoReplaceFilePlane(wstring file1, wstring file2, wstring fileback)
{
	int ret;
	
	ret = SHMoveFileEx(file1.c_str(), fileback.c_str());
	if (ret != 0)
	{
		ErrorExit(GetLastErrorString(ret), L"SHMoveFile");
	}
	ret = SHMoveFileEx(file2.c_str(), file1.c_str());
	if (ret != 0)
	{
		ErrorExit(GetLastErrorString(ret), L"SHMoveFile");
	}
	ret = SHMoveFileEx(fileback.c_str(), file2.c_str());
	if (ret != 0)
	{
		ErrorExit(GetLastErrorString(ret), L"SHMoveFile");
	}
	return TRUE;
}

enum ReplaceTransactionResult {
	RTR_TRNAS_NA,
	RTR_TRUE,
	RTR_FALSE,
};
ReplaceTransactionResult DoReplaceFileWithTransaction(wstring file1, wstring file2, wstring fileback)
{
	CAtlTransactionManager trans(FALSE);
	if (!trans.GetHandle())
		return RTR_TRNAS_NA;

	auto failfunc = [&]() {
		DVERIFY(trans.Rollback());
		DVERIFY(trans.Close());
	};
	if (!trans.MoveFile(file1.c_str(), fileback.c_str()))
	{
		failfunc();
		return RTR_FALSE;
	}
	if (!trans.MoveFile(file2.c_str(), file1.c_str()))
	{
		failfunc();
		return RTR_FALSE;
	}
	if (!trans.MoveFile(fileback.c_str(), file2.c_str()))
	{
		failfunc();
		return RTR_FALSE;
	}

	return trans.Commit() ? RTR_TRUE : RTR_FALSE;
}
BOOL DoReplaceFileWithReplaceFile(wstring file1, wstring file2, wstring fileback)
{
	if (!ReplaceFile(
		file1.c_str(),
		file2.c_str(),
		fileback.c_str(),
		0, 0, 0 // flags and reserved
		))
	{
		// ErrorExit(GetLastErrorString(GetLastError()), L"ReplaceFile");
		// Use regular method
		return FALSE;
	}
	if (PathFileExists(file2.c_str()))
	{
		ErrorExit(I18N(L"Unknown Error"), L"");
	}
	if (!MoveFile(fileback.c_str(), file2.c_str()))
	{
		ErrorExit(GetLastErrorString(GetLastError()), L"MoveFile");
	}
	return TRUE;
}

static wstring GetOlderFile(const wstring& file1, const wstring& file2, bool* pIsSame)
{
	Ambiesoft::CHandle h1(CreateFile(file1.c_str(),
		GENERIC_READ,
		FILE_SHARE_READ,
		NULL,
		OPEN_EXISTING,
		FILE_ATTRIBUTE_NORMAL,
		NULL));
	Ambiesoft::CHandle h2(CreateFile(file2.c_str(),
		GENERIC_READ,
		FILE_SHARE_READ,
		NULL,
		OPEN_EXISTING,
		FILE_ATTRIBUTE_NORMAL,
		NULL));
	if (!h1 || !h2)
		return wstring();

	FILETIME ft1, ft2;
	if (!GetFileTime(h1, NULL, NULL, &ft1) || !GetFileTime(h2, NULL, NULL, &ft2))
		return wstring();

	LONG result = CompareFileTime(&ft1, &ft2);
	*pIsSame = (result == 0);
	return result < 0 ? file1 : file2;
}
int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	i18nInitLangmap(NULL, NULL, APPNAME);
	Ambiesoft::InitHighDPISupport();

	CCommandLineParser parser(L"Swap filename of two files");

	//bool bRetry = false;
	//parser.AddOption(L"-retry", 0, &bRetry, ArgEncodingFlags::ArgEncodingFlags_Default,
	//	L"Retry if failed");

	bool bRemoveOld = false;
	parser.AddOption(L"-removeold", 0, &bRemoveOld, 
		ArgEncodingFlags::ArgEncodingFlags_Default,
		L"Remove an older file after swapping");

	bool bAlwaysYes = false;
	parser.AddOption(L"-alwaysyes", 0, &bAlwaysYes,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		L"Answer 'yes' in all questions");

	bool bHelp = false;
	parser.AddOptionRange({ L"-h",L"--help", L"/?" },
		0,
		&bHelp,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		L"Show Help");

	bool bShowVersion = false;
	parser.AddOptionRange({ L"-v",L"--version" },
		0,
		&bShowVersion,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		L"Show Version");

	COption opMain(L"", ExactCount::Exact_2, ArgEncodingFlags_Default, L"Input two files");
	parser.AddOption(&opMain);
	
	parser.Parse();

	if (parser.hadUnknownOption())
	{
		ErrorExit(I18N(L"Unknown Option:") + (L"\r\n" + parser.getUnknowOptionStrings()));
	}
	if (bShowVersion || bHelp || opMain.getValueCount() != 2)
	{
		MessageBox(NULL,
			parser.getHelpMessage().c_str(),
			stdFormat(L"%s v%s", APPNAME, GetVersionString(NULL, 3).c_str()).c_str(), 
			MB_ICONINFORMATION);
		return 0;
	}
	
	wstring file1 = opMain.getValue(0);
	wstring file2 = opMain.getValue(1);
	wstring fileback = GetBackupFile(file1.c_str());
#define STR_FILE_DOES_NOT_EXIST L"'%s' does not exist."
	if (!PathFileExists(file1.c_str()))
	{
		ErrorExit(stdFormat(I18N(STR_FILE_DOES_NOT_EXIST), file1.c_str()));
	}
	if (!PathFileExists(file2.c_str()))
	{
		ErrorExit(stdFormat(I18N(STR_FILE_DOES_NOT_EXIST), file2.c_str()));
	}
	if (PathFileExists(fileback.c_str()))
	{
		ErrorExit(stdFormat(I18N(L"'%s' does exist."), fileback.c_str()));
	}

	file1 = stdGetFullPathName(file1);
	file2 = stdGetFullPathName(file2);
	
	if (PathIsUNC(file1.c_str()) || PathIsUNC(file2.c_str()))
	{
		// Transaction does not support UNC
		if (!DoReplaceFileWithReplaceFile(file1, file2, fileback))
		{
			DoReplaceFilePlane(file1, file2, fileback);
		}
	}
	else
	{
		switch(DoReplaceFileWithTransaction(file1, file2, fileback))
		{
		case RTR_TRNAS_NA:
			// transaction is not available
			DoReplaceFilePlane(file1, file2, fileback);
		case RTR_FALSE:
			ErrorExit(GetLastErrorString(GetLastError()));
		}
	}

	wstring additionalMessage;
	bool bShowRemoveConfirmMessage = !bAlwaysYes;
	if (GetKeyState(VK_CONTROL) < 0)
	{
		bRemoveOld = true;
		bShowRemoveConfirmMessage = true;
	}
	if (bRemoveOld)
	{
		do
		{
			bool isSame = false;
			wstring olderfile = GetOlderFile(file1, file2, &isSame);
			if (isSame)
			{
				additionalMessage = I18N(L"File time is same.");
				break;
			}
			if (olderfile.empty())
			{
				ErrorExit(I18N(L"Failed to find an older file."));
			}
			if (bShowRemoveConfirmMessage)
			{
				if (IDYES != MessageBox(NULL,
					stdFormat(I18N(L"Do you want to remove '%s'?"), olderfile.c_str()).c_str(),
					stdFormat(I18N(L"Remove older file | %s"), APPNAME).c_str(),
					MB_ICONQUESTION | MB_YESNO | MB_DEFBUTTON2))
				{
					break;
				}
			}
			int nRet = SHDeleteFileEx(olderfile.c_str());
			if (nRet != 0)
			{
				ErrorExit(GetLastErrorString(nRet));
			}
			additionalMessage = stdFormat(I18N(L"'%s' has been removed."), stdGetFileName(olderfile).c_str());
		} while (false);
	}
	showballoon(NULL,
		APP_NAME,
		stdFormat(I18N(L"'%s' and '%s' have been swapped."), stdGetFileName(file1).c_str(), stdGetFileName(file2).c_str()) + 
			(additionalMessage.empty() ? L"" : L"\r\n" + additionalMessage),
		LoadIcon(hInstance, MAKEINTRESOURCE(IDI_ICON_MAIN)),
		5000,
		1,
		FALSE,
		1);
	return 0;
}