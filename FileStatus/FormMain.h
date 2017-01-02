#pragma once

namespace FileStatus {
	using namespace System;

	public ref class FormMain : public System::Windows::Forms::Form
	{
	public:
		FormMain(void)
		{
			InitializeComponent();
			//
			//TODO: ここにコンストラクタ コードを追加します
			//
		}

	protected:
		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		~FormMain()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::TextBox^  txtFile;
	protected:
	private: System::Windows::Forms::Button^  btnRead;
	private: System::Windows::Forms::TextBox^  txtResult;
	private: System::Windows::Forms::Button^  btnOK;

	private:
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		void InitializeComponent(void)
		{
			this->txtFile = (gcnew System::Windows::Forms::TextBox());
			this->btnRead = (gcnew System::Windows::Forms::Button());
			this->txtResult = (gcnew System::Windows::Forms::TextBox());
			this->btnOK = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// txtFile
			// 
			this->txtFile->Location = System::Drawing::Point(12, 12);
			this->txtFile->Name = L"txtFile";
			this->txtFile->ReadOnly = true;
			this->txtFile->Size = System::Drawing::Size(733, 19);
			this->txtFile->TabIndex = 0;
			// 
			// btnRead
			// 
			this->btnRead->Location = System::Drawing::Point(12, 56);
			this->btnRead->Name = L"btnRead";
			this->btnRead->Size = System::Drawing::Size(75, 23);
			this->btnRead->TabIndex = 1;
			this->btnRead->Text = L"&Read Test";
			this->btnRead->UseVisualStyleBackColor = true;
			this->btnRead->Click += gcnew System::EventHandler(this, &FormMain::btnRead_Click);
			// 
			// txtResult
			// 
			this->txtResult->Location = System::Drawing::Point(12, 85);
			this->txtResult->Multiline = true;
			this->txtResult->Name = L"txtResult";
			this->txtResult->Size = System::Drawing::Size(733, 236);
			this->txtResult->TabIndex = 2;
			// 
			// btnOK
			// 
			this->btnOK->DialogResult = System::Windows::Forms::DialogResult::OK;
			this->btnOK->Location = System::Drawing::Point(670, 341);
			this->btnOK->Name = L"btnOK";
			this->btnOK->Size = System::Drawing::Size(75, 23);
			this->btnOK->TabIndex = 3;
			this->btnOK->Text = L"&OK";
			this->btnOK->UseVisualStyleBackColor = true;
			// 
			// FormMain
			// 
			this->AcceptButton = this->btnOK;
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 12);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(757, 376);
			this->Controls->Add(this->btnOK);
			this->Controls->Add(this->txtResult);
			this->Controls->Add(this->btnRead);
			this->Controls->Add(this->txtFile);
			this->Name = L"FormMain";
			this->Text = L"FormMain";
			this->Load += gcnew System::EventHandler(this, &FormMain::FormMain_Load);
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

	private:
		String^ file_;
	public:
		bool InitParam(String^ file) {
			file_ = file;
			return true;
		}

		property String^ TargetFile
		{
			String^ get() {
				return file_;
			}
		}


	private: System::Void btnRead_Click(System::Object^  sender, System::EventArgs^  e);
	private: System::Void FormMain_Load(System::Object^  sender, System::EventArgs^  e);






};

}