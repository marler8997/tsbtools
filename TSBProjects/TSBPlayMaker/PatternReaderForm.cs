using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternReader
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class PatternReaderForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox m_FileNameTextBox;
		private System.Windows.Forms.RichTextBox m_RichTextBox;
		private System.Windows.Forms.Button m_BrowseButton;
		private System.Windows.Forms.Button m_GoButton;

		//private int m_CurrentLineNo = 0;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private  StringCollection m_Errors = null;

		private Regex m_LineRegex, m_ConditionalJumpRegex1, m_ConditionalJumpRegex2,
			m_ManToManRegex, m_PassRushRegex;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton m_OffenseRaidoButton;
		private System.Windows.Forms.RadioButton m_DefenseRadioButton;
		private System.Windows.Forms.CheckBox m_ShowPointersheckBox;
		private System.Windows.Forms.CheckBox m_ShowPatternsheckBox;

		public PatternReaderForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//                        pos          address      rest
			m_LineRegex = new Regex("([A-Z12]{2,4}) ([0-9A-F]{4}):(.+)");

			m_ConditionalJumpRegex1 = new Regex("2([0-9A-F])-JumpTo ([0-9A-F]{4})");
			
			m_ConditionalJumpRegex2 = new Regex("C8-JumpTo ([0-9A-F],{4})-([0-9A-F]{2}):");

			m_ManToManRegex = new Regex("-m2m-([A-Z1-4]+)");

			m_PassRushRegex = new Regex("PassRush");

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private void DoIt()
		{
			m_Errors = new StringCollection();

			string contents = GetFileContents( m_FileNameTextBox.Text );
			char[] seps = {'\n'};
			contents = contents.Replace("\r","");
			string[] lines = contents.Split(seps);
			StringBuilder sb = new StringBuilder(4*1024);
			StringBuilder currentLine = new StringBuilder(200);
			string line = "";
			string checkFor = null;
			
			if( m_DefenseRadioButton.Checked )
			{
				checkFor = "Defense";
				sb.Append("Defense #, RE, NT, LE, ROLB, RILB, LILB, LOLB, RCB, LCB, FS, SS,\n");
			}
			else
			{
				checkFor = "Offense";
				sb.Append("Offense #, QB, HB, FB, WR1, WR2, TE, C, LG, RG, LT, RT,\n");
			}

			for( int i = 0; i < lines.Length; i++)
			{
				line = lines[i].Trim();
				if( line == "" )
				{/*do nothing*/}
				else if( line.StartsWith(checkFor) )
				{
					sb.Append(currentLine.ToString() );
					sb.Append("\n");
					currentLine = new StringBuilder(200);
					currentLine.Append(line);
					currentLine.Append(",");
				}
				else
				{
					currentLine.Append( GetAssignment(line) );
					currentLine.Append(",");
				}
			}
			m_RichTextBox.Text = sb.ToString().Replace("//,",",");
			ShowErrors();
			m_RichTextBox.Focus();
		}
//		m_LineRegex = new Regex("([A-Z]{2,4}) ([0-9A-F]{4}):(.+)");
//		m_ConditionalJumpRegex1 = new Regex("2([0-9A-F])-JumpTo ([0-9A-F]{4})");
//		m_ConditionalJumpRegex2 = new Regex("C8-JumpTo ([0-9A-F],{4})-([0-9A-F]{2}):");
//		m_ManToManRegex = new Regex("-m2m-([A-Z1-4]+)");
//		m_PassRushRegex = new Regex("PassRush");

		private string GetAssignment(string line )
		{
			string ret = "";
			Match lineMatch;
			Match condJump1, condJump2, m2m, pr;

			lineMatch =  m_LineRegex.Match(line);
			condJump1 =  m_ConditionalJumpRegex1.Match(line);
			condJump2 =  m_ConditionalJumpRegex2.Match(line);
			m2m       =  m_ManToManRegex.Match(line);
			pr        =  m_PassRushRegex.Match(line);

			if( lineMatch == Match.Empty )
			{
				m_Errors.Add("Line '"+ line+"' could not be matched!");
				return ret;
			}

			// we can make the defensive pointer and pattern files by adjusting these lines.
			if( m_ShowPointersheckBox.Checked )
			{
				ret += lineMatch.Groups[2].ToString();
			}
			if(m_ShowPointersheckBox.Checked && m_ShowPatternsheckBox.Checked )
			{
				ret +=":";
			}
			if( m_ShowPatternsheckBox.Checked )
			{
				ret += lineMatch.Groups[3].ToString();
			}
			/*
			if( m2m != Match.Empty )
			{
				ret += string.Format(" m2m-{0}",m2m.Groups[1].ToString());
			}
			if( pr.Success )
			{
				ret += " PassRush";
			}*/
			
			return ret;
		}

		private string GetFileContents(string fileName)
		{
			string results = null;
			try
			{
				StreamReader sr = new StreamReader(fileName);
				results = sr.ReadToEnd();
				sr.Close();
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}
			return results;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_FileNameTextBox = new System.Windows.Forms.TextBox();
			this.m_RichTextBox = new System.Windows.Forms.RichTextBox();
			this.m_BrowseButton = new System.Windows.Forms.Button();
			this.m_GoButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_OffenseRaidoButton = new System.Windows.Forms.RadioButton();
			this.m_DefenseRadioButton = new System.Windows.Forms.RadioButton();
			this.m_ShowPointersheckBox = new System.Windows.Forms.CheckBox();
			this.m_ShowPatternsheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_FileNameTextBox
			// 
			this.m_FileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_FileNameTextBox.Location = new System.Drawing.Point(8, 16);
			this.m_FileNameTextBox.Name = "m_FileNameTextBox";
			this.m_FileNameTextBox.Size = new System.Drawing.Size(464, 20);
			this.m_FileNameTextBox.TabIndex = 10;
			this.m_FileNameTextBox.Text = "";
			// 
			// m_RichTextBox
			// 
			this.m_RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_RichTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_RichTextBox.Location = new System.Drawing.Point(8, 104);
			this.m_RichTextBox.Name = "m_RichTextBox";
			this.m_RichTextBox.Size = new System.Drawing.Size(640, 288);
			this.m_RichTextBox.TabIndex = 20;
			this.m_RichTextBox.Text = "";
			// 
			// m_BrowseButton
			// 
			this.m_BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_BrowseButton.Location = new System.Drawing.Point(488, 16);
			this.m_BrowseButton.Name = "m_BrowseButton";
			this.m_BrowseButton.TabIndex = 2;
			this.m_BrowseButton.Text = "&Browse";
			this.m_BrowseButton.Click += new System.EventHandler(this.m_BrowseButton_Click);
			// 
			// m_GoButton
			// 
			this.m_GoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_GoButton.Location = new System.Drawing.Point(568, 16);
			this.m_GoButton.Name = "m_GoButton";
			this.m_GoButton.TabIndex = 3;
			this.m_GoButton.Text = "&Go";
			this.m_GoButton.Click += new System.EventHandler(this.m_GoButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.m_DefenseRadioButton);
			this.groupBox1.Controls.Add(this.m_OffenseRaidoButton);
			this.groupBox1.Location = new System.Drawing.Point(8, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(232, 48);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Offense/Defense";
			// 
			// m_OffenseRaidoButton
			// 
			this.m_OffenseRaidoButton.Location = new System.Drawing.Point(8, 16);
			this.m_OffenseRaidoButton.Name = "m_OffenseRaidoButton";
			this.m_OffenseRaidoButton.TabIndex = 0;
			this.m_OffenseRaidoButton.Text = "&Offense";
			// 
			// m_DefenseRadioButton
			// 
			this.m_DefenseRadioButton.Checked = true;
			this.m_DefenseRadioButton.Location = new System.Drawing.Point(128, 16);
			this.m_DefenseRadioButton.Name = "m_DefenseRadioButton";
			this.m_DefenseRadioButton.Size = new System.Drawing.Size(90, 24);
			this.m_DefenseRadioButton.TabIndex = 1;
			this.m_DefenseRadioButton.TabStop = true;
			this.m_DefenseRadioButton.Text = "&Defense";
			// 
			// m_ShowPointersheckBox
			// 
			this.m_ShowPointersheckBox.Checked = true;
			this.m_ShowPointersheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_ShowPointersheckBox.Location = new System.Drawing.Point(248, 42);
			this.m_ShowPointersheckBox.Name = "m_ShowPointersheckBox";
			this.m_ShowPointersheckBox.TabIndex = 10;
			this.m_ShowPointersheckBox.Text = "Show Pointers";
			// 
			// m_ShowPatternsheckBox
			// 
			this.m_ShowPatternsheckBox.Checked = true;
			this.m_ShowPatternsheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_ShowPatternsheckBox.Location = new System.Drawing.Point(248, 66);
			this.m_ShowPatternsheckBox.Name = "m_ShowPatternsheckBox";
			this.m_ShowPatternsheckBox.TabIndex = 15;
			this.m_ShowPatternsheckBox.Text = "Show Patterns";
			// 
			// PatternReaderForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(656, 398);
			this.Controls.Add(this.m_ShowPatternsheckBox);
			this.Controls.Add(this.m_ShowPointersheckBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.m_GoButton);
			this.Controls.Add(this.m_BrowseButton);
			this.Controls.Add(this.m_RichTextBox);
			this.Controls.Add(this.m_FileNameTextBox);
			this.Name = "PatternReaderForm";
			this.Text = "Pattern Reader";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

//		/// <summary>
//		/// The main entry point for the application.
//		/// </summary>
//		[STAThread]
//		static void Main() 
//		{
//			Application.Run(new PatternReaderForm());
//		}

		private void ShowErrors()
		{
			if( m_Errors.Count > 0 )
			{
				StringBuilder sb = new StringBuilder();

				sb.Append("ERRORS!\n");
				foreach(string error in m_Errors)
				{
					sb.Append(error);
					sb.Append("\n");
				}
				m_Errors = new StringCollection();

				String errorString = sb.ToString();
				int start = m_RichTextBox.Text.Length;
				m_RichTextBox.Text += errorString;
				int end   = m_RichTextBox.Text.Length;

				// color the text
				m_RichTextBox.SelectionStart  = start;
				m_RichTextBox.SelectionLength = (end - start);
				m_RichTextBox.SelectionColor  = Color.Red;
				m_RichTextBox.SelectionStart  = 0;
				m_RichTextBox.SelectionLength = 0;
			}
		}

		private void m_BrowseButton_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			if( dlg.ShowDialog(this) == DialogResult.OK )
			{
				m_FileNameTextBox.Text = dlg.FileName;
			}
		}

		private void m_GoButton_Click(object sender, System.EventArgs e)
		{
			DoIt();
		}
	}
}
