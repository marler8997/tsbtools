using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace PlayProto
{
	/// <summary>
	/// Summary description for OffensePanel.
	/// </summary>
	public class OffensePanel : System.Windows.Forms.UserControl, IPlay
	{
		private System.Windows.Forms.Button mUpdateButton;
		private System.Windows.Forms.ComboBox mFormationComboBox;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.TextBox mPlayNameTextBox;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.GroupBox mSkillGroupBox;
		private System.Windows.Forms.ComboBox mQbComboBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label mQbLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button mCurrentPlayButton;
		private System.ComponentModel.IContainer components;

		private string[][] mPatternNames;
		private string[][] mPatternPointers;
		private System.Windows.Forms.ComboBox mWR2ComboBox;
		private System.Windows.Forms.ComboBox mHBComboBox;
		private System.Windows.Forms.ComboBox mWR1ComboBox;
		private System.Windows.Forms.ComboBox mTEComboBox;
		private System.Windows.Forms.ComboBox mFBComboBox;
		private System.Windows.Forms.ComboBox mLTComboBox;
		private System.Windows.Forms.ComboBox mLGComboBox;
		private System.Windows.Forms.ComboBox mRTComboBox;
		private System.Windows.Forms.ComboBox mRGComboBox;
		private System.Windows.Forms.ComboBox mCComboBox;
		private ComboBox[] mComboBoxes;

		private Hashtable  mPaternHash= null;

		public string mPatternFile = "DataFiles"+Path.DirectorySeparatorChar+"OffensivePatternNames.csv";
		private System.Windows.Forms.ToolTip mToolTip;
		public string mPointerFile = "DataFiles"+Path.DirectorySeparatorChar+"OffensivePointers.csv";

		public string[][] PatternNames
		{
			get{ return mPatternNames; }
		}

		public string[][] PointerNames
		{
			get{ return mPatternPointers; }
		}

		public OffensePanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			mPatternNames    = new string[11][];
			mPatternPointers = new string[11][];

			mComboBoxes = new ComboBox[11];
			mComboBoxes[0]  = mQbComboBox;
			mComboBoxes[1]  = mHBComboBox;
			mComboBoxes[2]  = mFBComboBox;
			mComboBoxes[3]  = mWR1ComboBox;
			mComboBoxes[4]  = mWR2ComboBox;
			mComboBoxes[5]  = mTEComboBox;
			mComboBoxes[6]  = mCComboBox;
			mComboBoxes[7]  = mLGComboBox;
			mComboBoxes[8]  = mRGComboBox;
			mComboBoxes[9]  = mLTComboBox;
			mComboBoxes[10] = mRTComboBox;
		}

		public void Init()
		{
			PopulateData();
		}

		private int m_CurrentPlay = 0;

		public int CurrentPlay
		{
			set 
			{
				m_CurrentPlay = value;
				mCurrentPlayButton.Text = string.Format("{0:x2}", m_CurrentPlay).ToUpper();
				ShowPatterns();
			}
			get{ return m_CurrentPlay;}
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
			this.components = new System.ComponentModel.Container();
			this.mUpdateButton = new System.Windows.Forms.Button();
			this.mFormationComboBox = new System.Windows.Forms.ComboBox();
			this.label26 = new System.Windows.Forms.Label();
			this.mPlayNameTextBox = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.mSkillGroupBox = new System.Windows.Forms.GroupBox();
			this.mWR2ComboBox = new System.Windows.Forms.ComboBox();
			this.mQbComboBox = new System.Windows.Forms.ComboBox();
			this.mHBComboBox = new System.Windows.Forms.ComboBox();
			this.mWR1ComboBox = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.mTEComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.mQbLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.mFBComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.mLTComboBox = new System.Windows.Forms.ComboBox();
			this.mLGComboBox = new System.Windows.Forms.ComboBox();
			this.mRTComboBox = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.mRGComboBox = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.mCComboBox = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.mCurrentPlayButton = new System.Windows.Forms.Button();
			this.mToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.mSkillGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mUpdateButton
			// 
			this.mUpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mUpdateButton.Location = new System.Drawing.Point(360, 368);
			this.mUpdateButton.Name = "mUpdateButton";
			this.mUpdateButton.TabIndex = 40;
			this.mUpdateButton.Text = "Update";
			this.mUpdateButton.Click += new System.EventHandler(this.mUpdateButton_Click);
			// 
			// mFormationComboBox
			// 
			this.mFormationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mFormationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mFormationComboBox.Items.AddRange(new object[] {
																	"04  PRO T",
																	"05 SLOT",
																	"06 ONEBACKSET",
																	"07 2TE ",
																	"08 MOTION D",
																	"09 SHIFTONE",
																	"0A ONEBACK",
																	"0B OFFSET I",
																	"0C R&S SHOOT",
																	"0D R&S 3 WING",
																	"0E SHOTGUN A",
																	"0F SHOTGUN B",
																	"10 SHOT 3 WING",
																	"11 Shotgun C",
																	"12 Redgun",
																	"13 NO BACK",
																	"14 PRO T(B)",
																	"15 ONEBACK B"});
			this.mFormationComboBox.Location = new System.Drawing.Point(8, 373);
			this.mFormationComboBox.Name = "mFormationComboBox";
			this.mFormationComboBox.Size = new System.Drawing.Size(176, 21);
			this.mFormationComboBox.TabIndex = 27;
			// 
			// label26
			// 
			this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label26.Location = new System.Drawing.Point(8, 357);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(100, 15);
			this.label26.TabIndex = 26;
			this.label26.Text = "Formation";
			// 
			// mPlayNameTextBox
			// 
			this.mPlayNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mPlayNameTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.mPlayNameTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mPlayNameTextBox.Location = new System.Drawing.Point(8, 333);
			this.mPlayNameTextBox.MaxLength = 15;
			this.mPlayNameTextBox.Name = "mPlayNameTextBox";
			this.mPlayNameTextBox.Size = new System.Drawing.Size(176, 20);
			this.mPlayNameTextBox.TabIndex = 25;
			this.mPlayNameTextBox.Text = "";
			// 
			// label25
			// 
			this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label25.Location = new System.Drawing.Point(8, 317);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(100, 15);
			this.label25.TabIndex = 24;
			this.label25.Text = "Play Name";
			// 
			// mSkillGroupBox
			// 
			this.mSkillGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mSkillGroupBox.Controls.Add(this.mWR2ComboBox);
			this.mSkillGroupBox.Controls.Add(this.mQbComboBox);
			this.mSkillGroupBox.Controls.Add(this.mHBComboBox);
			this.mSkillGroupBox.Controls.Add(this.mWR1ComboBox);
			this.mSkillGroupBox.Controls.Add(this.label4);
			this.mSkillGroupBox.Controls.Add(this.label5);
			this.mSkillGroupBox.Controls.Add(this.mTEComboBox);
			this.mSkillGroupBox.Controls.Add(this.label3);
			this.mSkillGroupBox.Controls.Add(this.mQbLabel);
			this.mSkillGroupBox.Controls.Add(this.label1);
			this.mSkillGroupBox.Controls.Add(this.mFBComboBox);
			this.mSkillGroupBox.Controls.Add(this.label2);
			this.mSkillGroupBox.Location = new System.Drawing.Point(8, 0);
			this.mSkillGroupBox.Name = "mSkillGroupBox";
			this.mSkillGroupBox.Size = new System.Drawing.Size(432, 160);
			this.mSkillGroupBox.TabIndex = 22;
			this.mSkillGroupBox.TabStop = false;
			this.mSkillGroupBox.Text = "Skill Players";
			// 
			// mWR2ComboBox
			// 
			this.mWR2ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mWR2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mWR2ComboBox.Location = new System.Drawing.Point(64, 136);
			this.mWR2ComboBox.Name = "mWR2ComboBox";
			this.mWR2ComboBox.Size = new System.Drawing.Size(360, 21);
			this.mWR2ComboBox.TabIndex = 12;
			this.mWR2ComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mWR2ComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// mQbComboBox
			// 
			this.mQbComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mQbComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mQbComboBox.Location = new System.Drawing.Point(64, 16);
			this.mQbComboBox.Name = "mQbComboBox";
			this.mQbComboBox.Size = new System.Drawing.Size(360, 21);
			this.mQbComboBox.TabIndex = 2;
			this.mQbComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mQbComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// mHBComboBox
			// 
			this.mHBComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mHBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mHBComboBox.Location = new System.Drawing.Point(64, 40);
			this.mHBComboBox.Name = "mHBComboBox";
			this.mHBComboBox.Size = new System.Drawing.Size(360, 21);
			this.mHBComboBox.TabIndex = 4;
			this.mHBComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mHBComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// mWR1ComboBox
			// 
			this.mWR1ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mWR1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mWR1ComboBox.Location = new System.Drawing.Point(64, 112);
			this.mWR1ComboBox.Name = "mWR1ComboBox";
			this.mWR1ComboBox.Size = new System.Drawing.Size(360, 21);
			this.mWR1ComboBox.TabIndex = 10;
			this.mWR1ComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mWR1ComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "WR1";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 23);
			this.label5.TabIndex = 11;
			this.label5.Text = "WR2";
			// 
			// mTEComboBox
			// 
			this.mTEComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mTEComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mTEComboBox.Location = new System.Drawing.Point(64, 88);
			this.mTEComboBox.Name = "mTEComboBox";
			this.mTEComboBox.Size = new System.Drawing.Size(360, 21);
			this.mTEComboBox.TabIndex = 8;
			this.mTEComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mTEComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 23);
			this.label3.TabIndex = 7;
			this.label3.Text = "TE";
			// 
			// mQbLabel
			// 
			this.mQbLabel.Location = new System.Drawing.Point(8, 16);
			this.mQbLabel.Name = "mQbLabel";
			this.mQbLabel.Size = new System.Drawing.Size(48, 23);
			this.mQbLabel.TabIndex = 1;
			this.mQbLabel.Text = "QB";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "HB";
			// 
			// mFBComboBox
			// 
			this.mFBComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mFBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mFBComboBox.Location = new System.Drawing.Point(64, 64);
			this.mFBComboBox.Name = "mFBComboBox";
			this.mFBComboBox.Size = new System.Drawing.Size(360, 21);
			this.mFBComboBox.TabIndex = 6;
			this.mFBComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mFBComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "FB";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.mLTComboBox);
			this.groupBox1.Controls.Add(this.mLGComboBox);
			this.groupBox1.Controls.Add(this.mRTComboBox);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.mRGComboBox);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.mCComboBox);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Location = new System.Drawing.Point(8, 168);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(432, 144);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Linemen";
			// 
			// mLTComboBox
			// 
			this.mLTComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mLTComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mLTComboBox.Location = new System.Drawing.Point(64, 16);
			this.mLTComboBox.Name = "mLTComboBox";
			this.mLTComboBox.Size = new System.Drawing.Size(360, 21);
			this.mLTComboBox.TabIndex = 2;
			this.mLTComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mLTComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// mLGComboBox
			// 
			this.mLGComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mLGComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mLGComboBox.Location = new System.Drawing.Point(64, 40);
			this.mLGComboBox.Name = "mLGComboBox";
			this.mLGComboBox.Size = new System.Drawing.Size(360, 21);
			this.mLGComboBox.TabIndex = 4;
			this.mLGComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mLGComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// mRTComboBox
			// 
			this.mRTComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mRTComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mRTComboBox.Location = new System.Drawing.Point(64, 112);
			this.mRTComboBox.Name = "mRTComboBox";
			this.mRTComboBox.Size = new System.Drawing.Size(360, 21);
			this.mRTComboBox.TabIndex = 10;
			this.mRTComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mRTComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 23);
			this.label6.TabIndex = 9;
			this.label6.Text = "RT";
			// 
			// mRGComboBox
			// 
			this.mRGComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mRGComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mRGComboBox.Location = new System.Drawing.Point(64, 88);
			this.mRGComboBox.Name = "mRGComboBox";
			this.mRGComboBox.Size = new System.Drawing.Size(360, 21);
			this.mRGComboBox.TabIndex = 8;
			this.mRGComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mRGComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 88);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 23);
			this.label8.TabIndex = 7;
			this.label8.Text = "RG";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 23);
			this.label9.TabIndex = 1;
			this.label9.Text = "LT";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 40);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(48, 23);
			this.label10.TabIndex = 3;
			this.label10.Text = "LG";
			// 
			// mCComboBox
			// 
			this.mCComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mCComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mCComboBox.Location = new System.Drawing.Point(64, 64);
			this.mCComboBox.Name = "mCComboBox";
			this.mCComboBox.Size = new System.Drawing.Size(360, 21);
			this.mCComboBox.TabIndex = 6;
			this.mCComboBox.SelectedIndexChanged += new System.EventHandler(this.mLTComboBox_MouseEnter);
			this.mCComboBox.MouseEnter += new System.EventHandler(this.mLTComboBox_MouseEnter);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 64);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 23);
			this.label11.TabIndex = 5;
			this.label11.Text = "C";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.Location = new System.Drawing.Point(216, 360);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 32);
			this.label7.TabIndex = 30;
			this.label7.Text = "Currently editing play #";
			// 
			// mCurrentPlayButton
			// 
			this.mCurrentPlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mCurrentPlayButton.BackColor = System.Drawing.Color.Green;
			this.mCurrentPlayButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mCurrentPlayButton.ForeColor = System.Drawing.Color.White;
			this.mCurrentPlayButton.Location = new System.Drawing.Point(304, 368);
			this.mCurrentPlayButton.Name = "mCurrentPlayButton";
			this.mCurrentPlayButton.Size = new System.Drawing.Size(40, 24);
			this.mCurrentPlayButton.TabIndex = 30;
			this.mCurrentPlayButton.Click += new System.EventHandler(this.mCurrentPlayButton_Click);
			// 
			// mToolTip
			// 
			this.mToolTip.AutoPopDelay = 10000;
			this.mToolTip.InitialDelay = 500;
			this.mToolTip.ReshowDelay = 100;
			// 
			// OffensePanel
			// 
			this.Controls.Add(this.mCurrentPlayButton);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.mUpdateButton);
			this.Controls.Add(this.mFormationComboBox);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.mPlayNameTextBox);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.mSkillGroupBox);
			this.Controls.Add(this.groupBox1);
			this.Name = "OffensePanel";
			this.Size = new System.Drawing.Size(448, 408);
			this.mSkillGroupBox.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Updates the combox based on the current play.
		/// </summary>
		private void ShowPatterns()
		{
			ComboBox box ;
			int index = -1;
			string tmp;

			if( MainForm.mPlayTool == null )
				return;

			for(int i =0; i < mComboBoxes.Length; i++)
			{
				index = -1;
				box = mComboBoxes[i];
				OPlayer p = (OPlayer)i;
				string pointer = MainForm.mPlayTool.
					GetOPatternLocationRelativeStr(p, CurrentPlay);
				string[] list = mPatternPointers[i];
				for(int j = 0; j < list.Length; j++)
				{
					tmp = list[j];
					if( pointer == tmp  )
					{
						index = j;
						break;
					}
				}
				if(index > -1 )
				{
					box.SelectedIndex = index;
					box.Enabled = true;
				}
				else
				{
					box.Enabled = false;
				}
			}
		}

		private void SavePatterns()
		{
			ComboBox box ;

			for(int i =0; i < mComboBoxes.Length; i++)
			{
				box = mComboBoxes[i];
				if( box.Enabled )
				{
					OPlayer p = (OPlayer)i;
					int index = box.SelectedIndex;
					string pointer = mPatternPointers[i][index];
					MainForm.mPlayTool.SetOPatternLocationRelative(p, CurrentPlay, pointer);
				}
			}
		}

		private void PopulateData()
		{
			ReadData();
			/*
			if( mPatternPointers[0][0] != null )
			{
				for(int i = 0; i < mComboBoxes.Length; i++)
				{
					string[] data = mPatternNames[i];
					ComboBox box = mComboBoxes[i];
					box.BeginUpdate();
					for(int j =0; j < data.Length; j++)
					{
						string str = data[j];
						if( str == null )
							str = "????";
						box.Items.Add(str);
					}
					box.EndUpdate();
					box.SelectedIndex =0;
				}
			}*/
			mPaternHash = new Hashtable();
			// eliminate duplicates in the combo boxes
			for(int k = 0; k < mPatternNames.Length; k++)
			{
				ArrayList list = 
					MainForm.EliminateDupsInArrays(mPatternPointers[k], mPatternNames[k]);
				string[] poi = list[0] as string[];
				string[] nam = list[1] as string[];

				mPatternPointers[k] = poi;
				mPatternNames[k]    = nam;
			}

			for(int i = 0; i < mComboBoxes.Length; i++)
			{
				string[] data = mPatternNames[i];
				ComboBox box = mComboBoxes[i];
				box.BeginUpdate();
				for(int j =0; j < data.Length; j++)
				{
					string str = data[j];
					if( str == null )
						str = "????";
					box.Items.Add(str);
				}
				box.EndUpdate();
				box.SelectedIndex =0;
			}
		}

		private void ReadData()
		{
			string patternData = null;
			string pointerData = null;
			if( System.IO.File.Exists(mPatternFile ) )
			{
				patternData = MainForm.GetFileContents(mPatternFile);
				pointerData = MainForm.GetFileContents(mPointerFile);
			}
			if( patternData != null && pointerData != null)
			{
				patternData = patternData.Replace("\r","");
				pointerData = pointerData.Replace("\r","");
				int lineCount = -1;
				for(int i = 0; i < pointerData.Length; i++)
				{
					if( pointerData[i] == '\n' )
						lineCount++;
				}
				for(int i = 0; i < mPatternNames.Length; i++)
				{
					mPatternNames[i]    = new string[lineCount];
					mPatternPointers[i] = new string[lineCount];
				}
				try
				{
					MainForm.FillArray(patternData, mPatternNames,   1,1);
				}
				catch(Exception e)
				{
					MessageBox.Show( string.Format(
                        "ERROR Processing File '{0}'.\n{1}\n{2}",
						mPatternFile,e.Message,e.StackTrace ));
				}
				try
				{
					MainForm.FillArray(pointerData, mPatternPointers,1,1);
				}
				catch(Exception e)
				{
					MessageBox.Show( string.Format(
						"ERROR Processing File '{0}'.\n{1}\n{2}",
						mPointerFile,e.Message,e.StackTrace ));
				}
			}
		}



		public void ShowContents()
		{
			int playIndex = MainForm.TheMainForm.PlayIndex;
			mPlayNameTextBox.Text = MainForm.TheMainForm.ThePlayTool.GetPlayName(playIndex);

			// set the formation combo box
			byte form = MainForm.TheMainForm.ThePlayTool.GetPlayFormation(playIndex);
			string formation = String.Format("{0:x2}", form);
			string item ="";
			for( int i = 0; i < mFormationComboBox.Items.Count; i++)
			{
				item = mFormationComboBox.Items[i] as String;
				if(item != null && item.StartsWith(formation) )
				{
					mFormationComboBox.SelectedIndex = i;
				}
			}
			byte[] vals = MainForm.TheMainForm.ThePlayTool.GetOffensivePlayToCall(playIndex);
			CurrentPlay = vals[0];
			ShowPatterns();	

		}

		private void mUpdateButton_Click(object sender, System.EventArgs e)
		{
			int playIndex = MainForm.TheMainForm.PlayIndex;
			MainForm.TheMainForm.ThePlayTool.SetPalyName(playIndex, mPlayNameTextBox.Text );

			string val = mFormationComboBox.SelectedItem.ToString();
			val = val.Substring(0,2);
			byte b = byte.Parse(val,System.Globalization.NumberStyles.AllowHexSpecifier);
			MainForm.TheMainForm.ThePlayTool.SetPlayFormation(playIndex, b);
			SavePatterns();
		}
		#region IPlay Members

		public void OnPlayChanged(int playIndex)
		{
			ShowContents();
		}

		public string GetXML()
		{
			String ret = "";
			int playIndex = MainForm.TheMainForm.PlayIndex;

			StringBuilder sb = new StringBuilder(2000);
			sb.Append("    PlayName='");
			sb.Append(mPlayNameTextBox.Text);
			sb.Append("'\r\n");
			sb.Append("    Formation='");
			sb.Append(mFormationComboBox.Text);
			sb.Append("'\r\n");
			// get all real plays associated with this play
			// and list their pointers.

			/*sb.Append("    Patterns='\r\n");
			Pattern p = new Pattern();
			string[] patterns = p.GetOffensivePatterns(playIndex);
			for( int i = 0; i < patterns.Length; i++)
			{
				sb.Append("        ");
				sb.Append(patterns[i]);
				if( i == patterns.Length-1 )
					sb.Append("'");
				else
					sb.Append("\r\n");
			}
			sb.Append("\r\n");*/
			ret = sb.ToString();

			return ret;
		}

		public void IPlayTabIndexChanged(int index)
		{
			mToolTip.RemoveAll();
		}

		#endregion

		private void UpdateToolTip( ComboBox box )
		{
			if( box != null )
			{
				//mToolTip.RemoveAll();
				string tip = box.Text;
				//mPatternPointers[k] = poi;
				//mPatternNames[k]    = nam;
				string text = box.SelectedItem.ToString();
				string ptr = "";
				string msg ="";
				string currentPlays = "";
				int len = mComboBoxes.Length;
				int boxLoc = 0;

				for( boxLoc = 0; boxLoc < len; boxLoc++)
				{
					if( mComboBoxes[boxLoc] == box )
						break;
				}

				int index = box.SelectedIndex;
				if( index < mPatternPointers[boxLoc].Length)
				{
					ptr = mPatternPointers[boxLoc][index];
					msg = mPatternNames[boxLoc][index].Replace("//","\n    //");
					currentPlays = MainForm.mPlayTool.GetPlaysPointerAppearsIn_str(ptr, true);
				}
				
				string plays ="";
				try
				{
					int pointer = Int32.Parse(ptr,System.Globalization.NumberStyles.AllowHexSpecifier);
					plays = GetPlayNamesForPointer( pointer );
				}
				catch
				{
				}
				// more here
				
				tip = string.Format(
"[{0}]  \n'{1}'\nPattern used in the following plays(Original TSB):\n{2}\nCurrently used in plays({3})",
					ptr, msg, plays, currentPlays);
				mToolTip.SetToolTip(box, tip);
			}
		}

		private Hashtable mPlayNameHash = null;

		private string GetPlayNamesForPointer( int pointer )
		{
			string ret = null;
			if( mPlayNameHash == null )
				mPlayNameHash = new Hashtable(600);

			if( mPlayNameHash.ContainsKey( pointer ) )
			{
				ret = mPlayNameHash[pointer] as string;
			}
			else
			{
				int current = 0;
				StringBuilder sb = new StringBuilder(400);
				ArrayList list =  UtilityClass.OffensivePlaysPointerIsIn(pointer);
				for(int i=0; i < list.Count; i++ )
				{
					current = (int)list[i];
					sb.Append(string.Format("({0:x2})    ",current).ToUpper());
					sb.Append( UtilityClass.GetPlayNameByPlayNumber( current ));
					sb.Append("\n");
				}
				ret = sb.ToString();
				mPlayNameHash.Add(pointer, ret);
			}
			return ret;
		}

		private void mCurrentPlayButton_Click(object sender, System.EventArgs e)
		{
			byte[] vals = MainForm.TheMainForm.ThePlayTool.GetOffensivePlayToCall(MainForm.TheMainForm.PlayIndex);
			string[] str_vals = new string[vals.Length];
			for(int i = 0; i < vals.Length; i++)
			{
				string s = string.Format("{0:x2}", vals[i]).ToUpper();
				str_vals[i] = s;
			}
			ButtonForm bf = new ButtonForm();
			bf.Title = "Choose Offensive Play number";
			bf.Info  = "Press a button below to choose a play to modify.";
			bf.ButtonValues = str_vals;
			if( bf.ShowDialog(this) == DialogResult.OK )
			{
				try
				{
					int g = Int32.Parse( bf.Result, System.Globalization.NumberStyles.AllowHexSpecifier);
					CurrentPlay = g;
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
			bf.Dispose();
		}

		private void mLTComboBox_MouseEnter(object sender, System.EventArgs e)
		{
			ComboBox box = sender as ComboBox;
			if( box != null )
				UpdateToolTip(box);
		}

	}
}
