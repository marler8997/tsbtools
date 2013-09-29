using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TSBTool
{
    public partial class SnesReturnTeamPlayerControl : UserControl, IAllStarPlayerControl
    {
        public SnesReturnTeamPlayerControl()
        {
            InitializeComponent();
        }

        public SnesReturnTeamPlayerControl(String retTeamPos)
        {
            InitializeComponent();
            RetTeamPosition = retTeamPos;
        }

        /// <summary>
        /// Can be "RET1", "RET2" or "RET3"
        /// </summary>
        public String RetTeamPosition
        {
            get { return mPositionLabel.Text; }
            set
            {
                if (value == "RET1" || value == "RET2" || value == "RET3")
                {
                    mPositionLabel.Text = value;
                }
                else
                {
                    throw new Exception("Error! invalid value for 'RetTeamPosition' ");
                }
            }
        }

        public void ReInitialize() { }
        #region Properties
        private Conference mConference = Conference.AFC;

        public Conference Conference
        {
            get { return mConference; }
            set
            {
                //if (mConference != value)
                {
                    mConference = value;
                    OnConferenceChanged();
                }
            }
        }

        private string mData = "";
        // the Data from the main text area of the Main Gui
        public String Data
        {
            get { return mData; }
            set { mData = value; OnDataChanged(); }
        }

        private TSBPlayer mPlayerPosition = TSBPlayer.QB1;

        public TSBPlayer PlayerPosition
        {
            get { return mPlayerPosition; }
            set
            {
                //if (mPlayerPosition != value)
                {
                    mPlayerPosition = value;
                    //OnPlayerPositionChanged();
                }
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetInitialValue();
        }

        public override string ToString()
        {
            String ret = String.Concat(Conference.ToString(), ",",RetTeamPosition, ",", 
                mTeamComboBox.SelectedItem.ToString(), ",", mPositionComboBox.SelectedItem.ToString());
            return ret;
        }

        private void SetInitialValue()
        {
            //search string ==> "AFC,QB1,dolphins,QB1"
            String searchstring = mConference.ToString() + "," + RetTeamPosition;
            if (mData != null)
            {
                int index = mData.IndexOf(searchstring);
                if (index > -1)
                {
                    int endLine = mData.IndexOf('\n', index + 9);
                    String line = mData.Substring(index, endLine - index);
                    Match m = AllstarPlayerControl.ProBowlPositionRegex.Match(line);
                    if (m != Match.Empty)
                    {
                        String allStarPos = m.Groups[2].Value.ToString();
                        String team = m.Groups[3].Value.ToString();
                        String pos = m.Groups[4].Value.ToString();

                        int team_index = mTeamComboBox.Items.IndexOf(team);
                        //mUpdating = true;
                        int pos_index = mPositionComboBox.Items.IndexOf(pos);
                        if (team_index > -1)
                            mTeamComboBox.SelectedIndex = team_index;
                        if (pos_index > -1)
                            mPositionComboBox.SelectedIndex = pos_index;
                        //mUpdating = false;
                    }
                }
            }
        }


        protected virtual void OnDataChanged()
        {
        }

        protected virtual void OnConferenceChanged()
        {
            mTeamComboBox.BeginUpdate();
            mTeamComboBox.Items.Clear();
            int start = 0;
            int end = 13;
            if (Conference == TSBTool.Conference.NFC)
            { //14-27
                start = 14;
                end = 27;
            }
            for (int i = start; i <= end; i++)
            {
                mTeamComboBox.Items.Add(TecmoTool.Teams[i]);
            }

            mTeamComboBox.EndUpdate();
        }

    }
}
