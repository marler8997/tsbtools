namespace TSBTool
{
    partial class ProBowlForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mAFCTabPage = new System.Windows.Forms.TabPage();
            this.mNFCTabPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.mAFCTabPage);
            this.tabControl1.Controls.Add(this.mNFCTabPage);
            this.tabControl1.Location = new System.Drawing.Point(7, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(511, 415);
            this.tabControl1.TabIndex = 0;
            // 
            // mAFCTabPage
            // 
            this.mAFCTabPage.Location = new System.Drawing.Point(4, 22);
            this.mAFCTabPage.Name = "mAFCTabPage";
            this.mAFCTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mAFCTabPage.Size = new System.Drawing.Size(503, 389);
            this.mAFCTabPage.TabIndex = 0;
            this.mAFCTabPage.Text = "AFC";
            this.mAFCTabPage.UseVisualStyleBackColor = true;
            // 
            // mNFCTabPage
            // 
            this.mNFCTabPage.Location = new System.Drawing.Point(4, 22);
            this.mNFCTabPage.Name = "mNFCTabPage";
            this.mNFCTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mNFCTabPage.Size = new System.Drawing.Size(503, 389);
            this.mNFCTabPage.TabIndex = 1;
            this.mNFCTabPage.Text = "NFC";
            this.mNFCTabPage.UseVisualStyleBackColor = true;
            // 
            // ProBowlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 444);
            this.Controls.Add(this.tabControl1);
            this.Name = "ProBowlForm";
            this.Text = "ProBowlForm";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mAFCTabPage;
        private System.Windows.Forms.TabPage mNFCTabPage;
    }
}