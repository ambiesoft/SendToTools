#pragma once


// CWaitingDialog dialog

class CWaitingDialog : public CDialogEx
{
	DECLARE_DYNAMIC(CWaitingDialog)
private:
	bool m_bMinimizeSent = false;
public:
	CWaitingDialog(CWnd* pParent = NULL);   // standard constructor
	virtual ~CWaitingDialog();

// Dialog Data
	enum { IDD = IDD_DIALOG_WAITING };
	enum { WM_APP_INITMINIMIZE  = WM_APP + 1};

public:
	HANDLE m_hWait = NULL;

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg LRESULT OnInitMinimize(WPARAM, LPARAM);

	virtual BOOL OnInitDialog();
	CString m_strAppName;
	CString m_strFrom;
	CString m_strTo;
};
