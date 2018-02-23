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

// PathToClipboard.cpp : Defines the entry point for the application.
//

#include "stdafx.h"

#include "../../lsMisc/CommandLineParser.h"
#include "CopyPath.h"


using namespace stdwin32;
using namespace std;
using namespace Ambiesoft;

// #define I18N(s) Ambiesoft::i

#define KAIGYO L"\r\n"
#define SPACE L" "

#define MAX_LOADSTRING 100

enum ConvertType {
	CT_NORMAL,
	CT_DOUBLESBACKLASH,
	CT_SLASH,
};

struct DialogData {
	ConvertType ct_;
	bool relativePath_;
	bool dq_;
	bool kaigyo_;

	bool code_;
	bool sort_;
	wstring codeName_;

	DialogData() {
		ct_ = CT_NORMAL;
		relativePath_ = false;
		dq_ = false;
		kaigyo_ = 0;
		code_ = false;
		sort_ = false;
	}
};
// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text



void showErrorAndExit(LPCTSTR pMessage)
{
	MessageBox(NULL,
		pMessage,
		szTitle,
		MB_ICONERROR);
	exit(1);
}

#define MAX_CODENAME 128
#define CODENAME_CPP			TEXT("C++")
#define CODENAME_CPPWIDE		TEXT("C++ (Wide)")
#define CODENAME_CSHARP			TEXT("C#")

static LPCTSTR codeNames[] =
{
	CODENAME_CPP,
	CODENAME_CPPWIDE,
	CODENAME_CSHARP,
};
INT_PTR CALLBACK DialogProc(
	_In_ HWND   hwndDlg,
	_In_ UINT   uMsg,
	_In_ WPARAM wParam,
	_In_ LPARAM lParam
)
{
	static DialogData* sDT = NULL;
	static HWND shCmbCode;
	switch (uMsg)
	{
		case WM_INITDIALOG:
		{
			sDT = (DialogData*)lParam;
			shCmbCode = GetDlgItem(hwndDlg, IDC_COMBO_PROGRAMCODE);
			assert(IsWindow(shCmbCode));

			i18nChangeChildWindowText(hwndDlg);

			SendDlgItemMessage(hwndDlg, IDC_RADIO_ABSOLUTEPATH, BM_SETCHECK, BST_CHECKED, 0);
			SendDlgItemMessage(hwndDlg, IDC_RADIO_NORMAL, BM_SETCHECK, BST_CHECKED, 0);

			EnableWindow(shCmbCode, FALSE);
			for (int i = 0; i < _countof(codeNames);++i)
				SendMessage(shCmbCode, CB_ADDSTRING, (WPARAM)0, (LPARAM)codeNames[i]);
			SendMessage(shCmbCode, CB_SETCURSEL, 0, 0);
			CenterWindow(hwndDlg);
			PostMessage(hwndDlg, WM_APP_INITIALUPDATE, 0, 0);
			return TRUE;
		}
		break;

		case WM_APP_INITIALUPDATE:
		{
			AllowSetForegroundWindow(GetCurrentProcessId());
			SetForegroundWindow(hwndDlg);
		}
		break;

		case WM_COMMAND:
		{
			switch (LOWORD(wParam))
			{
				case IDC_CHECK_PROGRAMCODE:
				{
					BOOL bCodeChecked=SendDlgItemMessage(hwndDlg, IDC_CHECK_PROGRAMCODE, BM_GETCHECK, 0, 0);
					EnableWindow(shCmbCode, bCodeChecked);
				}
				break;
				case IDOK:
				{
					if (SendDlgItemMessage(hwndDlg, IDC_RADIO_ABSOLUTEPATH, BM_GETCHECK, 0, 0))
						sDT->relativePath_ = false;
					else if (SendDlgItemMessage(hwndDlg, IDC_RADIO_RELATIVEPATH, BM_GETCHECK, 0, 0))
						sDT->relativePath_ = true;
					else
						assert(false);

					if (SendDlgItemMessage(hwndDlg, IDC_RADIO_NORMAL, BM_GETCHECK, 0, 0))
						sDT->ct_ = CT_NORMAL;
					else if (SendDlgItemMessage(hwndDlg, IDC_RADIO_TWOBACKSLASH, BM_GETCHECK, 0, 0))
						sDT->ct_ = CT_DOUBLESBACKLASH;
					else if (SendDlgItemMessage(hwndDlg, IDC_RADIO_SLASH, BM_GETCHECK, 0, 0))
						sDT->ct_ = CT_SLASH;
					else
						assert(false);

					sDT->dq_ = !!SendDlgItemMessage(hwndDlg, IDC_CHECK_DQ, BM_GETCHECK, 0, 0);
					sDT->kaigyo_ = !!SendDlgItemMessage(hwndDlg, IDC_CHECK_LINE, BM_GETCHECK, 0, 0);
					
					sDT->code_ = !!SendDlgItemMessage(hwndDlg, IDC_CHECK_PROGRAMCODE, BM_GETCHECK, 0, 0);
					int cursel = SendMessage(shCmbCode, CB_GETCURSEL, 0, 0);
					TCHAR buff[MAX_CODENAME]; buff[0] = 0;
					SendMessage(shCmbCode, CB_GETLBTEXT, cursel,(LPARAM)buff);
					sDT->codeName_ = buff;

					sDT->sort_ = !!SendDlgItemMessage(hwndDlg, IDC_CHECK_SORT, BM_GETCHECK, 0, 0);

					EndDialog(hwndDlg, IDOK);
					return TRUE;
				}
				break;
			
				case IDCANCEL:
				{
					EndDialog(hwndDlg, IDCANCEL);
					return TRUE;
				}
			}
			break;
		}
		break;
	}
	return FALSE;
}

tstring ConvertPath(const DialogData& dt, LPCTSTR pPath)
{
	tstring ret(pPath);
	if (dt.relativePath_)
		ret = stdwin32::stdGetFileName(ret);
	
	ConvertType ct = dt.ct_;
	bool dq = dt.dq_;
	if (dt.code_)
	{
		if (ct != CT_SLASH)
		{
			if (dt.codeName_ == CODENAME_CPP)
				ct = CT_DOUBLESBACKLASH;
			else if (dt.codeName_ == CODENAME_CPPWIDE)
				ct = CT_DOUBLESBACKLASH;
			else if (dt.codeName_ == CODENAME_CSHARP)
				ct = CT_NORMAL;
		}
		dq = true;
	}
	switch (ct)
	{
	case CT_NORMAL:
		break;

	case CT_DOUBLESBACKLASH:
		ret = StdStringReplace(ret, _T("\\"), _T("\\\\"));
		break;

	case CT_SLASH:
		ret = StdStringReplace(ret, _T("\\"), _T("/"));
		break;
	}

	if (dq)
	{
		ret = _T("\"") + ret;
		ret += _T("\"");

		if (dt.code_)
		{
			if (dt.codeName_ == CODENAME_CPPWIDE)
				ret = L"L" + ret;
			else if (dt.codeName_ == CODENAME_CSHARP)
				ret = L"@" + ret;
		}
	}
	if (dt.code_)
		ret += L",";

	return ret;
}

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);
	
	i18nInitLangmap(hInstance, NULL, stdGetFileNameWitoutExtension(stdGetModuleFileName()).c_str());
	hInst = hInstance;

    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);

	CCommandLineParser parser;
	bool bDetail = false;
	parser.AddOption(L"/d", 0, &bDetail);
	COption opMain;
	parser.AddOption(&opMain);
	parser.Parse();

	if (opMain.getValueCount()==0)
	{
		showErrorAndExit(I18N(L"No Arguments"));
	}

	DialogData dt;
	if (!bDetail)
	{
		bDetail = (GetAsyncKeyState(VK_SHIFT) < 0) ||
			(GetAsyncKeyState(VK_CONTROL) < 0) ||
			(GetAsyncKeyState(VK_RBUTTON) < 0);
	}
	if ( bDetail)
	{
		if (IDOK != DialogBoxParam(hInst,
			MAKEINTRESOURCE(IDD_PATHTOCLIPBOARD_DIALOG),
			NULL,
			DialogProc,
			(LPARAM)&dt))
		{
			return 1;
		}
	}

	wstring str;
	if (dt.code_)
	{
		if (dt.codeName_ == CODENAME_CPP)
		{
			str += L"char* filenames[] = {";
		}
		else if (dt.codeName_ == CODENAME_CPPWIDE)
		{
			str += L"wchar_t* filenames[] = {";
		}
		else if (dt.codeName_ == CODENAME_CSHARP)
		{
			str += L"string[] filenames = new string[]{";
		}
		else
		{
			assert(false);
		}

		str += dt.kaigyo_ ? KAIGYO : L"";
	}

	list<wstring> inputs;
	for (size_t i = 0; i<opMain.getValueCount(); ++i)
	{
		inputs.push_back(opMain.getValue(i));
	}
	if (dt.sort_)
		inputs.sort();

	for (wstring& s : inputs)
	{
		str += ConvertPath(dt, s.c_str());
		str += dt.kaigyo_ ? KAIGYO : SPACE;
	}
	if (dt.code_)
	{
		str += L"};";
		str += dt.kaigyo_ ? KAIGYO : L"";
	}

	str=trim(str, dt.kaigyo_ ? KAIGYO : SPACE);

	if (!SetClipboardText(NULL, str.c_str()))
	{
		DWORD dwLE = GetLastError();
		wstring lastError;
		if (dwLE != 0)
		{
			lastError = GetLastErrorString(GetLastError());
		}
		wstring message = I18N(L"Failed: SetClipboard");
		if (!lastError.empty())
		{
			message += KAIGYO;
			message += lastError;
		}
		showErrorAndExit(message.c_str());
	}

	HICON hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PATHTOCLIPBOARD));
	showballoon(
		NULL,
		szTitle,
		I18N(L"Path has been set onto the clipbard."),
		hIcon,
		5000,
		(UINT)time(NULL),
		FALSE,
		NIIF_INFO
		);
	DestroyIcon(hIcon);
	return 0;
}


