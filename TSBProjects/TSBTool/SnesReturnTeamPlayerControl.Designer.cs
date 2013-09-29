namespace TSBTool
{
    partial class SnesReturnTeamPlayerControl
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
            this.mTeamComboBox = new System.Windows.Forms.ComboBox();
            this.mPositionComboBox = new System.Windows.Forms.ComboBox();
            this.mPositionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mTeamComboBox
            // 
            this.mTeamComboBox.FormattingEnabled = true;
            this.mTeamComboBox.Location = new System.Drawing.Point(79, 3);
            this.mTeamComboBox.Name = "mTeamComboBox";
            this.mTeamComboBox.Size = new System.Drawing.Size(121, 21);
            this.mTeamComboBox.TabIndex = 0;
            // 
            // mPositionComboBox
            // 
            this.mPositionComboBox.FormattingEnabled = true;
            this.mPositionComboBox.Location = new System.Drawing.Point(206, 3);
            this.mPositionComboBox.Name = "mPositionComboBox";
            this.mPositionComboBox.Size = new System.Drawing.Size(121, 21);
            this.mPositionComboBox.TabIndex = 1;
            this.mPositionComboBox.Items.AddRange(new string[] { 
												"QB1", "QB2", "RB1", "RB2",  "RB3",  "RB4",  "WR1",  "WR2", "WR3", "WR4", "TE1", 
												"TE2", "C",   "LG",  "RG",   "LT",   "RT",
												"RE", "NT",   "LE",  "ROLB", "RILB", "LILB", "LOLB", "RCB", "LCB", "FS",  "SS",  "K", "P" 
											} );
            // 
            // mPositionLabel
            // 
            this.mPositionLabel.AutoSize = true;
            this.mPositionLabel.Location = new System.Drawing.Point(3, 6);
            this.mPositionLabel.Name = "mPositionLabel";
            this.mPositionLabel.Size = new System.Drawing.Size(43, 13);
            this.mPositionLabel.TabIndex = 2;
            this.mPositionLabel.Text = "{RET1}";
            // 
            // SnesReturnTeamPlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mPositionLabel);
            this.Controls.Add(this.mPositionComboBox);
            this.Controls.Add(this.mTeamComboBox);
            this.Name = "SnesReturnTeamPlayerControl";
            this.Size = new System.Drawing.Size(351, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox mTeamComboBox;
        private System.Windows.Forms.ComboBox mPositionComboBox;
        private System.Windows.Forms.Label mPositionLabel;
    }
}
