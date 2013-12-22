using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TSBTool
{
    /// <summary>
    /// GUI used to schedule games
    /// </summary>
    public partial class ScheduleForm : Form
    {
        public ScheduleForm()
        {
            InitializeComponent();
        }

        private int mCurrentWeek = 0;

        private String mData;

        /// <summary>
        /// The Text data.
        /// This form will operate on the Schedule portion.
        /// </summary>
        public String Data
        {
            get { return mData; }
            set
            {
                if (mData != value)
                {
                    mData = value;
                    OnDataChanged(value);
                }
            }
        }

        /// <summary>
        /// get/set the current week on the form.
        /// </summary>
        public int CurrentWeek
        {
            get { return mWeeksComboBox.SelectedIndex+1; }

            set
            {
                if (value <= mWeeksComboBox.Items.Count && value > 0)
                {
                    mWeeksComboBox.SelectedIndex = value - 1;
                }
            }
        }

        /// <summary>
        /// Called to setup the form after the string data has been assigned to the form.
        /// </summary>
        /// <param name="value">the string data</param>
        protected virtual void OnDataChanged(string value)
        {
            int weeks = SetWeekRange();
            if (weeks > 0)
            {
                mWeeksComboBox.Items.Clear();
                for (int i = 1; i <= weeks; i++)
                    mWeeksComboBox.Items.Add(i);
                mWeeksComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// goes through the data counting up the WEEKS'
        /// </summary>
        /// <returns>the number of weeks</returns>
        private int SetWeekRange()
        {
            int ret = 0;
            String[] lines = mData.Replace("\r\n", "\n").Split(new char[] {'\n'});
            foreach (String line in lines)
            {
                if (line.StartsWith("WEEK ", StringComparison.OrdinalIgnoreCase))
                    ret++;
            }

            return ret;
        }

        /// <summary>
        /// Sets the current 'WEEK' on the form.
        /// </summary>
        /// <param name="week">the week number to operate on.</param>
        private void SetWeek(int week)
        {   
            String[] lines = mData.Replace("\r\n", "\n").Split(new char[] { '\n' });
            List<String> weekList = new List<string>(20);
            String line = "";
            int currrentWeek = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i];
                if (line.StartsWith("WEEK", StringComparison.OrdinalIgnoreCase))
                {
                    currrentWeek++;
                }
                else if (week == currrentWeek)
                {
                    weekList.Add(line);
                }
            }
            if (weekList.Count > 0)
            {
                mWeekScheduler.SetGames(weekList);
            }
        }

        /// <summary>
        /// Will save the surrent week to 'Data'
        /// </summary>
        private void SaveWeek()
        {
            if (mCurrentWeek < 1)
                return;   // EARLY REATURN !!!!!!!!!!!!!!!!!!!!!

            String searchString = "WEEK " + mCurrentWeek;
            int index1 = mData.IndexOf(searchString, StringComparison.OrdinalIgnoreCase);

            int index2 = mData.IndexOf("WEEK ", index1 + 3, StringComparison.OrdinalIgnoreCase);
            if (index2 == -1)
            {
                index2 = mData.Length-1;
            }
            StringBuilder newData = new StringBuilder(mData.Length);
            newData.Append(mData.Substring(0, index1));
            newData.Append(searchString);
            newData.Append("\n");
            newData.Append(mWeekScheduler.ToString());
            newData.Append(mData.Substring(index2));

            mData = newData.ToString();
        }

        /// <summary>
        /// saves the current week, closes the form
        /// </summary>
        private void mOkButton_Click(object sender, EventArgs e)
        {
            SaveWeek();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Saves the seek, changes the week to the newly selected one.
        /// </summary>
        private void mWeeksComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveWeek();
            mCurrentWeek = mWeeksComboBox.SelectedIndex + 1;
            SetWeek(mCurrentWeek);
        }
    }
}
