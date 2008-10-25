using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for TeamFileSelector.
	/// </summary>
	public class TeamFileSelector : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Button mChooseFileButton;
		private System.Windows.Forms.TextBox mTeamTextBox;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TeamFileSelector()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}

		/// <summary>
		/// Get and set the team's file.
		/// </summary>
		public string Team
		{
			get{ return this.mTeamTextBox.Text; }
			set{ this.mTeamTextBox.Text = value;}
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mChooseFileButton = new System.Windows.Forms.Button();
			this.mTeamTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// mChooseFileButton
			// 
			this.mChooseFileButton.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(140)), ((System.Byte)(248)));
			this.mChooseFileButton.ForeColor = System.Drawing.Color.White;
			this.mChooseFileButton.Location = new System.Drawing.Point(120, 8);
			this.mChooseFileButton.Name = "mChooseFileButton";
			this.mChooseFileButton.Size = new System.Drawing.Size(32, 23);
			this.mChooseFileButton.TabIndex = 3;
			this.mChooseFileButton.Text = "...";
			this.mChooseFileButton.Click += new System.EventHandler(this.mChooseFileButton_Click);
			// 
			// mTeamTextBox
			// 
			this.mTeamTextBox.BackColor = System.Drawing.Color.White;
			this.mTeamTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mTeamTextBox.ForeColor = System.Drawing.Color.Black;
			this.mTeamTextBox.Location = new System.Drawing.Point(8, 8);
			this.mTeamTextBox.Name = "mTeamTextBox";
			this.mTeamTextBox.TabIndex = 2;
			this.mTeamTextBox.Text = "";
			// 
			// TeamFileSelector
			// 
			this.Controls.Add(this.mChooseFileButton);
			this.Controls.Add(this.mTeamTextBox);
			this.Name = "TeamFileSelector";
			this.Size = new System.Drawing.Size(160, 32);
			this.ResumeLayout(false);

		}
		#endregion

		private void mChooseFileButton_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.CheckFileExists = false;
			dlg.RestoreDirectory = true;
			dlg.Filter = "Text files (*.txt)|*.txt";
			
			DialogResult result = dlg.ShowDialog();
			if( result == DialogResult.OK )
			{
				Team = dlg.FileName;
			}
			dlg.Dispose();
		}

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged (e);
			this.mChooseFileButton.Enabled = this.Enabled;
			this.mTeamTextBox.Enabled      = this.Enabled;
			if( mTeamTextBox.Enabled )
				mTeamTextBox.BackColor = Color.White;
			else
				mTeamTextBox.BackColor = Color.Gainsboro;
		}

	}
}
