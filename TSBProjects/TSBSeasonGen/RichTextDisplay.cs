using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for RichTextDisplay.
	/// </summary>
	public class RichTextDisplay : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBox;
		private System.Windows.Forms.Panel mBottomPanel;
		private System.Windows.Forms.Button mOkButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RichTextBox ContentBox
		{
			get { return this.richTextBox; }
		}

		public static DialogResult ShowMessage(string title, string message)
		{
			DialogResult result = DialogResult.Cancel;
			RichTextDisplay dlg = new RichTextDisplay();
			dlg.Text = title;
			dlg.ContentBox.Text = message;
			result = dlg.ShowDialog();

			return result;
		}

		public RichTextDisplay()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public void ShowFile(string fileName )
		{
			try
			{
				this.richTextBox.LoadFile(fileName);
			}
			catch
			{
				MessageBox.Show("Could not find file "+ fileName);
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RichTextDisplay));
			this.richTextBox = new System.Windows.Forms.RichTextBox();
			this.mBottomPanel = new System.Windows.Forms.Panel();
			this.mOkButton = new System.Windows.Forms.Button();
			this.mBottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// richTextBox
			// 
			this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox.Location = new System.Drawing.Point(0, 0);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.ReadOnly = true;
			this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.richTextBox.Size = new System.Drawing.Size(680, 486);
			this.richTextBox.TabIndex = 0;
			this.richTextBox.Text = "";
			// 
			// mBottomPanel
			// 
			this.mBottomPanel.Controls.Add(this.mOkButton);
			this.mBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.mBottomPanel.Location = new System.Drawing.Point(0, 446);
			this.mBottomPanel.Name = "mBottomPanel";
			this.mBottomPanel.Size = new System.Drawing.Size(680, 40);
			this.mBottomPanel.TabIndex = 1;
			// 
			// mOkButton
			// 
			this.mOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.mOkButton.Location = new System.Drawing.Point(576, 8);
			this.mOkButton.Name = "mOkButton";
			this.mOkButton.TabIndex = 2;
			this.mOkButton.Text = "OK";
			// 
			// RichTextDisplay
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 14);
			this.ClientSize = new System.Drawing.Size(680, 486);
			this.Controls.Add(this.mBottomPanel);
			this.Controls.Add(this.richTextBox);
			this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RichTextDisplay";
			this.mBottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
