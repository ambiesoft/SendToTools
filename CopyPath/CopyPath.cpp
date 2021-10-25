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
#include "../../lsMisc/HighDPI.h"
#include "../../lsMisc/stdosd/stdosd.h"

#include "resource.h"
#include "CopyPath.h"


using namespace Ambiesoft::stdwin32;
using namespace std;
using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;

// #define I18N(s) Ambiesoft::i

#define KAIGYO L"\r\n"
#define SPACE L" "

#define MAX_LOADSTRING 100

enum ConvertType {
	CT_NORMAL,
	CT_DOUBLESBACKLASH,
	CT_SLASH,
};

enum DoubleQuoteType {
	DQ_DEFAULT,
	DQ_TRUE,
	DQ_FALSE,
};
struct DialogData {
	
	ConvertType ct_;
	bool nameonly_;
	DoubleQuoteType dqt_;
	bool kaigyo_;

	bool code_;
	bool sort_;
	wstring codeName_;

	DialogData() {
		ct_ = CT_NORMAL;
		nameonly_ = false;
		dqt_ = DQ_DEFAULT;
		kaigyo_ = 0;
		code_ = false;
		sort_ = false;
	}
};
// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text



void showMessageAndExit(LPCTSTR pMessage)
{
	MessageBox(NULL,
		pMessage,
		szTitle,
		MB_ICONINFORMATION);
	exit(1);
}
void showHelpAndExit(LPCTSTR pMessage)
{
	MessageBox(NULL,
		pMessage,
		szTitle,
		MB_ICONINFORMATION);
	exit(1);
}
void showErrorAndExit(LPCTSTR pMessage)
{
	MessageBox(NULL,
		pMessage,
		szTitle,
		MB_ICONERROR);
	exit(1);
}
void showErrorAndExit(const wstring& message)
{
	showErrorAndExit(message.c_str());
}
#define MAX_CODENAME 128
#define CODENAME_CPP			TEXT("C++")
#define CODENAME_CPPWIDE		TEXT("C++ (Wide)")
#define CODENAME_CSHARP			TEXT("C#")
#define CODENAME_JSON			TEXT("Json")

static LPCTSTR codeNames[] =
{
	CODENAME_CPP,
	CODENAME_CPPWIDE,
	CODENAME_CSHARP,
	CODENAME_JSON,
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

			SendDlgItemMessage(hwndDlg, IDC_RADIO_NAMEONLY, BM_SETCHECK, sDT->nameonly_ ? BST_CHECKED : 0, 0);
			SendDlgItemMessage(hwndDlg, IDC_RADIO_ABSOLUTEPATH, BM_SETCHECK, sDT->nameonly_ ? 0 : BST_CHECKED, 0);

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
					BOOL bCodeChecked = SendDlgItemMessage(hwndDlg, IDC_CHECK_PROGRAMCODE, BM_GETCHECK, 0, 0);
					EnableWindow(shCmbCode, bCodeChecked);
				}
				break;
				case IDC_CHECK_DQ:
				{
					BOOL bDQChecked = SendDlgItemMessage(hwndDlg, IDC_CHECK_DQ, BM_GETCHECK, 0, 0);
					if(bDQChecked)
						SendDlgItemMessage(hwndDlg, IDC_CHECK_NODQ, BM_SETCHECK, BST_UNCHECKED, 0);
				}
				break;
				case IDC_CHECK_NODQ:
				{
					BOOL bNoDQChecked = SendDlgItemMessage(hwndDlg, IDC_CHECK_NODQ, BM_GETCHECK, 0, 0);
					if(bNoDQChecked)
						SendDlgItemMessage(hwndDlg, IDC_CHECK_DQ, BM_SETCHECK, BST_UNCHECKED, 0);
				}
				break;

				case IDOK:
				{
					if (SendDlgItemMessage(hwndDlg, IDC_RADIO_ABSOLUTEPATH, BM_GETCHECK, 0, 0))
						sDT->nameonly_ = false;
					else if (SendDlgItemMessage(hwndDlg, IDC_RADIO_NAMEONLY, BM_GETCHECK, 0, 0))
						sDT->nameonly_ = true;
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

					sDT->dqt_ = DQ_DEFAULT;
					if (!!SendDlgItemMessage(hwndDlg, IDC_CHECK_DQ, BM_GETCHECK, 0, 0))
						sDT->dqt_ = DQ_TRUE;
					else if (!!SendDlgItemMessage(hwndDlg, IDC_CHECK_NODQ, BM_GETCHECK, 0, 0))
						sDT->dqt_ = DQ_FALSE;

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

tstring ConvertPath(const DialogData& dt, LPCTSTR pPath, const bool isLast)
{
	tstring ret(pPath);
	if (dt.nameonly_)
		ret = stdGetFileName(ret);
	else
		ret = stdGetFullPathName(ret);
	
	ConvertType ct = dt.ct_;
	DoubleQuoteType dqt = dt.dqt_;
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
			else if (dt.codeName_ == CODENAME_JSON)
				ct = CT_DOUBLESBACKLASH;
			else
			{
				showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), dt.codeName_.c_str()));
			}
		}
		dqt = DQ_TRUE;
	}
	switch (ct)
	{
	case CT_NORMAL:
		break;

	case CT_DOUBLESBACKLASH:
		ret = stdStringReplace(ret, _T("\\"), _T("\\\\"));
		break;

	case CT_SLASH:
		ret = stdStringReplace(ret, _T("\\"), _T("/"));
		break;
	}

	if (dqt == DQ_TRUE || (dqt == DQ_DEFAULT && stdIsDQNecessary(ret)))
	{
		ret = _T("\"") + ret;
		ret += _T("\"");

		if (dt.code_)
		{
			if(false)
			{ }
			else if (dt.codeName_ == CODENAME_CPP)
			{
				// nothing
			}
			else if (dt.codeName_ == CODENAME_CPPWIDE)
			{
				ret = L"L" + ret;
			}
			else if (dt.codeName_ == CODENAME_CSHARP)
			{
				ret = L"@" + ret;
			}
			else if (dt.codeName_ == CODENAME_JSON)
			{
			}
			else
			{
				showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), dt.codeName_.c_str()));
			}
		}
	}
	if (dt.code_)
	{
		if (false)
			;
		else if (dt.codeName_ == CODENAME_CPP)
			ret += L",";
		else if (dt.codeName_ == CODENAME_CPPWIDE)
			ret += L",";
		else if (dt.codeName_ == CODENAME_CSHARP)
			ret += L",";
		else if (dt.codeName_ == CODENAME_JSON)
		{
			if (!isLast)
				ret += L",";
		}
		else
		{
			showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), dt.codeName_.c_str()));
		}
	}

	return ret;
}

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	InitHighDPISupport();

	i18nInitLangmap(hInstance, NULL, stdGetFileNameWitoutExtension(stdGetModuleFileName<wchar_t>()).c_str());
	hInst = hInstance;

	// Initialize global strings
	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);

	CCommandLineParser parser;
	bool bDetail = false;
	parser.AddOptionRange({ L"/d",L"-d",L"--dialog" },
		0,
		&bDetail,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		L"Show Dialog");

	COption opMain(L"", ArgCount::ArgCount_OneToInfinite, ArgEncodingFlags_Default,
		L"Path to copy");
	parser.AddOption(&opMain);

	bool bNameOnly = false;
	parser.AddOptionRange({ L"/n",L"-n",L"--name-only" },
		0,
		&bNameOnly,
		ArgEncodingFlags_Default,
		L"Copy Name only");

	bool bHelp = false;
	parser.AddOptionRange({L"/h", L"/?", L"-h", L"--help"},
		0,
		&bHelp, 
		ArgEncodingFlags_Default, 
		L"Show Help");

	parser.Parse();

	if (bHelp)
	{
		wstring message = L"CopyPath version 1.0.1";
		message += KAIGYO;
		message += KAIGYO;
		message += parser.getHelpMessage().c_str();
		showHelpAndExit(message.c_str());
	}
	if (opMain.getValueCount()==0)
	{
		showErrorAndExit(I18N(L"No Arguments"));
	}
	if (!parser.getUnknowOptionStrings().empty())
	{
		showErrorAndExit(I18N(L"Unknown Options: ") + parser.getUnknowOptionStrings());
	}

	DialogData dt;
	dt.nameonly_ = bNameOnly;
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
		else if (dt.codeName_ == CODENAME_JSON)
		{
			str += L"[";
		}
		else
		{
			showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), dt.codeName_.c_str()));
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

	size_t i = 0;
	for (wstring& s : inputs)
	//for (size_t i = 0; i < inputs.size(); ++i)
	{
		// wstring s = inputs[i];
		str += ConvertPath(dt, s.c_str(), i==(inputs.size()-1));
		str += dt.kaigyo_ ? KAIGYO : SPACE;
		++i;
	}
	if (dt.code_)
	{
		if (dt.codeName_ == CODENAME_CPP)
		{
			str += L"};";
			str += dt.kaigyo_ ? KAIGYO : L"";
		}
		else if (dt.codeName_ == CODENAME_CPPWIDE)
		{
			str += L"};";
			str += dt.kaigyo_ ? KAIGYO : L"";
		}
		else if (dt.codeName_ == CODENAME_CSHARP)
		{
			str += L"};";
			str += dt.kaigyo_ ? KAIGYO : L"";
		}
		else if (dt.codeName_ == CODENAME_JSON)
		{
			str += L"]";
			str += dt.kaigyo_ ? KAIGYO : L"";
		}
		else
		{
			showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), dt.codeName_.c_str()));
			assert(false);
		}
	}

	str= stdTrim(str, dt.kaigyo_ ? KAIGYO : SPACE);

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


