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

#include "stdafx.h"

#include "../../lsMisc/CommandLineString.h"

#include "argCheck.h"

#define I18N(t) t

using namespace std;
using namespace stdwin32;
using namespace Ambiesoft;

#define MAX_LOADSTRING 100
#define APPNAME L"argCheck"
#define KAIGYO L"\r\n";

HINSTANCE hInst;
WCHAR szTitle[MAX_LOADSTRING];
WCHAR szWindowClass[MAX_LOADSTRING];

wstring gIni;

struct MainDialogData {
	wstring title_;
	wstring message_;
	bool again_;
	bool bWW_;
};


//// http://rarara.cafe.coocan.jp/cgi-bin/lng/vc/vclng.cgi?print+200807/08070047.txt
//BOOL GetRightTurn(HWND hEdit)
//{
//	LONG lStyle = GetWindowLong(hEdit, GWL_STYLE);
//
//	return (lStyle & ES_AUTOHSCROLL) ? FALSE : TRUE;
//}
//BOOL SetRightTurn(HWND hEdit, BOOL bRightTurn)
//{
//	BOOL bRight = GetRightTurn(hEdit);
//	LONG lStyle = GetWindowLong(hEdit, GWL_STYLE);
//
//	if (bRightTurn){
//		lStyle &= ~(WS_HSCROLL | ES_AUTOHSCROLL);
//	}
//	else{
//		lStyle |= (WS_HSCROLL | ES_AUTOHSCROLL);
//	}
//	SetWindowLong(hEdit, GWL_STYLE, lStyle);
//	SetWindowPos(hEdit, NULL, 0, 0, 0, 0,
//		(SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED));
//	return bRight;
//}

void setWW(HWND hDlg, BOOL bOn)
{
	if (bOn)
	{
		// WW on
		ShowWindow(GetDlgItem(hDlg, IDC_EDIT_MAINWW), SW_SHOW);
		ShowWindow(GetDlgItem(hDlg, IDC_EDIT_MAIN), SW_HIDE);
	}
	else
	{
		ShowWindow(GetDlgItem(hDlg, IDC_EDIT_MAINWW), SW_HIDE);
		ShowWindow(GetDlgItem(hDlg, IDC_EDIT_MAIN), SW_SHOW);
	}
}
BOOL CALLBACK MainDlgProc(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	static MainDialogData* spData;
	switch (uMsg)
	{
	case WM_INITDIALOG:
	{
		spData = (MainDialogData*)lParam;
		SetWindowText(hDlg, spData->title_.c_str());
		
		SetDlgItemText(hDlg, IDC_EDIT_MAIN, spData->message_.c_str());
		SetDlgItemText(hDlg, IDC_EDIT_MAINWW, spData->message_.c_str());

		int intval = GetPrivateProfileInt(L"Option", L"WordWrap", 0, gIni.c_str()) !=0;
		setWW(hDlg,intval);
		SendDlgItemMessage(hDlg, IDC_CHECK_WORDWRAP, BM_SETCHECK, intval, 0);

		CenterWindow(hDlg);
		return TRUE;
	}
	break;

	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
		case IDOK:
		{
			int ww = SendDlgItemMessage(hDlg, IDC_CHECK_WORDWRAP, BM_GETCHECK, 0, 0);
			if (!WritePrivateProfileString(L"Option", L"WordWrap", ww ? L"1" : L"0", gIni.c_str()))
			{
				MessageBox(hDlg, L"Save failed", APPNAME, MB_ICONERROR);
			}
			EndDialog(hDlg, IDOK);
			return 0;
		}
		break;

		case IDCANCEL:
		{
			EndDialog(hDlg, IDCANCEL);
			return 0;
		}
		break;

		case IDC_CHECK_WORDWRAP:
		{
			//SetRightTurn(GetDlgItem(hDlg, IDC_EDIT_MAIN),
			//	0 != SendDlgItemMessage(hDlg, IDC_CHECK_WORDWRAP, BM_GETCHECK, 0, 0));
			setWW(hDlg,0 != SendDlgItemMessage(hDlg, IDC_CHECK_WORDWRAP, BM_GETCHECK, 0, 0));
		}
		break;
		}
	}
	break;
	}
	return FALSE;
}

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	gIni = stdCombinePath(stdGetParentDirectory(stdGetModuleFileName()), 
		stdGetFileNameWitoutExtension(stdGetModuleFileName()) + L".ini");

	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_ARGCHECK, szWindowClass, MAX_LOADSTRING);

	wstring message;
	message += I18N(L"Current Directory");
	message += L":";
	message += KAIGYO;
	message += stdGetCurrentDirectory();
	message += KAIGYO;
	message += KAIGYO;



	message += I18N(L"Command Line");
	message += L":";
	message += KAIGYO;
	message += GetCommandLine();
	message += KAIGYO;
	message += KAIGYO;


	message += I18N(L"Length");
	message += L":";
	message += KAIGYO;
	message += std::to_wstring(lstrlen(GetCommandLine()));
	message += KAIGYO;
	message += KAIGYO;


	// CRT
	message += L"CRT";
	message += KAIGYO;
	message += L"----------------------------------";
	message += KAIGYO;
	{
		message += I18N(L"CRT argc");
		message += L":";
		message += KAIGYO;
		message += stdItoT(__argc);
		message += KAIGYO;
		message += KAIGYO;

		for (int i = 0; i < __argc; ++i)
		{
			message += I18N(L"CRT Argument");
			message += L" ";
			message += to_wstring(i);
			message += L":";
			message += KAIGYO;
			message += __wargv[i];
			message += KAIGYO;
			message += KAIGYO;
		}
	}

	// CommandLineToArgvW
	message += L"CommandLineToArgvW";
	message += KAIGYO;
	message += L"----------------------------------";
	message += KAIGYO;
	{
		int nNumArgs = 0;
		LPWSTR* pArgv = CommandLineToArgvW(GetCommandLine(), &nNumArgs);
		message += I18N(L"Shell argc");
		message += L":";
		message += KAIGYO;
		message += stdItoT(nNumArgs);
		message += KAIGYO;
		message += KAIGYO;

		for (int i = 0; i < nNumArgs; ++i)
		{
			message += I18N(L"Shell Argument");
			message += L" ";
			message += to_wstring(i);
			message += L":";
			message += KAIGYO;
			message += pArgv[i];
			message += KAIGYO;
			message += KAIGYO;
		}
		LocalFree(pArgv);
	}

	// CCommandLineString
	message += L"CCommandLineString";
	message += KAIGYO;
	message += L"----------------------------------";
	message += KAIGYO;
	{
		int nNumArgs = 0;
		LPWSTR* pArgv = CCommandLineString::getCommandLine(GetCommandLine(), &nNumArgs);
		message += I18N(L"CCommandLineString argc");
		message += L":";
		message += KAIGYO;
		message += stdItoT(nNumArgs);
		message += KAIGYO;
		message += KAIGYO;

		for (int i = 0; i < nNumArgs; ++i)
		{
			message += I18N(L"CCommandLineString Argument");
			message += L" ";
			message += to_wstring(i);
			message += L":";
			message += KAIGYO;
			message += pArgv[i];
			message += KAIGYO;
			message += KAIGYO;
		}
		CCommandLineString::freeCommandLine(pArgv);
	}
	//MessageBox(NULL,
	//	message.c_str(),
	//	szTitle,
	//	MB_ICONINFORMATION);

	MainDialogData data;
	do
	{
		data.title_ = szTitle;
		data.message_ = message;
		data.again_ = false;
		if (IDOK != DialogBoxParam(hInstance,
			MAKEINTRESOURCE(IDD_DIALOG_MAIN),
			NULL,
			MainDlgProc,
			(LPARAM)&data))
		{
			return 100;
		}
	} while (data.again_);

	return 0;
}


INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}
