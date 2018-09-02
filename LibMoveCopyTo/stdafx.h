#pragma once

#ifndef VC_EXTRALEAN
#define VC_EXTRALEAN
#endif

#include "targetver.h"

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS

#include <afxwin.h>
#include <afxext.h>

#ifndef _AFX_NO_OLE_SUPPORT
#include <afxole.h>
#include <afxodlgs.h>
#include <afxdisp.h>
#endif // _AFX_NO_OLE_SUPPORT

#ifndef _AFX_NO_DB_SUPPORT
#include <afxdb.h>
#endif // _AFX_NO_DB_SUPPORT

#ifndef _AFX_NO_DAO_SUPPORT
#include <afxdao.h>
#endif // _AFX_NO_DAO_SUPPORT

#ifndef _AFX_NO_OLE_SUPPORT
#include <afxdtctl.h>
#endif
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>
#endif // _AFX_NO_AFXCMN_SUPPORT


#include <string>
#include <vector>
#include <algorithm>
#include <set>

#include "../../lsMisc/stdwin32/stdwin32.h"
#include "../../lsMisc/stdosd/stdosd.h"

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