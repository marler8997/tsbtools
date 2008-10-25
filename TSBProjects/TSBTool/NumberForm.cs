using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace TSBTool
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class NumberForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox stopOnTextBox;
		private System.Windows.Forms.Label label3;

		private Regex numRegex   = null;
		private StringReader sr  = null;
		private StringBuilder sb = null;
		private System.Windows.Forms.TextBox playerNumberTextBox;
		private string currentLine;

		private Regex teamRegex = null;
		private System.Windows.Forms.Label positionLabel;
		private string team = "";

		public NumberForm(string text)
		{
			// groups[1] = name,groups[2] = number
			numRegex = new Regex("([A-Z1-4]{1,3})\\s*,\\s*([a-z A-Z\\.]+)\\s*,\\s*Face\\s*=\\s*0x[0-9a-f]{2}\\s*,\\s*#([0-9]{1,2})");
			teamRegex = new Regex("TEAM\\s*=\\s*([49a-zA-Z]+)");
			InitializeComponent();
			sr = new StringReader(text);
			sb = new StringBuilder(text.Length);
			currentLine = null;
			//Next();
		}

		private void Next()
		{
			currentLine = sr.ReadLine();
			
			while( currentLine != null && GoAhead( ) )
			{
				sb.Append(currentLine);
				sb.Append("\n");
				currentLine = sr.ReadLine();
			}
			UpdateFields();
		}

		private bool GoAhead(  )
		{
			Match m = numRegex.Match(currentLine);

			Match t = teamRegex.Match(currentLine);

			if( t != Match.Empty )
			{
				team = t.Groups[1].ToString();
				label1.Text = team;
			}
			if( m == Match.Empty)
				return true;

			string stop = stopOnTextBox.Text;
			string test = m.Groups[3].ToString();
			
			if(  test == stop || stop == "" )
				return false;

			return true;
		}

		private void UpdateFields()
		{
			if( currentLine == null )
			{
				state2();
				MessageBox.Show("Done");
				okButton.Focus();
				return;
			}
			Match m = numRegex.Match(currentLine);
			positionLabel.Text       = m.Groups[1].ToString();
			nameLabel.Text           = m.Groups[2].ToString();
			playerNumberTextBox.Text = m.Groups[3].ToString();
			playerNumberTextBox.Focus();
			playerNumberTextBox.SelectAll();
		}

		private void state2()
		{
			okButton.Enabled      = true;
			cancelButton.Enabled  = true;
			nextButton.Enabled    = false;
			stopOnTextBox.Enabled = false;
			playerNumberTextBox.Enabled = false;
		}

		private void state1()
		{
			okButton.Enabled            = true;
			cancelButton.Enabled        = true;
			nextButton.Enabled          = true;
			stopOnTextBox.Enabled       = true;
			playerNumberTextBox.Enabled = true;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NumberForm));
			this.label1 = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.playerNumberTextBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.nextButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.stopOnTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.positionLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(160, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "Team";
			// 
			// nameLabel
			// 
			this.nameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.nameLabel.ForeColor = System.Drawing.Color.Green;
			this.nameLabel.Location = new System.Drawing.Point(128, 48);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(168, 23);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Player Number";
			// 
			// playerNumberTextBox
			// 
			this.playerNumberTextBox.Location = new System.Drawing.Point(16, 48);
			this.playerNumberTextBox.Name = "playerNumberTextBox";
			this.playerNumberTextBox.Size = new System.Drawing.Size(48, 22);
			this.playerNumberTextBox.TabIndex = 0;
			this.playerNumberTextBox.Text = "";
			this.playerNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.playerNumberTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numberTextBox_KeyDown);
			this.playerNumberTextBox.TextChanged += new System.EventHandler(this.stopOnTextBox_TextChanged);
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(216, 96);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 3;
			this.okButton.Text = "&OK";
			this.okButton.Click += new System.EventHandler(this.okCancelButton_Click);
			// 
			// nextButton
			// 
			this.nextButton.Location = new System.Drawing.Point(16, 96);
			this.nextButton.Name = "nextButton";
			this.nextButton.TabIndex = 2;
			this.nextButton.Text = "&Next";
			this.nextButton.Click += new System.EventHandler(this.doNext);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(304, 96);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.Click += new System.EventHandler(this.okCancelButton_Click);
			// 
			// stopOnTextBox
			// 
			this.stopOnTextBox.Location = new System.Drawing.Point(312, 48);
			this.stopOnTextBox.Name = "stopOnTextBox";
			this.stopOnTextBox.Size = new System.Drawing.Size(56, 22);
			this.stopOnTextBox.TabIndex = 1;
			this.stopOnTextBox.Text = "0";
			this.stopOnTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.stopOnTextBox.TextChanged += new System.EventHandler(this.stopOnTextBox_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(304, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 23);
			this.label3.TabIndex = 8;
			this.label3.Text = "Stop on";
			// 
			// positionLabel
			// 
			this.positionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.positionLabel.ForeColor = System.Drawing.Color.Purple;
			this.positionLabel.Location = new System.Drawing.Point(72, 48);
			this.positionLabel.Name = "positionLabel";
			this.positionLabel.Size = new System.Drawing.Size(40, 23);
			this.positionLabel.TabIndex = 9;
			this.positionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// NumberForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 15);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(392, 142);
			this.Controls.Add(this.positionLabel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.stopOnTextBox);
			this.Controls.Add(this.playerNumberTextBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(400, 176);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(400, 176);
			this.Name = "NumberForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}
		#endregion

		private void numberTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.KeyCode == Keys.Enter)
			{
				doNext(sender, e);
			}
		}

		Regex numberRegex ;

		private void stopOnTextBox_TextChanged(object sender, System.EventArgs e)
		{
			TextBox box = null;
			
			if( sender == this.playerNumberTextBox)
				box = playerNumberTextBox;
			else if( sender == this.stopOnTextBox )
				box = stopOnTextBox;

			if( box == null )
				return;

			if( numberRegex == null )
				numberRegex = new Regex("^([0-9]{1,2})$");
			
			if( numberRegex.Match(box.Text) == Match.Empty && box.Text != "")
			{
				box.Text = "0";
				box.SelectAll();
			}
		}

		private void doNext(object sender, System.EventArgs e)
		{
			if( currentLine != null )
			{
				Match m = numRegex.Match(currentLine);
				if( m != Match.Empty )
				{
					string tmp = "";
					tmp += currentLine.Substring(0, m.Groups[3].Index);
					tmp += playerNumberTextBox.Text;
					tmp += currentLine.Substring(m.Groups[3].Index + m.Groups[3].Length);
					sb.Append( tmp +"\n" );
				}
			}
			Next();
		}

		public string GetResult()
		{
			return sb.ToString();
		}
/*
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			string text = "";
			try
			{
				StreamReader sr = new StreamReader("Data.txt");
				text = sr.ReadToEnd();
				sr.Close();
			}
			catch{}
			Application.Run(new NumberForm(text));
		}*/

		private void okCancelButton_Click(object sender, System.EventArgs e)
		{
			if( sender == okButton)
			{
				while( currentLine != null )
				{
					sb.Append(currentLine);
					sb.Append("\n");
					currentLine = sr.ReadLine();
				}
			}
			this.Close();
		}
	}
}
