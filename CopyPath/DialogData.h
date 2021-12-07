#pragma once

#define SECTION_DIALOGDATA "DialogData"
#define KEY_ISNAMEONLY "IsNameOnly"
#define KEY_PATHSEPTYPE "PathSepType"
#define KEY_DQTYPE "DQType"
#define KEY_ISKAIGYO "IsKaigyo"
#define KEY_ISCODE "IsCode"
#define KEY_ISSORT "IsSort"
#define KEY_CODENAME "CodeName"

enum PathSeptType {
	PST_NORMAL,
	PST_DOUBLESBACKLASH,
	PST_SLASH,
};

enum DQType {
	DQ_DEFAULT,
	DQ_TRUE,
	DQ_FALSE,
};
class DialogData {
	PathSeptType pathsep_;
	bool nameonly_;
	DQType dqt_;
	bool multiline_;

	bool code_;
	bool sort_;
	std::wstring codeName_;

	DialogData() {
		pathsep_ = PST_NORMAL;
		nameonly_ = false;
		dqt_ = DQ_DEFAULT;
		multiline_ = 0;
		code_ = false;
		sort_ = false;
	}
	static DialogData* pTheInst_;
	bool Serialize(Ambiesoft::Profile::CHashIni& ini, bool save);
public:
	bool isNameonly() const {
		return nameonly_;
	}
	void setNameonly(bool b) {
		nameonly_ = b;
	}
	PathSeptType getPST() const {
		return pathsep_;
	}
	void setPST(PathSeptType ct) {
		pathsep_ = ct;
	}
	DQType getDQT() const {
		return dqt_;
	}
	void setDQT(DQType dqt) {
		dqt_ = dqt;
	}
	bool isMultiLine() const {
		return multiline_;
	}
	void setMultiLine(bool b) {
		multiline_ = b;
	}
	bool isCode() const {
		return code_;
	}
	void setCode(bool b) {
		code_ = b;
	}
	std::wstring getCodeName() const {
		return codeName_;
	}
	void setCodeName(const std::wstring& t) {
		codeName_ = t;
	}
	bool isSort() const {
		return sort_;
	}
	void setSort(bool b) {
		sort_ = b;
	}

	bool Load(const std::wstring& iniPath);
	bool Save(const std::wstring& iniPath);
	static DialogData& getInst() {
		if (!pTheInst_)
			pTheInst_ = new DialogData();
		return *pTheInst_;
	}
	
};

#define theData DialogData::getInst()