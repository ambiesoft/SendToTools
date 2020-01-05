#pragma once


// CWaitingDialog dialog

class CWaitingDialog : public CDialogEx
{
	DECLARE_DYNAMIC(CWaitingDialog)

public:
	CWaitingDialog(CWnd* pParent = NULL);   // standard constructor
	virtual ~CWaitingDialog();

// Dialog Data
	enum { IDD = IDD_DIALOG_WAITING };

public:
	HANDLE m_hWait = NULL;

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	virtual BOOL OnInitDialog();
};
