#include "StdAfx.h"
#include <atlstr.h>
#include <atltransactionmanager.h>
#include "resource.h"

#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/HighDPI.h"
#include "../../lsMisc/GetLastErrorString.h"
#include "../../lsMisc/GetBackupFile.h"
#include "../../lsMisc/showballoon.h"

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


BOOL DoReplaceFilePlane(wstring file1, wstring file2, wstring fileback)
{
	if (!MoveFile(file1.c_str(), fileback.c_str()))
	{
		ErrorExit(GetLastErrorString(GetLastError()), L"MoveFile");
	}
	if (!MoveFile(file2.c_str(), file1.c_str()))
	{
		ErrorExit(GetLastErrorString(GetLastError()), L"MoveFile");
	}
	if (!MoveFile(fileback.c_str(), file2.c_str()))
	{
		ErrorExit(GetLastErrorString(GetLastError()), L"MoveFile");
	}
	return TRUE;
}
BOOL DoReplaceFile(wstring file1, wstring file2, wstring fileback)
{
	CAtlTransactionManager trans;
	if (!trans.GetHandle())
		return FALSE;

	if (!trans.MoveFile(file1.c_str(), fileback.c_str()))
	{
		ErrorExit(GetLastErrorString(GetLastError()), L"MoveFile");
	}
	if (!trans.MoveFile(file2.c_str(), file1.c_str()))
	{
		ErrorExit(GetLastErrorString(GetLastError()), L"MoveFile");
	}
	if (!trans.MoveFile(fileback.c_str(), file2.c_str()))
	{
		ErrorExit(GetLastErrorString(GetLastError()), L"MoveFile");
	}

	return trans.Commit();
}
BOOL DoReplaceFile_obsolete(wstring file1, wstring file2, wstring fileback)
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
	
	COption opMain(L"", ExactCount::Exact_2, ArgEncodingFlags_Default, L"Input two files");

	parser.AddOption(&opMain);
	
	parser.Parse();

	if (opMain.getValueCount() != 2)
	{
		MessageBox(NULL,
			parser.getHelpMessage().c_str(),
			APPNAME,
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
	
	//wstring dir1 = stdGetParentDirectory(file1);
	//wstring dir2 = stdGetParentDirectory(file2);

	//wstring tempfilenameOrig = stdCombinePath(dir1, L"3_v-jz");
	//wstring tempfilename = tempfilenameOrig;
	//int i = 1;
	//while (PathFileExists(tempfilename.c_str()))
	//{
	//	tempfilename = tempfilenameOrig + stdItoT(i++);
	//}

	if (!DoReplaceFile(file1, file2, fileback))
		DoReplaceFilePlane(file1, file2, fileback);

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