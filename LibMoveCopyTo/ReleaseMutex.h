#pragma once
#include <afxmt.h>

class CReleaseMutex :
	public CMutex
{
public:
	explicit CReleaseMutex(LPCWSTR pName, BOOL initialOwner) :
		CMutex(initialOwner, pName) {}
	virtual ~CReleaseMutex();

	DWORD Wait() {
		return WaitForSingleObject(m_hObject, INFINITE);
	}
	bool WillWait() {
		return WaitForSingleObject(m_hObject, 0) == WAIT_TIMEOUT;
	}
};

