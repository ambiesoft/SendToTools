// LibMoveCopyTo.cpp : DLL の初期化ルーチンです。
//

#include "stdafx.h"
#include "LibMoveCopyTo.h"





#pragma comment(lib,"shlwapi.lib")


using std::wstring;
using std::vector;
using std::find;
using std::set;
#include "../../MyUtility/CommandLineParser.h"
using Ambiesoft::COption;
using Ambiesoft::CCommandLineParser;



#include "ChooseDirDialog.h"


#define SEC_OPTION		L"option"
#define KEY_DIRS		L"Dirs"

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
//		MFC 内で呼び出されるこの DLL からエクスポートされたどの関数も
//		関数の最初に追加される AFX_MANAGE_STATE マクロを
//		持たなければなりません。
//
//		例:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// 通常関数の本体はこの位置にあります
//		}
//
//		このマクロが各関数に含まれていること、MFC 内の
//		どの呼び出しより優先することは非常に重要です。
//		これは関数内の最初のステートメントでなければな 
//		らないことを意味します、コンストラクターが MFC
//		DLL 内への呼び出しを行う可能性があるので、オブ
//		ジェクト変数の宣言よりも前でなければなりません。
//
//		詳細については MFC テクニカル ノート 33 および
//		58 を参照してください。
//






extern "C" {
	DllExport int libmain();
}

int libmain()
{
	//UNREFERENCED_PARAMETER(hPrevInstance);
	//UNREFERENCED_PARAMETER(lpCmdLine);
	

	COption opTarget(L"/T", L"/t", 1);
	COption opFile(L"");
	CCommandLineParser cmd;
	cmd.AddOption(&opTarget);
	cmd.AddOption(&opFile);
	cmd.Parse();

	wstring destDir = opTarget.getFirstValue();
	wstring sourcefile = opFile.getFirstValue();
	if (sourcefile.empty())
	{
		ShowError(I18N(L"Source file is empty."));
		return 1;
	}
	if (!PathFileExists(sourcefile.c_str()))
	{
		wstring msg = stdwin32::string_format(I18N(L"\"%s\" does not exist."), sourcefile.c_str());
		ShowError(msg.c_str());
		return 1;
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

	vector<wstring> allSaving;

	if (destDir.empty())
	{
		if (allPrevSave.empty())
		{
			TCHAR szFolder[MAX_PATH];
			if (!browseFolder(NULL, I18N(L"Move to"), szFolder))
				return 0;
			if (!PathIsDirectory(szFolder))
			{
				wstring msg = stdwin32::string_format(I18N(L"\"%s\" is not a folder."), szFolder);
				ShowError(msg.c_str());
				return 1;
			}
			destDir = szFolder;
		}
		else
		{
			CChooseDirDialog dlg;
			dlg.m_strSource = sourcefile.c_str();
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
	{
		wstring msg = L"SOURCE: " + sourcefile + L"\r\n";
		msg.append(L"DEST: " + destDir);
		MessageBox(NULL, msg.c_str(), L"DEBUG", MB_OK);
	}
#endif
	if (!SHMoveFile(destDir.c_str(), sourcefile.c_str()))
		return 1;


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

