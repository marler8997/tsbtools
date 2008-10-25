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
	/* ****
	 * Ideas.
	 * 1. You always have 8 defensive plays. 
	 *    One of those plays is a super blitz (Like when your opponent calls your play) 
	 *      should be dangerous to call
	 *    One of those plays is man to man ( with randomness built in so it's different each time)
	 *    
	 * 
	 * 
	 * Feature Ideas:
	 * Find patterns used exactly 'x' times,    // easy to do, all we have to do 
	 *  Find patterns used less than 'x' times, // is count the times the pointer
	 *  Find patterns used more than 'x' times. // occurs in the defensive plays.
	 * 
	 * 
	 * Questions:
	 * Can I jump into the 'DD20' range? (defensive play code starts at A010 )
	 * 
	 **** */
	/// <summary>
	/// Summary description for DefensePanel.
	/// RILB - m2m-TE = D0 E0 18 Ec 05 00 fe fe
	/// </summary>
	public class DefensePanel : System.Windows.Forms.UserControl,IPlay
	{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox m_NTComboBox;
		private System.Windows.Forms.ComboBox m_REComboBox;
		private System.Windows.Forms.ComboBox m_LEComboBox;
		private System.Windows.Forms.ComboBox m_ROLBComboBox;
		private System.Windows.Forms.ComboBox m_LILBComboBox;
		private System.Windows.Forms.ComboBox m_RILBComboBox;
		private System.Windows.Forms.ComboBox m_LOLBComboBox;
		private System.Windows.Forms.ComboBox m_RCBComboBox;
		private System.Windows.Forms.ComboBox m_FSComboBox;
		private System.Windows.Forms.ComboBox m_SSComboBox;
		private System.Windows.Forms.ComboBox m_LCBComboBox;

		private ComboBox[] m_ComboBoxes = null;
		private string[][] m_PatternNames = null;
		private string[][] m_PatternPointers = null;

		private string m_DefensivePointerFile = "DataFiles"+Path.DirectorySeparatorChar+"DefensivePointers.csv";
		private string m_DefensivePatternFile = "DataFiles"+Path.DirectorySeparatorChar+"DefensivePatterns.csv";

		private System.Windows.Forms.Button m_UpdateButton;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.ComponentModel.IContainer components;

		private System.Windows.Forms.Button m_CurrentDefensiveReactionButton;

		private int m_CurrentDefensiveReaction = -1;
		public int CurrentDefensiveReaction
		{
			get{ return m_CurrentDefensiveReaction; }
			set
			{
				m_CurrentDefensiveReaction = value;
				m_CurrentDefensiveReactionButton.Text =
					string.Format("{0:x2}",m_CurrentDefensiveReaction).ToUpper();
				ShowPatterns();
			}
		}

		public DefensePanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			m_ComboBoxes= new ComboBox[11];

			m_ComboBoxes[0] = m_REComboBox;
			m_ComboBoxes[1] = m_NTComboBox;
			m_ComboBoxes[2] = m_LEComboBox;
			m_ComboBoxes[3] = m_ROLBComboBox;
			m_ComboBoxes[4] = m_RILBComboBox;
			m_ComboBoxes[5] = m_LILBComboBox;
			m_ComboBoxes[6] = m_LOLBComboBox;
			m_ComboBoxes[7] = m_RCBComboBox;
			m_ComboBoxes[8] = m_LCBComboBox;
			m_ComboBoxes[9] = m_FSComboBox;
			m_ComboBoxes[10]= m_SSComboBox;
		}

		public void Init()
		{
			PopulateData();
		}

		private void PopulateData()
		{
			m_PatternNames    = new string[11][];
			m_PatternPointers = new string[11][];
			int count = 0;
			string pointerData = null;
			string patternData = null;
			char[] seps = {'\n'};

			if( File.Exists( m_DefensivePointerFile ) && File.Exists( m_DefensivePatternFile ))
			{
				pointerData = MainForm.GetFileContents(m_DefensivePointerFile);
				pointerData = pointerData.Trim().Replace("\r","");
				count = pointerData.Split(seps).Length -1;//  subtract 1 because of headder row

				patternData = MainForm.GetFileContents(m_DefensivePatternFile);
				patternData = patternData.Trim().Replace("\r","");
			
				for(int i = 0; i < m_PatternNames.Length; i++)
				{
					m_PatternNames[i]    = new string[count];
					m_PatternPointers[i] = new string[count];
				}

				try
				{
					MainForm.FillArray(patternData, m_PatternNames,   1,1);
				}
				catch(Exception e)
				{
					MessageBox.Show( string.Format(
						"ERROR Processing File '{0}'.\n{1}\n{2}",
						m_DefensivePatternFile,e.Message,e.StackTrace ));
				}
				try
				{
					MainForm.FillArray(pointerData, m_PatternPointers,1,1);
				}
				catch(Exception e)
				{
					MessageBox.Show( string.Format(
						"ERROR Processing File '{0}'.\n{1}\n{2}",
						m_DefensivePointerFile,e.Message,e.StackTrace ));
				}

				for(int k = 0; k < m_PatternNames.Length; k++)
				{
					ArrayList list = 
						MainForm.EliminateDupsInArrays(m_PatternPointers[k], m_PatternNames[k]);
					string[] poi = list[0] as string[];
					string[] nam = list[1] as string[];

					m_PatternPointers[k] = poi;
					m_PatternNames[k]    = nam;
				}

				for(int i = 0; i < m_ComboBoxes.Length; i++)
				{
					string[] data = m_PatternNames[i];
					ComboBox box = m_ComboBoxes[i];
					box.BeginUpdate();
					string str = null;
					for(int j =0; j < data.Length; j++)
					{
						str = data[j];
						if( str == null )
							str = "????";
						box.Items.Add(str);
					}
					box.EndUpdate();
					box.SelectedIndex =0;
				}
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.m_NTComboBox = new System.Windows.Forms.ComboBox();
			this.m_REComboBox = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.m_LEComboBox = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.m_ROLBComboBox = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.m_LILBComboBox = new System.Windows.Forms.ComboBox();
			this.m_RILBComboBox = new System.Windows.Forms.ComboBox();
			this.label15 = new System.Windows.Forms.Label();
			this.m_LOLBComboBox = new System.Windows.Forms.ComboBox();
			this.label16 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.m_RCBComboBox = new System.Windows.Forms.ComboBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.m_FSComboBox = new System.Windows.Forms.ComboBox();
			this.m_SSComboBox = new System.Windows.Forms.ComboBox();
			this.label20 = new System.Windows.Forms.Label();
			this.m_LCBComboBox = new System.Windows.Forms.ComboBox();
			this.label21 = new System.Windows.Forms.Label();
			this.m_CurrentDefensiveReactionButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.m_UpdateButton = new System.Windows.Forms.Button();
			this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.m_NTComboBox);
			this.groupBox2.Controls.Add(this.m_REComboBox);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.m_LEComboBox);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(448, 100);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Defensive Line";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(16, 48);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(48, 23);
			this.label12.TabIndex = 5;
			this.label12.Text = "NT";
			// 
			// m_NTComboBox
			// 
			this.m_NTComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_NTComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_NTComboBox.Location = new System.Drawing.Point(72, 48);
			this.m_NTComboBox.Name = "m_NTComboBox";
			this.m_NTComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_NTComboBox.TabIndex = 6;
			this.m_NTComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_NTComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// m_REComboBox
			// 
			this.m_REComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_REComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_REComboBox.Location = new System.Drawing.Point(72, 72);
			this.m_REComboBox.Name = "m_REComboBox";
			this.m_REComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_REComboBox.TabIndex = 8;
			this.m_REComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_REComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(16, 72);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(48, 23);
			this.label13.TabIndex = 7;
			this.label13.Text = "RE";
			// 
			// m_LEComboBox
			// 
			this.m_LEComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_LEComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_LEComboBox.Location = new System.Drawing.Point(72, 24);
			this.m_LEComboBox.Name = "m_LEComboBox";
			this.m_LEComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_LEComboBox.TabIndex = 4;
			this.m_LEComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_LEComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 24);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 23);
			this.label7.TabIndex = 3;
			this.label7.Text = "LE";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.m_ROLBComboBox);
			this.groupBox3.Controls.Add(this.label17);
			this.groupBox3.Controls.Add(this.label14);
			this.groupBox3.Controls.Add(this.m_LILBComboBox);
			this.groupBox3.Controls.Add(this.m_RILBComboBox);
			this.groupBox3.Controls.Add(this.label15);
			this.groupBox3.Controls.Add(this.m_LOLBComboBox);
			this.groupBox3.Controls.Add(this.label16);
			this.groupBox3.Location = new System.Drawing.Point(8, 112);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(448, 128);
			this.groupBox3.TabIndex = 14;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Linebackers";
			// 
			// m_ROLBComboBox
			// 
			this.m_ROLBComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_ROLBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_ROLBComboBox.Location = new System.Drawing.Point(72, 96);
			this.m_ROLBComboBox.Name = "m_ROLBComboBox";
			this.m_ROLBComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_ROLBComboBox.TabIndex = 10;
			this.m_ROLBComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_ROLBComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(16, 96);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(48, 23);
			this.label17.TabIndex = 9;
			this.label17.Text = "ROLB";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(16, 48);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(48, 23);
			this.label14.TabIndex = 5;
			this.label14.Text = "LILB";
			// 
			// m_LILBComboBox
			// 
			this.m_LILBComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_LILBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_LILBComboBox.Location = new System.Drawing.Point(72, 48);
			this.m_LILBComboBox.Name = "m_LILBComboBox";
			this.m_LILBComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_LILBComboBox.TabIndex = 6;
			this.m_LILBComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_LILBComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// m_RILBComboBox
			// 
			this.m_RILBComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_RILBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_RILBComboBox.Location = new System.Drawing.Point(72, 72);
			this.m_RILBComboBox.Name = "m_RILBComboBox";
			this.m_RILBComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_RILBComboBox.TabIndex = 8;
			this.m_RILBComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_RILBComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(16, 72);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(48, 23);
			this.label15.TabIndex = 7;
			this.label15.Text = "RILB";
			// 
			// m_LOLBComboBox
			// 
			this.m_LOLBComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_LOLBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_LOLBComboBox.Location = new System.Drawing.Point(72, 24);
			this.m_LOLBComboBox.Name = "m_LOLBComboBox";
			this.m_LOLBComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_LOLBComboBox.TabIndex = 4;
			this.m_LOLBComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_LOLBComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(16, 24);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(48, 23);
			this.label16.TabIndex = 3;
			this.label16.Text = "LOLB";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.m_RCBComboBox);
			this.groupBox4.Controls.Add(this.label18);
			this.groupBox4.Controls.Add(this.label19);
			this.groupBox4.Controls.Add(this.m_FSComboBox);
			this.groupBox4.Controls.Add(this.m_SSComboBox);
			this.groupBox4.Controls.Add(this.label20);
			this.groupBox4.Controls.Add(this.m_LCBComboBox);
			this.groupBox4.Controls.Add(this.label21);
			this.groupBox4.Location = new System.Drawing.Point(8, 240);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(448, 128);
			this.groupBox4.TabIndex = 15;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Defensive Backs";
			// 
			// m_RCBComboBox
			// 
			this.m_RCBComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_RCBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_RCBComboBox.Location = new System.Drawing.Point(72, 96);
			this.m_RCBComboBox.Name = "m_RCBComboBox";
			this.m_RCBComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_RCBComboBox.TabIndex = 10;
			this.m_RCBComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_RCBComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(16, 96);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(40, 23);
			this.label18.TabIndex = 9;
			this.label18.Text = "RCB";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(16, 48);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(40, 23);
			this.label19.TabIndex = 5;
			this.label19.Text = "FS";
			// 
			// m_FSComboBox
			// 
			this.m_FSComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_FSComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_FSComboBox.Location = new System.Drawing.Point(72, 48);
			this.m_FSComboBox.Name = "m_FSComboBox";
			this.m_FSComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_FSComboBox.TabIndex = 6;
			this.m_FSComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_FSComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// m_SSComboBox
			// 
			this.m_SSComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_SSComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_SSComboBox.Location = new System.Drawing.Point(72, 72);
			this.m_SSComboBox.Name = "m_SSComboBox";
			this.m_SSComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_SSComboBox.TabIndex = 8;
			this.m_SSComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_SSComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(16, 72);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(40, 23);
			this.label20.TabIndex = 7;
			this.label20.Text = "SS";
			// 
			// m_LCBComboBox
			// 
			this.m_LCBComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_LCBComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_LCBComboBox.Location = new System.Drawing.Point(72, 24);
			this.m_LCBComboBox.Name = "m_LCBComboBox";
			this.m_LCBComboBox.Size = new System.Drawing.Size(368, 21);
			this.m_LCBComboBox.TabIndex = 4;
			this.m_LCBComboBox.SelectedIndexChanged += new System.EventHandler(this.m_SSComboBox_SelectedIndexChanged);
			this.m_LCBComboBox.MouseEnter += new System.EventHandler(this.m_SSComboBox_MouseEnter);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(16, 24);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(40, 23);
			this.label21.TabIndex = 3;
			this.label21.Text = "LCB";
			// 
			// m_CurrentDefensiveReactionButton
			// 
			this.m_CurrentDefensiveReactionButton.BackColor = System.Drawing.Color.Green;
			this.m_CurrentDefensiveReactionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_CurrentDefensiveReactionButton.ForeColor = System.Drawing.Color.White;
			this.m_CurrentDefensiveReactionButton.Location = new System.Drawing.Point(296, 376);
			this.m_CurrentDefensiveReactionButton.Name = "m_CurrentDefensiveReactionButton";
			this.m_CurrentDefensiveReactionButton.Size = new System.Drawing.Size(40, 24);
			this.m_CurrentDefensiveReactionButton.TabIndex = 31;
			this.m_CurrentDefensiveReactionButton.Text = "5B";
			this.m_CurrentDefensiveReactionButton.Click += new System.EventHandler(this.mCurrentDefensiveReactionButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(176, 368);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 32);
			this.label1.TabIndex = 32;
			this.label1.Text = "Currently editing defensive reaction #";
			// 
			// m_UpdateButton
			// 
			this.m_UpdateButton.Location = new System.Drawing.Point(352, 376);
			this.m_UpdateButton.Name = "m_UpdateButton";
			this.m_UpdateButton.TabIndex = 33;
			this.m_UpdateButton.Text = "Update";
			this.m_UpdateButton.Click += new System.EventHandler(this.m_UpdateButton_Click);
			// 
			// m_ToolTip
			// 
			this.m_ToolTip.AutoPopDelay = 10000;
			this.m_ToolTip.InitialDelay = 500;
			this.m_ToolTip.ReshowDelay = 100;
			// 
			// DefensePanel
			// 
			this.Controls.Add(this.m_UpdateButton);
			this.Controls.Add(this.m_CurrentDefensiveReactionButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox4);
			this.Name = "DefensePanel";
			this.Size = new System.Drawing.Size(464, 408);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region IPlay Members

		public void OnPlayChanged(int playIndex)
		{
			ShowContents();
		}

		public void ShowContents()
		{
			int playIndex = MainForm.TheMainForm.PlayIndex;
			byte[] vals = MainForm.TheMainForm.ThePlayTool.GetDefensiveReactions(playIndex);
			CurrentDefensiveReaction = vals[0];
			ShowPatterns();	
		}

		public string GetXML()
		{
			return "";
		}

		public void IPlayTabIndexChanged(int index)
		{
			m_ToolTip.RemoveAll();
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

			for(int i =0; i < m_ComboBoxes.Length; i++)
			{
				index = -1;
				box = m_ComboBoxes[i];
				DPlayer p = (DPlayer)i;
				string pointer = MainForm.mPlayTool.
					GetDPatternPointerRelativeStr(p,CurrentDefensiveReaction).ToUpper();
				string[] list = m_PatternPointers[i];
				
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
					box.ForeColor = Color.Black;
				}
				else
				{
					box.ForeColor = Color.Red;
				}
			}
		}

		private void mCurrentDefensiveReactionButton_Click(object sender, System.EventArgs e)
		{
			byte[] vals = MainForm.TheMainForm.ThePlayTool.GetDefensiveReactions(MainForm.TheMainForm.PlayIndex);
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
					CurrentDefensiveReaction = g;
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
			bf.Dispose();
		}

		private void SavePatterns()
		{
			ComboBox box ;

			for(int i =0; i < m_ComboBoxes.Length; i++)
			{
				box = m_ComboBoxes[i];
				if( box.Enabled )
				{
					DPlayer p = (DPlayer)i;
					int index = box.SelectedIndex;
					string pointer = m_PatternPointers[i][index];
					MainForm.mPlayTool.SetDPatternPointerRelative(p, CurrentDefensiveReaction, pointer);
				}
			}
		}

		private void UpdateToolTip( ComboBox box, string tip)
		{
			if( box != null )
			{
				//if(m_ToolTip.
				//m_ToolTip.RemoveAll();
				if( tip == null)
					tip = box.Text;
				//m_ToolTip.SetToolTip(box, tip);
				UpdateToolTip(box);
			}
		}
		private void UpdateToolTip( ComboBox box )
		{
			if( box != null )
			{
				//mToolTip.RemoveAll();
				string tip = box.Text;
				//mPatternPointers[k] = poi;
				//mPatternNames[k]    = nam;
				string ptr = "";
				string msg ="";
				string currentPlays = "";
				int len = m_ComboBoxes.Length;
				int boxLoc = 0;

				for( boxLoc = 0; boxLoc < len; boxLoc++)
				{
					if( m_ComboBoxes[boxLoc] == box )
						break;
				}

				if( m_PatternNames != null && 
					boxLoc > -1            && 
					boxLoc < m_ComboBoxes.Length )
				{
					int index = box.SelectedIndex;
					if( index < m_PatternPointers[boxLoc].Length)
					{
						ptr = m_PatternPointers[boxLoc][index];
						msg = m_PatternNames[boxLoc][index].Replace("//","\n    //");
					}
				}
				string plays ="";
				try
				{
					int pointer = Int32.Parse(ptr,System.Globalization.NumberStyles.AllowHexSpecifier);
					currentPlays = MainForm.mPlayTool.GetPlaysPointerAppearsIn_str(ptr, false);
					plays = GetPlayNamesForPointer( pointer );
				}
				catch
				{
				}
				// more here
				tip = string.Format(
   "[{0}]  \n'{1}'\nPattern used in the following plays (Original TSB):\n{2}\nCurrently used in({3})",
					ptr, msg, plays, currentPlays);
				m_ToolTip.SetToolTip(box, tip);
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
				int current  = 0;
				StringBuilder sb = new StringBuilder(400);
				ArrayList list =  UtilityClass.DefensivePlaysPointerIsIn(pointer);
				int count = list.Count;
				for(int i=0; i < count; i++ )
				{
					if( count > 20 )
					{
						current  = (int)list[i];
						sb.Append(string.Format("({0:x2}",current));
						if( i+1 < count )
						{
							current = (int)list[++i];
							sb.Append(string.Format(", {0:x2}",current));
						}
						sb.Append(")    \n");
					}
					else
					{
						current = (int)list[i];
						sb.Append(string.Format("({0:x2})    \n",current));
					}
				}
				ret = sb.ToString();
				mPlayNameHash.Add(pointer, ret);
			}
			return ret;
		}


		private void m_SSComboBox_MouseEnter(object sender, System.EventArgs e)
		{
			ComboBox box = sender as ComboBox;
			if( box != null )
			{
				int i=0;
				for( i =0; i < m_ComboBoxes.Length; i++)
				{
					if( m_ComboBoxes[i] == box )
						break;
				}
				string address ="";
				if( i < m_PatternPointers.Length && 
					box.SelectedIndex < m_PatternPointers[i].Length )
				{
					address = m_PatternPointers[i][ box.SelectedIndex ];
				}
				string tip = string.Format("[{0}]  {1}",address,box.Text);
				UpdateToolTip(box, tip);
			}
		}

		private void m_SSComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_SSComboBox_MouseEnter(sender,e);
//			ComboBox box = sender as ComboBox;
//			if( box != null )
//				m_ToolTip.SetToolTip(box, box.SelectedText);
		}

		private void m_UpdateButton_Click(object sender, System.EventArgs e)
		{
			SavePatterns();
		}
	}
}
