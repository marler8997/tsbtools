using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox yearTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button generateButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RichTextBox OutputTextBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			OutputTextBox.Text =
@"Please don't use this crappy GUI.

TSBTool has a menu item that will exec this program and place it's output
in TSBTool's Text area. Use TSBTool instead.

The only requirement is that they both programs be in the same directory.

";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.yearTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.generateButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.OutputTextBox = new System.Windows.Forms.RichTextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// yearTextBox
			// 
			this.yearTextBox.Location = new System.Drawing.Point(64, 8);
			this.yearTextBox.Name = "yearTextBox";
			this.yearTextBox.Size = new System.Drawing.Size(56, 20);
			this.yearTextBox.TabIndex = 0;
			this.yearTextBox.Text = "";
			this.yearTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.yearTextBox_KeyDown);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Year";
			// 
			// generateButton
			// 
			this.generateButton.Location = new System.Drawing.Point(152, 8);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new System.Drawing.Size(112, 23);
			this.generateButton.TabIndex = 2;
			this.generateButton.Text = "Generate";
			this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.generateButton);
			this.panel1.Controls.Add(this.yearTextBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 478);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(640, 48);
			this.panel1.TabIndex = 3;
			// 
			// OutputTextBox
			// 
			this.OutputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OutputTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OutputTextBox.Location = new System.Drawing.Point(0, 0);
			this.OutputTextBox.Name = "OutputTextBox";
			this.OutputTextBox.Size = new System.Drawing.Size(640, 478);
			this.OutputTextBox.TabIndex = 4;
			this.OutputTextBox.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 526);
			this.Controls.Add(this.OutputTextBox);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void Go()
		{
			int year= 1990;
			//try
			//{
				year = Int32.Parse(yearTextBox.Text);
				Season s = new Season(year);
				this.OutputTextBox.Text = s.GenerateSeason();
			//}
			//catch (Exception e )
			//{
			//	MessageBox.Show(e.Message);
			//}
		}
		private void generateButton_Click(object sender, System.EventArgs e)
		{
			Go();
			
		}

		private void yearTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.KeyCode == Keys.Enter )
			{
				Go();
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			string fileName = GetFileName("*.txt");
			if( fileName != null )
			{
				MainClass.WriteFile(fileName, OutputTextBox.Text );
			}
		}

		private string GetFileName(string filter)
		{
			string ret=null;
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.CheckFileExists = false;
			dlg.RestoreDirectory = true;
			//dlg.Filter="nes files (*.nes)|*.nes";
			if(filter != null && filter.Length > 0)
				dlg.Filter = filter;
			if(dlg.ShowDialog() == DialogResult.OK) 
			{
				ret = dlg.FileName;
			}
			return ret;
		}

		
	}
}
