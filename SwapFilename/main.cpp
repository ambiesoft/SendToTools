#include "StdAfx.h"
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
int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	Ambiesoft::InitHighDPISupport();

	CCommandLineParser parser(L"Swap filename of two files");

	//bool bRetry = false;
	//parser.AddOption(L"-retry", 0, &bRetry, ArgEncodingFlags::ArgEncodingFlags_Default,
	//	L"Retry if failed");

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

	if (!PathFileExists(file1.c_str()))
	{
		ErrorExit(stdFormat(I18N(L"%s does not exist."), file1.c_str()), L"PathFileExists");
	}
	if (!PathFileExists(file2.c_str()))
	{
		ErrorExit(stdFormat(I18N(L"%s does not exist."), file2.c_str()), L"PathFileExists");
	}
	if (PathFileExists(fileback.c_str()))
	{
		ErrorExit(stdFormat(I18N(L"%s does exist."), fileback.c_str()), L"PathFileExists");
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

	showballoon(NULL,
		APP_NAME,
		stdFormat(I18N(L"'%s' and '%s' are swapped."), stdGetFileName(file1).c_str(), stdGetFileName(file2).c_str()),
		LoadIcon(hInstance, MAKEINTRESOURCE(IDI_ICON_MAIN)),
		5000,
		1,
		FALSE,
		1);
	return 0;
}