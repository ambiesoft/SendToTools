#pragma once


// CChooseDirDialog �_�C�A���O

class CChooseDirDialog : public CDialogEx
{
	DECLARE_DYNAMIC(CChooseDirDialog)

public:
	CChooseDirDialog(CWnd* pParent = NULL);   // �W���R���X�g���N�^�[
	virtual ~CChooseDirDialog();

// �_�C�A���O �f�[�^
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DIALOG_CHOOSEDIR };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV �T�|�[�g

	DECLARE_MESSAGE_MAP()
};
