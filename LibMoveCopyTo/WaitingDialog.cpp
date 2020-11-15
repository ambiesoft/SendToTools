// WaitingDialog.cpp : implementation file
//

#include "stdafx.h"

#include "../../lsMisc/stdosd/stdosd.h"

#include "LibMoveCopyTo.h"
#include "WaitingDialog.h"
#include "afxdialogex.h"

using namespace Ambiesoft;
using namespace Ambiesoft::stdosd;

// CWaitingDialog dialog

IMPLEMENT_DYNAMIC(CWaitingDialog, CDialogEx)

CWaitingDialog::CWaitingDialog(CWnd* pParent /*=NULL*/)
	: CDialogEx(CWaitingDialog::IDD, pParent)
	, m_strFrom(_T(""))
	, m_strTo(_T(""))
{

}

CWaitingDialog::~CWaitingDialog()
{
}

void CWaitingDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_FROM, m_strFrom);
	DDX_Text(pDX, IDC_EDIT_TO, m_strTo);
}


BEGIN_MESSAGE_MAP(CWaitingDialog, CDialogEx)
	ON_WM_TIMER()
	ON_MESSAGE(WM_APP_INITMINIMIZE, OnInitMinimize)
	ON_BN_CLICKED(IDC_BUTTON_STARTNOW, &CWaitingDialog::OnBnClickedButtonStartnow)
	ON_WM_SYSCOMMAND()
END_MESSAGE_MAP()


// CWaitingDialog message handlers


void CWaitingDialog::OnTimer(UINT_PTR nIDEvent)
{
	if (!m_bMinimizeSent)
	{
		PostMessage(WM_APP_INITMINIMIZE);
		m_bMinimizeSent = true;
	}

	if (WaitForSingleObject(m_hWait, 0) != WAIT_TIMEOUT)
	{
		this->OnOK();
		return;
	}
	CDialogEx::OnTimer(nIDEvent);
}


BOOL CWaitingDialog::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		pSysMenu->AppendMenu(MF_SEPARATOR);
		pSysMenu->AppendMenu(MF_STRING, IDR_MENU_STARTNOW, I18N(L"&Start Now"));
	}

	i18nChangeChildWindowText(*this);

	CString strTitle;
	GetWindowText(strTitle);
	SetWindowText(stdFormat(L"%s - %s", (LPCWSTR)strTitle, (LPCWSTR)m_strAppName).c_str());
	SetTimer(1, 500, NULL);
	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}

void CWaitingDialog::OnSysCommand(UINT nID, LPARAM lParam)
{
	if (nID == IDR_MENU_STARTNOW)
	{
		this->EndDialog(IDC_BUTTON_STARTNOW);
		return;
	}

	ParentClass::OnSysCommand(nID, lParam);
}

LRESULT CWaitingDialog::OnInitMinimize(WPARAM, LPARAM)
{
	ShowWindow(SW_MINIMIZE);
	return 0;
}

void CWaitingDialog::OnBnClickedButtonStartnow()
{
	this->EndDialog(IDC_BUTTON_STARTNOW);
}


