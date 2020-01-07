using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ambiesoft.RegexFilenameRenamer
{
    public partial class FormPrepare : Form
    {
        SimpleCommandLineParserWritable parser;
        public FormPrepare(SimpleCommandLineParserWritable parser)
        {
            this.parser = parser;
            InitializeComponent();

            Serialize(false);
        }
        void Serialize(bool write)
        {
            if (write)
            {
                if (!string.IsNullOrEmpty(txtSearchRegex.Text))
                    parser["rf"] = txtSearchRegex.Text;
                else
                    parser["rf"] = null;

                parser["rt"] = txtReplace.Text;

                if (chkIncludeExtention.Checked)
                    parser["ie"] = true;
                else
                    parser["ie"] = null;

                if (chkIgnoreCase.Checked)
                    parser["ic"] = true;
                else
                    parser["ic"] = null;

                if (chkShowConfirm.Checked)
                {
                    parser["cf"] = true;
                    parser["ncf"] = null;
                }
                else
                {
                    parser["cf"] = null;
                    parser["ncf"] = true;
                }

                if (chkContainsGlobs.Checked)
                    parser["glob"] = true;
                else
                    parser["glob"] = null;

                Environment.CurrentDirectory = txtCurrentDirectory.Text;
                parser.SetMainArgs(txtInputs.Lines);
            }
            else
            {
                if (parser["rf"] != null)
                {
                    txtSearchRegex.Text = parser["rf"].ToString();
                }
                if (parser["rfu"] != null)
                {
                    txtSearchRegex.Text = System.Web.HttpUtility.UrlDecode(parser["rfu"].ToString());
                }
                if (parser["rt"] != null)
                {
                    txtReplace.Text = parser["rt"].ToString();
                }
                if (parser["rtu"] != null)
                {
                    txtReplace.Text = System.Web.HttpUtility.UrlDecode(parser["rtu"].ToString());
                }
                if (parser["ie"] != null)
                {
                    chkIncludeExtention.Checked = true;
                }
                if (parser["ic"] != null)
                {
                    chkIgnoreCase.Checked = true;
                }

                // default is true
                chkShowConfirm.Checked = true;
                if (parser["cf"] != null)
                {
                    chkShowConfirm.Checked = true;
                }
                if (parser["ncf"] != null)
                {
                    chkShowConfirm.Checked = false;
                }

                if (parser["glob"] != null)
                {
                    chkContainsGlobs.Checked = true;
                }
                txtCurrentDirectory.Text = Environment.CurrentDirectory;
                txtInputs.Lines = Program.ConstructMainArgs(parser);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Serialize(true);
            }
            catch(Exception ex)
            {
                CppUtils.Alert(ex);
                DialogResult = DialogResult.None;
            }
        }
    }
}
