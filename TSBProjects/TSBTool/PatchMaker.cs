using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TSBTool
{
    public partial class PatchMaker : Form
    {
        public PatchMaker()
        {
            InitializeComponent();
        }

        private String GetFileName()
        {
            String retVal = "";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Filter = "TSB files (*.nes;*.smc)|*.nes;*.smc";

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                retVal = dlg.FileName;
            }
            dlg.Dispose();
            return retVal;
        }

        private void mBrowseBaseButton_Click(object sender, EventArgs e)
        {
            String fileName = GetFileName();
            if (!String.IsNullOrEmpty(fileName))
            {
                mBaseTextBox.Text = fileName;
            }
        }

        private void mBrowseModifiedButton_Click(object sender, EventArgs e)
        {
            String fileName = GetFileName();
            if (!String.IsNullOrEmpty(fileName))
            {
                mModifiedTextBox.Text = fileName;
            }
        }

        private void mTextBox_TextChanged(object sender, EventArgs e)
        {
            if (mBaseTextBox.Text.Length > 4 && mModifiedTextBox.Text.Length > 4)
            {
                if (mGoButton.Enabled == false)
                    mGoButton.Enabled = true;
            }
            else
            {
                if (mGoButton.Enabled == true) 
                    mGoButton.Enabled = false;
            }
        }

        private void mGoButton_Click(object sender, EventArgs e)
        {
            mResulTextBox.Text = CreateSetPatch(mBaseTextBox.Text, mModifiedTextBox.Text);
        }

        private string CreateSetPatch(string file1, string file2)
        {
            String retVal = "";
            if (!File.Exists(file1))
                retVal = String.Format("'{0}' does not exist", file1);
            else if (!File.Exists(file2))
                retVal = String.Format("'{0}' does not exist", file2);
            else
            {
                StringBuilder builder = new StringBuilder(500);
                try
                {
                    FileInfo f1 = new FileInfo(file1);
                    long len1 = f1.Length;
                    FileStream s1 = new FileStream(file1, FileMode.Open);
                    byte[] rom1 = new byte[(int)len1];
                    s1.Read(rom1, 0, (int)len1);
                    s1.Close();

                    FileInfo f2 = new FileInfo(file2);
                    long len2 = f2.Length;
                    FileStream s2 = new FileStream(file2, FileMode.Open);
                    byte[] rom2 = new byte[(int)len2];
                    s2.Read(rom2, 0, (int)len2);
                    s2.Close();

                    for (int i = 0; i < rom1.Length && i < rom2.Length; i++)
                    {
                        if (rom1[i] != rom2[i])
                        {
                            builder.Append(String.Format("SET(0x{0:x}, 0x{1:x})\r\n", i, rom2[i]));
                        }
                    }
                    if (builder.Length == 0)
                        builder.Append("No differences!\n");
                }
                catch (Exception e)
                {
                    builder.Append(e.Message);
                }

                retVal = builder.ToString();
            }
            return retVal;
        }

        private void mSelectAllItem_Click(object sender, EventArgs e)
        {
            mResulTextBox.SelectAll();
        }

        private void mCopyItem_Click(object sender, EventArgs e)
        {
            mResulTextBox.Copy();
        }
    }
}
