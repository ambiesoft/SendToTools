#include "StdAfx.h"


#include "../../lsMisc/CommandLineParser.h"

using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;
using namespace std;

#define APPNAME L"SwapFilename"

void ErrorExit(const wstring& error)
{
	MessageBox(nullptr,
		error.c_str(),
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

	if (!PathFileExists(file1.c_str()))
	{
		ErrorExit(stdFormat(I18N(L"%s does not exist."), file1));
	}
	if (!PathFileExists(file2.c_str()))
	{
		ErrorExit(stdFormat(I18N(L"%s does not exist."), file2));
	}

	file1 = stdGetFullPathName(file1);
	file2 = stdGetFullPathName(file2);

	wstring dir1 = stdGetParentDirectory(file1);
	wstring dir2 = stdGetParentDirectory(file2);

	wstring tempfilenameOrig = stdCombinePath(dir1, L"3_v-jz");
	wstring tempfilename = tempfilenameOrig;
	int i = 1;
	while (PathFileExists(tempfilename.c_str()))
	{
		tempfilename = tempfilenameOrig + stdItoT(i++);
	}
	// tukare
	return 0;
}