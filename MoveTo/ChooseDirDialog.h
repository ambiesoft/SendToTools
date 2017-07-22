#pragma once


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
};
