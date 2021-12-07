#include <Windows.h>
#include <Shlwapi.h>
#include <string>
#include <thread>
#include <sstream>
#include <vector>
#include "../../lsMisc/stdosd/stdosd.h"
#include "../../lsMisc/OpenCommon.h"
#include "../../lsMisc/SetClipboardText.h"
#include "../../lsMisc/GetClipboardText.h"
#include "../../lsMisc/CHandle.h"
#include "../../lsMisc/UTF16toUTF8.h"

using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;
using namespace std;

#pragma comment(lib, "Shlwapi.lib")

void ErrorExit(wstring message)
{
	MessageBox(nullptr, message.c_str(), L"testCopyPath", MB_ICONERROR);
	exit(1);
}
void ShowMessage(wstring message)
{
	MessageBox(nullptr, message.c_str(), L"testCopyPath", MB_ICONINFORMATION);
}
#define ExT(b) do                              \
{                                              \
	if (!(b))                                  \
    {                                          \
        ErrorExit(stdFormat(L"%s:%d", __FILEW__, __LINE__));\
    }                                          \
} while (false)

template<class T>
wstring Report(string aName, T a, string bName, T b)
{
	stringstream ss;
	ss << aName << "=" << a << endl;
	ss << bName << "=" << b << endl;
	return toStdWstringFromACP(ss.str());
}

wstring Report(string aName, wstring a, string bName, wstring b)
{
	stringstream ss;
	ss << aName << "=" << toStdAcpString(a) << endl;
	ss << bName << "=" << toStdAcpString(b) << endl;
	return toStdWstringFromACP(ss.str());
}
#define ExEq(a,b) do{ if(a!=b) { ErrorExit(stdFormat(L"%s\r\n\r\n%s:%d",  Report(#a,a,#b,b).c_str(), __FILEW__, __LINE__));         }}while(false)
int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPWSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	wstring exe = stdFormat(L"C:\\Linkout\\SendTo Tools%s\\tools\\CopyPath.exe",
#ifndef _DEBUG
		L""
#else
		L"D"
#endif
	);
	if(!PathFileExists(exe.c_str()))
		ErrorExit(L"exe not found");

	wstring thisexe = stdGetModuleFileName();

	bool bShowHelpTest = false;
	if (bShowHelpTest)
	{
		ShowMessage(L"Showing 4 helps");
		thread t1([&]() {
			OpenCommon(nullptr, exe.c_str(), L"-h");
			});
		thread t2([&]() {
			OpenCommon(nullptr, exe.c_str(), L"/h");
			});
		thread t3([&]() {
			OpenCommon(nullptr, exe.c_str(), L"/?");
			});
		thread t4([&]() {
			OpenCommon(nullptr, exe.c_str(), L"--help");
			});
		t1.join(); t2.join(); t3.join(); t4.join();
	}

	CKernelHandle handle;
	wstring clip;

	// unknown option
	{
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			L"--no-ini --no-balloon --unknown-opppppppppppppppption",
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
	}
	// normal
	{
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(L"--no-ini --no-balloon " + stdAddDQIfNecessary(thisexe)).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(thisexe, clip);
	}
	{
		LPCWSTR pFile = L"C:\\a a\\b";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(L"--no-ini --no-balloon " + stdAddDQIfNecessary(pFile)).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(stdAddDQIfNecessary(pFile), clip);
	}

	// nameonly true
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"AAA BBB CCC";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -n 1 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(p, clip);
	}

	// pathsep = slash
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"C:\\aaa\\bbb\\ccc C:\\xxx\\yyy\\zzz";
		LPCWSTR pr = L"C:/aaa/bbb/ccc C:/xxx/yyy/zzz";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -p 2 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(pr, clip);
	}
	// dq default
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"\"C:\\aaa\\bb b\\ccc\" C:\\xxx\\yyy\\zzz";
		LPCWSTR pr = L"\"C:\\aaa\\bb b\\ccc\" C:\\xxx\\yyy\\zzz";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -q 0 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(pr, clip);
	}
	// dq yes
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"C:\\aaa\\bbb\\ccc C:\\xxx\\yyy\\zzz";
		LPCWSTR pr = L"\"C:\\aaa\\bbb\\ccc\" \"C:\\xxx\\yyy\\zzz\"";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -q 1 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(pr, clip);
	}
	// dq no
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"\"C:\\aaa\\bb b\\ccc\" C:\\xxx\\yyy\\zzz";
		LPCWSTR pr = L"C:\\aaa\\bb b\\ccc C:\\xxx\\yyy\\zzz";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -q 2 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(pr, clip);
	}
	// multiline
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"\"C:\\aaa\\bb b\\ccc\" C:\\xxx\\yyy\\zzz";
		LPCWSTR pr = L"\"C:\\aaa\\bb b\\ccc\"\r\nC:\\xxx\\yyy\\zzz";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -m 1 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(pr, clip);
	}
	// multiline nodq
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"\"C:\\aaa\\bb b\\ccc\" C:\\xxx\\yyy\\zzz";
		LPCWSTR pr = L"C:\\aaa\\bb b\\ccc\r\nC:\\xxx\\yyy\\zzz";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -q 2 -m 1 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(pr, clip);
	}
	// multiline dq
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"\"C:\\aaa\\bb b\\ccc\" C:\\xxx\\yyy\\zzz";
		LPCWSTR pr = L"\"C:\\aaa\\bb b\\ccc\"\r\n\"C:\\xxx\\yyy\\zzz\"";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -q 1 -m 1 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(pr, clip);
	}
	// code 0 C++
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"\"C:\\aaa\\bb b\\ccc\" C:\\xxx\\yyy\\zzz";
		LPCWSTR pr = L"const char* filenames[] = {\"C:\\\\aaa\\\\bb b\\\\ccc\", \"C:\\\\xxx\\\\yyy\\\\zzz\", };";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -c 0 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExEq(pr, clip);
	}
	// code 0 C++ multiline
	{
		vector<wstring> vs;
		vs.push_back(L"AAA");
		vs.push_back(L"BBB");
		vs.push_back(L"CCC");
		LPCWSTR p = L"\"C:\\aaa\\bb b\\ccc\" C:\\xxx\\yyy\\zzz";
		LPCWSTR pr1 = L"\"C:\\\\aaa\\\\bb b\\\\ccc\"";
		LPCWSTR pr2 = L"\"C:\\\\xxx\\\\yyy\\\\zzz\"";
		ExT(OpenCommon(nullptr,
			exe.c_str(),
			(wstring(L"--no-ini --no-balloon -c 0 -m 1 ") + p).c_str(),
			nullptr, &handle));
		ExEq(WaitForSingleObject(handle, INFINITE), WAIT_OBJECT_0);
		ExT(GetClipboardText(nullptr, clip));
		ExT(clip.find(pr1) != clip.npos);
		ExT(clip.find(pr2) != clip.npos);
	}

	return 0;
}