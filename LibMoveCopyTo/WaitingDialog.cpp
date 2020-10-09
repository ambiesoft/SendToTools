// WaitingDialog.cpp : implementation file
//

#include "stdafx.h"

#include "../../lsMisc/stdosd/stdosd.h"

#include "LibMoveCopyTo.h"
#include "WaitingDialog.h"
#include "afxdialogex.h"

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
	CString strTitle;
	GetWindowText(strTitle);
	SetWindowText(stdFormat(L"%s - %s", (LPCWSTR)strTitle, (LPCWSTR)m_strAppName).c_str());
	SetTimer(1, 500, NULL);
	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


LRESULT CWaitingDialog::OnInitMinimize(WPARAM, LPARAM)
{
	ShowWindow(SW_MINIMIZE);
	// MessageBox(L"afwejo");
	// PostMessage(WM_SYSCOMMAND, SC_MINIMIZE);
	return 0;
}

void CWaitingDialog::OnBnClickedButtonStartnow()
{
	this->EndDialog(IDC_BUTTON_STARTNOW);
}
