namespace TSBTool
{
    partial class WeekScheduler
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mScheduleListBox = new System.Windows.Forms.ListBox();
            this.mRemoveButton = new System.Windows.Forms.Button();
            this.mTeamsListBox = new System.Windows.Forms.ListBox();
            this.mMoveTeamButton = new System.Windows.Forms.Button();
            this.mSelectAllGamesButton = new System.Windows.Forms.Button();
            this.mAddTeamButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schedule";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(244, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Teams";
            // 
            // mScheduleListBox
            // 
            this.mScheduleListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mScheduleListBox.Font = new System.Drawing.Font("Courier New", 8F);
            this.mScheduleListBox.FormattingEnabled = true;
            this.mScheduleListBox.ItemHeight = 14;
            this.mScheduleListBox.Location = new System.Drawing.Point(3, 27);
            this.mScheduleListBox.Name = "mScheduleListBox";
            this.mScheduleListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.mScheduleListBox.Size = new System.Drawing.Size(181, 186);
            this.mScheduleListBox.TabIndex = 2;
            this.mScheduleListBox.DoubleClick += new System.EventHandler(this.mScheduleListBox_DoubleClick);
            // 
            // mRemoveButton
            // 
            this.mRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mRemoveButton.ForeColor = System.Drawing.Color.White;
            this.mRemoveButton.Location = new System.Drawing.Point(3, 219);
            this.mRemoveButton.Name = "mRemoveButton";
            this.mRemoveButton.Size = new System.Drawing.Size(181, 23);
            this.mRemoveButton.TabIndex = 3;
            this.mRemoveButton.Text = "Remove Selected";
            this.mRemoveButton.UseVisualStyleBackColor = true;
            this.mRemoveButton.Click += new System.EventHandler(this.mRemoveButton_Click);
            // 
            // mTeamsListBox
            // 
            this.mTeamsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mTeamsListBox.Font = new System.Drawing.Font("Courier New", 8F);
            this.mTeamsListBox.FormattingEnabled = true;
            this.mTeamsListBox.ItemHeight = 14;
            this.mTeamsListBox.Location = new System.Drawing.Point(247, 27);
            this.mTeamsListBox.Name = "mTeamsListBox";
            this.mTeamsListBox.Size = new System.Drawing.Size(120, 186);
            this.mTeamsListBox.TabIndex = 4;
            this.mTeamsListBox.DoubleClick += new System.EventHandler(this.mTeamsListBox_DoubleClick);
            // 
            // mMoveTeamButton
            // 
            this.mMoveTeamButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mMoveTeamButton.ForeColor = System.Drawing.Color.White;
            this.mMoveTeamButton.Location = new System.Drawing.Point(247, 219);
            this.mMoveTeamButton.Name = "mMoveTeamButton";
            this.mMoveTeamButton.Size = new System.Drawing.Size(120, 23);
            this.mMoveTeamButton.TabIndex = 5;
            this.mMoveTeamButton.Text = "<= Schedule Team";
            this.mMoveTeamButton.UseVisualStyleBackColor = true;
            this.mMoveTeamButton.Click += new System.EventHandler(this.mMoveTeamButton_Click);
            // 
            // mSelectAllGamesButton
            // 
            this.mSelectAllGamesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mSelectAllGamesButton.ForeColor = System.Drawing.Color.White;
            this.mSelectAllGamesButton.Location = new System.Drawing.Point(3, 248);
            this.mSelectAllGamesButton.Name = "mSelectAllGamesButton";
            this.mSelectAllGamesButton.Size = new System.Drawing.Size(181, 23);
            this.mSelectAllGamesButton.TabIndex = 6;
            this.mSelectAllGamesButton.Text = "Select All Games";
            this.mSelectAllGamesButton.UseVisualStyleBackColor = true;
            this.mSelectAllGamesButton.Click += new System.EventHandler(this.mSelectAllGamesButton_Click);
            // 
            // mAddTeamButton
            // 
            this.mAddTeamButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mAddTeamButton.ForeColor = System.Drawing.Color.White;
            this.mAddTeamButton.Location = new System.Drawing.Point(247, 247);
            this.mAddTeamButton.Name = "mAddTeamButton";
            this.mAddTeamButton.Size = new System.Drawing.Size(120, 23);
            this.mAddTeamButton.TabIndex = 7;
            this.mAddTeamButton.Text = "Add Team";
            this.mAddTeamButton.UseVisualStyleBackColor = true;
            this.mAddTeamButton.Click += new System.EventHandler(this.mAddTeamButton_Click);
            // 
            // WeekScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(16)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.mAddTeamButton);
            this.Controls.Add(this.mSelectAllGamesButton);
            this.Controls.Add(this.mMoveTeamButton);
            this.Controls.Add(this.mTeamsListBox);
            this.Controls.Add(this.mRemoveButton);
            this.Controls.Add(this.mScheduleListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "WeekScheduler";
            this.Size = new System.Drawing.Size(392, 273);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox mScheduleListBox;
        private System.Windows.Forms.Button mRemoveButton;
        private System.Windows.Forms.ListBox mTeamsListBox;
        private System.Windows.Forms.Button mMoveTeamButton;
        private System.Windows.Forms.Button mSelectAllGamesButton;
        private System.Windows.Forms.Button mAddTeamButton;
    }
}
