#include "stdafx.h"

#include "argCheck.h"

#define I18N(t) t

using namespace std;
using namespace stdwin32;

#define MAX_LOADSTRING 100

#define KAIGYO L"\r\n";

HINSTANCE hInst;
WCHAR szTitle[MAX_LOADSTRING];
WCHAR szWindowClass[MAX_LOADSTRING];



struct MyDialogData {
	wstring title_;
	wstring message_;
};
BOOL CALLBACK MyDlgProc(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	static MyDialogData* spData;
	switch (uMsg)
	{
	case WM_INITDIALOG:
	{
		spData = (MyDialogData*)lParam;
		SetWindowText(hDlg, spData->title_.c_str());
		SetDlgItemText(hDlg, IDC_EDIT_MAIN, spData->message_.c_str());

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

		}
		break;
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

	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_ARGCHECK, szWindowClass, MAX_LOADSTRING);

	wstring message;
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

	message += I18N(L"argc");
	message += L":";
	message += KAIGYO;
	message += stdItoT(__argc);
	message += KAIGYO;
	message += KAIGYO;

	for (int i = 0; i < __argc; ++i)
	{
		message += I18N(L"Argument");
		message += to_wstring(i);
		message += L":";
		message += KAIGYO;
		message += __wargv[i];
		message += KAIGYO;
		message += KAIGYO;
	}

	//MessageBox(NULL,
	//	message.c_str(),
	//	szTitle,
	//	MB_ICONINFORMATION);


	MyDialogData data;
	data.title_ = szTitle;
	data.message_ = message;
	if (IDOK != DialogBoxParam(hInstance,
		MAKEINTRESOURCE(IDD_DIALOG_MAIN),
		NULL,
		MyDlgProc,
		(LPARAM)&data))
	{
		return 100;
	}

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
