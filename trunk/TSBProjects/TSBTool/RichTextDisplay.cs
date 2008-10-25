using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TSBTool
{
	/// <summary>
	/// Summary description for RichTextDisplay.
	/// </summary>
	public class RichTextDisplay : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBox;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mLoadMenuItem;
		private System.Windows.Forms.MenuItem mSaveMenuItem;
		private System.Windows.Forms.MenuItem mExitMenuItem;
		private System.Windows.Forms.MenuItem mSaveAsMenuItem;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem mContextLoadMenuItem;
		private System.Windows.Forms.MenuItem mContextSaveMenuItem;
		private System.Windows.Forms.MenuItem mContextSaveAsMenuItem;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mCopyMenuItem;
		private System.Windows.Forms.MenuItem mPasteMenuItem;
		private System.Windows.Forms.MenuItem mCopyMenuItemC;
		private System.Windows.Forms.MenuItem mPateMenuItemC;

		public RichTextBox ContentBox
		{
			get { return this.richTextBox; }
		}

		private RichTextBoxStreamType mRichTextStreamType = RichTextBoxStreamType.PlainText;

		public bool AllowRichText 
		{
			get
			{
				return mRichTextStreamType == RichTextBoxStreamType.RichText;
			}
			set
			{
				if( value)
				{
					mRichTextStreamType = RichTextBoxStreamType.RichText;
				}
				else
				{
					mRichTextStreamType = RichTextBoxStreamType.PlainText;
				}
			}
		}

		public RichTextDisplay()
		{
			InitializeComponent();
//			this.contextMenu1.MenuItems.AddRange(
//				new System.Windows.Forms.MenuItem[] {
//					this.mLoadMenuItem,
//					this.mSaveMenuItem,
//					this.mSaveAsMenuItem,
//					this.mExitMenuItem});
		}

		public void ShowFile(string fileName )
		{
			try
			{
				this.richTextBox.LoadFile(fileName,mRichTextStreamType);
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mLoadMenuItem = new System.Windows.Forms.MenuItem();
			this.mSaveMenuItem = new System.Windows.Forms.MenuItem();
			this.mSaveAsMenuItem = new System.Windows.Forms.MenuItem();
			this.mExitMenuItem = new System.Windows.Forms.MenuItem();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.mContextLoadMenuItem = new System.Windows.Forms.MenuItem();
			this.mContextSaveMenuItem = new System.Windows.Forms.MenuItem();
			this.mContextSaveAsMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mCopyMenuItem = new System.Windows.Forms.MenuItem();
			this.mPasteMenuItem = new System.Windows.Forms.MenuItem();
			this.mCopyMenuItemC = new System.Windows.Forms.MenuItem();
			this.mPateMenuItemC = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// richTextBox
			// 
			this.richTextBox.AcceptsTab = true;
			this.richTextBox.ContextMenu = this.contextMenu1;
			this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox.Location = new System.Drawing.Point(0, 0);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.richTextBox.Size = new System.Drawing.Size(692, 566);
			this.richTextBox.TabIndex = 0;
			this.richTextBox.Text = "";
			this.richTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyDown);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mLoadMenuItem,
																					  this.mSaveMenuItem,
																					  this.mSaveAsMenuItem,
																					  this.mExitMenuItem});
			this.menuItem1.Text = "&File";
			// 
			// mLoadMenuItem
			// 
			this.mLoadMenuItem.Index = 0;
			this.mLoadMenuItem.Text = "&Load";
			this.mLoadMenuItem.Click += new System.EventHandler(this.mLoadMenuItem_Click);
			// 
			// mSaveMenuItem
			// 
			this.mSaveMenuItem.Index = 1;
			this.mSaveMenuItem.Text = "&Save";
			this.mSaveMenuItem.Click += new System.EventHandler(this.mSaveMenuItem_Click);
			// 
			// mSaveAsMenuItem
			// 
			this.mSaveAsMenuItem.Index = 2;
			this.mSaveAsMenuItem.Text = "Save &as";
			this.mSaveAsMenuItem.Click += new System.EventHandler(this.mSaveAsMenuItem_Click);
			// 
			// mExitMenuItem
			// 
			this.mExitMenuItem.Index = 3;
			this.mExitMenuItem.Text = "E&xit";
			this.mExitMenuItem.Click += new System.EventHandler(this.mExitMenuItem_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mContextLoadMenuItem,
																						 this.mContextSaveMenuItem,
																						 this.mContextSaveAsMenuItem,
																						 this.mCopyMenuItemC,
																						 this.mPateMenuItemC});
			// 
			// mContextLoadMenuItem
			// 
			this.mContextLoadMenuItem.Index = 0;
			this.mContextLoadMenuItem.Text = "&Load";
			this.mContextLoadMenuItem.Click += new System.EventHandler(this.mLoadMenuItem_Click);
			// 
			// mContextSaveMenuItem
			// 
			this.mContextSaveMenuItem.Index = 1;
			this.mContextSaveMenuItem.Text = "&Save";
			this.mContextSaveMenuItem.Click += new System.EventHandler(this.mSaveAsMenuItem_Click);
			// 
			// mContextSaveAsMenuItem
			// 
			this.mContextSaveAsMenuItem.Index = 2;
			this.mContextSaveAsMenuItem.Text = "Save &as";
			this.mContextSaveAsMenuItem.Click += new System.EventHandler(this.mSaveAsMenuItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mCopyMenuItem,
																					  this.mPasteMenuItem});
			this.menuItem2.Text = "&Edit";
			// 
			// mCopyMenuItem
			// 
			this.mCopyMenuItem.Index = 0;
			this.mCopyMenuItem.Text = "&Copy";
			this.mCopyMenuItem.Click += new System.EventHandler(this.mCopyMenuItemC_Click);
			// 
			// mPasteMenuItem
			// 
			this.mPasteMenuItem.Index = 1;
			this.mPasteMenuItem.Text = "&Paste";
			this.mPasteMenuItem.Click += new System.EventHandler(this.mPasteMenuItem_Click);
			// 
			// mCopyMenuItemC
			// 
			this.mCopyMenuItemC.Index = 3;
			this.mCopyMenuItemC.Text = "&Copy";
			this.mCopyMenuItemC.Click += new System.EventHandler(this.mCopyMenuItemC_Click);
			// 
			// mPateMenuItemC
			// 
			this.mPateMenuItemC.Index = 4;
			this.mPateMenuItemC.Text = "&Paste";
			this.mPateMenuItemC.Click += new System.EventHandler(this.mPasteMenuItem_Click);
			// 
			// RichTextDisplay
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(692, 566);
			this.Controls.Add(this.richTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "RichTextDisplay";
			this.ResumeLayout(false);

		}
		#endregion

		private void mExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void mLoadMenuItem_Click(object sender, System.EventArgs e)
		{
			LoadFile();
		}


		private void mSaveAsMenuItem_Click(object sender, System.EventArgs e)
		{
			SaveAs();
		}

		private void mSaveMenuItem_Click(object sender, System.EventArgs e)
		{
			Save();
		}
		private string mCurrentFileName = null;
		
		private string CurrentFileName
		{
			get
			{
				return mCurrentFileName;
			}
			set
			{
				mCurrentFileName = value;
				this.Text = String.Concat("Working on file '", value,"'");
			}
		}

		private void LoadFile()
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.RestoreDirectory = true;
			if( dlg.ShowDialog(this) == DialogResult.OK)
			{
				richTextBox.LoadFile(dlg.FileName, mRichTextStreamType);
				CurrentFileName = dlg.FileName;
			}
			dlg.Dispose();
		}

		private void SaveAs()
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.RestoreDirectory = true;
			if( dlg.ShowDialog(this) == DialogResult.OK)
			{
				richTextBox.SaveFile(dlg.FileName, mRichTextStreamType);
				CurrentFileName = dlg.FileName;
			}
			dlg.Dispose();
		}

		private void Save()
		{
			if( CurrentFileName == null )
			{
				SaveAs();
			}
			else
			{
				richTextBox.SaveFile(mCurrentFileName, mRichTextStreamType);
			}
		}

		private void richTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.Control && e.KeyCode == Keys.S )
			{
				Save();
			}
			else if( e.Control && e.KeyCode == Keys.L)
			{
				LoadFile();
			}
			else if(e.Control && e.KeyCode == Keys.V )
			{
				richTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));
				e.Handled = true;
			}
		}

		private void mCopyMenuItemC_Click(object sender, System.EventArgs e)
		{
			richTextBox.Copy();
		}

		private void mPasteMenuItem_Click(object sender, System.EventArgs e)
		{
			richTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));
		}

		
	}
}
