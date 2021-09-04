#include <Windows.h>
#include <Shlwapi.h>

#include <atlstr.h>
#include <atltransactionmanager.h>

#include <string>

#include "../../lsMisc/DebugMacro.h"
#include "../../lsMisc/stdosd/stdosd.h"
#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/HighDPI.h"
#include "../../lsMisc/GetLastErrorString.h"
#include "../../lsMisc/GetBackupFile.h"
#include "../../lsMisc/showballoon.h"
#include "../../lsMisc/SHMoveFile.h"
#include "../../lsMisc/GetVersionString.h"
#include "../../lsMisc/I18N.h"

#define APP_NAME L"SwapFilename"