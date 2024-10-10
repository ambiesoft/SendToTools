#pragma once
#include "afxwin.h"


class CChooseDirDialog : public CDialogEx
{
	DECLARE_DYNAMIC(CChooseDirDialog)

	HICON m_hIcon;
public:
	CChooseDirDialog(
		CString strButtonText,
		HICON hIcon = nullptr, 
		CWnd* pParent = NULL);
	virtual ~CChooseDirDialog();

#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_CHOOSEDIR };
#endif

	CString m_strButtonText;

protected:
	virtual void DoDataExchange(CDataExchange* pDX);

	DECLARE_MESSAGE_MAP()

public:
	static BOOL IsValidPriority(int nPriority);
	static DWORD GetPriorityValue(int nPriority);
	CStringArray m_arDirs;
	virtual BOOL OnInitDialog();
	CListBox m_listDirs;
	CString m_strDirResult;
	afx_msg void OnSelchangeListDirs();
	afx_msg void OnClickedButtonBrowse();
	CString m_strSource;
	afx_msg void OnDestroy();
	CComboBox m_cmbPriority;
	afx_msg void OnEnChangeEditDir();
	CEdit m_editDirResult;
	CButton m_btnOK;
	int m_nCmbPriority;
	afx_msg void OnContextMenu(CWnd* /*pWnd*/, CPoint /*point*/);
	afx_msg void OnListSort();
	afx_msg void OnListRemove();
	BOOL m_bOpenAfterOperation;
	BOOL m_bOpenFolderAfterOperation;
};
