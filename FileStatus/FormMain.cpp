#include "stdafx.h"
#include "FormMain.h"


namespace FileStatus {
	using namespace System;
	using namespace System::IO;
	using namespace System::Windows::Forms;


	System::Void FormMain::FormMain_Load(System::Object^  sender, System::EventArgs^  e)
	{
		txtFile->Text = TargetFile;
	}
	System::Void FormMain::btnRead_Click(System::Object^  sender, System::EventArgs^  e)
	{
		try
		{
			System::Text::StringBuilder result;
			System::IO::FileInfo fi(TargetFile);

			result.Append(L"Length=");
			result.Append(fi.Length.ToString());
			result.AppendLine();

			result.Append(L"Creation Date=");
			result.AppendLine(fi.CreationTime.ToShortDateString());

			FileStream^ fs = fi.Open(FileMode::Open);
			
			array<System::Byte>^ buff = gcnew array<System::Byte>(100);
			int readed = fs->Read(buff, 0, buff->Length);
			String^ readstring;
			for (int i = 0; i < readed; ++i)
			{
				readstring += buff[i].ToString();
			}

			result.AppendLine(readstring);

			txtResult->Text = result.ToString();
			
		}
		catch (Exception^ ex)
		{
			MessageBox::Show(ex->Message);
		}
	}
}