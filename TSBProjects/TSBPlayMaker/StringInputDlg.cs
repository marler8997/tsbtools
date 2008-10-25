using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlayProto
{
	/// <summary>
	/// Summary description for StringInputDlg.
	/// </summary>
	public class StringInputDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox userInput;
		private string result = "";
		private System.Windows.Forms.Button cancelButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StringInputDlg(string title, string message, string initialText)
		{
			InitializeComponent();
			this.Text= title;
			this.label1.Text = message;
			this.userInput.Text= initialText;
			this.userInput.SelectAll();
		}

		public static string GetString(string title, string message)
		{
			StringInputDlg sid = new StringInputDlg(title, message,"");
			sid.ShowDialog();
			string ret = sid.getResult();
			sid.Dispose();
			return ret;
		}

		public static string GetString(string title, string message, string initialText)
		{
			StringInputDlg sid = new StringInputDlg(title, message, initialText);
			sid.ShowDialog();
			string ret = sid.getResult();
			sid.Dispose();
			return ret;
		}

		public string getResult()
		{
			return result;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.userInput = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(65, 64);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(60, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(141, 64);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(60, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// userInput
			// 
			this.userInput.Location = new System.Drawing.Point(56, 40);
			this.userInput.Name = "userInput";
			this.userInput.Size = new System.Drawing.Size(200, 20);
			this.userInput.TabIndex = 0;
			this.userInput.Text = "";
			this.userInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userInput_KeyDown);
			this.userInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(56, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(208, 32);
			this.label1.TabIndex = 3;
			// 
			// StringInputDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(282, 104);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.userInput);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StringInputDlg";
			this.Text = "StringInputDlg";
			this.ResumeLayout(false);

		}
		#endregion

		private void textBox1_TextChanged(object sender, System.EventArgs e) 
		{
        
		}

		private void userInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) 
		{
			if(e.KeyCode == Keys.Enter)
			{
				//result = userInput.Text;
				okButton_Click(sender, new System.EventArgs());
			}
		}

		private void okButton_Click(object sender, System.EventArgs e) 
		{
			result = userInput.Text;
			this.Close();
		}

		private void cancelButton_Click(object sender, System.EventArgs e) 
		{
			result = "";
			this.Close();
		}
	}
}
