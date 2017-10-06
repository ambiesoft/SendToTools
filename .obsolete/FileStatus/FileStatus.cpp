#include "stdafx.h"
#include "FormMain.h"
#include "FileStatus.h"

using namespace System;
using namespace System::Windows::Forms;
using namespace Microsoft::Win32;

using namespace FileStatus;

[STAThreadAttribute]
int main(array<System::String ^> ^args)
{

	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false);

	if (args->Length == 0)
	{
		MessageBox::Show(I18NS(L"No Arguments"),
			Application::ProductName,
			MessageBoxButtons::OK,
			MessageBoxIcon::Exclamation);
		return 1;
	}
	else if (args->Length != 1)
	{
		MessageBox::Show(I18NS(L"Too Many Arguments"),
			Application::ProductName,
			MessageBoxButtons::OK,
			MessageBoxIcon::Exclamation);
		return 1;
	}

	FormMain form;
	form.InitParam(args[0]);
	// Application::Run(gcnew FormMain());
	form.ShowDialog();
	return 0;
}