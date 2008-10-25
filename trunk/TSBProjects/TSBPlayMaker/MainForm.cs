using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PlayProto
{
	public interface IPlay
	{
		void OnPlayChanged(int playIndex);
		void Init();
		void IPlayTabIndexChanged(int index);

		string GetXML();
	}

	/// <summary>
	/// The enum that tells you which type of game you are working on.
	/// </summary>
	public enum GameType
	{
		NES_TSB = 0,
		SNES_TSB,
		SNES_TSB3
	}

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.TabControl mTabControl;
		private System.Windows.Forms.TabPage mOffensiveTab;
		private System.Windows.Forms.TabPage mDefensiveTab;
		private System.Windows.Forms.TabPage mGraphicTab;
		private System.Windows.Forms.TabPage mTextTab;
		private System.Windows.Forms.RichTextBox mTextCommandRichTextBox;
		private System.Windows.Forms.StatusBar mStatusBar;
		private System.Windows.Forms.GroupBox mPlayGroupBox;
		private System.Windows.Forms.RadioButton mRunRadioButton;
		private System.Windows.Forms.RadioButton mPassRadioButton;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.ComboBox mPlaySlotComboBox;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Button mOpenRomButton;
		private System.Windows.Forms.Button mApplyToRomButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ComboBox mPlayNumberComboBox;

		public static byte[] TheROM;
		public static byte[] TheOriginalROM;

		public static PlayTool mPlayTool = null;
		
		private System.Windows.Forms.TabPage mGeneralTab;
		public static MainForm TheMainForm = null;

		public static GameType TheGameType = GameType.NES_TSB;

		public static string NES_FILTER = "nes files (*.nes)|*.nes";

		public static OffensePanel TheOffensePanel = null;
		public static DefensePanel TheDefensePanel = null;
		private PlayProto.DefensePanel m_DefensePanel;
		private System.Windows.Forms.MenuItem m_GuideMenuItem;
		public static GeneralPanel TheGeneralPanel = null;
		private System.Windows.Forms.MenuItem mFileMenuItem;
		private System.Windows.Forms.MenuItem mOpenMenuItem;
		private System.Windows.Forms.MenuItem mSaveMenuItem;
		private System.Windows.Forms.MenuItem mTestFormMenuItem;
		private System.Windows.Forms.MenuItem mExitMenuItem;
		private System.Windows.Forms.MenuItem mHelpMenuItem;
		private PlayProto.OffensePanel m_OffensePanel;
		private PlayProto.GeneralPanel m_GeneralPanel;
		private PlayProto.TileControl m_TileControl;
		private string mGuideFile = "Instructions.txt";

		public static string Version
		{
			get{ return "0.2";}
		}

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			mPlaySlotComboBox.SelectedIndex = 0;
			mPlayNumberComboBox.SelectedIndex = 0;
			if( mPlayTool == null )
				mPlayTool = new PlayTool();
			if( TheMainForm == null )
				TheMainForm = this;

			TheOffensePanel = m_OffensePanel;
			TheDefensePanel = m_DefensePanel;
			TheGeneralPanel = m_GeneralPanel;

			mTabControl_SelectedIndexChanged(null, null);
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			Enable(false);
			
		}

		private void Enable(bool b)
		{
			mPlayGroupBox.Enabled = b;
			mTabControl.Enabled = b;
			if( b )
			{
				foreach( TabPage t in mTabControl.TabPages )
				{
					t.ForeColor = Color.Black;
				}
			}
			else
			{
				foreach( TabPage t in mTabControl.TabPages )
				{
					t.ForeColor = Color.Gray;
				}
			}
		}

		public bool PlayIsRun()
		{
			return mRunRadioButton.Checked;
		}

		public ComboBox PlaySlotComboBox
		{
			get{ return mPlaySlotComboBox; }
		}

		public ComboBox PlayNumberComboBox
		{
			get{ return mPlayNumberComboBox;}
		}

		public PlayTool ThePlayTool
		{
			get{ return mPlayTool;}
		}

		public int PlayIndex
		{
			get
			{
				char c = 'r';
				if( !MainForm.TheMainForm.PlayIsRun() )
					c = 'p';
				int slot = PlaySlotComboBox.SelectedIndex;
				int play = PlayNumberComboBox.SelectedIndex;
				int playIndex = ThePlayTool.GetPlayIndex(c,slot,play);
				return playIndex;
			}
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.mFileMenuItem = new System.Windows.Forms.MenuItem();
			this.mOpenMenuItem = new System.Windows.Forms.MenuItem();
			this.mSaveMenuItem = new System.Windows.Forms.MenuItem();
			this.mTestFormMenuItem = new System.Windows.Forms.MenuItem();
			this.mExitMenuItem = new System.Windows.Forms.MenuItem();
			this.mHelpMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.m_GuideMenuItem = new System.Windows.Forms.MenuItem();
			this.mTabControl = new System.Windows.Forms.TabControl();
			this.mOffensiveTab = new System.Windows.Forms.TabPage();
			this.m_OffensePanel = new PlayProto.OffensePanel();
			this.mDefensiveTab = new System.Windows.Forms.TabPage();
			this.m_DefensePanel = new PlayProto.DefensePanel();
			this.mGeneralTab = new System.Windows.Forms.TabPage();
			this.m_GeneralPanel = new PlayProto.GeneralPanel();
			this.mGraphicTab = new System.Windows.Forms.TabPage();
			this.m_TileControl = new PlayProto.TileControl();
			this.mTextTab = new System.Windows.Forms.TabPage();
			this.mTextCommandRichTextBox = new System.Windows.Forms.RichTextBox();
			this.mStatusBar = new System.Windows.Forms.StatusBar();
			this.mPlayGroupBox = new System.Windows.Forms.GroupBox();
			this.mPlayNumberComboBox = new System.Windows.Forms.ComboBox();
			this.label24 = new System.Windows.Forms.Label();
			this.mPlaySlotComboBox = new System.Windows.Forms.ComboBox();
			this.label23 = new System.Windows.Forms.Label();
			this.mPassRadioButton = new System.Windows.Forms.RadioButton();
			this.mRunRadioButton = new System.Windows.Forms.RadioButton();
			this.mOpenRomButton = new System.Windows.Forms.Button();
			this.mApplyToRomButton = new System.Windows.Forms.Button();
			this.mTabControl.SuspendLayout();
			this.mOffensiveTab.SuspendLayout();
			this.mDefensiveTab.SuspendLayout();
			this.mGeneralTab.SuspendLayout();
			this.mGraphicTab.SuspendLayout();
			this.mTextTab.SuspendLayout();
			this.mPlayGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mFileMenuItem,
																					  this.mHelpMenuItem});
			// 
			// mFileMenuItem
			// 
			this.mFileMenuItem.Index = 0;
			this.mFileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mOpenMenuItem,
																						  this.mSaveMenuItem,
																						  this.mTestFormMenuItem,
																						  this.mExitMenuItem});
			this.mFileMenuItem.Text = "&File";
			// 
			// mOpenMenuItem
			// 
			this.mOpenMenuItem.Index = 0;
			this.mOpenMenuItem.Text = "&Open";
			this.mOpenMenuItem.Click += new System.EventHandler(this.mOpenRomButton_Click);
			// 
			// mSaveMenuItem
			// 
			this.mSaveMenuItem.Index = 1;
			this.mSaveMenuItem.Text = "&Save";
			// 
			// mTestFormMenuItem
			// 
			this.mTestFormMenuItem.Index = 2;
			this.mTestFormMenuItem.Text = "&Test Form";
			this.mTestFormMenuItem.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// mExitMenuItem
			// 
			this.mExitMenuItem.Index = 3;
			this.mExitMenuItem.Text = "E&xit";
			this.mExitMenuItem.Click += new System.EventHandler(this.mExitMenuItem_Click);
			// 
			// mHelpMenuItem
			// 
			this.mHelpMenuItem.Index = 1;
			this.mHelpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.menuItem6,
																						  this.m_GuideMenuItem});
			this.mHelpMenuItem.Text = "&Help";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 0;
			this.menuItem6.Text = "&About";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// m_GuideMenuItem
			// 
			this.m_GuideMenuItem.Index = 1;
			this.m_GuideMenuItem.Text = "&Guide/ How-to";
			this.m_GuideMenuItem.Click += new System.EventHandler(this.m_GuideMenuItem_Click);
			// 
			// mTabControl
			// 
			this.mTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mTabControl.Controls.Add(this.mOffensiveTab);
			this.mTabControl.Controls.Add(this.mDefensiveTab);
			this.mTabControl.Controls.Add(this.mGeneralTab);
			this.mTabControl.Controls.Add(this.mGraphicTab);
			this.mTabControl.Controls.Add(this.mTextTab);
			this.mTabControl.Location = new System.Drawing.Point(0, 2);
			this.mTabControl.Name = "mTabControl";
			this.mTabControl.SelectedIndex = 0;
			this.mTabControl.Size = new System.Drawing.Size(472, 432);
			this.mTabControl.TabIndex = 5;
			this.mTabControl.SelectedIndexChanged += new System.EventHandler(this.mTabControl_SelectedIndexChanged);
			// 
			// mOffensiveTab
			// 
			this.mOffensiveTab.Controls.Add(this.m_OffensePanel);
			this.mOffensiveTab.Location = new System.Drawing.Point(4, 22);
			this.mOffensiveTab.Name = "mOffensiveTab";
			this.mOffensiveTab.Size = new System.Drawing.Size(464, 406);
			this.mOffensiveTab.TabIndex = 0;
			this.mOffensiveTab.Text = "Offense";
			// 
			// m_OffensePanel
			// 
			this.m_OffensePanel.CurrentPlay = 0;
			this.m_OffensePanel.Location = new System.Drawing.Point(0, 0);
			this.m_OffensePanel.Name = "m_OffensePanel";
			this.m_OffensePanel.Size = new System.Drawing.Size(456, 403);
			this.m_OffensePanel.TabIndex = 0;
			// 
			// mDefensiveTab
			// 
			this.mDefensiveTab.Controls.Add(this.m_DefensePanel);
			this.mDefensiveTab.Location = new System.Drawing.Point(4, 22);
			this.mDefensiveTab.Name = "mDefensiveTab";
			this.mDefensiveTab.Size = new System.Drawing.Size(464, 406);
			this.mDefensiveTab.TabIndex = 1;
			this.mDefensiveTab.Text = "Defense";
			// 
			// m_DefensePanel
			// 
			this.m_DefensePanel.CurrentDefensiveReaction = -1;
			this.m_DefensePanel.Location = new System.Drawing.Point(0, 0);
			this.m_DefensePanel.Name = "m_DefensePanel";
			this.m_DefensePanel.Size = new System.Drawing.Size(456, 400);
			this.m_DefensePanel.TabIndex = 0;
			// 
			// mGeneralTab
			// 
			this.mGeneralTab.Controls.Add(this.m_GeneralPanel);
			this.mGeneralTab.Location = new System.Drawing.Point(4, 22);
			this.mGeneralTab.Name = "mGeneralTab";
			this.mGeneralTab.Size = new System.Drawing.Size(464, 406);
			this.mGeneralTab.TabIndex = 4;
			this.mGeneralTab.Text = "General";
			// 
			// m_GeneralPanel
			// 
			this.m_GeneralPanel.Location = new System.Drawing.Point(8, 8);
			this.m_GeneralPanel.Name = "m_GeneralPanel";
			this.m_GeneralPanel.Size = new System.Drawing.Size(440, 392);
			this.m_GeneralPanel.TabIndex = 0;
			// 
			// mGraphicTab
			// 
			this.mGraphicTab.Controls.Add(this.m_TileControl);
			this.mGraphicTab.Location = new System.Drawing.Point(4, 22);
			this.mGraphicTab.Name = "mGraphicTab";
			this.mGraphicTab.Size = new System.Drawing.Size(464, 406);
			this.mGraphicTab.TabIndex = 2;
			this.mGraphicTab.Text = "Play Graphic";
			// 
			// m_TileControl
			// 
			this.m_TileControl.Location = new System.Drawing.Point(8, 8);
			this.m_TileControl.Name = "m_TileControl";
			this.m_TileControl.PlayNumber = 0;
			this.m_TileControl.Size = new System.Drawing.Size(480, 392);
			this.m_TileControl.StandAlone = false;
			this.m_TileControl.TabIndex = 0;
			// 
			// mTextTab
			// 
			this.mTextTab.Controls.Add(this.mTextCommandRichTextBox);
			this.mTextTab.Location = new System.Drawing.Point(4, 22);
			this.mTextTab.Name = "mTextTab";
			this.mTextTab.Size = new System.Drawing.Size(464, 406);
			this.mTextTab.TabIndex = 3;
			this.mTextTab.Text = "Text Commands";
			// 
			// mTextCommandRichTextBox
			// 
			this.mTextCommandRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mTextCommandRichTextBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mTextCommandRichTextBox.Location = new System.Drawing.Point(0, 0);
			this.mTextCommandRichTextBox.Name = "mTextCommandRichTextBox";
			this.mTextCommandRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.mTextCommandRichTextBox.Size = new System.Drawing.Size(464, 406);
			this.mTextCommandRichTextBox.TabIndex = 0;
			this.mTextCommandRichTextBox.Text = "#Text describing the plays goes here";
			// 
			// mStatusBar
			// 
			this.mStatusBar.Location = new System.Drawing.Point(0, 520);
			this.mStatusBar.Name = "mStatusBar";
			this.mStatusBar.Size = new System.Drawing.Size(472, 22);
			this.mStatusBar.TabIndex = 1;
			// 
			// mPlayGroupBox
			// 
			this.mPlayGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mPlayGroupBox.Controls.Add(this.mPlayNumberComboBox);
			this.mPlayGroupBox.Controls.Add(this.label24);
			this.mPlayGroupBox.Controls.Add(this.mPlaySlotComboBox);
			this.mPlayGroupBox.Controls.Add(this.label23);
			this.mPlayGroupBox.Controls.Add(this.mPassRadioButton);
			this.mPlayGroupBox.Controls.Add(this.mRunRadioButton);
			this.mPlayGroupBox.Location = new System.Drawing.Point(8, 438);
			this.mPlayGroupBox.Name = "mPlayGroupBox";
			this.mPlayGroupBox.Size = new System.Drawing.Size(464, 40);
			this.mPlayGroupBox.TabIndex = 4;
			this.mPlayGroupBox.TabStop = false;
			this.mPlayGroupBox.Text = "Play";
			// 
			// mPlayNumberComboBox
			// 
			this.mPlayNumberComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mPlayNumberComboBox.Items.AddRange(new object[] {
																	 "0",
																	 "1",
																	 "2",
																	 "3",
																	 "4",
																	 "5",
																	 "6",
																	 "7"});
			this.mPlayNumberComboBox.Location = new System.Drawing.Point(312, 14);
			this.mPlayNumberComboBox.Name = "mPlayNumberComboBox";
			this.mPlayNumberComboBox.Size = new System.Drawing.Size(48, 21);
			this.mPlayNumberComboBox.TabIndex = 5;
			this.mPlayNumberComboBox.SelectedIndexChanged += new System.EventHandler(this.SendUpdate);
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(232, 15);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(70, 16);
			this.label24.TabIndex = 4;
			this.label24.Text = "Play Number";
			// 
			// mPlaySlotComboBox
			// 
			this.mPlaySlotComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mPlaySlotComboBox.Items.AddRange(new object[] {
																   "0",
																   "1",
																   "2",
																   "3"});
			this.mPlaySlotComboBox.Location = new System.Drawing.Point(176, 14);
			this.mPlaySlotComboBox.Name = "mPlaySlotComboBox";
			this.mPlaySlotComboBox.Size = new System.Drawing.Size(48, 21);
			this.mPlaySlotComboBox.TabIndex = 3;
			this.mPlaySlotComboBox.SelectedIndexChanged += new System.EventHandler(this.SendUpdate);
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(120, 15);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(49, 16);
			this.label23.TabIndex = 2;
			this.label23.Text = "Play Slot";
			// 
			// mPassRadioButton
			// 
			this.mPassRadioButton.Location = new System.Drawing.Point(64, 12);
			this.mPassRadioButton.Name = "mPassRadioButton";
			this.mPassRadioButton.Size = new System.Drawing.Size(56, 20);
			this.mPassRadioButton.TabIndex = 1;
			this.mPassRadioButton.Text = "P&ass";
			this.mPassRadioButton.CheckedChanged += new System.EventHandler(this.SendUpdate);
			// 
			// mRunRadioButton
			// 
			this.mRunRadioButton.Checked = true;
			this.mRunRadioButton.Location = new System.Drawing.Point(8, 12);
			this.mRunRadioButton.Name = "mRunRadioButton";
			this.mRunRadioButton.Size = new System.Drawing.Size(48, 20);
			this.mRunRadioButton.TabIndex = 0;
			this.mRunRadioButton.TabStop = true;
			this.mRunRadioButton.Text = "&Run";
			// 
			// mOpenRomButton
			// 
			this.mOpenRomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mOpenRomButton.Location = new System.Drawing.Point(8, 478);
			this.mOpenRomButton.Name = "mOpenRomButton";
			this.mOpenRomButton.Size = new System.Drawing.Size(168, 20);
			this.mOpenRomButton.TabIndex = 0;
			this.mOpenRomButton.Text = "Open Rom";
			this.mOpenRomButton.Click += new System.EventHandler(this.mOpenRomButton_Click);
			// 
			// mApplyToRomButton
			// 
			this.mApplyToRomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mApplyToRomButton.Location = new System.Drawing.Point(304, 478);
			this.mApplyToRomButton.Name = "mApplyToRomButton";
			this.mApplyToRomButton.Size = new System.Drawing.Size(168, 20);
			this.mApplyToRomButton.TabIndex = 3;
			this.mApplyToRomButton.Text = "Save ROM";
			this.mApplyToRomButton.Click += new System.EventHandler(this.mApplyToRomButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 542);
			this.Controls.Add(this.mApplyToRomButton);
			this.Controls.Add(this.mOpenRomButton);
			this.Controls.Add(this.mPlayGroupBox);
			this.Controls.Add(this.mStatusBar);
			this.Controls.Add(this.mTabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.MinimumSize = new System.Drawing.Size(480, 576);
			this.Name = "MainForm";
			this.Text = "TSB Play Maker";
			this.mTabControl.ResumeLayout(false);
			this.mOffensiveTab.ResumeLayout(false);
			this.mDefensiveTab.ResumeLayout(false);
			this.mGeneralTab.ResumeLayout(false);
			this.mGraphicTab.ResumeLayout(false);
			this.mTextTab.ResumeLayout(false);
			this.mPlayGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private static ButtonForm form = null;
		/// <summary>
		/// Get an image.
		/// If you want to Get an image from the 'Tiles' directory,
		/// the input string needs to be in the format:
		/// "PlayProto.Tiles.Filename.ext"
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static Image GetImage( string file )
		{
			Image ret = null;
			try
			{
				if( form == null )
					form = new ButtonForm();
				System.IO.Stream s =  
					form.GetType().Assembly.GetManifestResourceStream(file);
				if( s != null )
					ret = Image.FromStream(s);
			}
			catch(Exception e )
			{
				MessageBox.Show(e.Message);
			}
			return ret;
		}

		private void mTabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mStatusBar.Text = mTabControl.SelectedTab.Text;
			if( mTabControl.SelectedTab.Text == "Text Commands")
			{
				mPlayGroupBox.Enabled = false;
			}
			else
				mPlayGroupBox.Enabled = true;

			TheOffensePanel.IPlayTabIndexChanged(mTabControl.SelectedIndex);
			TheDefensePanel.IPlayTabIndexChanged(mTabControl.SelectedIndex);
		}

		/// <summary>
		/// Reads the ROM 'fileName' and returns the contents.
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static byte[] ReadRom(string fileName)
		{
			byte[] rom = null;
			if( fileName != null )
			{
				try
				{
					FileInfo f1 = new FileInfo(fileName);
					long len = f1.Length;
					FileStream s1 = new FileStream(fileName, FileMode.Open);
					rom = new byte[(int)len];
					s1.Read(rom,0,(int)len);
					s1.Close();
				}
				catch(Exception ex)
				{
					MessageBox.Show("Error Reading the file! " +ex.Message);
				}
			}
			return rom;
		}

		/// <summary>
		/// Saves the byte array passed to the filename passed.
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="fileName"></param>
		public static void SaveRom( byte[] rom, string fileName )
		{
			FileStream s1 = null;
			try
			{
				long len = rom.Length;
				s1 = new FileStream(fileName, FileMode.OpenOrCreate);
				s1.Write (rom,0,(int)len);
			}
			catch(Exception e )
			{
				MessageBox.Show(null, e.Message + "\n"+ e.StackTrace, "Error Saving Rom");
			}
			finally
			{
				if( s1 != null )
					s1.Close();
			}
		}

		/// <summary>
		/// Returns the contents of 'fileName' as a string.
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static string GetFileContents(string fileName)
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


		/// <summary>
		/// returns an arraylist of strings made up from chunks of 'line'.
		/// </summary>
		/// <param name="line"></param>
		/// <param name="seps"></param>
		/// <returns></returns>
		public static ArrayList SplitLine(string line, char[] seps)
		{
			string[] chunks = line.Split(seps);
			ArrayList ret = new ArrayList(chunks.Length);
			foreach( string c in chunks)
			{
				if( c.Length > 0 )
					ret.Add(c);
			}
			return ret;
		}

		private void OpenRom()
		{
			string filename = GetFileName(true, NES_FILTER);
			if( filename != null )
			{
				try
				{
					TheROM = ReadRom(filename);
					if( TheROM != null && TheROM.Length > 0 )
					{
						TheOriginalROM = new byte[(int)TheROM.Length];
						Array.Copy(TheROM,0,TheOriginalROM,0, TheROM.Length);
						//TheOffensePanel.PopulateData();
						Enable(true);
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show("Error Reading the file! " +ex.Message);
				}
				InitTabs();
				
				if( TheROM != null )
					SendUpdate();
			}
		}

		private void InitTabs()
		{
			m_OffensePanel.Init();
			m_DefensePanel.Init();
			m_GeneralPanel.Init();
			m_TileControl.Init();
		}

		private void mOpenRomButton_Click(object sender, System.EventArgs e)
		{
			OpenRom();
		}

		public static string GetFileName(bool openFileDlg, string filter)
		{
			string ret = null;
			FileDialog dlg = null; 
			
			if( openFileDlg )
				dlg = new OpenFileDialog();
			else
				dlg = new SaveFileDialog();

			dlg.RestoreDirectory = true;
			
			if( filter != null )
				dlg.Filter = filter;

			if( dlg.ShowDialog() == DialogResult.OK )
			{
				ret = dlg.FileName;
			}
			
			return ret;
		}

		public static void FillArray(string data, string[][] theArray, int startLineNo, int startCol)
		{
			if( data != null && theArray != null )
			{
				char[] nl= {'\n'};
				char[] comma = {','};
				string[] lines = data.Split(nl);
				int index   = 0;
				int index2  = 0;
				string line ="";

				for(int i =startLineNo; i < lines.Length; i++)
				{
					line = lines[i];
					ArrayList chunks = MainForm.SplitLine(line, comma);
					index2 = 0;
					for(int j=startCol; j< chunks.Count; j++)
					{
						theArray[index2++][index] = chunks[j] as string;
					}
					index++;
				}
			}
		}

		/// <summary>
		/// Returns an arraylist of 2 string[], with the order perserved for both arrays
		/// but with the duplicates removed.
		/// </summary>
		/// <param name="pointers"></param>
		/// <param name="names"></param>
		/// <returns></returns>
		public static ArrayList EliminateDupsInArrays(string[] pointers, string[] names)
		{
			ArrayList ret = new ArrayList(2);
			// figure out length
			StringCollection tmp1 = new StringCollection();
			StringCollection tmp2 = new StringCollection();
			string po = null;
			string na = null;
			
			for(int i =0; i < pointers.Length; i++)
			{
				po = pointers[i];
				na = names[i];
				if( !tmp1.Contains(po) )
				{
					tmp1.Add(po);
					tmp2.Add(na);
				}
			}

			string[] ret1 = new string[tmp1.Count];
			string[] ret2 = new string[tmp1.Count];

			for(int i =0; i < tmp1.Count; i++)
			{
				ret1[i] = tmp1[i];
				ret2[i] = tmp2[i];
			}

			ret.Add(ret1);
			ret.Add(ret2);
			return ret;
		}


		/// <summary>
		/// Prompts the user for a file to save the data to.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mApplyToRomButton_Click(object sender, System.EventArgs e)
		{
			if( TheROM != null && TheROM.Length > 0 )
			{
				string filename = GetFileName(false,NES_FILTER);
				if( filename != null )
				{
					try
					{
						FileStream s1 = new FileStream(filename, FileMode.OpenOrCreate);
						s1.Write(TheROM, 0, TheROM.Length);
						s1.Close();
						//MessageBox.Show("Data saved to file '"+filename+"'");
					}
					catch(Exception ex)
					{
						MessageBox.Show("Error Writing the file! " +ex.Message);
					}
				}
			}
			else
			{
				MessageBox.Show("ERROR! You did not open a ROM yet.");
			}
		}

		public void SendUpdate()
		{
			if( TheROM == null )
				return;
			UpdatePlayPointer();

			string xml ="";
			
			TabPage c;
			IPlay p = null;
			int playIndex = PlayIndex;
			for( int i = 0; i < mTabControl.TabPages.Count; i++)
			{
				c = mTabControl.TabPages[i] as TabPage;
				if( c != null )
				{
					for( int j = 0; j < c.Controls.Count; j++)
					{
						p = c.Controls[j] as IPlay;
						if( p != null)
						{
							p.OnPlayChanged(playIndex);
							xml += p.GetXML();
						}
					}
				}
			}
			mTextCommandRichTextBox.Text = xml;
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(this, "TSBPlayMaker version "+MainForm.Version, "About");
		}

		private void mExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void m_GuideMenuItem_Click(object sender, System.EventArgs e)
		{
			if( File.Exists(mGuideFile) )
			{
				Process process = new Process();
				process.StartInfo.UseShellExecute        = false;
			
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError  = true;
				process.StartInfo.CreateNoWindow         = true;
				process.StartInfo.FileName               = "Notepad.exe";
				process.StartInfo.Arguments              = mGuideFile;
				process.StartInfo.WorkingDirectory       = ".";
				//process = Process.Start(programExecName, argument );
			
				//process.WaitForExit();
				try
				{
					process.Start();
				}
				catch{}
			}
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			TestForm tf = new TestForm();
			tf.Show();
		}

		private void UpdatePlayPointer()
		{
			int playIndex = PlayIndex;
			string test =  ThePlayTool.GetPlayToCallText(playIndex);
			// todo: finish this.
		}

		private void SendUpdate(object sender, System.EventArgs e)
		{
			SendUpdate();
		}

		
	}
}
/*
// 24 = number of bytes in (name, formation, )
Playname = 0x1D420 + (playNumberIndex * 24 ) 
 * */
