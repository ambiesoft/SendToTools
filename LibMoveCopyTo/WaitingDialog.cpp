// WaitingDialog.cpp : implementation file
//

#include "stdafx.h"
#include "LibMoveCopyTo.h"
#include "WaitingDialog.h"
#include "afxdialogex.h"


// CWaitingDialog dialog

IMPLEMENT_DYNAMIC(CWaitingDialog, CDialogEx)

CWaitingDialog::CWaitingDialog(CWnd* pParent /*=NULL*/)
	: CDialogEx(CWaitingDialog::IDD, pParent)
{

}

CWaitingDialog::~CWaitingDialog()
{
}

void CWaitingDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CWaitingDialog, CDialogEx)
	ON_WM_TIMER()
END_MESSAGE_MAP()


// CWaitingDialog message handlers


void CWaitingDialog::OnTimer(UINT_PTR nIDEvent)
{
	// TODO: Add your message handler code here and/or call default

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

	SetTimer(1, 3000, NULL);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


