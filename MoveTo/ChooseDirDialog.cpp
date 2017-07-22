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
{

}

CChooseDirDialog::~CChooseDirDialog()
{
}

void CChooseDirDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CChooseDirDialog, CDialogEx)
END_MESSAGE_MAP()


// CChooseDirDialog メッセージ ハンドラー
