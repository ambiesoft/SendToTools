
// WaitNextDlg.h : header file
//

#pragma once


// CWaitNextDlg dialog
class CWaitNextDlg : public CDialogEx
{
// Construction
public:
	CWaitNextDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_WAITNEXT_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	CString m_strMainText;
	CString m_strOption;
	virtual LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);
};
