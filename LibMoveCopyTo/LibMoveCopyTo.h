// LibMoveCopyTo.h : LibMoveCopyTo.DLL �̃��C�� �w�b�_�[ �t�@�C��
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH �ɑ΂��Ă��̃t�@�C�����C���N���[�h����O�� 'stdafx.h' ���C���N���[�h���Ă�������"
#endif

#include "resource.h"		// ���C�� �V���{��


// CLibMoveCopyToApp
// ���̃N���X�̎����Ɋւ��Ă� LibMoveCopyTo.cpp ���Q�Ƃ��Ă��������B
//

class CLibMoveCopyToApp : public CWinApp
{
public:
	CLibMoveCopyToApp();

// �I�[�o�[���C�h
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
