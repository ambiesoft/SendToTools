// LibMoveCopyTo.h : LibMoveCopyTo.DLL のメイン ヘッダー ファイル
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'stdafx.h' をインクルードしてください"
#endif

#include "resource.h"		// メイン シンボル



class CLibMoveCopyToApp : public CWinApp
{
public:
	CLibMoveCopyToApp();

// オーバーライド
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
