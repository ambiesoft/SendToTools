// MoreSendTo.cpp : Defines the entry point for the application.
//

#include "../../lsMisc/CreateSimpleWindow.h"

#include "framework.h"
#include "MoreSendTo.h"

using namespace Ambiesoft;

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    HWND hSimpleWindow = CreateSimpleWindow();
    ShowWindow(hSimpleWindow, SW_SHOW);
    // TODO: Place code here.
    HMENU popup = CreatePopupMenu();
    UINT id = IDM_MENU_START;
    UINT pos = 0;
    InsertMenu(popup, pos++, MF_BYPOSITION, id++, L"AAA");
    InsertMenu(popup, pos++, MF_BYPOSITION, id++, L"BBB");
    InsertMenu(popup, pos++, MF_BYPOSITION, id++, L"CCC");
    InsertMenu(popup, pos++, MF_BYPOSITION, id++, L"DDD");

    POINT cursorPoint;
    GetCursorPos(&cursorPoint);
    PostMessage(hSimpleWindow, WM_NULL, 0, 0);
    id = TrackPopupMenu(popup,
        TPM_RETURNCMD,
        cursorPoint.x, cursorPoint.y,
        0,
        hSimpleWindow,
        NULL);

    return 0;
}
