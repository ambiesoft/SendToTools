//BSD 2-Clause License
//
//Copyright (c) 2017, Ambiesoft
//All rights reserved.
//
//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:
//
//* Redistributions of source code must retain the above copyright notice, this
//  list of conditions and the following disclaimer.
//
//* Redistributions in binary form must reproduce the above copyright notice,
//  this list of conditions and the following disclaimer in the documentation
//  and/or other materials provided with the distribution.
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
//AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
//FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
//DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
//CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
//OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
//OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

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
#define OPTION_ARGS_TOPASS L"--args-topass"

wstring getErrorMessage(CCommandLineParser& parser)
{
	wstring shown;
	shown += parser.getHelpMessage();
	shown += L"\r\n";
	shown += L"Arguments to the launching program should be placed after ";
	shown += OPTION_ARGS_TOPASS;

	return shown;
}
void ErrorExit(const wstring& message, CCommandLineParser& parser)
{
	wstring shown;
	if (!message.empty())
	{
		shown += message;
		shown += L"\r\n--------------------------";

		shown += L"\r\n\r\n";
		
	}
	shown += getErrorMessage(parser).c_str();
	MessageBox(NULL,
		shown.c_str(),
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

	CCommandLineParser parser(CaseFlags_Default, L"Execute 32bit app or 64 bit app determined by execution environment");

	wstring exe32, exe64;
	bool bHelp = false;
	parser.AddOption(L"-32", 1, &exe32, ArgEncodingFlags_Default, L"Specify 32bit executable");
	parser.AddOption(L"-64", 1, &exe64, ArgEncodingFlags_Default, L"Specify 64bit executable");
	
	vector<wstring> helpoptions{ L"-h", L"-?", L"--help" };
	parser.AddOption(helpoptions.begin(), helpoptions.end(),
		0,
		&bHelp,
		ArgEncodingFlags_Default,
		L"Show help");
	
	parser.Parse();

	if (bHelp)
	{
		MessageBox(NULL,
			getErrorMessage(parser).c_str(),
			APPNAME,
			MB_ICONINFORMATION);
		return 0;
	}
	if (exe32.empty() || exe64.empty())
	{
		ErrorExit(I18N(L"-32 and -64 must be specified."),parser);
	}

	wstring argToProg;
	CCommandLineStringBase<wchar_t> cls;
	int index = cls.getIndex(OPTION_ARGS_TOPASS);
	if (index >= 0)
	{
		argToProg = cls.subString(index+1);
	}
	else
	{
		int argc = 0;
		wchar_t** argv = NULL;
		//argv = CommandLineToArgvW(GetCommandLine(), &argc);
		argv = CCommandLineString::getCommandLineArg(GetCommandLine(), &argc);
		if (argc >= 5)
		{
			argToProg = cls.subString(5);
		}
		CCommandLineString::freeCommandLineArg(argv);
	}
	wstring exe;
	if (Is64BitWindows())
		exe = exe64;
	else
		exe = exe32;

	OpenCommon(NULL, exe.c_str(), argToProg.c_str());
	return 0;
}