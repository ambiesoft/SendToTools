


#include "stdafx.h"
#include <string>
#include <Winternl.h>
#include <Shellapi.h>

#include "argCheckProcess.h"

#define I18N(t) t

using namespace std;

#define MAX_LOADSTRING 100

#define KAIGYO L"\n";

HINSTANCE hInst;
WCHAR szTitle[MAX_LOADSTRING];
WCHAR szWindowClass[MAX_LOADSTRING];

// http://forum.sysinternals.com/get-commandline-of-running-processes_topic6510_page1.html
typedef NTSTATUS(__stdcall *PZWQUERYSYSTEMINFORMATION)(
	IN SYSTEM_INFORMATION_CLASS  SystemInformationClass,
	OUT PVOID  SystemInformation,
	IN ULONG  Length,
	OUT PULONG  ReturnLength
	);
PZWQUERYSYSTEMINFORMATION ZwQuerySystemInformation = NULL;

LPWSTR SkipSpace(LPWSTR pCommand)
{
	while (*pCommand)
	{
		if (!iswspace(*pCommand))
			break;
		++pCommand;
	}
	return pCommand;
}

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadStringW(hInstance, IDC_ARGCHECKPROCESS, szWindowClass, MAX_LOADSTRING);

	if (__argc < 2)
	{
		MessageBox(NULL,
			I18N(L"No Arguments"),
			szTitle,
			MB_ICONASTERISK);
		return 1;
	}

	ZwQuerySystemInformation = (PZWQUERYSYSTEMINFORMATION)GetProcAddress(GetModuleHandle(L"ntdll.dll"), "ZwQuerySystemInformation");
	if (!ZwQuerySystemInformation) {
		MessageBox(NULL,
			I18N(L"ZwQuerySystemInformation is unavailable."),
			szTitle,
			MB_ICONASTERISK);
		return 1;
	}

	LPWSTR pCommand = GetCommandLine();
	pCommand = SkipSpace(pCommand);
	if (*pCommand == L'\'' || *pCommand == L'"')
	{
		WCHAR q = *pCommand;
		while (++*pCommand) {
			if (*pCommand != q)
				break;
		}
	}
	pCommand = SkipSpace(pCommand);

	STARTUPINFO si = { 0 };
	si.cb = sizeof(si);
	PROCESS_INFORMATION pi = { 0 };
	
	if (!CreateProcess(
		NULL,
		pCommand,
		NULL, //sec
		NULL, //sec
		FALSE, // inheritHandle
		NORMAL_PRIORITY_CLASS, // flag
		NULL, //env
		NULL, //dir,
		&si,
		&pi))
	{
		MessageBox(NULL,
			I18N(L"CreateProcess failed."),
			szTitle,
			MB_ICONASTERISK);
		return 1;
	}




	return 0;
}


