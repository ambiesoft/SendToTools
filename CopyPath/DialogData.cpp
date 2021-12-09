#include "stdafx.h"


#include "CopyPath.h"
#include "DialogData.h"

using namespace Ambiesoft;
using namespace std;

DialogData* DialogData::pTheInst_ = nullptr;


bool DialogData::Serialize(Profile::CHashIni& ini, bool save)
{
	bool ok = true;
	if(save)
		ok &= Profile::WriteBool(SECTION_DIALOGDATA, KEY_ISNAMEONLY, nameonly_, ini);
	else
		Profile::GetBool(SECTION_DIALOGDATA, KEY_ISNAMEONLY, false, nameonly_, ini);
	
	if(save)
		ok &= Profile::WriteInt(SECTION_DIALOGDATA, KEY_PATHSEPTYPE, pathsep_, ini);
	else
		Profile::GetInt(SECTION_DIALOGDATA, KEY_PATHSEPTYPE, 0, (int&)pathsep_, ini);

	if(save)
		ok &= Profile::WriteInt(SECTION_DIALOGDATA, KEY_DQTYPE, dqt_, ini);
	else
		Profile::GetInt(SECTION_DIALOGDATA, KEY_DQTYPE, 0, (int&)dqt_, ini);

	if(save)
		ok &= Profile::WriteBool(SECTION_DIALOGDATA, KEY_ISKAIGYO, multiline_, ini);
	else
		Profile::GetBool(SECTION_DIALOGDATA, KEY_ISKAIGYO, false, multiline_, ini);

	if(save)
		ok &= Profile::WriteBool(SECTION_DIALOGDATA, KEY_ISCODE, code_, ini);
	else
		Profile::GetBool(SECTION_DIALOGDATA, KEY_ISCODE, false, code_, ini);

	if(save)
		ok &= Profile::WriteBool(SECTION_DIALOGDATA, KEY_ISSORT, sort_, ini);
	else
		Profile::GetBool(SECTION_DIALOGDATA, KEY_ISSORT, false, sort_, ini);

	if(save)
		ok &= Profile::WriteString(SECTION_DIALOGDATA, KEY_CODENAME, toStdUtf8String(codeName_), ini);
	else
		Profile::GetString(SECTION_DIALOGDATA, KEY_CODENAME, "", fromUtf8ReturnedFuncToWstring(codeName_), ini);

	return ok;
}
bool DialogData::Load(const std::wstring& iniPath)
{
	Profile::CHashIni ini;
	try
	{
		ini.reset(Profile::ReadAll(iniPath, true));
		return Serialize(ini, false);
	}
	catch(file_not_found_error&)
	{ }
	catch (std::exception& ex)
	{
		MessageBox(nullptr,  
			toStdWstringFromACP(ex.what()).c_str(),
			gszTitle,
			MB_ICONERROR);
		return false;
	}
	return true;
}
bool DialogData::Save(const std::wstring& iniPath)
{
	try
	{
		Profile::CHashIni ini;
		try
		{
			ini.reset(Profile::ReadAll(iniPath, true));
		}
		catch(file_not_found_error&)
		{ 
			ini.reset(HashIni::CreateEmptyInstanceForSpecialUse());
		}

		return Serialize(ini, true) &&
			Profile::WriteAll(ini, iniPath, true);
	}
	catch (std::exception& ex)
	{
		MessageBox(nullptr,
			toStdWstringFromACP(ex.what()).c_str(),
			gszTitle,
			MB_ICONERROR);
		return false;
	}
}
