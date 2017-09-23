#include <Windows.h>

#include <string>

#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/Is64.h"
#include "../../lsMisc/OpenCommon.h"
#include "../../lsMisc/CommandLineString.h"

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

	wstring argToProg;
	CCommandLineStringBase<wchar_t> cls;
	int index = cls.getIndex(L"--");
	if (index >= 0)
	{
		argToProg = cls.subString(index+1);
	}
	else
	{
		int argc = 0;
		wchar_t** argv = NULL;
		//argv = CommandLineToArgvW(GetCommandLine(), &argc);
		argv = CCommandLineString::getCommandLine(GetCommandLine(), &argc);
		if (argc >= 5)
		{
			argToProg = cls.subString(5);
		}
	}
	wstring exe;
	if (Is64BitWindows())
		exe = exe64;
	else
		exe = exe32;

	OpenCommon(NULL, exe.c_str(), argToProg.c_str());
	return 0;
}