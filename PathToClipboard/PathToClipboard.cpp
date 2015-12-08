// PathToClipboard.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include <ctime>
#include "PathToClipboard.h"
#include "../../MyUtility/SetClipboardText.h"
#include "../../MyUtility/showballoon.h"
#include "../../MyUtility/stdwin32/stdwin32.h"
using namespace stdwin32;
using namespace std;

#define I18N(s) (s)

#define KAIGYO L"\r\n"

#define MAX_LOADSTRING 100

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


int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: Place code here.

    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);


	if (__argc < 2)
	{
		showErrorAndExit(I18N(L"No Arguments"));
	}

	wstring str;
	for (int i = 1; i < __argc;++i)
	{
		str += __wargv[i];
		str += KAIGYO;
	}
	str=trim(str, KAIGYO);

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


