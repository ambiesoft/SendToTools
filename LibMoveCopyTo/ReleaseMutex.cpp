#include "stdafx.h"
#include "ReleaseMutex.h"




CReleaseMutex::~CReleaseMutex()
{
	if (m_hObject)
	{
		ReleaseMutex(m_hObject);
	}
}
