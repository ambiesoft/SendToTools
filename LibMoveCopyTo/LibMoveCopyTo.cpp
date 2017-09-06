// LibMoveCopyTo.cpp : DLL の初期化ルーチンです。
//

#include "stdafx.h"

#include "LibMoveCopyTo.h"

#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/GetLastErrorString.h"
#include "ChooseDirDialog.h"


#pragma comment(lib,"shlwapi.lib")


using std::wstring;
using std::vector;
using std::find;
using std::set;

using Ambiesoft::ArgCount;
using Ambiesoft::COption;
using Ambiesoft::CCommandLineParser;
using Ambiesoft::sqlGetPrivateProfileStringArray;
using Ambiesoft::sqlWritePrivateProfileStringArray;

typedef vector<wstring> STRINGVECTOR;



#define		DE_SAMEFILE			0x71
#define		DE_MANYSRC1DEST			0x72
#define		DE_DIFFDIR			0x73
#define		DE_ROOTDIR			0x74
#define		DE_OPCANCELLED			0x75
#define		DE_DESTSUBTREE			0x76
#define		DE_ACCESSDENIEDSRC			0x78
#define		DE_PATHTOODEEP			0x79
#define		DE_MANYDEST			0x7A
#define		DE_INVALIDFILES			0x7C
#define		DE_DESTSAMETREE			0x7D
#define		DE_FLDDESTISFILE			0x7E
#define		DE_FILEDESTISFLD			0x80
#define		DE_FILENAMETOOLONG			0x81
#define		DE_DEST_IS_CDROM			0x82
#define		DE_DEST_IS_DVD			0x83
#define		DE_DEST_IS_CDRECORD			0x84
#define		DE_FILE_TOO_LARGE			0x85
#define		DE_SRC_IS_CDROM			0x86
#define		DE_SRC_IS_DVD			0x87
#define		DE_SRC_IS_CDRECORD			0x88
#define		DE_ERROR_MAX			0xB7
//#define		0x402			0x402
#define		ERRORONDEST			0x10000


#define SEC_OPTION L"option"
#define KEY_DIRS L"Dirs"

void ShowError(LPCWSTR pMessage)
{
	MessageBox(NULL,
		pMessage,
		APPNAME,
		MB_ICONERROR);
}

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

//
//TODO: この DLL が MFC DLL に対して動的にリンクされる場合、
//MFC 内で呼び出されるこの DLL からエクスポートされたどの関数も
//関数の最初に追加される AFX_MANAGE_STATE マクロを
//持たなければなりません。
//
//例:
//
//extern "C" BOOL PASCAL EXPORT ExportedFunction()
//{
//AFX_MANAGE_STATE(AfxGetStaticModuleState());
//// 通常関数の本体はこの位置にあります
//}
//
//このマクロが各関数に含まれていること、MFC 内の
//どの呼び出しより優先することは非常に重要です。
//これは関数内の最初のステートメントでなければな 
//らないことを意味します、コンストラクターが MFC
//DLL 内への呼び出しを行う可能性があるので、オブ
//ジェクト変数の宣言よりも前でなければなりません。
//
//詳細については MFC テクニカル ノート 33 および
//58 を参照してください。
//






extern "C" {
	DllExport int libmain();
}

int libmain()
{
	//UNREFERENCED_PARAMETER(hPrevInstance);
	//UNREFERENCED_PARAMETER(lpCmdLine);


	COption opTarget(L"/T", L"/t", 1);
	COption opFile(L"", Ambiesoft::ArgCount_Infinite);
	CCommandLineParser cmd;
	cmd.AddOption(&opTarget);
	cmd.AddOption(&opFile);
	cmd.Parse();

	wstring destDir = opTarget.getFirstValue();
	STRINGVECTOR sourcefiles;
	for (unsigned int i = 0; i < opFile.getValueCount(); ++i)
	{
		sourcefiles.push_back(opFile.getValue(i));
	}
	if (sourcefiles.empty())
	{
		ShowError(I18N(L"Source file is empty."));
		return 1;
	}

	for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
	{
		if (!PathFileExists(it->c_str()))
		{
			wstring msg = stdwin32::string_format(I18N(L"\"%s\" does not exist."), it->c_str());
			ShowError(msg.c_str());
			return 1;
		}
	}


	wstring dbFile = stdwin32::stdCombinePath(
		stdwin32::stdGetParentDirectory(stdwin32::stdGetModuleFileName()),
		APPNAME L".db");
	vector<wstring> allPrevSave;

	if (!sqlGetPrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allPrevSave, dbFile.c_str()))
	{
		ShowError(I18N(L"Failed to load from db."));
		return 1;
	}

	STRINGVECTOR allSaving;

	if (destDir.empty())
	{
		//if (allPrevSave.empty())
		//{
		//	TCHAR szFolder[MAX_PATH];
		//	if (!browseFolder(NULL, I18N(L"Move to"), szFolder))
		//		return 0;
		//	if (!PathIsDirectory(szFolder))
		//	{
		//		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is not a folder."), szFolder);
		//		ShowError(msg.c_str());
		//		return 1;
		//	}
		//	destDir = szFolder;
		//}
		//else
		{
			CChooseDirDialog dlg;
			for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
			{
				dlg.m_strSource += it->c_str();
				dlg.m_strSource += L"\r\n";
			}
			
			set<wstring> dupcheck;
			for each(wstring s in allPrevSave)
			{
				if (dupcheck.find(s) == dupcheck.end())
				{
					dlg.m_arDirs.Add(s.c_str());
					dupcheck.insert(s);
				}
			}
			if (IDOK != dlg.DoModal())
				return 0;

			destDir = dlg.m_strDirResult;

			for (int i = 0; i < dlg.m_arDirs.GetSize(); ++i)
			{
				allSaving.push_back(wstring(dlg.m_arDirs[i]));
			}
		}
	}

	if (destDir.empty() || !stdwin32::stdIsFullPath(destDir.c_str()))
	{
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is empty or not full path."), destDir.c_str());
		ShowError(msg.c_str());
		return 1;
	}

	if (!PathIsDirectory(destDir.c_str()))
	{
		if (PathFileExists(destDir.c_str()))
		{
			wstring msg = stdwin32::string_format(I18N(L"\"%s\" is a file."), destDir.c_str());
			ShowError(msg.c_str());
			return 1;
		}

		wstring msg = stdwin32::string_format(I18N(L"\"%s\" does not exist. Do you want to create a new folder?"), destDir.c_str());
		if (IDYES != MessageBox(NULL,
			msg.c_str(),
			APPNAME,
			MB_ICONQUESTION | MB_YESNO))
		{
			return 1;
		}

		CreateDirectory(destDir.c_str(), NULL);
	}

	// final check
	if (!PathIsDirectory(destDir.c_str()))
	{
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" is not a folder."), destDir.c_str());
		ShowError(msg.c_str());
		return 1;
	}


	destDir = stdwin32::stdAddBackSlash(destDir);

#ifdef _DEBUG
	bool bShowDebugInfo = true;
#else
	bool bShowDebugInfo = (GetAsyncKeyState(VK_CONTROL) < 0) && (GetAsyncKeyState(VK_SHIFT) < 0);
#endif

	if(bShowDebugInfo)
	{
		wstring msg = L"SOURCE: \r\n";
		for (STRINGVECTOR::iterator it = sourcefiles.begin(); it != sourcefiles.end(); ++it)
		{
			msg += *it;
			msg += L"\r\n";
		}

		msg.append(L"\r\n");
		msg.append(L"DEST: \r\n" + destDir);
		MessageBox(NULL, msg.c_str(), L"DEBUG", MB_OK);
	}

	int nRet = 0;
	if (!SHMoveFile(destDir.c_str(), sourcefiles, &nRet))
	{
		wstring error;
		switch (nRet)
		{
		case DE_SAMEFILE: /*0x71*/error = L"The source and destination files are the same file."; break;
		case DE_MANYSRC1DEST:/*0x72*/error = L"Multiple file paths were specified in the source buffer, but only one destination file path."; break;
		case DE_DIFFDIR:/*0x73*/error = L"Rename operation was specified but the destination path is a different directory.Use the move operation instead."; break;
		case DE_ROOTDIR:/*0x74*/error = L"The source is a root directory, which cannot be moved or renamed."; break;
		case DE_OPCANCELLED:/*0x75*/error = L"The operation was canceled by the user, or silently canceled if the appropriate flags were supplied to SHFileOperation."; break;
		case DE_DESTSUBTREE:/*0x76*/error = L"The destination is a subtree of the source."; break;
		case DE_ACCESSDENIEDSRC:/*0x78*/error = L"Security settings denied access to the source."; break;
		case DE_PATHTOODEEP:/*0x79*/error = L"The source or destination path exceeded or would exceed MAX_PATH."; break;
		case DE_MANYDEST:/*0x7A*/error = L"The operation involved multiple destination paths, which can fail in the case of a move operation."; break;
		case DE_INVALIDFILES:/*0x7C*/error = L"The path in the source or destination or both was invalid."; break;
		case DE_DESTSAMETREE:/*0x7D*/error = L"The source and destination have the same parent folder."; break;
		case DE_FLDDESTISFILE:/*0x7E*/error = L"The destination path is an existing file."; break;
		case DE_FILEDESTISFLD:/*0x80*/error = L"The destination path is an existing folder."; break;
		case DE_FILENAMETOOLONG:/*0x81*/error = L"The name of the file exceeds MAX_PATH."; break;
		case DE_DEST_IS_CDROM:/*0x82*/error = L"The destination is a read - only CD - ROM, possibly unformatted."; break;
		case DE_DEST_IS_DVD:/*0x83*/error = L"The destination is a read - only DVD, possibly unformatted."; break;
		case DE_DEST_IS_CDRECORD:/*0x84*/error = L"The destination is a writable CD - ROM, possibly unformatted."; break;
		case DE_FILE_TOO_LARGE:/*0x85*/error = L"The file involved in the operation is too large for the destination media or file system."; break;
		case DE_SRC_IS_CDROM:/*0x86*/error = L"The source is a read - only CD - ROM, possibly unformatted."; break;
		case DE_SRC_IS_DVD:/*0x87*/error = L"The source is a read - only DVD, possibly unformatted."; break;
		case DE_SRC_IS_CDRECORD:/*0x88*/error = L"The source is a writable CD - ROM, possibly unformatted."; break;
		case DE_ERROR_MAX:/*0xB7*/error = L"MAX_PATH was exceeded during the operation."; break;
		case 0x402:/*0x402*/error = L"An unknown error occurred.This is typically due to an invalid path in the source or destination.This error does not occur on Windows Vista and later."; break;
		case ERRORONDEST:/*0x10000*/error = L"An unspecified error occurred on the destination."; break;
		case DE_ROOTDIR | ERRORONDEST:/*0x10074*/error = L"Destination is a root directory and cannot be renamed."; break;
		default:
			error = GetLastErrorString(nRet);
		}
		ShowError(error.c_str());
	}


	vector<wstring>::iterator cIter = find(allSaving.begin(), allSaving.end(), destDir);
	if (cIter == allSaving.end())
		allSaving.push_back(destDir);

	if (!sqlWritePrivateProfileStringArray(SEC_OPTION, KEY_DIRS, allSaving, dbFile.c_str()))
	{
		ShowError(I18N(L"Failed to save to db."));
		return 1;
	}


	return 0;
}















// CLibMoveCopyToApp

BEGIN_MESSAGE_MAP(CLibMoveCopyToApp, CWinApp)
END_MESSAGE_MAP()


// CLibMoveCopyToApp コンストラクション

CLibMoveCopyToApp::CLibMoveCopyToApp()
{
	// TODO: この位置に構築用コードを追加してください。
	// ここに InitInstance 中の重要な初期化処理をすべて記述してください。
}


// 唯一の CLibMoveCopyToApp オブジェクトです。

CLibMoveCopyToApp theApp;


// CLibMoveCopyToApp 初期化

BOOL CLibMoveCopyToApp::InitInstance()
{
	CWinApp::InitInstance();

	return TRUE;
}

