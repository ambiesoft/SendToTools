// ChooseDirDialog.cpp : 実装ファイル
//

#include "stdafx.h"
#include <afxdialogex.h>

#include "resource.h"
#include "ChooseDirDialog.h"



// CChooseDirDialog ダイアログ

IMPLEMENT_DYNAMIC(CChooseDirDialog, CDialogEx)

CChooseDirDialog::CChooseDirDialog(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_DIALOG_CHOOSEDIR, pParent)
	, m_strDirResult(_T(""))
	, m_strSource(_T(""))
{

}

CChooseDirDialog::~CChooseDirDialog()
{
}

void CChooseDirDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_DIRS, m_listDirs);
	DDX_Text(pDX, IDC_EDIT_DIR, m_strDirResult);
	DDX_Text(pDX, IDC_EDIT_SOURCE, m_strSource);
	DDX_Control(pDX, IDC_COMBO_PRIORITY, m_cmbPriority);
}


BEGIN_MESSAGE_MAP(CChooseDirDialog, CDialogEx)
	ON_LBN_SELCHANGE(IDC_LIST_DIRS, &CChooseDirDialog::OnSelchangeListDirs)
	ON_BN_CLICKED(IDC_BUTTON_BROWSE, &CChooseDirDialog::OnClickedButtonBrowse)
	ON_WM_DESTROY()
END_MESSAGE_MAP()


// CChooseDirDialog メッセージ ハンドラー


BOOL CChooseDirDialog::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// TODO:  Add extra initialization here
	for (int i = 0; i < m_arDirs.GetSize(); ++i)
	{
		m_listDirs.AddString(m_arDirs[i]);
	}

	m_cmbPriority.AddString(L"Priority: High");
	return TRUE;  // return TRUE unless you set the focus to a control
				  // EXCEPTION: OCX Property Pages should return FALSE
}


void CChooseDirDialog::OnSelchangeListDirs()
{
	// TODO: Add your control notification handler code here
	int cur = m_listDirs.GetCurSel();
	if (cur < 0)
		return;

	m_listDirs.GetText(cur, m_strDirResult);
	UpdateData(FALSE);
}


void CChooseDirDialog::OnClickedButtonBrowse()
{
	WCHAR szFolder[MAX_PATH]; szFolder[0] = 0;

	if (!browseFolder(*this, I18N(L"Move to"), szFolder))
		return;

	int sel = m_listDirs.InsertString(0, szFolder);
	m_listDirs.SetCurSel(sel);
	m_strDirResult = szFolder;
	UpdateData(FALSE);
}


void CChooseDirDialog::OnDestroy()
{
	m_arDirs.RemoveAll();
	std::set<CString> dupcheck;
	if (!m_strDirResult.IsEmpty())
	{
		m_arDirs.Add(m_strDirResult);
		dupcheck.insert(m_strDirResult);
	}

	for (int i = 0; i < m_listDirs.GetCount(); ++i)
	{
		CString strT;
		m_listDirs.GetText(i, strT);
		if (!strT.IsEmpty() && dupcheck.find(strT) == dupcheck.end())
		{
			m_arDirs.Add(strT);
			dupcheck.insert(strT);
		}
	}

	CDialogEx::OnDestroy();


}
