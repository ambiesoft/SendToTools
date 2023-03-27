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
#include <functional>
#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/HighDPI.h"
#include "../../lsMisc/stdosd/stdosd.h"

#include "resource.h"
#include "DialogData.h"
#include "CopyPath.h"


using namespace std;
using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;

#define KAIGYO L"\r\n"
#define SPACE L" "

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR gszTitle[MAX_LOADSTRING];                  // The title bar text

static wstring GetIniPath()
{
	return stdCombinePath(
		stdGetParentDirectory(stdGetModuleFileName()),
		stdGetFileNameWitoutExtension(stdGetModuleFileName()) + L".ini");
}

void showMessageAndExit(LPCTSTR pMessage)
{
	MessageBox(NULL,
		pMessage,
		gszTitle,
		MB_ICONINFORMATION);
	exit(1);
}
void showHelpAndExit(LPCTSTR pMessage)
{
	MessageBox(NULL,
		pMessage,
		gszTitle,
		MB_ICONINFORMATION);
	exit(1);
}
void showErrorAndExit(LPCTSTR pMessage)
{
	MessageBox(NULL,
		pMessage,
		gszTitle,
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
static wstring GetCodeNameForCLHelp_obsolete()
{
	wstring ret;
	for (int i = 0; i < _countof(codeNames); ++i)
	{
		ret += stdFormat(L"'%s'", codeNames[i]);
		ret += L", ";
		if (i == _countof(codeNames))
		{
			ret += I18N(L"or ");
		}
	}
	return ret;
}
static wstring GetCodeNameForCLHelp()
{
	wstring ret;
	for (int i = 0; i < _countof(codeNames); ++i)
	{
		ret += stdFormat(L"%d='%s'", i, codeNames[i]);
		if ((i + 1) != _countof(codeNames))
		{
			ret += L", ";
		}
	}
	return ret;
}
static LPCTSTR dtqNames[] =
{
	I18N(L"Default"),
	I18N(L"Double Quote"),
	I18N(L"No Double Quote"),
};

enum UPDATEDATA
{
	DIALOG2DATA,
	DATA2DIALOG,
};
void UpdateData(HWND hwndDlg, HWND shCmbCode, HWND shCmbDQT,
	DialogData* sDT, UPDATEDATA upd)
{
	if (upd == DATA2DIALOG)
	{
		SendDlgItemMessage(hwndDlg, IDC_RADIO_NAMEONLY, BM_SETCHECK, sDT->isNameonly() ? BST_CHECKED : 0, 0);
		SendDlgItemMessage(hwndDlg, IDC_RADIO_ABSOLUTEPATH, BM_SETCHECK, sDT->isNameonly() ? 0 : BST_CHECKED, 0);

		switch (sDT->getPST())
		{
		case PST_NORMAL:
			SendDlgItemMessage(hwndDlg, IDC_RADIO_NORMAL, BM_SETCHECK, BST_CHECKED, 0);
			break;
		case PST_DOUBLESBACKLASH:
			SendDlgItemMessage(hwndDlg, IDC_RADIO_TWOBACKSLASH, BM_SETCHECK, BST_CHECKED, 0);
			break;
		case PST_SLASH:
			SendDlgItemMessage(hwndDlg, IDC_RADIO_SLASH, BM_SETCHECK, BST_CHECKED, 0);
			break;
		default:
			assert(false);
		}

		// DQT
		SendMessage(shCmbDQT, CB_SETCURSEL, sDT->getDQT(), 0);

		// kaigyo
		SendDlgItemMessage(hwndDlg, IDC_CHECK_LINE, BM_SETCHECK,
			sDT->isMultiLine() ? BST_CHECKED : 0, 0);

		// isCode
		SendDlgItemMessage(hwndDlg, IDC_CHECK_PROGRAMCODE, BM_SETCHECK,
			sDT->isCode() ? BST_CHECKED : 0, 0);

		// codename
		int codeIndex = -1;
		for (int i = 0; SendMessage(shCmbCode, CB_GETCOUNT, 0, 0); ++i)
		{
			TCHAR buff[MAX_CODENAME]; buff[0] = 0;
			SendMessage(shCmbCode, CB_GETLBTEXT, i, (LPARAM)buff);
			if (sDT->getCodeName() == buff)
			{
				SendMessage(shCmbCode, CB_SETCURSEL, i, 0);
				break;
			}
		}
		
		// isSort
		SendDlgItemMessage(hwndDlg, IDC_CHECK_SORT, BM_SETCHECK,
			sDT->isSort() ? BST_CHECKED : 0, 0);
	}
	else if (upd == DIALOG2DATA)
	{
		// nameonly
		if (SendDlgItemMessage(hwndDlg, IDC_RADIO_ABSOLUTEPATH, BM_GETCHECK, 0, 0))
			sDT->setNameonly(false);
		else if (SendDlgItemMessage(hwndDlg, IDC_RADIO_NAMEONLY, BM_GETCHECK, 0, 0))
			sDT->setNameonly(true);
		else
			assert(false);

		// convert type
		if (SendDlgItemMessage(hwndDlg, IDC_RADIO_NORMAL, BM_GETCHECK, 0, 0))
			sDT->setPST(PST_NORMAL);
		else if (SendDlgItemMessage(hwndDlg, IDC_RADIO_TWOBACKSLASH, BM_GETCHECK, 0, 0))
			sDT->setPST(PST_DOUBLESBACKLASH);
		else if (SendDlgItemMessage(hwndDlg, IDC_RADIO_SLASH, BM_GETCHECK, 0, 0))
			sDT->setPST(PST_SLASH);
		else
			assert(false);

		// New DQT
		sDT->setDQT((DQType)SendMessage(shCmbDQT, CB_GETCURSEL, 0, 0));

		// kaigyo
		sDT->setMultiLine(!!SendDlgItemMessage(hwndDlg, IDC_CHECK_LINE, BM_GETCHECK, 0, 0));

		// isCode
		sDT->setCode(!!SendDlgItemMessage(hwndDlg, IDC_CHECK_PROGRAMCODE, BM_GETCHECK, 0, 0));

		// codename
		int cursel = SendMessage(shCmbCode, CB_GETCURSEL, 0, 0);
		TCHAR buff[MAX_CODENAME]; buff[0] = 0;
		SendMessage(shCmbCode, CB_GETLBTEXT, cursel, (LPARAM)buff);
		sDT->setCodeName(buff);

		// isSort
		sDT->setSort(!!SendDlgItemMessage(hwndDlg, IDC_CHECK_SORT, BM_GETCHECK, 0, 0));
	}
	else
		assert(false);
}

INT_PTR CALLBACK DialogProc(
	_In_ HWND   hwndDlg,
	_In_ UINT   uMsg,
	_In_ WPARAM wParam,
	_In_ LPARAM lParam
)
{
	static DialogData* sDT = NULL;
	static HWND shCmbCode;
	static HWND shCmbDQT;
	switch (uMsg)
	{
		case WM_INITDIALOG:
		{
			sDT = (DialogData*)lParam;
			shCmbCode = GetDlgItem(hwndDlg, IDC_COMBO_PROGRAMCODE);
			assert(IsWindow(shCmbCode));
			shCmbDQT = GetDlgItem(hwndDlg, IDC_COMBO_DQT);
			assert(IsWindow(shCmbDQT));

			i18nChangeChildWindowText(hwndDlg);

			// prepare combobox for codename
			EnableWindow(shCmbCode, FALSE);
			for (int i = 0; i < _countof(codeNames); ++i)
				SendMessage(shCmbCode, CB_ADDSTRING, (WPARAM)0, (LPARAM)codeNames[i]);
			SendMessage(shCmbCode, CB_SETCURSEL, 0, 0);

			// prepare combobox for DQT
			for (int i = 0; i < _countof(dtqNames); ++i)
				SendMessage(shCmbDQT, CB_ADDSTRING, (WPARAM)0, (LPARAM)dtqNames[i]);
			SendMessage(shCmbDQT, CB_SETCURSEL, 0, 0);

			UpdateData(hwndDlg, shCmbCode, shCmbDQT, sDT, DATA2DIALOG);

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
				case IDC_BUTTON_ABOUT:
				{
					OpenCommon(hwndDlg, stdGetModuleFileName().c_str(), L"--help");
				}
				break;
				case IDC_BUTTON_SETASDEFAULT:
				case IDOK:
				{
					UpdateData(hwndDlg, shCmbCode, shCmbDQT,
						sDT, DIALOG2DATA);
					if (LOWORD(wParam) == IDOK)
					{
						EndDialog(hwndDlg, IDOK);
						return TRUE;
					}
					else if (LOWORD(wParam) == IDC_BUTTON_SETASDEFAULT)
					{
						sDT->Save(GetIniPath());
					}
					else
						assert(false);
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
	if (dt.isNameonly())
		ret = stdGetFileName(ret);
	else
		ret = stdGetFullPathName(ret);
	
	PathSeptType ct = dt.getPST();
	DQType dqt = dt.getDQT();
	if (dt.isCode())
	{
		if (ct != PST_SLASH)
		{
			if (dt.getCodeName() == CODENAME_CPP)
				ct = PST_DOUBLESBACKLASH;
			else if (dt.getCodeName() == CODENAME_CPPWIDE)
				ct = PST_DOUBLESBACKLASH;
			else if (dt.getCodeName() == CODENAME_CSHARP)
				ct = PST_NORMAL;
			else if (dt.getCodeName() == CODENAME_JSON)
				ct = PST_DOUBLESBACKLASH;
			else
			{
				showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), dt.getCodeName().c_str()));
			}
		}
		dqt = DQ_TRUE;
	}
	switch (ct)
	{
	case PST_NORMAL:
		break;

	case PST_DOUBLESBACKLASH:
		ret = stdStringReplace(ret, _T("\\"), _T("\\\\"));
		break;

	case PST_SLASH:
		ret = stdStringReplace(ret, _T("\\"), _T("/"));
		break;
	}

	if (dqt == DQ_TRUE || (dqt == DQ_DEFAULT && stdIsDQNecessary(ret)))
	{
		ret = _T("\"") + ret;
		ret += _T("\"");

		if (dt.isCode())
		{
			if(false)
			{ }
			else if (dt.getCodeName() == CODENAME_CPP)
			{
				// nothing
			}
			else if (dt.getCodeName() == CODENAME_CPPWIDE)
			{
				ret = L"L" + ret;
			}
			else if (dt.getCodeName() == CODENAME_CSHARP)
			{
				ret = L"@" + ret;
			}
			else if (dt.getCodeName() == CODENAME_JSON)
			{
			}
			else
			{
				showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), dt.getCodeName().c_str()));
			}
		}
	}
	if (dt.isCode())
	{
		if (false)
			;
		else if (dt.getCodeName() == CODENAME_CPP)
			ret += L",";
		else if (dt.getCodeName() == CODENAME_CPPWIDE)
			ret += L",";
		else if (dt.getCodeName() == CODENAME_CSHARP)
			ret += L",";
		else if (dt.getCodeName() == CODENAME_JSON)
		{
			if (!isLast)
				ret += L",";
		}
		else
		{
			showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), dt.getCodeName().c_str()));
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
	LoadStringW(hInstance, IDS_APP_TITLE, gszTitle, MAX_LOADSTRING);

	auto fNoLoadIni = [](CCommandLineParser& parser, bool* pResult) {
		parser.AddOption(L"--no-ini",
			0,
			pResult ,
			ArgEncodingFlags::ArgEncodingFlags_Default,
			L"No load from ini");
	};
	bool bNoLoadIni = false;
	{
		CCommandLineParser parser;
		fNoLoadIni(parser, &bNoLoadIni);
		parser.Parse();
	}
	if(!bNoLoadIni)
		theData.Load(GetIniPath());

	CCommandLineParser parser;
	bool bShowDialog = false;
	parser.AddOptionRange({ L"/d",L"-d",L"--dialog" },
		0,
		&bShowDialog,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		L"Show Dialog");

	COption opMain(L"", ArgCount::ArgCount_OneToInfinite, ArgEncodingFlags_Default,
		L"Path to copy");
	parser.AddOption(&opMain);

	int nNameOnly = -1;
	parser.AddOptionRange({ L"/n",L"-n",L"--name-only" },
		ArgCount::ArgCount_One,
		&nNameOnly,
		ArgEncodingFlags_Default,
		L"Name only: 0=false, 1=true");

	int pathSep = -1;
	parser.AddOptionRange({ L"/p",L"-p",L"--path-separator" },
		ArgCount::ArgCount_One,
		&pathSep,
		ArgEncodingFlags_Default,
		I18N(L"Path Separator: 0=Default, 1=DoubleBackSlash, 2=Slash"));

	int dqt = -1;
	parser.AddOptionRange({ L"/q",L"-q",L"--double-quote" },
		ArgCount::ArgCount_One,
		&dqt,
		ArgEncodingFlags_Default,
		I18N(L"Double Quote: 0=Default, 1=DoubleQuote, 2=NoDoubleQuote"));

	int nMultiLine = -1;
	parser.AddOptionRange({ L"/m", L"-m", L"--multi-lines" },
		ArgCount::ArgCount_One,
		&nMultiLine,
		ArgEncodingFlags_Default,
		L"Multilines: 0=No, 1=Yes");

	int codeName = -1;
	parser.AddOptionRange({ L"/c", L"-c", L"--programming-code" },
		ArgCount::ArgCount_One,
		&codeName,
		ArgEncodingFlags_Default,
		I18N(L"Copy as programming code:") + GetCodeNameForCLHelp());

	int nSort = -1;
	parser.AddOptionRange({ L"/s", L"-s", L"--sort" },
		ArgCount::ArgCount_One,
		&nSort,
		ArgEncodingFlags_Default,
		L"Sort: 0=No, 1=Yes");

	fNoLoadIni(parser, &bNoLoadIni);


	bool bNoBalloon = false;
	parser.AddOption(L"--no-balloon",
		0,
		&bNoBalloon,
		ArgEncodingFlags_Default,
		L"Show no balloon");

	bool bHelp = false;
	parser.AddOptionRange({ L"/h", L"/?", L"-h", L"--help" },
		0,
		&bHelp,
		ArgEncodingFlags_Default,
		L"Show Help");

	parser.Parse();

	if (!parser.getUnknowOptionStrings().empty())
	{
		showErrorAndExit(wstring() + I18N(L"Unknow option(s):") + KAIGYO + 
			parser.getUnknowOptionStrings());
	}
	if (bHelp)
	{
		wstring message = stdFormat(L"CopyPath v%s", 
			GetVersionString(nullptr, 3).c_str());
		message += KAIGYO;
		message += L"Ambiesoft https://ambiesoft.com/";
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

	if (nNameOnly != -1)
	{
		if (!(0 <= nNameOnly && nNameOnly <= 1))
			showHelpAndExit(I18N(L"Name only must be 0 or 1."));
		theData.setNameonly(nNameOnly == 0 ? false : true);
	}
	if (pathSep != -1)
	{
		if (!(0 <= pathSep && pathSep <= 2))
			showErrorAndExit(I18N(L"Path separator must be one of 0, 1, or 2."));
		theData.setPST((PathSeptType)pathSep);
	}
	if (dqt != -1)
	{
		if (!(0 <= dqt && dqt <= 2))
			showErrorAndExit(I18N(L"Double quote must be one of 0, 1, or 2."));
		theData.setDQT((DQType)dqt);
	}
	if (nMultiLine != -1)
	{
		if (!(0 <= nMultiLine && nMultiLine <= 1))
			showErrorAndExit(I18N(L"Multiline must be 0 or 1."));

		theData.setMultiLine(nMultiLine == 0 ? false : true);
	}
	if (codeName != -1)
	{
		if (!(0 <= codeName && codeName < _countof(codeNames)))
			showErrorAndExit(I18N(L"Codename must be one of 0, 1, 2, or 3."));
		theData.setCode(true);
		theData.setCodeName(codeNames[codeName]);
	}
	if (nSort != -1)
	{
		if (!(0 <= nSort && nSort <= 1))
			showErrorAndExit(I18N(L"Sort must be 0 or 1."));

		theData.setSort(nSort == 0 ? false : true);
	}

	if (!bShowDialog)
	{
		bShowDialog = (GetAsyncKeyState(VK_SHIFT) < 0) ||
			(GetAsyncKeyState(VK_CONTROL) < 0) ||
			(GetAsyncKeyState(VK_RBUTTON) < 0);
	}
	if (bShowDialog)
	{
		if (IDOK != DialogBoxParam(hInst,
			MAKEINTRESOURCE(IDD_PATHTOCLIPBOARD_DIALOG),
			NULL,
			DialogProc,
			(LPARAM)&theData))
		{
			return 1;
		}
	}

	wstring str;
	if (theData.isCode())
	{
		if (theData.getCodeName() == CODENAME_CPP)
		{
			str += L"const char* filenames[] = {";
		}
		else if (theData.getCodeName() == CODENAME_CPPWIDE)
		{
			str += L"const wchar_t* filenames[] = {";
		}
		else if (theData.getCodeName() == CODENAME_CSHARP)
		{
			str += L"string[] filenames = new string[]{";
		}
		else if (theData.getCodeName() == CODENAME_JSON)
		{
			str += L"[";
		}
		else
		{
			showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), theData.getCodeName().c_str()));
			assert(false);
		}

		str += theData.isMultiLine() ? KAIGYO : L"";
	}

	list<wstring> inputs;
	for (size_t i = 0; i<opMain.getValueCount(); ++i)
	{
		inputs.push_back(opMain.getValue(i));
	}
	if (theData.isSort())
		inputs.sort();

	size_t i = 0;
	for (wstring& s : inputs)
	//for (size_t i = 0; i < inputs.size(); ++i)
	{
		// wstring s = inputs[i];
		str += ConvertPath(theData, s.c_str(), i==(inputs.size()-1));
		str += theData.isMultiLine() ? KAIGYO : SPACE;
		++i;
	}
	if (theData.isCode())
	{
		if (theData.getCodeName() == CODENAME_CPP)
		{
			str += L"};";
			str += theData.isMultiLine() ? KAIGYO : L"";
		}
		else if (theData.getCodeName() == CODENAME_CPPWIDE)
		{
			str += L"};";
			str += theData.isMultiLine() ? KAIGYO : L"";
		}
		else if (theData.getCodeName() == CODENAME_CSHARP)
		{
			str += L"};";
			str += theData.isMultiLine() ? KAIGYO : L"";
		}
		else if (theData.getCodeName() == CODENAME_JSON)
		{
			str += L"]";
			str += theData.isMultiLine() ? KAIGYO : L"";
		}
		else
		{
			showErrorAndExit(stdFormat(I18N(L"Unknown code name '%s'."), theData.getCodeName().c_str()));
			assert(false);
		}
	}

	str= stdTrim(str, theData.isMultiLine() ? KAIGYO : SPACE);

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

	if (!bNoBalloon)
	{
		HICON hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PATHTOCLIPBOARD));
		showballoon(
			NULL,
			gszTitle,
			I18N(L"Path has been set onto the clipbard."),
			hIcon,
			5000,
			(UINT)time(NULL),
			FALSE,
			NIIF_INFO
		);
		DestroyIcon(hIcon);
	}
	return 0;
}


