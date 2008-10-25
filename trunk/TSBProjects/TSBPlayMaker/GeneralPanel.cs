using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Text;


namespace PlayProto
{
	/// <summary>
	/// Summary description for GeneralPanel.
	/// </summary>
	public class GeneralPanel : System.Windows.Forms.UserControl, IPlay
	{
		private System.Windows.Forms.TextBox mDefensiveReactiontextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox mOffensivePlayTextBox;
		private System.Windows.Forms.Button mUpdateButton;
		private System.Windows.Forms.Button m_PlaybookCopyButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button m_PlaybookCopyFromButton;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GeneralPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		public void Init()
		{
		}

		/// <summary>
		/// Gets the text in mOffensivePlayTextBox.
		/// </summary>
		public string OffensivePlayIndexString
		{
			get { return mOffensivePlayTextBox.Text; }
		}

		/// <summary>
		/// gets the text in mDefensiveReactiontextBox.
		/// </summary>
		public string DefensiveReactionIndexString
		{
			get { return mDefensiveReactiontextBox.Text; }
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
			this.mDefensiveReactiontextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.mOffensivePlayTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.mUpdateButton = new System.Windows.Forms.Button();
			this.m_PlaybookCopyButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.m_PlaybookCopyFromButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// mDefensiveReactiontextBox
			// 
			this.mDefensiveReactiontextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.mDefensiveReactiontextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mDefensiveReactiontextBox.Location = new System.Drawing.Point(136, 32);
			this.mDefensiveReactiontextBox.MaxLength = 30;
			this.mDefensiveReactiontextBox.Name = "mDefensiveReactiontextBox";
			this.mDefensiveReactiontextBox.Size = new System.Drawing.Size(232, 20);
			this.mDefensiveReactiontextBox.TabIndex = 20;
			this.mDefensiveReactiontextBox.Text = "00, 00, 00, 00, 00, 00, 00, 00";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 19;
			this.label2.Text = "Defensive Reactions";
			// 
			// mOffensivePlayTextBox
			// 
			this.mOffensivePlayTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.mOffensivePlayTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mOffensivePlayTextBox.Location = new System.Drawing.Point(136, 8);
			this.mOffensivePlayTextBox.MaxLength = 30;
			this.mOffensivePlayTextBox.Name = "mOffensivePlayTextBox";
			this.mOffensivePlayTextBox.Size = new System.Drawing.Size(232, 20);
			this.mOffensivePlayTextBox.TabIndex = 10;
			this.mOffensivePlayTextBox.Text = "00, 00, 00, 00, 00, 00, 00, 00";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 21;
			this.label1.Text = "Offensive Play";
			// 
			// mUpdateButton
			// 
			this.mUpdateButton.Location = new System.Drawing.Point(344, 368);
			this.mUpdateButton.Name = "mUpdateButton";
			this.mUpdateButton.TabIndex = 30;
			this.mUpdateButton.Text = "Update";
			this.mUpdateButton.Click += new System.EventHandler(this.mUpdateButton_Click);
			// 
			// m_PlaybookCopyButton
			// 
			this.m_PlaybookCopyButton.Location = new System.Drawing.Point(8, 168);
			this.m_PlaybookCopyButton.Name = "m_PlaybookCopyButton";
			this.m_PlaybookCopyButton.Size = new System.Drawing.Size(144, 23);
			this.m_PlaybookCopyButton.TabIndex = 25;
			this.m_PlaybookCopyButton.Text = "Copy playbook To ...";
			this.m_PlaybookCopyButton.Click += new System.EventHandler(this.m_PlaybookCopyButton_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(176, 32);
			this.label4.TabIndex = 26;
			this.label4.Text = "Copy the playbook from the current Rom to Another Rom";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 272);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(176, 32);
			this.label5.TabIndex = 28;
			this.label5.Text = "Copy the playbook from another Rom into the current Rom";
			// 
			// m_PlaybookCopyFromButton
			// 
			this.m_PlaybookCopyFromButton.Location = new System.Drawing.Point(8, 312);
			this.m_PlaybookCopyFromButton.Name = "m_PlaybookCopyFromButton";
			this.m_PlaybookCopyFromButton.Size = new System.Drawing.Size(144, 23);
			this.m_PlaybookCopyFromButton.TabIndex = 27;
			this.m_PlaybookCopyFromButton.Text = "Copy playbook From ...";
			this.m_PlaybookCopyFromButton.Click += new System.EventHandler(this.m_PlaybookCopyFromButton_Click);
			// 
			// GeneralPanel
			// 
			this.Controls.Add(this.label5);
			this.Controls.Add(this.m_PlaybookCopyFromButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.m_PlaybookCopyButton);
			this.Controls.Add(this.mUpdateButton);
			this.Controls.Add(this.mOffensivePlayTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.mDefensiveReactiontextBox);
			this.Controls.Add(this.label2);
			this.Name = "GeneralPanel";
			this.Size = new System.Drawing.Size(432, 408);
			this.ResumeLayout(false);

		}
		#endregion
	

		public void ShowDefensiveReactions()
		{
			int playIndex = MainForm.TheMainForm.PlayIndex;
			mDefensiveReactiontextBox.Text = MainForm.TheMainForm.
				ThePlayTool.GetDefensiveReactionText(playIndex);
		}

		public void ShowOffensivePlayToCall()
		{
			int playIndex = MainForm.TheMainForm.PlayIndex;
			mOffensivePlayTextBox.Text = MainForm.TheMainForm.
				ThePlayTool.GetPlayToCallText(playIndex);
		}

		private void mUpdateButton_Click(object sender, System.EventArgs e)
		{
			int playIndex = MainForm.TheMainForm.PlayIndex;
			MainForm.TheMainForm.ThePlayTool.
				SetDefensivereactionsFromText(mDefensiveReactiontextBox.Text,playIndex);
			MainForm.TheMainForm.ThePlayTool.
				SetOffensivePlayToCallFromText(mOffensivePlayTextBox.Text,playIndex);
		}
		#region IPlay Members

		public void OnPlayChanged(int playIndex)
		{
			ShowDefensiveReactions();
			ShowOffensivePlayToCall();
		}

		public string GetXML()
		{
			string ret = "";
			StringBuilder sb = new StringBuilder(300);
			sb.Append("    OffensivePlay='");
			sb.Append(mOffensivePlayTextBox.Text);
			sb.Append("'\r\n");
			sb.Append("    DefensiveReactions='");
			sb.Append(mDefensiveReactiontextBox.Text );
			sb.Append("'\r\n");
			ret = sb.ToString();
			return ret;
		}

		public void IPlayTabIndexChanged(int index)
		{
		}

		#endregion

		private void m_PlaybookCopyButton_Click(object sender, System.EventArgs e)
		{
			string fileName = MainForm.GetFileName(true, MainForm.NES_FILTER);
			if( fileName != null )
			{
				byte[] rom = MainForm.ReadRom(fileName);
				// copy the play stuff.
//				Array.Copy(MainForm.TheROM, 0x4010,  rom, 0x4010,  0xF80);
//				Array.Copy(MainForm.TheROM, 0x6010,  rom, 0x6010,  0x1780);
//				Array.Copy(MainForm.TheROM, 0x8010,  rom, 0x8010,  0x4000);
//				Array.Copy(MainForm.TheROM, 0x1d410, rom, 0x1d410, 0xb00);
//				Array.Copy(MainForm.TheROM, 0x27546, rom, 0x27546, 0xa8a);
				CopyPlayBook(MainForm.TheROM,rom);
				// Save the changes.
				MainForm.SaveRom(rom, fileName);
				MessageBox.Show(this,"Done!", 
					String.Format("Done Saving Playbook to {0}.",fileName), 
					MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}

		private void m_PlaybookCopyFromButton_Click(object sender, System.EventArgs e)
		{
			string fileName = MainForm.GetFileName(true, MainForm.NES_FILTER);
			if( fileName != null )
			{
				byte[] rom = MainForm.ReadRom(fileName);
				// copy the play stuff.
//				Array.Copy(rom, 0x4010,  MainForm.TheROM, 0x4010,  0xF80);
//				Array.Copy(rom, 0x6010,  MainForm.TheROM, 0x6010,  0x1780);
//				Array.Copy(rom, 0x8010,  MainForm.TheROM, 0x8010,  0x4000);
//				Array.Copy(rom, 0x1d410, MainForm.TheROM, 0x1d410, 0xb00);
//				Array.Copy(rom, 0x27546, MainForm.TheROM, 0x27546, 0xa8a);
				CopyPlayBook(rom, MainForm.TheROM);
				MainForm.TheMainForm.SendUpdate();
				//MessageBox.Show(this, "Done.");
				MessageBox.Show(this,"Done!", 
							"Done importing Playbook to current ROM.", 
							MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}

		private void CopyPlayBook(byte[] fromRom, byte[] toRom)
		{
			//Array.Copy(fromRom, PlayTool.FORMATION_POINTER_STARTING_INDEX,  toRom, PlayTool.FORMATION_POINTER_STARTING_INDEX,  0xF80);
			Array.Copy(fromRom, 0x4010,  toRom, 0x4010,  0x1f2);
			Array.Copy(fromRom, 0x4330,  toRom, 0x4330,  0xd51);
			Array.Copy(fromRom, PlayTool.DEFENSIVE_REACTION_POINTERS,  toRom, PlayTool.DEFENSIVE_REACTION_POINTERS,  0x1780);
			Array.Copy(fromRom, 0x8010,  toRom, 0x8010,  0x4000);
			Array.Copy(fromRom, PlayTool.PLAY_NAME_STARTING_INDEX, toRom, PlayTool.PLAY_NAME_STARTING_INDEX, 0xb00);
			Array.Copy(fromRom, TileControl.GRAPHIC_START, toRom, TileControl.GRAPHIC_START, 0xa8a);

		}
	}
}
