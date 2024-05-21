#include <Windows.h>
#include <shlobj_core.h>
#include <Shlwapi.h>

#include <string>
#include <map>
#include <memory>
#include <deque>
#include <list>

#include "../../lsMisc/CreateSimpleWindow.h"
#include "../../lsMisc/GetLastErrorString.h"
#include "../../lsMisc/GetFilesInfo.h"
#include "../../lsMisc/OpenCommon.h"
#include "../../lsMisc/DebugMacro.h"
#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/CommandLineString.h"
#include "../../lsMisc/GetVersionStringFromResource.h"
#include "../../lsMisc/HighDPI.h"
#include "../../lsMisc/CHandle.h"
#include "../../lsMisc/stop_watch.h"
#include "../../lsMisc/Is64.h"
#include "../../lsMisc/CreateShortcutFile.h"
#include "../../lsMisc/stdosd/stdosd.h"
#include "../../lsMisc/I18N.h"
#include "../../profile/cpp/Profile/include/ambiesoft.profile.h"

#include "MoreSendTo.h"

#pragma comment(lib, "Shell32.lib")
#pragma comment(lib, "Msimg32.lib")

using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;
using namespace std;

#define APPNAME L"MoreSendTo"
#define WAIT_AFTER_LAUNCH (5 * 1000)
#define WAIT_FOR_PROCESSIDLE (30 * 1000)

CHMenu ghPopup;
TCHAR szT[MAX_PATH];
CHFont gMenuFont;

enum {
	MENUID_DUMMY = 1,
	MENUID_NOITEM,
	MENUID_NORECENTITEM,
	MENUID_OPTIONS,
	MENUID_CLEAR_RECENT_ITEMS,
	MENUID_START,
	MENUID_END = 65535 / 2,
	MENUID_RECENT_START,
	MENUID_RECENT_END,
};
HINSTANCE ghInst;
WORD gMenuIndex;
map<UINT, wstring> gCmdMap;
map<HMENU, wstring> gPopupMap;
wstring qlRoot;
bool gbNoIcon = false;
bool gbShowHidden = false;
int gIconWidth, gIconHeight;
UINT gItemHeight;
UINT gItemDeltaY;
UINT gItemDeltaX;
bool gbNoRecentItems = false;
list<string> gRecents_;
wstring gArgToPass;

#ifndef NDEBUG

#define DEBUG_DRAW
//#define DEBUG_INITPOPUP

unique_ptr<wstop_watch> gsw;
#define TRACE_STOPWATCH(S) do { OutputDebugString(S);OutputDebugString(L" "); OutputDebugString(gsw->lookDiff().c_str()); OutputDebugString(L"\r\n"); }while(false)

#ifdef DEBUG_DRAW
#define DTRACE_DRAW(s) DTRACE(s)
#else
#define DTRACE_DRAW(s) do{}while(false)
#endif

#ifdef DEBUG_INITPOPUP
#define DTRACE_INITPOPUP(s) DTRACE(s)
#else
#define DTRACE_INITPOPUP(s) do{}while(false)
#endif

#else
#define TRACE_STOPWATCH(S) do{}while(false)
#define DTRACE_DRAW(s) do{}while(false)
#define DTRACE_INITPOPUP(s) do{}while(false)
#endif

std::wstring GetIniPath()
{
	return stdCombinePath(
		stdGetParentDirectory(stdGetModuleFileName()),
		L"MoreSendTo.ini");
}

void ErrorExit(const wchar_t* pMessage, int ret = -1)
{
	MessageBox(nullptr,
		pMessage,
		APPNAME,
		MB_ICONERROR);
	ExitProcess(ret);
}
void ErrorExit(const wstring& message)
{
	ErrorExit(message.c_str());
}
void ErrorExit(DWORD le)
{
	ErrorExit(GetLastErrorString(le).c_str(), le);
}

CHIcon getIconFromPath(LPCWSTR pShortcut)
{
	DASSERT(pShortcut && pShortcut[0]);
	bool bUseTargetExe = true;

	wstring targetExe;
	wstring iconPath;
	if (bUseTargetExe)
		GetShortcutFileInfo(pShortcut, &targetExe, &iconPath, nullptr, nullptr, nullptr);
	wstring loadString;
	if (bUseTargetExe)
		loadString = !iconPath.empty() ? iconPath.c_str() : (!targetExe.empty() ? targetExe.c_str() : pShortcut);
	SHFILEINFO sfi = { 0 };
	if (!SHGetFileInfo(bUseTargetExe ? loadString.c_str() : pShortcut,
		0,
		&sfi,
		sizeof(sfi),
		SHGFI_ICON | SHGFI_SMALLICON))
	{
		if (!SHGetFileInfo(pShortcut,
			0,
			&sfi,
			sizeof(sfi),
			SHGFI_ICON | SHGFI_SMALLICON))
		{
			ErrorExit(GetLastError());
		}
	}
	return CHIcon(sfi.hIcon);

}
void makeOwnerDraw(HMENU hMenu, UINT cmd)
{
	MENUITEMINFO mii;
	ZeroMemory(&mii, sizeof(mii));
	mii.cbSize = sizeof(mii);
	mii.fMask = MIIM_TYPE;
	if (!GetMenuItemInfo(hMenu, cmd, FALSE, &mii))
		ErrorExit(GetLastError());
	mii.cbSize = sizeof(mii);
	mii.fMask = MIIM_TYPE;
	mii.fType |= MFT_OWNERDRAW;
	if (!SetMenuItemInfo(hMenu, cmd, FALSE, &mii))
		ErrorExit(GetLastError());
}
void setMenuHidden(HMENU hMenu, UINT_PTR cmd)
{
	MENUITEMINFO mii = { 0 };
	mii.cbSize = sizeof(mii);
	mii.fMask = MIIM_DATA;
	mii.dwItemData = 1;
	if (!SetMenuItemInfo(hMenu, (UINT)cmd, FALSE, &mii))
		ErrorExit(GetLastError());
}
LRESULT CALLBACK WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		//case WM_MENUSELECT:
		//{
		//	WORD index = LOWORD(wParam);
		//	HMENU hMenu = (HMENU)lParam;
		//	GetMenuString(hMenu, index, szT, _countof(szT), MF_BYPOSITION);
		//	DTRACE(wstring() + L"WM_MENUSELECT : " + szT);
		//	glastSelectedPopup = szT;
		//}
		//break;

	case WM_MEASUREITEM:
	{
		MEASUREITEMSTRUCT* mis = (MEASUREITEMSTRUCT*)lParam;
		GetMenuString(ghPopup, mis->itemID, szT, _countof(szT), MF_BYCOMMAND);
		CHDC dc(GetDC(hWnd));
		HFONT old = (HFONT)SelectObject(dc, gMenuFont);
		SIZE size;
		if (!GetTextExtentPoint32(dc, szT, lstrlen(szT), &size))
			ErrorExit(GetLastError());
		mis->itemHeight = gItemHeight;
		mis->itemWidth = gIconWidth + gItemDeltaX + size.cx;
		SelectObject(dc, old);
		return TRUE;
	}
	break;

	case WM_DRAWITEM:
	{
		DRAWITEMSTRUCT* dis = (DRAWITEMSTRUCT*)lParam;

		const bool bHidden = dis->itemData ? true : false;

		DTRACE_DRAW(stdFormat(L"itemAction=%d, itemState=%d, hidden=%s",
			dis->itemAction,
			dis->itemState,
			bHidden ? L"Hidden" : L"Normal"
		));

		int colorMenuText = -1;
		int colorMuenuTextBk = -1;
		int colorBk = -1;
		if (dis->itemAction == ODA_DRAWENTIRE)
		{
			colorMenuText = bHidden ? COLOR_GRAYTEXT : COLOR_MENUTEXT;
		}
		else if (dis->itemAction == ODA_SELECT)
		{
			if (dis->itemState & ODS_SELECTED)
			{
				// newly selected
				colorMuenuTextBk = COLOR_MENUHILIGHT;
				colorMenuText = COLOR_HIGHLIGHTTEXT;
				colorBk = COLOR_MENUHILIGHT;
			}
			else
			{
				// deselected
				colorMenuText = bHidden ? COLOR_GRAYTEXT : COLOR_MENUTEXT;
				colorBk = COLOR_MENU;
			}
		}
		else
			assert(false);

		assert(colorMenuText != -1);
		SetTextColor(dis->hDC, GetSysColor(colorMenuText));
		if (colorMuenuTextBk != -1)
			SetBkColor(dis->hDC, GetSysColor(colorMuenuTextBk));
		if (colorBk != -1)
		{
			HBRUSH brush = GetSysColorBrush(colorBk);
			HBRUSH old = (HBRUSH)SelectObject(dis->hDC, brush);
			FillRect(dis->hDC, &dis->rcItem, brush);
			SelectObject(dis->hDC, old);
		}

		if (!DrawIconEx(dis->hDC,
			dis->rcItem.left, dis->rcItem.top + gItemDeltaY,
			(MENUID_END < dis->itemID) ?
			getIconFromPath(gPopupMap[(HMENU)(UINT_PTR)dis->itemID].c_str()) :
			getIconFromPath(gCmdMap[dis->itemID].c_str()),
			gIconWidth, gIconHeight,
			0, 0, DI_MASK | DI_IMAGE))
		{
			ErrorExit(GetLastError());
		}
		RECT rS = dis->rcItem;
		rS.top += gItemDeltaY;
		rS.left += gIconWidth + gItemDeltaX;
		GetMenuString(ghPopup, dis->itemID, szT, _countof(szT), MF_BYCOMMAND);
		DTRACE_DRAW(stdFormat(L"DrawText=%s", szT));
		DrawText(dis->hDC, szT, lstrlen(szT), &rS, 0);

		return TRUE;
	}
	break;
	case WM_INITMENUPOPUP:
	{
		HMENU hMenu = (HMENU)wParam;
		WORD index = LOWORD(lParam);
		TRACE_STOPWATCH(L"WM_INITMENUPOPUP");
		while (DeleteMenu(hMenu, 0, MF_BYPOSITION))
			;

		const wstring sel = gPopupMap[hMenu];
		if (sel == stdGetModuleFileName())
		{
			DASSERT(!gbNoRecentItems);

			// recent
			bool bRecentAdded = false;
			for (auto&& recentItem : gRecents_)
			{
				if (PathFileExists(toStdWstringFromUtf8(recentItem).c_str()))
				{
					UINT cmd = gMenuIndex++ + MENUID_START;
					AppendMenu(hMenu,
						MF_BYCOMMAND,
						cmd,
						stdGetFileNameWithoutExtension(toStdWstringFromUtf8(recentItem)).c_str());
					if (!gbNoIcon)
					{
						makeOwnerDraw(hMenu, cmd);
					}
					gCmdMap[cmd] = toStdWstringFromUtf8(recentItem);
					bRecentAdded = true;
				}
			}
			if (gRecents_.empty() || !bRecentAdded)
			{
				InsertMenu(hMenu, 0, MF_BYCOMMAND | MF_DISABLED, MENUID_NORECENTITEM, L"<No Recent Items>");
			}
			if (!gRecents_.empty())
			{
				InsertMenu(hMenu, GetMenuItemCount(hMenu), MF_BYPOSITION | MF_SEPARATOR, 0, 0);
				InsertMenu(hMenu, GetMenuItemCount(hMenu), MF_BYPOSITION | MF_BYCOMMAND, MENUID_CLEAR_RECENT_ITEMS, I18N(L"&Clear Recent Items"));
			}
			break;
		}
		const bool bTopPopup = sel.empty();
		DTRACE_INITPOPUP(L"sel=" + sel);
		wstring qlDir = bTopPopup ? qlRoot : sel;
		DTRACE_INITPOPUP(L"qlDir=" + qlDir);
		FILESINFOW fi;
		if (!GetFilesInfoW(qlDir.c_str(), fi))
			ErrorExit(GetLastError());
		TRACE_STOPWATCH(L"WM_INITMENUPOPUP GetFilesInfoW");
		for (UINT i = 0; i < fi.GetCount(); ++i)
		{
			const bool bHidden = (fi[i].dwFileAttributes & FILE_ATTRIBUTE_HIDDEN);
			if (!gbShowHidden && bHidden)
				continue;
			if (fi[i].dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
			{
				DTRACE_INITPOPUP(stdFormat(L"WM_INITMENUPOPUP:DIR:") + fi[i].cFileName);
				HMENU hPopup = CreatePopupMenu();
				InsertMenu(hPopup, 0, MF_BYCOMMAND, MENUID_DUMMY, L"<DUMMY>");
				AppendMenu(hMenu,
					MF_POPUP,
					(UINT_PTR)hPopup, fi[i].cFileName);
				if (bHidden)
					setMenuHidden(hMenu, (UINT_PTR)hPopup);
				if (!gbNoIcon)
				{
					makeOwnerDraw(hMenu, (UINT)(UINT_PTR)hPopup);
				}
				gPopupMap[hPopup] = stdCombinePath(qlDir, fi[i].cFileName);
			}
		}

		for (UINT i = 0; i < fi.GetCount(); ++i)
		{
			const bool bHidden = (fi[i].dwFileAttributes & FILE_ATTRIBUTE_HIDDEN);
			if (!gbShowHidden && bHidden)
				continue;
			if (!(fi[i].dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
			{
				DTRACE_INITPOPUP(stdFormat(L"WM_INITMENUPOPUP:FILE:") + fi[i].cFileName);
				TRACE_STOPWATCH(L"WM_INITMENUPOPUP process file");
				UINT cmd = gMenuIndex++ + MENUID_START;
				AppendMenu(hMenu,
					MF_BYCOMMAND,
					cmd,
					stdGetFileNameWithoutExtension(fi[i].cFileName).c_str());
				if (bHidden)
					setMenuHidden(hMenu, cmd);
				if (!gbNoIcon)
				{
					makeOwnerDraw(hMenu, cmd);
				}

				const wstring full = stdCombinePath(qlDir, fi[i].cFileName);
				gCmdMap[cmd] = full;
			}
		}
		if (0 == GetMenuItemCount(hMenu))
		{
			InsertMenu(hMenu, 0, MF_BYCOMMAND | MF_DISABLED, MENUID_NOITEM, L"<No Items>");
		}
		if (bTopPopup)
		{
			AppendMenu(hMenu, 0, MF_SEPARATOR, 0);
			InsertMenu(hMenu, GetMenuItemCount(hMenu), MF_BYPOSITION | MF_BYCOMMAND, MENUID_OPTIONS, I18N(L"&Options..."));

			if (!gbNoRecentItems)
			{
				// Insert recent at top
				HMENU hPopupRecent = CreatePopupMenu();
				InsertMenu(hMenu, 0, MF_POPUP | MF_BYPOSITION, (UINT_PTR)hPopupRecent, I18N(L"Recent Items"));
				InsertMenu(hMenu, 1, MF_SEPARATOR | MF_BYPOSITION, 0, 0);
				UINT cmd = gMenuIndex++ + MENUID_START;
				AppendMenu(hPopupRecent,
					MF_BYCOMMAND,
					cmd,
					L"Dummy");
				if (!gbNoIcon)
				{
					makeOwnerDraw(hMenu, (UINT)(UINT_PTR)hPopupRecent);
				}
				gPopupMap[hPopupRecent] = stdGetModuleFileName();
			}

			// Show command line at the top
			int iTopIndex = 0;
			InsertMenu(hMenu, iTopIndex++, MF_BYPOSITION | MF_DISABLED, MENUID_DUMMY, I18N(L"The command line arguments are"));
			InsertMenu(hMenu, iTopIndex++, MF_BYPOSITION | MF_DISABLED, MENUID_DUMMY, gArgToPass.c_str());
			InsertMenu(hMenu, iTopIndex++, MF_BYPOSITION | MF_SEPARATOR, 0, 0);
		}
	}
	break;
	}
	return DefWindowProc(hWnd, msg, wParam, lParam);
}

wstring getMessageTitleString()
{
	return stdFormat(L"%s v%s (%s)",
		APPNAME,
		GetVersionStringFromResource(nullptr, 3).c_str(),
		Is64BitProcess() ? L"x64" : L"x86");
}

void runOption()
{
	OpenCommon(nullptr,
		stdCombinePath(stdGetParentDirectory(stdGetModuleFileName()),
			L"MoreSendtoOption.exe").c_str());
}

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	ghInst = hInstance;
#ifndef NDEBUG
	gsw = make_unique<wstop_watch>();
#endif

	i18nInitLangmap(hInstance, NULL, APPNAME);

	InitHighDPISupport();

	int nRecentItemCount = DEFAULT_RECENT_ITEMCOUNT;
	Profile::CHashIni ini(Profile::ReadAll(GetIniPath()));
	{
		Profile::GetBool(SECTION_OPTION, KEY_SHOW_HIDDEN, false, gbShowHidden, ini);
		Profile::GetBool(SECTION_OPTION, KEY_NO_ICON, false, gbNoIcon, ini);
		Profile::GetBool(SECTION_OPTION, KEY_NO_RECENTITEMS, false, gbNoRecentItems, ini);
		Profile::GetInt(SECTION_OPTION, KEY_RECENTITEMCOUNT, DEFAULT_RECENT_ITEMCOUNT,
			nRecentItemCount, ini);
	}

	// If no command line or Pressed with RButton,
	// Shows Option dialog.
	// Thought this app is supposed to be launched from SendTo menu,
	// it has arguments of file name(s).
	if (stdTrim(wstring(lpCmdLine)).empty() ||
		::GetAsyncKeyState(VK_RBUTTON) < 0)
	{
		runOption();
		return 0;
	}
	CCommandLineParser parser(I18N(L"MoreSendTo"), APPNAME);

	bool bVersion = false;
	parser.AddOptionRange({ L"--moresendto-version" }, 0, &bVersion, ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Shows version"));

	bool bHelp = false;
	parser.AddOptionRange({ L"--moresendto-help" }, 0, &bHelp, ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Shows Help"));

	bool bExplorer = false;
	parser.AddOptionRange({ L"--moresendto-explorer" }, 0, &bExplorer, ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Shows in Explorer (Press SHIFT in startup)"));

	bool bPinMe = false;
	parser.AddOptionRange({ L"--moresendto-pinme" }, 0, &bPinMe, ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Shows MessageBox for pinning this app."));

	parser.AddOptionRange({ L"--moresendto-noicon" }, 0, &gbNoIcon, ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Shows no icons"));

	parser.AddOptionRange({ L"--moresendto-showhidden" }, 0, &gbShowHidden, ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Shows hidden directories"));
	
	wstring targetFolder;
	parser.AddOptionRange({ L"--moresendto-sendtofolder" }, ArgCount::ArgCount_One, &targetFolder, ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"More SendTo Folder"));

	parser.Parse();

	// Find if arguments have only "/?" or other help option and set bHelp
	{
		CCommandLineString cmdLine;
		// 2 means this app and one command
		if (cmdLine.getCount() == 2)
		{
			if (cmdLine.getArg(1) == L"/?" ||
				cmdLine.getArg(1) == L"/h" ||
				cmdLine.getArg(1) == L"-h" ||
				cmdLine.getArg(1) == L"--help")
			{
				bHelp = true;
			}
		}
	}

	if (bVersion)
	{
		wstring message = stdFormat(L"%s v%s",
			APPNAME, GetVersionStringFromResource(nullptr, 3).c_str()).c_str();
		MessageBox(nullptr,
			message.c_str(),
			getMessageTitleString().c_str(),
			MB_ICONINFORMATION);
		return 0;
	}
	if (bHelp)
	{
		MessageBox(nullptr,
			parser.getHelpMessage().c_str(),
			getMessageTitleString().c_str(),
			MB_ICONINFORMATION);
		return 0;
	}
	if (bPinMe)
	{
		MessageBox(nullptr,
			I18N(L"Please pin this in the taskbar."),
			getMessageTitleString().c_str(),
			MB_ICONINFORMATION);
		return 0;
	}

	if (!gbNoRecentItems)
	{
		Profile::GetStringArray(SECTION_RECENTS, KEY_RECENT_ITEMS, gRecents_, ini);
		DASSERT(nRecentItemCount >= 0);
		if (gRecents_.size() > static_cast<size_t>(nRecentItemCount))
			gRecents_.resize(nRecentItemCount);
	}
	TRACE_STOPWATCH(L"Started");

	if (targetFolder.empty())
	{
		string targetFolderUtf8;
		Profile::GetString(SECTION_OPTION, KEY_MORESENDTO_FOLDER, string(), targetFolderUtf8, ini);
		if (targetFolderUtf8.empty())
		{
			ErrorExit(I18N(L"More Sendto Folder is empty"));
		}
		targetFolder = toStdWstringFromUtf8(targetFolderUtf8);
	}
	if (!stdDirectoryExists(targetFolder))
	{
		ErrorExit(stdFormat(I18N(L"More Sendto Folder '%s' does not exist."), targetFolder.c_str()));
	}
	
	CcoInitializer coIniter;
	CHWnd wnd(CreateSimpleWindow(WndProc));

	qlRoot = targetFolder;
	if ((GetFileAttributes(qlRoot.c_str()) & FILE_ATTRIBUTE_DIRECTORY) == 0)
	{
		ErrorExit(stdFormat(I18N(L"'%s' is not a directory."), qlRoot.c_str()));
	}

	gArgToPass = []() {
		// Create arg from aruguments of this app except this app exe
		CCommandLineString cls(GetCommandLineW());
		size_t i = 1;
		for (; i < cls.getCount(); ++i)
		{
			if (cls.getArg(i).rfind(L"--moresendto-sendtofolder", 0) == 0)
			{
				// pos=0 limits the search to the prefix
				// arg starts with prefix
				i += 2;
				continue;
			}
			else if (cls.getArg(i).rfind(L"--moresendto-", 0) == 0)
			{
				i++;
				continue;
			}
			break;
		}
		if ((i - 1) == 0)
			return cls.subString(1);
		return cls.subString(i - 1);
	}();

	DTRACE(stdFormat(L"gArgToPass:%s", gArgToPass.c_str()).c_str());

	if (bExplorer || GetAsyncKeyState(VK_SHIFT) < 0)
	{
		qlRoot = stdAddPathSeparator(qlRoot);
		OpenCommon(wnd, qlRoot.c_str());
		return 0;
	}

	if (!gbNoIcon)
	{
		constexpr int heihtDelta = 2;
		gIconWidth = GetSystemMetrics(SM_CXSMICON);
		gIconHeight = GetSystemMetrics(SM_CYSMICON);
		gItemHeight = gIconHeight + (2 * heihtDelta);
		gItemDeltaX = 4;
		gItemDeltaY = heihtDelta;

		NONCLIENTMETRICS ncm = { 0 };
		ncm.cbSize = sizeof(ncm);
		if (!SystemParametersInfo(SPI_GETNONCLIENTMETRICS, 0, &ncm, 0))
			ErrorExit(GetLastError());
		gMenuFont = CreateFontIndirect(&ncm.lfMenuFont);
	}

	ghPopup = CreatePopupMenu();
	gPopupMap[ghPopup] = wstring();
	InsertMenu(ghPopup, 0, MF_BYCOMMAND, MENUID_DUMMY, L"<DUMMY>");

	POINT curPos;
	GetCursorPos(&curPos);
	SetForegroundWindow(wnd);
	TRACE_STOPWATCH(L"Before TrackPopup");
	const UINT cmd = TrackPopupMenu(ghPopup,
		TPM_RETURNCMD,
		curPos.x, curPos.y,
		0,
		wnd,
		NULL);
	TRACE_STOPWATCH(L"After TrackPopup");

	if (false)
	{
	}
	else if (cmd == MENUID_OPTIONS)
	{
		runOption();
	}
	else if (cmd == MENUID_CLEAR_RECENT_ITEMS)
	{
		if (!Profile::WriteStringArray(SECTION_RECENTS, KEY_RECENT_ITEMS,
			decltype(gRecents_)(),
			GetIniPath()))
		{
			ErrorExit(I18N(L"Failed to save recent items"));
		}
	}
	else if (gCmdMap.find(cmd) != gCmdMap.end())
	{
		const wstring command = gCmdMap[cmd];
		CKernelHandle processHandle;
		const BOOL bOpened =
			OpenCommon(wnd, command.c_str(), gArgToPass.c_str(), NULL, &processHandle);
		if (!gbNoRecentItems)
		{
			gRecents_.remove(toStdUtf8String(gCmdMap[cmd]));
			gRecents_.push_front(toStdUtf8String(gCmdMap[cmd]));

			DASSERT(nRecentItemCount >= 0);
			if (gRecents_.size() > static_cast<size_t>(nRecentItemCount))
				gRecents_.resize(nRecentItemCount);
			if (!Profile::WriteStringArray(SECTION_RECENTS, KEY_RECENT_ITEMS,
				gRecents_, GetIniPath()))
			{
				ErrorExit(I18N(L"Failed to save recent items"));
			}
		}
		if (bOpened)
		{
			WaitForInputIdle(processHandle, WAIT_FOR_PROCESSIDLE);
		}
	}
	return 0;
}
