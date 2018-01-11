// stdafx.h : �W���̃V�X�e�� �C���N���[�h �t�@�C���̃C���N���[�h �t�@�C���A�܂���
// �Q�Ɖ񐔂������A�����܂�ύX����Ȃ��A�v���W�F�N�g��p�̃C���N���[�h �t�@�C��
// ���L�q���܂��B

#pragma once

#ifndef VC_EXTRALEAN
#define VC_EXTRALEAN            // Windows �w�b�_�[����g�p����Ă��Ȃ����������O���܂��B
#endif

#include "targetver.h"

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS      // �ꕔ�� CString �R���X�g���N�^�[�͖����I�ł��B

#include <afxwin.h>         // MFC �̃R�A����ѕW���R���|�[�l���g
#include <afxext.h>         // MFC �̊g������

#ifndef _AFX_NO_OLE_SUPPORT
#include <afxole.h>         // MFC OLE �N���X
#include <afxodlgs.h>       // MFC OLE �_�C�A���O �N���X
#include <afxdisp.h>        // MFC �I�[�g���[�V���� �N���X
#endif // _AFX_NO_OLE_SUPPORT

#ifndef _AFX_NO_DB_SUPPORT
#include <afxdb.h>                      // MFC ODBC �f�[�^�x�[�X �N���X
#endif // _AFX_NO_DB_SUPPORT

#ifndef _AFX_NO_DAO_SUPPORT
#include <afxdao.h>                     // MFC DAO �f�[�^�x�[�X �N���X
#endif // _AFX_NO_DAO_SUPPORT

#ifndef _AFX_NO_OLE_SUPPORT
#include <afxdtctl.h>           // MFC �� Internet Explorer 4 �R���� �R���g���[�� �T�|�[�g
#endif
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>                     // MFC �� Windows �R���� �R���g���[�� �T�|�[�g
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