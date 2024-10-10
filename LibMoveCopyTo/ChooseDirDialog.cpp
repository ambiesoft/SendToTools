#include "stdafx.h"


#include "resource.h"
#include "ChooseDirDialog.h"

using namespace Ambiesoft;
using namespace std;


IMPLEMENT_DYNAMIC(CChooseDirDialog, CDialogEx)

CChooseDirDialog::CChooseDirDialog(
	CString strButtonText,
	HICON hIcon, 
	CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_DIALOG_CHOOSEDIR, pParent)
	, m_strButtonText(strButtonText)
	, m_hIcon(hIcon)
	, m_strDirResult(_T(""))
	, m_strSource(_T(""))
	, m_nCmbPriority(0)
	, m_bOpenAfterOperation(FALSE)
	, m_bOpenFolderAfterOperation(FALSE)
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
	DDX_Control(pDX, IDC_EDIT_DIR, m_editDirResult);
	DDX_Control(pDX, IDOK, m_btnOK);
	DDX_CBIndex(pDX, IDC_COMBO_PRIORITY, m_nCmbPriority);
	DDX_Check(pDX, IDC_CHECK_OPENAFTEROPERATION, m_bOpenAfterOperation);
	DDX_Check(pDX, IDC_CHECK_OPENFOLDERAFTEROPERATION, m_bOpenFolderAfterOperation);
}


BEGIN_MESSAGE_MAP(CChooseDirDialog, CDialogEx)
	ON_LBN_SELCHANGE(IDC_LIST_DIRS, &CChooseDirDialog::OnSelchangeListDirs)
	ON_BN_CLICKED(IDC_BUTTON_BROWSE, &CChooseDirDialog::OnClickedButtonBrowse)
	ON_WM_DESTROY()
	ON_EN_CHANGE(IDC_EDIT_DIR, &CChooseDirDialog::OnEnChangeEditDir)
	ON_WM_CONTEXTMENU()
	ON_COMMAND(ID_LIST_SORT, &CChooseDirDialog::OnListSort)
	ON_COMMAND(ID_LIST_REMOVE, &CChooseDirDialog::OnListRemove)
END_MESSAGE_MAP()


BOOL CChooseDirDialog::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	i18nChangeChildWindowText(*this);

	// TODO:  Add extra initialization here
	for (int i = 0; i < m_arDirs.GetSize(); ++i)
	{
		m_listDirs.AddString(m_arDirs[i]);
	}

	m_cmbPriority.AddString(I18N(L"Priority: High"));
	m_cmbPriority.AddString(I18N(L"Priority: Normal"));
	m_cmbPriority.AddString(I18N(L"Priority: Low"));
	m_cmbPriority.AddString(I18N(L"Priority: Background"));

	SetWindowText(gAppName);
	GetDlgItem(IDOK)->SetWindowText(m_strButtonText);

	UpdateData(FALSE);

	if (m_hIcon != nullptr) {
		SetIcon(m_hIcon, FALSE);
	}
	return TRUE;  // return TRUE unless you set the focus to a control
				  // EXCEPTION: OCX Property Pages should return FALSE
}


void CChooseDirDialog::OnSelchangeListDirs()
{
	// TODO: Add your control notification handler code here
	int cur = m_listDirs.GetCurSel();
	if (cur < 0)
		return;

	UpdateData(TRUE);
	m_listDirs.GetText(cur, m_strDirResult);
	UpdateData(FALSE);

	OnEnChangeEditDir();
}


void CChooseDirDialog::OnClickedButtonBrowse()
{
	WCHAR szFolder[MAX_PATH]; szFolder[0] = 0;

	if (!browseFolder(*this, I18N(L"Move to"), szFolder))
		return;

	PathAddBackslash(szFolder);
	int sel = m_listDirs.InsertString(0, szFolder);
	m_listDirs.SetCurSel(sel);
	m_strDirResult = szFolder;
	UpdateData(FALSE);

	OnEnChangeEditDir();
}


void CChooseDirDialog::OnDestroy()
{
	m_arDirs.RemoveAll();
	std::set<CString> dupcheck;
	
	//if (!m_strDirResult.IsEmpty())
	//{
	//	m_arDirs.Add(m_strDirResult);
	//	dupcheck.insert(m_strDirResult);
	//}

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


void CChooseDirDialog::OnEnChangeEditDir()
{
	UpdateData();
	m_btnOK.EnableWindow(PathIsDirectory(m_strDirResult));
}

BOOL CChooseDirDialog::IsValidPriority(int nPriority)
{
	return nPriority != -1;
}
DWORD CChooseDirDialog::GetPriorityValue(int nPriority)
{
	switch (nPriority)
	{
	case 0: return HIGH_PRIORITY_CLASS;
	case 1: return NORMAL_PRIORITY_CLASS;
	case 2: return IDLE_PRIORITY_CLASS;
	case 3: return PROCESS_MODE_BACKGROUND_BEGIN;
	}
	return -1;
}


void CChooseDirDialog::OnContextMenu(CWnd* pWnd, CPoint point)
{
	CMenu menu;
	VERIFY(menu.LoadMenu(IDR_MENU_LIST_CONTEXT));
	i18nChangeMenuText(menu);
	CMenu* pPopup = menu.GetSubMenu(0);
	ASSERT_VALID(pPopup);
	pPopup->TrackPopupMenu(TPM_LEFTALIGN, point.x, point.y, this);
}


void CChooseDirDialog::OnListSort()
{
	vector<wstring> all;
	int count = m_listDirs.GetCount();
	if (count <= 0)
		return;

	for (int i = 0; i < count; ++i)
	{
		CString r;
		m_listDirs.GetText(i, r);
		all.push_back((LPCTSTR)r);
	}
	
	std::sort(all.begin(), all.end());
	m_listDirs.ResetContent();

	for (size_t i = 0; i < all.size(); ++i)
	{
		m_listDirs.AddString(all[i].c_str());
	}
}


void CChooseDirDialog::OnListRemove()
{
	int sel = m_listDirs.GetCurSel();
	if (sel < 0)
		return;

	CString data;
	m_listDirs.GetText(sel, data);

	CString message;
	message.Format(I18N(L"Are you sure you want to remove entry '%s'?"), data);
	if (IDYES != MessageBox(message, NULL, MB_ICONQUESTION | MB_YESNO | MB_DEFBUTTON2))
		return;
	
	m_listDirs.DeleteString(sel);
}
