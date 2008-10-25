using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for TeamSelectForm.
	/// </summary>
	public class TeamSelectForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox mColtsTextBox;
		private System.Windows.Forms.TextBox mDolphinsTextBox;
		private System.Windows.Forms.TextBox mPatsTextBox;
		public TSBSeasonGen.TeamFileSelector mBills;
		public TSBSeasonGen.TeamFileSelector mColts;
		public TSBSeasonGen.TeamFileSelector mDolphins;
		public TSBSeasonGen.TeamFileSelector mJets;
		public TSBSeasonGen.TeamFileSelector mRedskins;
		public TSBSeasonGen.TeamFileSelector mGiants;
		public TSBSeasonGen.TeamFileSelector mEagles;
		public TSBSeasonGen.TeamFileSelector mCowboys;
		public TSBSeasonGen.TeamFileSelector mBengals;
		public TSBSeasonGen.TeamFileSelector mBrowns;
		public TSBSeasonGen.TeamFileSelector mOilers;
		public TSBSeasonGen.TeamFileSelector mSteelers;
		public TSBSeasonGen.TeamFileSelector mBears;
		public TSBSeasonGen.TeamFileSelector mLions;
		public TSBSeasonGen.TeamFileSelector mPackers;
		public TSBSeasonGen.TeamFileSelector mVikings;
		public TSBSeasonGen.TeamFileSelector mBroncos;
		public TSBSeasonGen.TeamFileSelector mChiefs;
		public TSBSeasonGen.TeamFileSelector mRaiders;
		public TSBSeasonGen.TeamFileSelector mChargers;
		public TSBSeasonGen.TeamFileSelector mSeahawks;
		public TSBSeasonGen.TeamFileSelector m49ers;
		public TSBSeasonGen.TeamFileSelector mRams;
		public TSBSeasonGen.TeamFileSelector mSaints;
		public TSBSeasonGen.TeamFileSelector mFalcons;
		private System.Windows.Forms.Panel mControlPanel;
		private System.Windows.Forms.Label mYearLabel;
		private System.Windows.Forms.TextBox mYearTextBox;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mHowToMenuItem;
		private System.Windows.Forms.MenuItem mAboutMenuItem;
		private System.Windows.Forms.MenuItem mSaveConfigMenuItem;
		private System.Windows.Forms.MenuItem mExitMenuItem;
		private System.Windows.Forms.Button mSaveConfigFileButton;
		private System.Windows.Forms.Button mGenerateSeasonButton;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mLoadConfigFileMenuItem;
		private System.Windows.Forms.Button mLoadConfigFileButton;
		public TSBSeasonGen.TeamFileSelector mPatriots;
		public TSBSeasonGen.TeamFileSelector mCardinals;
		public TSBSeasonGen.TeamFileSelector mBuccaneers;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Regex mYearRegex, mYearTeam;
		private System.Windows.Forms.CheckBox mAutoCorrectSim;
		private System.Windows.Forms.CheckBox mGenerateBasedOnYear;
		private System.Windows.Forms.MenuItem m_OptionsMenuItem;
		private System.Windows.Forms.CheckBox mGenerateForSnesCheckBox;

		public TeamSelectForm()
		{
			InitializeComponent();
			//this.mGenerateScheduleCheckBox.Checked = MainClass.GenerateSchedule;
			mYearRegex = new Regex("^([0-9]+)$");
			mYearTeam  = new Regex("^([0-9]+)\\s+([49a-z]+)$");
			EnableTeamSelectors(!mGenerateBasedOnYear.Checked);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TeamSelectForm));
			this.mColtsTextBox = new System.Windows.Forms.TextBox();
			this.mDolphinsTextBox = new System.Windows.Forms.TextBox();
			this.mPatsTextBox = new System.Windows.Forms.TextBox();
			this.mBills = new TSBSeasonGen.TeamFileSelector();
			this.mColts = new TSBSeasonGen.TeamFileSelector();
			this.mDolphins = new TSBSeasonGen.TeamFileSelector();
			this.mPatriots = new TSBSeasonGen.TeamFileSelector();
			this.mJets = new TSBSeasonGen.TeamFileSelector();
			this.mRedskins = new TSBSeasonGen.TeamFileSelector();
			this.mGiants = new TSBSeasonGen.TeamFileSelector();
			this.mEagles = new TSBSeasonGen.TeamFileSelector();
			this.mCardinals = new TSBSeasonGen.TeamFileSelector();
			this.mCowboys = new TSBSeasonGen.TeamFileSelector();
			this.mBengals = new TSBSeasonGen.TeamFileSelector();
			this.mBrowns = new TSBSeasonGen.TeamFileSelector();
			this.mOilers = new TSBSeasonGen.TeamFileSelector();
			this.mSteelers = new TSBSeasonGen.TeamFileSelector();
			this.mBears = new TSBSeasonGen.TeamFileSelector();
			this.mLions = new TSBSeasonGen.TeamFileSelector();
			this.mPackers = new TSBSeasonGen.TeamFileSelector();
			this.mVikings = new TSBSeasonGen.TeamFileSelector();
			this.mBuccaneers = new TSBSeasonGen.TeamFileSelector();
			this.mBroncos = new TSBSeasonGen.TeamFileSelector();
			this.mChiefs = new TSBSeasonGen.TeamFileSelector();
			this.mRaiders = new TSBSeasonGen.TeamFileSelector();
			this.mChargers = new TSBSeasonGen.TeamFileSelector();
			this.mSeahawks = new TSBSeasonGen.TeamFileSelector();
			this.m49ers = new TSBSeasonGen.TeamFileSelector();
			this.mRams = new TSBSeasonGen.TeamFileSelector();
			this.mSaints = new TSBSeasonGen.TeamFileSelector();
			this.mFalcons = new TSBSeasonGen.TeamFileSelector();
			this.mControlPanel = new System.Windows.Forms.Panel();
			this.mGenerateBasedOnYear = new System.Windows.Forms.CheckBox();
			this.mAutoCorrectSim = new System.Windows.Forms.CheckBox();
			this.mGenerateForSnesCheckBox = new System.Windows.Forms.CheckBox();
			this.mLoadConfigFileButton = new System.Windows.Forms.Button();
			this.mGenerateSeasonButton = new System.Windows.Forms.Button();
			this.mSaveConfigFileButton = new System.Windows.Forms.Button();
			this.mYearTextBox = new System.Windows.Forms.TextBox();
			this.mYearLabel = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mSaveConfigMenuItem = new System.Windows.Forms.MenuItem();
			this.mLoadConfigFileMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mExitMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mHowToMenuItem = new System.Windows.Forms.MenuItem();
			this.mAboutMenuItem = new System.Windows.Forms.MenuItem();
			this.m_OptionsMenuItem = new System.Windows.Forms.MenuItem();
			this.mControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mColtsTextBox
			// 
			this.mColtsTextBox.Location = new System.Drawing.Point(112, 80);
			this.mColtsTextBox.Name = "mColtsTextBox";
			this.mColtsTextBox.TabIndex = 2;
			this.mColtsTextBox.Text = "";
			// 
			// mDolphinsTextBox
			// 
			this.mDolphinsTextBox.Location = new System.Drawing.Point(112, 128);
			this.mDolphinsTextBox.Name = "mDolphinsTextBox";
			this.mDolphinsTextBox.TabIndex = 4;
			this.mDolphinsTextBox.Text = "";
			// 
			// mPatsTextBox
			// 
			this.mPatsTextBox.Location = new System.Drawing.Point(112, 176);
			this.mPatsTextBox.Name = "mPatsTextBox";
			this.mPatsTextBox.TabIndex = 6;
			this.mPatsTextBox.Text = "";
			// 
			// mBills
			// 
			this.mBills.Location = new System.Drawing.Point(96, 24);
			this.mBills.Name = "mBills";
			this.mBills.Size = new System.Drawing.Size(160, 32);
			this.mBills.TabIndex = 0;
			this.mBills.Team = "";
			// 
			// mColts
			// 
			this.mColts.Location = new System.Drawing.Point(96, 72);
			this.mColts.Name = "mColts";
			this.mColts.Size = new System.Drawing.Size(160, 32);
			this.mColts.TabIndex = 1;
			this.mColts.Team = "";
			// 
			// mDolphins
			// 
			this.mDolphins.Location = new System.Drawing.Point(96, 120);
			this.mDolphins.Name = "mDolphins";
			this.mDolphins.Size = new System.Drawing.Size(160, 32);
			this.mDolphins.TabIndex = 2;
			this.mDolphins.Team = "";
			// 
			// mPatriots
			// 
			this.mPatriots.Location = new System.Drawing.Point(96, 168);
			this.mPatriots.Name = "mPatriots";
			this.mPatriots.Size = new System.Drawing.Size(160, 32);
			this.mPatriots.TabIndex = 3;
			this.mPatriots.Team = "";
			// 
			// mJets
			// 
			this.mJets.Location = new System.Drawing.Point(96, 224);
			this.mJets.Name = "mJets";
			this.mJets.Size = new System.Drawing.Size(160, 32);
			this.mJets.TabIndex = 4;
			this.mJets.Team = "";
			// 
			// mRedskins
			// 
			this.mRedskins.Location = new System.Drawing.Point(96, 272);
			this.mRedskins.Name = "mRedskins";
			this.mRedskins.Size = new System.Drawing.Size(160, 32);
			this.mRedskins.TabIndex = 5;
			this.mRedskins.Team = "";
			// 
			// mGiants
			// 
			this.mGiants.Location = new System.Drawing.Point(96, 320);
			this.mGiants.Name = "mGiants";
			this.mGiants.Size = new System.Drawing.Size(160, 32);
			this.mGiants.TabIndex = 6;
			this.mGiants.Team = "";
			// 
			// mEagles
			// 
			this.mEagles.Location = new System.Drawing.Point(96, 368);
			this.mEagles.Name = "mEagles";
			this.mEagles.Size = new System.Drawing.Size(160, 32);
			this.mEagles.TabIndex = 7;
			this.mEagles.Team = "";
			// 
			// mCardinals
			// 
			this.mCardinals.Location = new System.Drawing.Point(96, 416);
			this.mCardinals.Name = "mCardinals";
			this.mCardinals.Size = new System.Drawing.Size(160, 32);
			this.mCardinals.TabIndex = 8;
			this.mCardinals.Team = "";
			// 
			// mCowboys
			// 
			this.mCowboys.Location = new System.Drawing.Point(96, 464);
			this.mCowboys.Name = "mCowboys";
			this.mCowboys.Size = new System.Drawing.Size(160, 32);
			this.mCowboys.TabIndex = 9;
			this.mCowboys.Team = "";
			// 
			// mBengals
			// 
			this.mBengals.Location = new System.Drawing.Point(352, 24);
			this.mBengals.Name = "mBengals";
			this.mBengals.Size = new System.Drawing.Size(160, 32);
			this.mBengals.TabIndex = 10;
			this.mBengals.Team = "";
			// 
			// mBrowns
			// 
			this.mBrowns.Location = new System.Drawing.Point(352, 72);
			this.mBrowns.Name = "mBrowns";
			this.mBrowns.Size = new System.Drawing.Size(160, 32);
			this.mBrowns.TabIndex = 11;
			this.mBrowns.Team = "";
			// 
			// mOilers
			// 
			this.mOilers.Location = new System.Drawing.Point(352, 120);
			this.mOilers.Name = "mOilers";
			this.mOilers.Size = new System.Drawing.Size(160, 32);
			this.mOilers.TabIndex = 12;
			this.mOilers.Team = "";
			// 
			// mSteelers
			// 
			this.mSteelers.Location = new System.Drawing.Point(352, 168);
			this.mSteelers.Name = "mSteelers";
			this.mSteelers.Size = new System.Drawing.Size(160, 32);
			this.mSteelers.TabIndex = 13;
			this.mSteelers.Team = "";
			// 
			// mBears
			// 
			this.mBears.Location = new System.Drawing.Point(352, 264);
			this.mBears.Name = "mBears";
			this.mBears.Size = new System.Drawing.Size(160, 32);
			this.mBears.TabIndex = 14;
			this.mBears.Team = "";
			// 
			// mLions
			// 
			this.mLions.Location = new System.Drawing.Point(352, 312);
			this.mLions.Name = "mLions";
			this.mLions.Size = new System.Drawing.Size(160, 32);
			this.mLions.TabIndex = 15;
			this.mLions.Team = "";
			// 
			// mPackers
			// 
			this.mPackers.Location = new System.Drawing.Point(352, 368);
			this.mPackers.Name = "mPackers";
			this.mPackers.Size = new System.Drawing.Size(160, 32);
			this.mPackers.TabIndex = 16;
			this.mPackers.Team = "";
			// 
			// mVikings
			// 
			this.mVikings.Location = new System.Drawing.Point(352, 416);
			this.mVikings.Name = "mVikings";
			this.mVikings.Size = new System.Drawing.Size(160, 32);
			this.mVikings.TabIndex = 17;
			this.mVikings.Team = "";
			// 
			// mBuccaneers
			// 
			this.mBuccaneers.Location = new System.Drawing.Point(352, 464);
			this.mBuccaneers.Name = "mBuccaneers";
			this.mBuccaneers.Size = new System.Drawing.Size(160, 32);
			this.mBuccaneers.TabIndex = 18;
			this.mBuccaneers.Team = "";
			// 
			// mBroncos
			// 
			this.mBroncos.Location = new System.Drawing.Point(608, 24);
			this.mBroncos.Name = "mBroncos";
			this.mBroncos.Size = new System.Drawing.Size(160, 32);
			this.mBroncos.TabIndex = 19;
			this.mBroncos.Team = "";
			// 
			// mChiefs
			// 
			this.mChiefs.Location = new System.Drawing.Point(608, 72);
			this.mChiefs.Name = "mChiefs";
			this.mChiefs.Size = new System.Drawing.Size(160, 32);
			this.mChiefs.TabIndex = 20;
			this.mChiefs.Team = "";
			// 
			// mRaiders
			// 
			this.mRaiders.Location = new System.Drawing.Point(608, 120);
			this.mRaiders.Name = "mRaiders";
			this.mRaiders.Size = new System.Drawing.Size(160, 32);
			this.mRaiders.TabIndex = 21;
			this.mRaiders.Team = "";
			// 
			// mChargers
			// 
			this.mChargers.Location = new System.Drawing.Point(608, 168);
			this.mChargers.Name = "mChargers";
			this.mChargers.Size = new System.Drawing.Size(160, 32);
			this.mChargers.TabIndex = 22;
			this.mChargers.Team = "";
			// 
			// mSeahawks
			// 
			this.mSeahawks.Location = new System.Drawing.Point(608, 216);
			this.mSeahawks.Name = "mSeahawks";
			this.mSeahawks.Size = new System.Drawing.Size(160, 32);
			this.mSeahawks.TabIndex = 23;
			this.mSeahawks.Team = "";
			// 
			// m49ers
			// 
			this.m49ers.Location = new System.Drawing.Point(608, 320);
			this.m49ers.Name = "m49ers";
			this.m49ers.Size = new System.Drawing.Size(160, 32);
			this.m49ers.TabIndex = 24;
			this.m49ers.Team = "";
			// 
			// mRams
			// 
			this.mRams.Location = new System.Drawing.Point(608, 368);
			this.mRams.Name = "mRams";
			this.mRams.Size = new System.Drawing.Size(160, 32);
			this.mRams.TabIndex = 25;
			this.mRams.Team = "";
			// 
			// mSaints
			// 
			this.mSaints.Location = new System.Drawing.Point(608, 416);
			this.mSaints.Name = "mSaints";
			this.mSaints.Size = new System.Drawing.Size(160, 32);
			this.mSaints.TabIndex = 26;
			this.mSaints.Team = "";
			// 
			// mFalcons
			// 
			this.mFalcons.Location = new System.Drawing.Point(608, 464);
			this.mFalcons.Name = "mFalcons";
			this.mFalcons.Size = new System.Drawing.Size(160, 32);
			this.mFalcons.TabIndex = 27;
			this.mFalcons.Team = "";
			// 
			// mControlPanel
			// 
			this.mControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mControlPanel.Controls.Add(this.mGenerateBasedOnYear);
			this.mControlPanel.Controls.Add(this.mAutoCorrectSim);
			this.mControlPanel.Controls.Add(this.mGenerateForSnesCheckBox);
			this.mControlPanel.Controls.Add(this.mLoadConfigFileButton);
			this.mControlPanel.Controls.Add(this.mGenerateSeasonButton);
			this.mControlPanel.Controls.Add(this.mSaveConfigFileButton);
			this.mControlPanel.Controls.Add(this.mYearTextBox);
			this.mControlPanel.Controls.Add(this.mYearLabel);
			this.mControlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.mControlPanel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mControlPanel.ForeColor = System.Drawing.Color.White;
			this.mControlPanel.Location = new System.Drawing.Point(0, 513);
			this.mControlPanel.Name = "mControlPanel";
			this.mControlPanel.Size = new System.Drawing.Size(808, 80);
			this.mControlPanel.TabIndex = 28;
			// 
			// mGenerateBasedOnYear
			// 
			this.mGenerateBasedOnYear.Location = new System.Drawing.Point(16, 40);
			this.mGenerateBasedOnYear.Name = "mGenerateBasedOnYear";
			this.mGenerateBasedOnYear.Size = new System.Drawing.Size(144, 32);
			this.mGenerateBasedOnYear.TabIndex = 9;
			this.mGenerateBasedOnYear.Text = "Generate season based on year";
			this.mGenerateBasedOnYear.CheckedChanged += new System.EventHandler(this.mGenerateBasedOnYear_CheckedChanged);
			// 
			// mAutoCorrectSim
			// 
			this.mAutoCorrectSim.Checked = true;
			this.mAutoCorrectSim.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mAutoCorrectSim.Location = new System.Drawing.Point(184, 32);
			this.mAutoCorrectSim.Name = "mAutoCorrectSim";
			this.mAutoCorrectSim.Size = new System.Drawing.Size(208, 32);
			this.mAutoCorrectSim.TabIndex = 8;
			this.mAutoCorrectSim.Text = "&Auto Correct Defensive Sim attributes";
			this.mAutoCorrectSim.CheckedChanged += new System.EventHandler(this.mAutoCorrectSim_CheckedChanged);
			// 
			// mGenerateForSnesCheckBox
			// 
			this.mGenerateForSnesCheckBox.Location = new System.Drawing.Point(184, 8);
			this.mGenerateForSnesCheckBox.Name = "mGenerateForSnesCheckBox";
			this.mGenerateForSnesCheckBox.Size = new System.Drawing.Size(168, 24);
			this.mGenerateForSnesCheckBox.TabIndex = 7;
			this.mGenerateForSnesCheckBox.Text = "Generate &for Snes";
			this.mGenerateForSnesCheckBox.CheckedChanged += new System.EventHandler(this.mGenerateForSnesCheckBox_CheckedChanged);
			// 
			// mLoadConfigFileButton
			// 
			this.mLoadConfigFileButton.Location = new System.Drawing.Point(440, 8);
			this.mLoadConfigFileButton.Name = "mLoadConfigFileButton";
			this.mLoadConfigFileButton.Size = new System.Drawing.Size(168, 32);
			this.mLoadConfigFileButton.TabIndex = 6;
			this.mLoadConfigFileButton.Text = "&Load Config File";
			this.mLoadConfigFileButton.Click += new System.EventHandler(this.mLoadConfigFileButton_Click);
			// 
			// mGenerateSeasonButton
			// 
			this.mGenerateSeasonButton.Location = new System.Drawing.Point(632, 40);
			this.mGenerateSeasonButton.Name = "mGenerateSeasonButton";
			this.mGenerateSeasonButton.Size = new System.Drawing.Size(168, 32);
			this.mGenerateSeasonButton.TabIndex = 5;
			this.mGenerateSeasonButton.Text = "&Generate Season";
			this.mGenerateSeasonButton.Click += new System.EventHandler(this.mGenerateSeasonButton_Click);
			// 
			// mSaveConfigFileButton
			// 
			this.mSaveConfigFileButton.Location = new System.Drawing.Point(440, 40);
			this.mSaveConfigFileButton.Name = "mSaveConfigFileButton";
			this.mSaveConfigFileButton.Size = new System.Drawing.Size(168, 32);
			this.mSaveConfigFileButton.TabIndex = 4;
			this.mSaveConfigFileButton.Text = "&Save Config File";
			this.mSaveConfigFileButton.Click += new System.EventHandler(this.mSaveConfigFileButton_Click);
			// 
			// mYearTextBox
			// 
			this.mYearTextBox.Location = new System.Drawing.Point(96, 8);
			this.mYearTextBox.Name = "mYearTextBox";
			this.mYearTextBox.Size = new System.Drawing.Size(72, 22);
			this.mYearTextBox.TabIndex = 1;
			this.mYearTextBox.Text = "1991";
			this.mYearTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mYearTextBox_KeyPress);
			// 
			// mYearLabel
			// 
			this.mYearLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mYearLabel.Location = new System.Drawing.Point(8, 8);
			this.mYearLabel.Name = "mYearLabel";
			this.mYearLabel.Size = new System.Drawing.Size(80, 23);
			this.mYearLabel.TabIndex = 0;
			this.mYearLabel.Text = "Schedule";
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
																					  this.mSaveConfigMenuItem,
																					  this.mLoadConfigFileMenuItem,
																					  this.menuItem3,
																					  this.m_OptionsMenuItem,
																					  this.mExitMenuItem});
			this.menuItem1.Text = "&File";
			// 
			// mSaveConfigMenuItem
			// 
			this.mSaveConfigMenuItem.Index = 0;
			this.mSaveConfigMenuItem.Text = "&Save Config File";
			this.mSaveConfigMenuItem.Click += new System.EventHandler(this.mSaveConfigMenuItem_Click);
			// 
			// mLoadConfigFileMenuItem
			// 
			this.mLoadConfigFileMenuItem.Index = 1;
			this.mLoadConfigFileMenuItem.Text = "&Load Config File";
			this.mLoadConfigFileMenuItem.Click += new System.EventHandler(this.mLoadConfigFileMenuItem_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "&Generate Season";
			// 
			// mExitMenuItem
			// 
			this.mExitMenuItem.Index = 4;
			this.mExitMenuItem.Text = "E&xit";
			this.mExitMenuItem.Click += new System.EventHandler(this.mExitMenuItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mHowToMenuItem,
																					  this.mAboutMenuItem});
			this.menuItem2.Text = "&Help";
			// 
			// mHowToMenuItem
			// 
			this.mHowToMenuItem.Index = 0;
			this.mHowToMenuItem.Text = "&How To";
			this.mHowToMenuItem.Click += new System.EventHandler(this.mHowToMenuItem_Click);
			// 
			// mAboutMenuItem
			// 
			this.mAboutMenuItem.Index = 1;
			this.mAboutMenuItem.Text = "&About";
			this.mAboutMenuItem.Click += new System.EventHandler(this.mAboutMenuItem_Click);
			// 
			// m_OptionsMenuItem
			// 
			this.m_OptionsMenuItem.Index = 3;
			this.m_OptionsMenuItem.Text = "Season Gen &Options";
			this.m_OptionsMenuItem.Click += new System.EventHandler(this.m_OptionsMenuItem_Click);
			// 
			// TeamSelectForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(96)), ((System.Byte)(248)));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(808, 593);
			this.Controls.Add(this.mControlPanel);
			this.Controls.Add(this.mFalcons);
			this.Controls.Add(this.mSaints);
			this.Controls.Add(this.mRams);
			this.Controls.Add(this.m49ers);
			this.Controls.Add(this.mSeahawks);
			this.Controls.Add(this.mChargers);
			this.Controls.Add(this.mRaiders);
			this.Controls.Add(this.mChiefs);
			this.Controls.Add(this.mBroncos);
			this.Controls.Add(this.mBuccaneers);
			this.Controls.Add(this.mVikings);
			this.Controls.Add(this.mPackers);
			this.Controls.Add(this.mLions);
			this.Controls.Add(this.mBears);
			this.Controls.Add(this.mSteelers);
			this.Controls.Add(this.mOilers);
			this.Controls.Add(this.mBrowns);
			this.Controls.Add(this.mBengals);
			this.Controls.Add(this.mCowboys);
			this.Controls.Add(this.mCardinals);
			this.Controls.Add(this.mEagles);
			this.Controls.Add(this.mGiants);
			this.Controls.Add(this.mRedskins);
			this.Controls.Add(this.mJets);
			this.Controls.Add(this.mPatriots);
			this.Controls.Add(this.mDolphins);
			this.Controls.Add(this.mColts);
			this.Controls.Add(this.mBills);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.Color.Black;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(816, 648);
			this.Menu = this.mainMenu1;
			this.Name = "TeamSelectForm";
			this.Text = "TSBSeasonGen";
			this.mControlPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Asks the user for a file name.
		/// </summary>
		/// <param name="openFile">true if you want an 'OpenFileDialog', false
		/// if you want a 'SaveFileDialog'.</param>
		/// <param name="filter">null for no filter, otherwise a valid FileDialog
		/// filter string is required.</param>
		/// <returns>The file name chosen by the user, null if operation was canceled.</returns>
		public static string GetFileName(bool openFile, string filter)
		{
			string ret = null;

			FileDialog dlg; 

			if( openFile )
			{
				dlg = new OpenFileDialog();
				dlg.CheckFileExists = false;
			}
			else
			{
				dlg = new SaveFileDialog();
			}

			dlg.RestoreDirectory = true;

			if( filter != null )
				dlg.Filter = "Text files (*.txt)|*.txt";
			
			DialogResult result = dlg.ShowDialog();
			if( result == DialogResult.OK )
			{
				ret = dlg.FileName;
			}
			dlg.Dispose();

			return ret;
		}

		public void SaveConfigFile()
		{
			string contents = GetConfigFile();
			string fileName = GetFileName(false,null);
			if( fileName != null )
			{
				MainClass.WriteFile(fileName, contents);
			}
		}

		public string GetConfigFile()
		{
			string ret ="";
			StringBuilder sb = new StringBuilder();
			if( MainClass.GenerateSchedule && this.mYearTextBox.Text.Length > 0 )
			{
				sb.Append(string.Format("SCHEDULE={0}\r\n",this.mYearTextBox.Text));
			}
			foreach(string teamName in Season.team_names)
			{
				sb.Append(GetTeam(teamName));
			}
			ret = sb.ToString();
			return ret;
		}

		public string GetTeam(string teamName)
		{
			string ret = "";
			TeamFileSelector team = GetTeamFileSelector(teamName);
			
			if( team != null )
			{
				if(team.Team == "")
				{
					// nothing specified
				}
				else if(mYearRegex.Match(team.Team) != Match.Empty )
				{
					ret = string.Format("{0} {1}\r\n",team.Team, teamName);
				}
				else if( mYearTeam.Match(team.Team) != Match.Empty )
				{
					ret = string.Format("{0} = {1}\r\n",teamName, team.Team);
				}
				else
					ret = string.Format("{0}={1}\r\n",teamName, team.Team);
			}

			return ret;
		}

		private TeamFileSelector GetTeamFileSelector(string teamName)
		{
			TeamFileSelector ret = null;
			TeamFileSelector tmp = null;

			object o;
			string str;
			for(int i = 0; i < this.Controls.Count; i++)
			{
				o = this.Controls[i];
				if( o is TeamFileSelector && 
					(tmp = o as TeamFileSelector) != null )
				{
					str = tmp.Name.ToLower();
					if(str.IndexOf(teamName) > -1 )
					{
						ret = tmp;
						break;
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// returns true if all teamFileSelectors are empty.
		/// </summary>
		/// <returns></returns>
		private bool AreTeamFileSelectorsEmpty()
		{
			bool ret = true;
			TeamFileSelector tmp = null;

			object o;
			string str;
			for(int i = 0; i < this.Controls.Count; i++)
			{
				o = this.Controls[i];
				if( (tmp = o as TeamFileSelector) != null )
				{
					if(tmp.Team.Length > 0 )
					{
						ret = false;
					}
				}
			}
			return ret;
		}

		public void LoadConfigFile()
		{
			string fileName = GetFileName(true, null);
			TeamFileSelector tfs;
			if( fileName != null )
			{
				ArrayList elements = ConfigFileUtil.GetConfigFileElements( fileName);
				foreach(ConfigFileElement element in elements)
				{
					switch(element.Type)
					{
						case ConfigFileElementType.Schedule:
							this.mYearTextBox.Text = ""+element.Year;
							break;
						case ConfigFileElementType.TeamFromYear:
							tfs = GetTeamFileSelector(element.InRomTeam);
							if( tfs != null )
							{
								if(element.InRomTeam == element.RealTeam )
									tfs.Team = ""+element.Year;
								else
									tfs.Team = element.Year+" "+element.RealTeam;
							}
							else
							{
								MainClass.AddError(string.Format("Error with team '{0}'",element.InRomTeam));
							}
							break;
						case ConfigFileElementType.TeamFromFile:
							tfs = GetTeamFileSelector(element.InRomTeam);
							if( tfs != null )
								tfs.Team = element.TeamFileName;
							else
							{
								MainClass.AddError(string.Format("Error with team '{0}'",element.InRomTeam));
							}
							break;
					}
				}
			}
			MainClass.ShowErrors();
		}

		private void EnableTeamSelectors(bool enable)
		{
			TeamFileSelector current;
			string teamName;
			for( int i = 0 ; i < 28; i++)
			{
				teamName = Season.team_names[i];
				current = GetTeamFileSelector(teamName);
	//			if(current != null )
					current.Enabled = enable;
			}
		}

		/// <summary>
		/// Write 'results' to 'fileName' and exec 'Notepad' to view the results.
		/// </summary>
		/// <param name="results"></param>
		public void ShowResults(string results, string fileName)
		{
			//string fileName = "tempResults.txt";
				//System.IO.Path.GetTempFileName();
			results = results.Replace("\r\n", "\n");
			results = results.Replace("\n", "\r\n");
			MainClass.WriteFile(fileName,results);

			Process process = new Process();
			process.StartInfo.UseShellExecute        = false;
			
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError  = true;
			process.StartInfo.CreateNoWindow         = true;
			process.StartInfo.FileName               = "Notepad.exe";
			process.StartInfo.Arguments              = fileName;
			process.StartInfo.WorkingDirectory       = ".";
			//process = Process.Start(programExecName, argument );
			
			//process.WaitForExit();
			try
			{
				process.Start();
			}
			catch{}
			
		}

		private void GenerateBasedOnConfig()
		{
			string configFile = GetConfigFile();
			string configFileName = "tsbseasongen_temp.tmp";
			MainClass.WriteFile(configFileName, configFile);
			Season s = new Season(configFileName);
			
			string season = s.GenerateSeason();
			string fileName = "tmpSeasonGen";
			ShowResults(season,fileName);
		}

		private void GenerateBasedOnYear()
		{
			int year = 0;
			string s_year = mYearTextBox.Text;
			string test = MainClass.LeagueFolder+Path.DirectorySeparatorChar+ s_year;
			if( Directory.Exists(test) )
			{
				year = Int32.Parse( s_year);
				Season s = new Season(year);
			
				string season = s.GenerateSeason();
				string fileName = "tmpSeasonGen";
				ShowResults(season,fileName);
			}
			else
			{
				MessageBox.Show(string.Format("Invalid year {0}",s_year));
				mYearTextBox.Focus();
				mYearTextBox.SelectAll();
			}
		}

		#region event handling functions

		private void mYearTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			switch( e.KeyChar )
			{
				case '0': case '1': case '2': case '3':
				case '4': case '5': case '6': case '7':
				case '8': case '9': 
					break;
				default:
					e.Handled = true;
					break;
			}
			if( mYearTextBox.Text.Length > 3)
				e.Handled = true;

			if( e.KeyChar == (char)8)
				e.Handled = false;
		}

		private void mGenerateScheduleCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
		}

		private void mSaveConfigMenuItem_Click(object sender, System.EventArgs e)
		{
			SaveConfigFile();
		}

		private void mSaveConfigFileButton_Click(object sender, System.EventArgs e)
		{
			SaveConfigFile();
		}

		private void mExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void mGenerateSeasonButton_Click(object sender, System.EventArgs e)
		{
			MainClass.GenerateSchedule = (mYearTextBox.Text.Length != 0);
			if( mGenerateBasedOnYear.Checked || AreTeamFileSelectorsEmpty() )
				GenerateBasedOnYear();
			else
				GenerateBasedOnConfig();
		}

		private void mLoadConfigFileMenuItem_Click(object sender, System.EventArgs e)
		{
			LoadConfigFile();
		}

		private void mLoadConfigFileButton_Click(object sender, System.EventArgs e)
		{
			mGenerateBasedOnYear.Checked = false;
			LoadConfigFile();
		}

		private void mGenerateForSnesCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			MainClass.GenerateForSNES = mGenerateForSnesCheckBox.Checked;
		}

		#endregion

		private void mAutoCorrectSim_CheckedChanged(object sender, System.EventArgs e)
		{
			MainClass.AutoCorrectDefenseSimData = mAutoCorrectSim.Checked;
		}

		private void mAboutMenuItem_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show(string.Format ("TSBSeasonGen Version {0}.",MainClass.VERSION), MainClass.VERSION);
		}

		private void mHowToMenuItem_Click(object sender, System.EventArgs e)
		{
			RichTextDisplay.ShowMessage("How To", MainClass.TEAM_HOW_TO);
				
		}

		private void mGenerateBasedOnYear_CheckedChanged(object sender, System.EventArgs e)
		{
			EnableTeamSelectors( !mGenerateBasedOnYear.Checked );
		}

		private void m_OptionsMenuItem_Click(object sender, System.EventArgs e)
		{
			string seasonGenOptionFile = Path.GetFullPath("SeasonGenOptions.txt");
			if( File.Exists( seasonGenOptionFile) && Path.DirectorySeparatorChar == '\\' )
			{
				Process process = new Process();
				process.StartInfo.UseShellExecute        = false;
			
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError  = true;
				process.StartInfo.CreateNoWindow         = true;
				process.StartInfo.FileName               = "Notepad.exe";
				process.StartInfo.Arguments              = seasonGenOptionFile;
				process.StartInfo.WorkingDirectory       = ".";
				//process = Process.Start(programExecName, argument );
			
				//process.WaitForExit();
				try
				{
					process.Start();
				}
				catch{}
			}
			else
			{
				MessageBox.Show("Couldn't find Season gen options file.");
			}
		}

	}
}
