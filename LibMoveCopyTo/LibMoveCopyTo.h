
#pragma once

#ifndef __AFXWIN_H__
	#error "PCH"
#endif

#include "resource.h"



class CLibMoveCopyToApp : public CWinApp
{
public:
	CLibMoveCopyToApp();

public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
