
// WaitNext.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"

#include "../../lsMisc/SingleAppMutex.h"
#include "../../lsMisc/CommandLineParser.h"
#include "../../lsMisc/CommandLineString.h"
#include "../../lsMisc/stdwin32/stdwin32.h"

#include "WaitNext.h"
#include "WaitNextDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

using namespace Ambiesoft;
using namespace std;
using namespace stdwin32;

// CWaitNextApp

BEGIN_MESSAGE_MAP(CWaitNextApp, CWinApp)
	ON_COMMAND(ID_HELP, &CWinApp::OnHelp)
END_MESSAGE_MAP()


// CWaitNextApp construction

CWaitNextApp::CWaitNextApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}


// The one and only CWaitNextApp object

CWaitNextApp theApp;


// CWaitNextApp initialization
BOOL CWaitNextApp::parseCommandLine()
{
	ASSERT(mainArgs_.empty());

	CCommandLineParser parser(CaseFlags::CaseFlags_Default, I18N(L"Wait additional argument and launch"));
	COption mainOption;
	parser.AddOption(&mainOption);

	parser.Parse();

	if (!mainOption.hadValue())
	{
		CString strMsg = I18N(L"Main argument not found.");
		strMsg += L"\r\n\r\n";
		strMsg += parser.getHelpMessage().c_str();

		AfxMessageBox(strMsg);
		return FALSE;
	}

	for (size_t i = 0; i < mainOption.getValueCount(); ++i)
	{
		mainArgs_.emplace_back(mainOption.getValue(i));
	}
	return TRUE;
}


BOOL CWaitNextApp::InitInstance()
{

	// InitCommonControlsEx() is required on Windows XP if an application
	// manifest specifies use of ComCtl32.dll version 6 or later to enable
	// visual styles.  Otherwise, any window creation will fail.
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// Set this to include all the common control classes you want to use
	// in your application.
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();

	if (!parseCommandLine())
		return FALSE;

	CSingleAppMutex singleApp(L"WaitNext-SingleApp-Mutex");
	if (!singleApp.IsOK())
	{
		ASSERT(FALSE);
		AfxMessageBox(I18N(L"Failed to create mutex."));
		return FALSE;
	}
	if (singleApp.IsSecond())
	{
		vector<HWND> vHwnds = CSingleAppMutex::GetExistingWindow(AfxGetAppName(), nullptr);
		if (vHwnds.empty())
		{
			AfxMessageBox(I18N(L"Existing window not found."));
			return FALSE;
		}

		wstring dataS = CCommandLineString::getCommandLine(mainArgs_);
		unsigned int len = (dataS.size()+1) * sizeof(wchar_t);
		unsigned char* data = (unsigned char*)(dataS.data());
		if (!CSingleAppMutex::SendInfo(vHwnds[0], nullptr, 0, data, len))
		{
			AfxMessageBox(I18N(L"Failed to send data to existing window."));
			return FALSE;
		}
		return FALSE;
	}


	// Create the shell manager, in case the dialog contains
	// any shell tree view or shell list view controls.
	CShellManager *pShellManager = new CShellManager;

	// Activate "Windows Native" visual manager for enabling themes in MFC controls
	CMFCVisualManager::SetDefaultManager(RUNTIME_CLASS(CMFCVisualManagerWindows));

	{
		wstring inipathfull = stdCombinePath(stdGetParentDirectory(stdGetModuleFileName()),
			stdGetFileNameWitoutExtension(stdGetModuleFileName()) + L".ini");
		free((void*)m_pszProfileName);
		m_pszProfileName = _wcsdup(inipathfull.c_str());
	}
	CWaitNextDlg dlg;
	
	dlg.m_strMainText = CCommandLineString::getCommandLine(mainArgs_, L"\r\n").c_str();
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}
	else if (nResponse == -1)
	{
		TRACE(traceAppMsg, 0, "Warning: dialog creation failed, so application is terminating unexpectedly.\n");
		TRACE(traceAppMsg, 0, "Warning: if you are using MFC controls on the dialog, you cannot #define _AFX_NO_MFC_CONTROLS_IN_DIALOGS.\n");
	}

	// Delete the shell manager created above.
	if (pShellManager != NULL)
	{
		delete pShellManager;
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}



