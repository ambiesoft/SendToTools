using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChangeFileName
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

	private bool SafeProcessStart(string s, bool showerrorbox)
	{
		try
		{
			System.Diagnostics.Process.Start(s);
			return true;
		}
		catch(System.Exception e)
		{
			if ( showerrorbox )
			{
				MessageBox.Show(e.Message, 
					Application.ProductName,
					MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
			}
		}

		return false;
	}

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            SafeProcessStart(this.textName.Tag.ToString(), true);
        }

        private void btnTrim_Click(object sender, EventArgs e)
        {
            textName.Text = textName.Text.Trim();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                textName.Text = Clipboard.GetText();
            }
            catch (Exception) { }
        }
    }
}