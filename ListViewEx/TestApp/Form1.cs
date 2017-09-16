using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestApp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private ListViewEx.ListViewEx listViewEx1;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.CheckBox checkBoxDoubleClickActivation;
		private System.Windows.Forms.TextBox textBoxComment;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		private Control[] Editors;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			listViewEx1.SubItemClicked += new ListViewEx.SubItemEventHandler(listViewEx1_SubItemClicked);
			listViewEx1.SubItemEndEditing += new ListViewEx.SubItemEndEditingEventHandler(listViewEx1_SubItemEndEditing);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.textBoxComment = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.checkBoxDoubleClickActivation = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.listViewEx1 = new ListViewEx.ListViewEx();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePicker1.Location = new System.Drawing.Point(32, 56);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(80, 20);
			this.dateTimePicker1.TabIndex = 2;
			this.dateTimePicker1.Visible = false;
			// 
			// textBoxComment
			// 
			this.textBoxComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxComment.Location = new System.Drawing.Point(32, 104);
			this.textBoxComment.Multiline = true;
			this.textBoxComment.Name = "textBoxComment";
			this.textBoxComment.Size = new System.Drawing.Size(80, 16);
			this.textBoxComment.TabIndex = 3;
			this.textBoxComment.Text = "";
			this.textBoxComment.Visible = false;
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.IntegralHeight = false;
			this.comboBox1.ItemHeight = 13;
			this.comboBox1.Location = new System.Drawing.Point(32, 80);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(80, 21);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.Visible = false;
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxPassword.Location = new System.Drawing.Point(32, 128);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '*';
			this.textBoxPassword.Size = new System.Drawing.Size(80, 20);
			this.textBoxPassword.TabIndex = 4;
			this.textBoxPassword.Text = "";
			this.textBoxPassword.Visible = false;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numericUpDown1.Location = new System.Drawing.Point(32, 152);
			this.numericUpDown1.Maximum = new System.Decimal(new int[] {
																		   230,
																		   0,
																		   0,
																		   0});
			this.numericUpDown1.Minimum = new System.Decimal(new int[] {
																		   120,
																		   0,
																		   0,
																		   0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(80, 20);
			this.numericUpDown1.TabIndex = 5;
			this.numericUpDown1.Value = new System.Decimal(new int[] {
																		 120,
																		 0,
																		 0,
																		 0});
			this.numericUpDown1.Visible = false;
			// 
			// checkBoxDoubleClickActivation
			// 
			this.checkBoxDoubleClickActivation.Checked = true;
			this.checkBoxDoubleClickActivation.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxDoubleClickActivation.Location = new System.Drawing.Point(8, 8);
			this.checkBoxDoubleClickActivation.Name = "checkBoxDoubleClickActivation";
			this.checkBoxDoubleClickActivation.Size = new System.Drawing.Size(176, 16);
			this.checkBoxDoubleClickActivation.TabIndex = 6;
			this.checkBoxDoubleClickActivation.Text = "DoubleClickActivation";
			this.checkBoxDoubleClickActivation.CheckedChanged += new System.EventHandler(this.checkBoxDoubleClickActivation_CheckedChanged);
			// 
			// listViewEx1
			// 
			this.listViewEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewEx1.DoubleClickActivation = false;
			this.listViewEx1.Location = new System.Drawing.Point(8, 32);
			this.listViewEx1.Name = "listViewEx1";
			this.listViewEx1.Size = new System.Drawing.Size(344, 168);
			this.listViewEx1.TabIndex = 7;
			this.listViewEx1.View = System.Windows.Forms.View.Details;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 205);
			this.Controls.Add(this.checkBoxDoubleClickActivation);
			this.Controls.Add(this.textBoxPassword);
			this.Controls.Add(this.textBoxComment);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.listViewEx1);
			this.Name = "Form1";
			this.Text = "ListViewEx Demo";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// Fill combo
			comboBox1.Items.AddRange(new string[] {"Peter", "Paul", "Mary", "Jack", "Betty"});

			// Add Columns
			listViewEx1.Columns.Add("Birthday", 80, HorizontalAlignment.Left);
			listViewEx1.Columns.Add("Name", 50, HorizontalAlignment.Left);
			listViewEx1.Columns.Add("Note", 80, HorizontalAlignment.Left);
			listViewEx1.Columns.Add("Password", 60, HorizontalAlignment.Left);
			listViewEx1.Columns.Add("Height", 60, HorizontalAlignment.Left);

			ListViewItem lvi;

			// Create sample ListView data.
			lvi = new ListViewItem("01.02.1964");
			lvi.SubItems.Add("Peter");
			lvi.SubItems.Add("");
			lvi.SubItems.Add("****");	// This is what's displayed in the password column
			lvi.Tag = "pwd1";			// and that's the real password
			lvi.SubItems.Add("180");
			this.listViewEx1.Items.Add(lvi);

			lvi = new ListViewItem("12.04.1980");
			lvi.SubItems.Add("Jack");
			lvi.SubItems.Add("Hates sushi");
			lvi.SubItems.Add("****");
			lvi.Tag = "pwd2";
			lvi.SubItems.Add("185");
			this.listViewEx1.Items.Add(lvi);

			lvi = new ListViewItem("02.06.1976");
			lvi.SubItems.Add("Paul");
			lvi.SubItems.Add("");
			lvi.SubItems.Add("****");
			lvi.Tag = "pwd3";
			lvi.SubItems.Add("172");
			this.listViewEx1.Items.Add(lvi);

			lvi = new ListViewItem("09.01.2000");
			lvi.SubItems.Add("Betty");
			lvi.SubItems.Add("");
			lvi.SubItems.Add("****");
			lvi.Tag = "pwd4";
			lvi.SubItems.Add("165");
			this.listViewEx1.Items.Add(lvi);

			Editors = new Control[] {
									dateTimePicker1,	// for column 0
									comboBox1,			// for column 1
									textBoxComment,		// for column 2
									textBoxPassword,	// for column 3
									numericUpDown1		// for column 4
									};
			
			// Immediately accept the new value once the value of the control has changed
			// (for example, the dateTimePicker and the comboBox)
			dateTimePicker1.ValueChanged += new EventHandler(control_SelectedValueChanged);
			comboBox1.SelectedIndexChanged += new EventHandler(control_SelectedValueChanged);

			listViewEx1.DoubleClickActivation = true;
		}

		private void control_SelectedValueChanged(object sender, System.EventArgs e)
		{
			listViewEx1.EndEditing(true);
		}

		private void listViewEx1_SubItemClicked(object sender, ListViewEx.SubItemEventArgs e)
		{
			if (e.SubItem == 3) // Password field
			{
				// the current value (text) of the subitem is ****, so we have to provide
				// the control with the actual text (that's been saved in the item's Tag property)
				e.Item.SubItems[e.SubItem].Text = e.Item.Tag.ToString();
			}

			listViewEx1.StartEditing(Editors[e.SubItem], e.Item, e.SubItem);
		}

		private void listViewEx1_SubItemEndEditing(object sender, ListViewEx.SubItemEndEditingEventArgs e)
		{
			if (e.SubItem == 3) // Password field
			{
				if (e.Cancel)
				{
					e.DisplayText = new string(textBoxPassword.PasswordChar, e.Item.Tag.ToString().Length);
				}
				else
				{
					// in order to display a series of asterisks instead of the plain password text
					// (textBox.Text _gives_ plain text, after all), we have to modify what'll get
					// displayed and save the plain value somewhere else.
					string plain = e.DisplayText;
					e.DisplayText = new string(textBoxPassword.PasswordChar, plain.Length);
					e.Item.Tag = plain;
				}
			}
		}

		private void checkBoxDoubleClickActivation_CheckedChanged(object sender, System.EventArgs e)
		{
			listViewEx1.DoubleClickActivation = checkBoxDoubleClickActivation.Checked;
		}

		private void listViewEx1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// To show the real password (remember, the subitem's Text _is_ '*******'),
			// set the tooltip to the ListViewItem's tag (that's where the password is stored)
			ListViewItem item;
			int idx = listViewEx1.GetSubItemAt(e.X, e.Y, out item);
			if (item != null && idx == 3)
				toolTip1.SetToolTip(listViewEx1, item.Tag.ToString());
			else
				toolTip1.SetToolTip(listViewEx1, null);
		}
	}
}
