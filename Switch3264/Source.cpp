#include <Windows.h>

#include <string>

#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/Is64.h"
#include "../../lsMisc/OpenCommon.h"

using namespace std;
using namespace Ambiesoft;

#define I18N(s) (s)
#define APPNAME L"Switch3264"

void ErrorExit(LPCWSTR message)
{
	MessageBox(NULL,
		message,
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

	CCommandLineParser parser;

	wstring exe32, exe64;
	parser.AddOption(L"-32", 1, &exe32);
	parser.AddOption(L"-64", 1, &exe64);

	parser.Parse();

	if (exe32.empty() || exe64.empty())
	{
		ErrorExit(I18N(L"-32 and -64 must be specified."));
	}

	wstring exe;
	if (Is64BitWindows())
		exe = exe64;
	else
		exe = exe32;

	OpenCommon(NULL, exe.c_str());
	return 0;
}