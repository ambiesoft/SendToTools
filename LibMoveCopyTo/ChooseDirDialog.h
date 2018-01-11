#pragma once
#include "afxwin.h"


// CChooseDirDialog ダイアログ

class CChooseDirDialog : public CDialogEx
{
	DECLARE_DYNAMIC(CChooseDirDialog)

public:
	CChooseDirDialog(CWnd* pParent = NULL);   // 標準コンストラクター
	virtual ~CChooseDirDialog();

// ダイアログ データ
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_CHOOSEDIR };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

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
};
