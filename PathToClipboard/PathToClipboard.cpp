// PathToClipboard.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include <ctime>
#include "PathToClipboard.h"
#include "../../MyUtility/SetClipboardText.h"
#include "../../MyUtility/showballoon.h"
#include "../../MyUtility/stdwin32/stdwin32.h"
#include "../../MyUtility/StdStringReplace.h"
#include "../../MyUtility/CenterWindow.h"

using namespace stdwin32;
using namespace std;

#define I18N(s) (s)

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
	bool dq_;
	bool kaigyo_;
	DialogData() {
		ZeroMemory(this, sizeof(*this));
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

INT_PTR CALLBACK DialogProc(
	_In_ HWND   hwndDlg,
	_In_ UINT   uMsg,
	_In_ WPARAM wParam,
	_In_ LPARAM lParam
)
{
	static DialogData* sDT = NULL;
	switch (uMsg)
	{
		case WM_INITDIALOG:
		{
			sDT = (DialogData*)lParam;
			SendDlgItemMessage(hwndDlg, IDC_RADIO_NORMAL, BM_SETCHECK, BST_CHECKED, 0);
			CenterWindow(hwndDlg);

			return TRUE;
		}
		break;

		case WM_COMMAND:
		{
			switch (LOWORD(wParam))
			{
				case IDOK:
				{
					if (SendDlgItemMessage(hwndDlg, IDC_RADIO_NORMAL, BM_GETCHECK, 0, 0))
						sDT->ct_ = CT_NORMAL;
					else if (SendDlgItemMessage(hwndDlg, IDC_RADIO_TWOBACKSLASH, BM_GETCHECK, 0, 0))
						sDT->ct_ = CT_DOUBLESBACKLASH;
					else if (SendDlgItemMessage(hwndDlg, IDC_RADIO_SLASH, BM_GETCHECK, 0, 0))
						sDT->ct_ = CT_SLASH;

					sDT->dq_ = !!SendDlgItemMessage(hwndDlg, IDC_CHECK_DQ, BM_GETCHECK, 0, 0);
					sDT->kaigyo_ = !!SendDlgItemMessage(hwndDlg, IDC_CHECK_LINE, BM_GETCHECK, 0, 0);
					
					
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

	switch (dt.ct_)
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

	if (dt.dq_)
	{
		ret = _T("\"") + ret;
		ret += _T("\"");
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

	hInst = hInstance;

    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);


	if (__argc < 2)
	{
		showErrorAndExit(I18N(L"No Arguments"));
	}

	DialogData dt;
	if (GetAsyncKeyState(VK_CONTROL) < 0)
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
	for (int i = 1; i < __argc;++i)
	{
		str += ConvertPath(dt, __wargv[i]);
		str += dt.kaigyo_ ? KAIGYO : SPACE;
	}
	str=trim(str, dt.kaigyo_ ? KAIGYO : SPACE);

	if (!SetClipboardText(NULL, str.c_str()))
	{
		showErrorAndExit(I18N(L"Failed to SetClipbard"));
	}

	HICON hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_PATHTOCLIPBOARD));
	showballoon(
		NULL,
		szTitle,
		I18N(L"Path has been set on Clipbard."),
		hIcon,
		5000,
		(UINT)time(NULL)
		);
	DestroyIcon(hIcon);
	return 0;
}


