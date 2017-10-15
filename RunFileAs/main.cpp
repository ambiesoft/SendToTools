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
#include <Shlobj.h>
#pragma comment(lib, "Shell32.lib")


#include <string>

#include <stlsoft/smartptr/scoped_handle.hpp>
#include "../../lsMisc/OpenCommon.h"
#include "../../lsMisc/stdwin32/stdwin32.h"

using namespace std;
using namespace stdwin32;
using namespace Ambiesoft;

#define APPNAME L"RunFileAs"
#define I18N(s) (s)

int Alert(LPCWSTR pMessage)
{
	return MessageBox(NULL,
		pMessage,
		APPNAME,
		MB_ICONEXCLAMATION);
}

enum RETVAL {
	ERROR_NO_ARGUMENT = 1,
	ERROR_NO_RUNARGUMENT,
	ERROR_NO_FAILED_TO_OPENPROGRAM,
};

LPCWSTR skipSapce(LPCWSTR p)
{
	if (p == NULL || p[0] == 0)
		return p;

	while (*p && iswspace(*p))
		++p;

	return p;
}

bool isIncludeSpace(LPCWSTR p)
{
	if (p == NULL || p[0] == 0)
		return false;

	for (; *p; ++p)
	{
		if (iswspace(*p))
			return true;
	}
	return false;
}
wstring quote(LPCWSTR p)
{
	wstring ret;
	if (p == NULL || p[0] == 0)
		return ret;

	p = skipSapce(p);
	if (p[0] == 0)
		return ret;
	if (p[0] == '"')
		return p;
	
	if (!isIncludeSpace(p))
		return p;

	ret = L"\"";
	ret += p;
	ret += L"\"";
	return ret;
}

wstring getArgument(int nStart, bool toLast = false, bool noQuate = false)
{
	int nArgs = 0;
	LPWSTR* p = CommandLineToArgvW(GetCommandLine(), &nArgs);
	stlsoft::scoped_handle<HLOCAL> ha(p, LocalFree);

	wstring ret;
	for (int i = 0; (i < nArgs) && *p ; ++i, ++p)
	{
		if (i < nStart)
			continue;

		LPCWSTR p1Arg = *p;
		p1Arg = skipSapce(p1Arg);

		wstring arg(noQuate ? p1Arg : quote(p1Arg));

		if (!toLast)
			return arg;

		if (ret.empty())
			ret = arg;
		else
		{
			ret += L" ";
			ret += arg;
		}
	}
	return ret;
}

int WINAPI wWinMain(
	HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	PWSTR pCmdLine,
	int nCmdShow)
{
	if (__argc < 2)
	{
		Alert(I18N(L"No arguments."));
		return ERROR_NO_ARGUMENT;
	}


	if (lstrcmp(__wargv[1], L"/run") == 0)
	{
		if (__argc < 3)
		{
			Alert(I18N(L"No run argument."));
			return ERROR_NO_RUNARGUMENT;
		}

		wstring file = getArgument(2);
		wstring cmd = getArgument(3, true);

		if (!OpenCommon(
			NULL,
			file.c_str(),
			cmd.c_str()))
		{
			//MessageBox.Show(ex.Message, Application.ProductName,
			//	MessageBoxButtons.OK,
			//	MessageBoxIcon.Error);
			return ERROR_NO_FAILED_TO_OPENPROGRAM;
		}
	}
	else  // not with /run
	{
		wstring exe = stdGetModuleFileName();
		wstring cmd = L"/run " + getArgument(1, true);
		wstring dir = stdGetParentDirectory(getArgument(1, false, true));
		LPCWSTR pVerb = IsUserAnAdmin() ? L"" : L"runas";
		if (!OpenCommon(
			NULL,
			exe.c_str(),
			cmd.c_str(),
			dir.c_str(),
			NULL,
			pVerb))
		{
			return ERROR_NO_FAILED_TO_OPENPROGRAM;
		}
		//ProcessStartInfo startInfo = new ProcessStartInfo();
		//startInfo.FileName = Application.ExecutablePath;
		//startInfo.UseShellExecute = true;
		//startInfo.Verb = IsAdmin() ? null : "runas";
		//startInfo.Arguments = "/run " + theArguments;
		//startInfo.WorkingDirectory = System.IO.Directory.GetParent(AmbLib.unDoubleQuote(AmbLib.getAllArgs(theArguments, 0, true))).FullName;;


		//try
		//{
		//	Process proc = Process.Start(startInfo);
		//	// proc.WaitForExit();
		//}
		//catch (Exception ex)
		//{
		//	MessageBox.Show(ex.Message, Application.ProductName,
		//		MessageBoxButtons.OK,
		//		MessageBoxIcon.Error);
		//}
		//return;
	}

	return 0;
}