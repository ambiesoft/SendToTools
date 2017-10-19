// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>

// C RunTime Header Files
#include <stdlib.h>
#include <malloc.h>
#include <memory.h>
#include <tchar.h>
#include <string>
#include <vector>
#include <ctime>

#include <assert.h>


// TODO: reference additional headers your program requires here
#include "../../lsMisc/SetClipboardText.h"
#include "../../lsMisc/showballoon.h"
#include "../../lsMisc/stdwin32/stdwin32.h"
#include "../../lsMisc/StdStringReplace.h"
#include "../../lsMisc/CenterWindow.h"
#include "../../lsMisc/I18N.h"

enum {
	WM_APP_INITIALUPDATE = (WM_APP + 1)
};