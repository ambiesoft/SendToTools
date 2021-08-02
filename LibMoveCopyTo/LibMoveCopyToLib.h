#pragma once
#include <Windows.h>

#ifdef LIBMOVECOPYTO_LIB
#define EXORIM DllExport
#else
#define EXORIM DllImport
#endif
extern "C" {
	EXORIM int libmain(LPCWSTR pAppName, LPCWSTR pButtonText, HICON hIcon);
}
