// stdafx.h : 標準のシステム インクルード ファイルのインクルード ファイル、または
// 参照回数が多く、かつあまり変更されない、プロジェクト専用のインクルード ファイル
// を記述します。

#pragma once

#ifndef VC_EXTRALEAN
#define VC_EXTRALEAN            // Windows ヘッダーから使用されていない部分を除外します。
#endif

#include "targetver.h"

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS      // 一部の CString コンストラクターは明示的です。

#include <afxwin.h>         // MFC のコアおよび標準コンポーネント
#include <afxext.h>         // MFC の拡張部分

#ifndef _AFX_NO_OLE_SUPPORT
#include <afxole.h>         // MFC OLE クラス
#include <afxodlgs.h>       // MFC OLE ダイアログ クラス
#include <afxdisp.h>        // MFC オートメーション クラス
#endif // _AFX_NO_OLE_SUPPORT

#ifndef _AFX_NO_DB_SUPPORT
#include <afxdb.h>                      // MFC ODBC データベース クラス
#endif // _AFX_NO_DB_SUPPORT

#ifndef _AFX_NO_DAO_SUPPORT
#include <afxdao.h>                     // MFC DAO データベース クラス
#endif // _AFX_NO_DAO_SUPPORT

#ifndef _AFX_NO_OLE_SUPPORT
#include <afxdtctl.h>           // MFC の Internet Explorer 4 コモン コントロール サポート
#endif
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>                     // MFC の Windows コモン コントロール サポート
#endif // _AFX_NO_AFXCMN_SUPPORT


#include <string>
#include <vector>
#include <algorithm>
#include <set>

#include "../../lsMisc/stdwin32/stdwin32.h"

#include "../../lsMisc/browseFolder.h"
#include "../../lsMisc/SHMoveFile.h"
#include "../../lsMisc/sqliteserialize.h"
#include "../../lsMisc/tstring.h"

#include <afxcontrolbars.h>

#include <windows.h>
#include <comdef.h> 
#include <tchar.h>
#include <objbase.h>
#include <shlobj.h>
#include <ShObjIdl.h>

#include <io.h>
#include <Shellapi.h>

#include <afxdialogex.h>

#include <windows.h>
#include <tchar.h>
#include <assert.h>

#include <vector>
#include <string>

#include <cassert>
#include <string>

#define STRING2(x) #x
#define STRING(x) STRING2(x)
#pragma message("WINVER        : " STRING(WINVER))
#pragma message("_MSC_VER: " STRING(_MSC_VER))
#include <stlsoft/smartptr/scoped_handle.hpp>

#include <windows.h>

#include <algorithm> 
#include <cassert>
#include <cctype>
#include <cstdarg>
#include <cstdio>
#include <functional> 
#include <locale>
#include <string>
#include <vector>
#include <memory.h>

#include <shlwapi.h>


#include "../../lsMisc/I18N.h"
// #define I18N(s) (s)


#define DllImport   __declspec( dllimport )  
#define DllExport   __declspec( dllexport ) 

extern LPCWSTR gAppName;