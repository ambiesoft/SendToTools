#include "StdAfx.h"
#include <map>
#include "../../lsMisc/CHandle.h"
#include "../../lsMisc/UrlEncode.h"
#include "../../lsMisc/OpenCommon.h"
#include "../../lsMisc/FormatSizeof.h"

#include "resource.h"

using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;
using namespace std;

#define APPNAME L"SwapFilename"

void ErrorExit(const wstring& error, LPCTSTR funcname)
{
	MessageBox(nullptr,
		stdFormat(L"%s\n%s", error.c_str(), funcname).c_str(),
		APPNAME,
		MB_ICONERROR);

	exit(1);
}
void ErrorExit(const wstring& error)
{
	ErrorExit(error, L"");
}

BOOL DoReplaceFilePlane(wstring file1, wstring file2, wstring fileback)
{
	int ret;
	
	ret = SHMoveFileEx(file1.c_str(), fileback.c_str());
	if (ret != 0)
	{
		ErrorExit(GetLastErrorString(ret), L"SHMoveFile");
	}
	ret = SHMoveFileEx(file2.c_str(), file1.c_str());
	if (ret != 0)
	{
		ErrorExit(GetLastErrorString(ret), L"SHMoveFile");
	}
	ret = SHMoveFileEx(fileback.c_str(), file2.c_str());
	if (ret != 0)
	{
		ErrorExit(GetLastErrorString(ret), L"SHMoveFile");
	}
	return TRUE;
}

enum ReplaceTransactionResult {
	RTR_TRNAS_NA,
	RTR_TRUE,
	RTR_FALSE,
};
ReplaceTransactionResult DoReplaceFileWithTransaction(wstring file1, wstring file2, wstring fileback)
{
	CAtlTransactionManager trans(FALSE);
	if (!trans.GetHandle())
		return RTR_TRNAS_NA;

	auto failfunc = [&]() {
		DVERIFY(trans.Rollback());
		DVERIFY(trans.Close());
	};
	if (!trans.MoveFile(file1.c_str(), fileback.c_str()))
	{
		failfunc();
		return RTR_FALSE;
	}
	if (!trans.MoveFile(file2.c_str(), file1.c_str()))
	{
		failfunc();
		return RTR_FALSE;
	}
	if (!trans.MoveFile(fileback.c_str(), file2.c_str()))
	{
		failfunc();
		return RTR_FALSE;
	}

	return trans.Commit() ? RTR_TRUE : RTR_FALSE;
}
BOOL DoReplaceFileWithReplaceFile(wstring file1, wstring file2, wstring fileback)
{
	if (!ReplaceFile(
		file1.c_str(),
		file2.c_str(),
		fileback.c_str(),
		0, 0, 0 // flags and reserved
		))
	{
		// ErrorExit(GetLastErrorString(GetLastError()), L"ReplaceFile");
		// Use regular method
		return FALSE;
	}
	if (PathFileExists(file2.c_str()))
	{
		ErrorExit(I18N(L"Unknown Error"), L"");
	}
	if (!MoveFile(fileback.c_str(), file2.c_str()))
	{
		ErrorExit(GetLastErrorString(GetLastError()), L"MoveFile");
	}
	return TRUE;
}

struct OldAndNew
{
	wstring olderFile;
	wstring newerFile;
};
static OldAndNew GetOlderFile(const wstring& file1, const wstring& file2, bool* pIsSame)
{
	CFileHandle h1(CreateFile(file1.c_str(),
		GENERIC_READ,
		FILE_SHARE_READ,
		NULL,
		OPEN_EXISTING,
		FILE_ATTRIBUTE_NORMAL,
		NULL));
	CFileHandle h2(CreateFile(file2.c_str(),
		GENERIC_READ,
		FILE_SHARE_READ,
		NULL,
		OPEN_EXISTING,
		FILE_ATTRIBUTE_NORMAL,
		NULL));
	if (!h1 || !h2)
		return OldAndNew();

	FILETIME ftLastWrite1, ftLastWrite2;
	if (!GetFileTime(h1, NULL, NULL, &ftLastWrite1) || !GetFileTime(h2, NULL, NULL, &ftLastWrite2))
		return OldAndNew();

	LONG result = CompareFileTime(&ftLastWrite1, &ftLastWrite2);
	*pIsSame = (result == 0);

	OldAndNew ret;
	if (result < 0) 
	{
		ret.olderFile = file1;
		ret.newerFile = file2;
	} 
	else
	{
		ret.olderFile = file2;
		ret.newerFile = file1;
	}
	return ret;
}

static bool showballoonexe(
	const wchar_t* title,
	const wchar_t* text,
	const wchar_t* iconexe,
	int duration,
	DWORD dwBalloonIcon)
{
	const wstring thisapp = stdGetModuleFileName();
	wstring app = stdCombinePath(
		stdGetParentDirectory(thisapp),
		L"showballoon.exe");
	wstring cmd = stdFormat(L"/title:%s /icon:\"%s\" /defaulticon /duration:%d /balloonicon:%d %s",
		UrlEncodeStd(title).c_str(),
		iconexe,
		duration,
		dwBalloonIcon,
		UrlEncodeStd(text).c_str());
		
	return OpenCommon(NULL,
		app.c_str(),
		cmd.c_str());
}

wstring GetLangFromCommandLine()
{
	CCommandLineParser parser;

	wstring lang;
	parser.AddOption(L"-lang",
		1,
		&lang,
		ArgEncodingFlags_Default,
		L"Language in 3-letter");

	parser.Parse();

	return lang;
}
bool GetFileSizeFromPath(const wchar_t* pPath, LARGE_INTEGER* pLI)
{
	CFileHandle h1(CreateFile(pPath,
		GENERIC_READ,
		FILE_SHARE_READ,
		NULL,
		OPEN_EXISTING,
		FILE_ATTRIBUTE_NORMAL,
		NULL));
	if (!h1)
		return false;
	return !!GetFileSizeEx(h1, pLI);
}


bool doWork(wstring file1, wstring file2,
	const bool bAlwaysYes, const bool bRemoveOld,
	wstring* pAdditionalMessage,
	function<bool(wstring oldFile, wstring newFile)> fnCondition)
{
	wstring fileback = GetBackupFile(file1.c_str());

	constexpr const wchar_t* STR_FILE_DOES_NOT_EXIST = L"'%s' does not exist.";
	if (!PathFileExists(file1.c_str()))
	{
		ErrorExit(stdFormat(I18N(STR_FILE_DOES_NOT_EXIST), file1.c_str()));
	}
	if (!PathFileExists(file2.c_str()))
	{
		ErrorExit(stdFormat(I18N(STR_FILE_DOES_NOT_EXIST), file2.c_str()));
	}
	if (PathFileExists(fileback.c_str()))
	{
		ErrorExit(stdFormat(I18N(STR_FILE_DOES_NOT_EXIST), fileback.c_str()));
	}

	file1 = stdGetFullPathName(file1);
	file2 = stdGetFullPathName(file2);

	FILETIME ftCreation1, ftLastAccess1, ftLastWrite1;
	FILETIME ftCreation2, ftLastAccess2, ftLastWrite2;
	bool bFtGot = false;
	do
	{
		CFileHandle h1(CreateFile(file1.c_str(),
			GENERIC_READ,
			FILE_SHARE_READ,
			NULL,
			OPEN_EXISTING,
			FILE_ATTRIBUTE_NORMAL,
			NULL));
		CFileHandle h2(CreateFile(file2.c_str(),
			GENERIC_READ,
			FILE_SHARE_READ,
			NULL,
			OPEN_EXISTING,
			FILE_ATTRIBUTE_NORMAL,
			NULL));
		if (!h1 || !h2)
			break;
		if (!GetFileTime(h1, &ftCreation1, &ftLastAccess1, &ftLastWrite1))
			break;
		if (!GetFileTime(h2, &ftCreation2, &ftLastAccess2, &ftLastWrite2))
			break;
		bFtGot = true;
	} while (false);

	// rename
	if (PathIsUNC(file1.c_str()) || PathIsUNC(file2.c_str()))
	{
		// Transaction does not support UNC
		if (!DoReplaceFileWithReplaceFile(file1, file2, fileback))
		{
			DoReplaceFilePlane(file1, file2, fileback);
		}
	}
	else
	{
		switch (DoReplaceFileWithTransaction(file1, file2, fileback))
		{
		case RTR_TRNAS_NA:
			// transaction is not available
			DoReplaceFilePlane(file1, file2, fileback);
		case RTR_FALSE:
			ErrorExit(GetLastErrorString(GetLastError()));
		}
	}

	// change time
	if (bFtGot)
	{
		do {
			CFileHandle h1(CreateFile(file1.c_str(),
				GENERIC_WRITE,
				FILE_SHARE_READ,
				NULL,
				OPEN_EXISTING,
				FILE_ATTRIBUTE_NORMAL,
				NULL));
			CFileHandle h2(CreateFile(file2.c_str(),
				GENERIC_WRITE,
				FILE_SHARE_READ,
				NULL,
				OPEN_EXISTING,
				FILE_ATTRIBUTE_NORMAL,
				NULL));
			if (!h1 || !h2)
				break;
			DVERIFY(SetFileTime(h1, &ftCreation2, &ftLastAccess2, &ftLastWrite2));
			DVERIFY(SetFileTime(h2, &ftCreation1, &ftLastAccess1, &ftLastWrite1));
		} while (false);
	}

	wstring additionalMessage;
	bool bShowRemoveConfirmMessage = !bAlwaysYes;

	if (bRemoveOld)
	{
		do
		{
			bool isSame = false;
			auto oldnew = GetOlderFile(file1, file2, &isSame);
			if (isSame)
			{
				additionalMessage = I18N(L"File time is same.");
				break;
			}
			if (oldnew.olderFile.empty())
			{
				ErrorExit(I18N(L"Failed to find an older file."));
			}
			if (fnCondition)
			{
				if (!fnCondition(oldnew.olderFile, oldnew.newerFile))
					break;
			}
			if (bShowRemoveConfirmMessage)
			{
				wstring sizeString;
				LARGE_INTEGER liFileSize;
				liFileSize.QuadPart = 0;
				if (GetFileSizeFromPath(oldnew.olderFile.c_str(), &liFileSize))
					sizeString = FormatSizeof(liFileSize.QuadPart);
				else
					sizeString = I18N(L"Unknown");

				wstring msg = stdFormat(I18N(L"Do you want to remove '%s' of size '%s'?"),
					oldnew.olderFile.c_str(),
					sizeString.c_str()
				).c_str();

				if (IDYES != MessageBox(NULL,
					msg.c_str(),
					stdFormat(I18N(L"Remove older file | %s"), APPNAME).c_str(),
					MB_ICONQUESTION | MB_YESNO | MB_DEFBUTTON2))
				{
					break;
				}
			}
			int nRet = SHDeleteFileEx(oldnew.olderFile.c_str());
			if (nRet != 0)
			{
				ErrorExit(GetLastErrorString(nRet));
			}
			*pAdditionalMessage = stdFormat(I18N(L"'%s' has been removed."), stdGetFileName(oldnew.olderFile).c_str());
		} while (false);
	}

	return true;
}

bool IsOldFileIsLessSize(wstring oldFile, wstring newFile)
{
	LARGE_INTEGER oldSize;
	LARGE_INTEGER newSize;
	if (!GetFileSizeFromPath(oldFile.c_str(), &oldSize) ||
		!GetFileSizeFromPath(newFile.c_str(), &newSize))
	{
		ErrorExit(I18N(L"Failed to get file size"));
	}
	return oldSize.QuadPart < newSize.QuadPart;
}

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	Ambiesoft::InitHighDPISupport();
	wstring lang = GetLangFromCommandLine();
	i18nInitLangmap(NULL, lang.c_str(), APPNAME);

	CCommandLineParser parser(I18N(L"Swap filename of two files"));

	bool bRemoveOld = false;
	parser.AddOption(L"-removeold", 0, &bRemoveOld,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Remove an older file after swapping"));

	wstring strRemoveOldIf;
	parser.AddOption(L"-removeoldif", 1, &strRemoveOldIf,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Remove an older file after swapping if the condition is met. The condition is one of 'older_is_less_size', 'older_is_greater_size'"));

	bool bAutoDetect = false;
	parser.AddOption(L"-autodetect", 0, &bAutoDetect,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Detects a pair of files automatically, files with same name but extensions will be detected"));

	bool bAutoDetectAll = false;
	parser.AddOption(L"-autodetectall", 0, &bAutoDetectAll,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Detects all files automatically, files with same name but extensions will be detected"));

	bool bAlwaysYes = false;
	parser.AddOption(L"-alwaysyes", 0, &bAlwaysYes,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Answer 'yes' in all questions"));

	bool bHelp = false;
	parser.AddOptionRange({ L"-h",L"--help", L"/?" },
		0,
		&bHelp,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Show Help"));

	bool bShowVersion = false;
	parser.AddOptionRange({ L"-v",L"--version" },
		0,
		&bShowVersion,
		ArgEncodingFlags::ArgEncodingFlags_Default,
		I18N(L"Show Version"));

	COption opMain(L"",
		ArgCount::ArgCount_OneToInfinite,
		ArgEncodingFlags_Default,
		I18N(L"Input two files or directories if '-autodetect'"));
	parser.AddOption(&opMain);

	// lang is already defined
	parser.AddOption(L"-lang",
		1,
		&lang,
		ArgEncodingFlags_Default,
		I18N(L"Language in 3-letter"));

	parser.Parse();

	if (parser.hadUnknownOption())
	{
		ErrorExit(I18N(L"Unknown Option:") + (L"\r\n" + parser.getUnknowOptionStrings()));
	}
	if (bShowVersion || bHelp)
	{
		MessageBox(NULL,
			parser.getHelpMessage().c_str(),
			stdFormat(L"%s v%s", APPNAME, GetVersionStringFromResource(NULL, 3).c_str()).c_str(),
			MB_ICONINFORMATION);
		return 0;
	}

	struct F1F2
	{
		wstring file1;
		wstring file2;
	};
	
	vector<F1F2> filepairs;

	if (bAutoDetect|| bAutoDetectAll)
	{
		// get dirs from command line
		if (opMain.getValueCount() == 0)
		{
			ErrorExit(stdFormat(
				I18N(L"Directory must be specified if '-autodetect'"),
				opMain.getValueCount()));
		}
		wstring dir = opMain.getValue(0);
		if (!stdDirectoryExists(dir))
		{
			ErrorExit(stdFormat(
				I18N(L"'%s' is not a directory"),
				dir.c_str()));
		}
		// detect files
		vector<wstring> files = stdGetFiles(dir,
			FILEITERATEMODE::SKIP_DOT_AND_DOTDOT,
			GETFILESEMODE::FILE);

		map<wstring, vector<wstring>> nameToFileNames;
		for (auto&& file : files)
		{
			nameToFileNames[stdStringLower(stdGetFileNameWithoutExtension(file))].push_back(file);
		}

		for (auto&& kv : nameToFileNames)
		{
			if (nameToFileNames[kv.first].size() == 2)
			{
				filepairs.push_back(F1F2{ nameToFileNames[kv.first][0], nameToFileNames[kv.first][1] });
				if (bAutoDetect)
					break;  // detect only one pair
			}
		}
	}
	else
	{
		if (opMain.getValueCount() != 2)
		{
			ErrorExit(stdFormat(
				I18N(L"The count of files provided by command line is %d. Two files must be provided."),
				opMain.getValueCount()));
		}
		filepairs.push_back(F1F2{ opMain.getValue(0), opMain.getValue(1) });
	}

	// Detect Ctrl or RButton early, PathIsUNC can take time
	const bool bCtrlOrRbuttonPressed = ((GetKeyState(VK_CONTROL) < 0) || GetKeyState(VK_RBUTTON) < 0);
	if (bCtrlOrRbuttonPressed)
	{
		bRemoveOld = true;
	}

	function<bool(wstring oldFile, wstring newFile)> fnCondition = nullptr;
	if (!strRemoveOldIf.empty())
	{
		if (strRemoveOldIf == L"older_is_less_size") {
			fnCondition = IsOldFileIsLessSize;
		} 
		else if (strRemoveOldIf == L"older_is_greater_size") {
			fnCondition = [](wstring oldFile, wstring newFile) {
				return !IsOldFileIsLessSize(oldFile, newFile);
			};
		}
		else
		{
			ErrorExit(I18N(L"Invalid condition:") + strRemoveOldIf);
		}
		bRemoveOld = true;
	}

	for (auto&& fp : filepairs)
	{
		wstring additionalMessage;
		doWork(fp.file1, fp.file2,
			bAlwaysYes, bRemoveOld,
			&additionalMessage,
			fnCondition);

		wstring balloontext = stdFormat(
			I18N(L"'%s' and '%s' have been swapped."),
			stdGetFileName(fp.file1).c_str(),
			stdGetFileName(fp.file2).c_str()) +
			(additionalMessage.empty() ? L"" : L"\r\n" + additionalMessage);

		showballoonexe(
			APP_NAME,
			balloontext.c_str(),
			stdGetModuleFileName().c_str(),
			5000,
			1);
	}
	return 0;
}

