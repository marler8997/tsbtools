using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TSBTool
{
    /// <summary>
    /// Input should be a schedule, it's output should be a Schedule string:
    /// Week 1
    /// bills at dolphins 
    /// ...
    /// </summary>
    public partial class WeekScheduler : UserControl
    {
        public WeekScheduler()
        {
            InitializeComponent();
            ToolTip teamTip = new ToolTip();
            teamTip.SetToolTip(mTeamsListBox,"DoubleClick item or press 'Schedule Team' to add a team to the schedule.");

            ToolTip scheduleTip = new ToolTip();
            scheduleTip.SetToolTip(mScheduleListBox, "Double Click item or press 'Remove Selected' to remove games from the schedule.");

            ToolTip addTeamTip = new ToolTip();
            addTeamTip.SetToolTip(mAddTeamButton, "Click to add another team to the teams list box.");
        }

        private List<String> mOriginalGames = null;

        /// <summary>
        /// Set the games for this week.
        /// </summary>
        /// <param name="games"></param>
        public void SetGames(List<String> games)
        {
            mScheduleListBox.Items.Clear();
            mTeamsListBox.Items.Clear();

            mOriginalGames = games;
            foreach (string line in games)
            {
                if (line.IndexOf(" at ") > -1)
                {
                    mScheduleListBox.Items.Add(line);
                }
            }
        }

        private void mRemoveButton_Click(object sender, EventArgs e)
        {
            RemoveSelectedGames();
        }

        /// <summary>
        /// Removes the selected games
        /// </summary>
        private void RemoveSelectedGames()
        {
            if (mScheduleListBox.SelectedIndices.Count > 0)
            {
                for (int i = mScheduleListBox.SelectedIndices.Count - 1; i > -1; i--)
                {
                    int index = mScheduleListBox.SelectedIndices[i];
                    String game = mScheduleListBox.Items[index].ToString();

                    int firstSpaceIndex = game.IndexOf(' ');
                    if (firstSpaceIndex > -1)
                    {
                        String team1 = game.Substring(0, firstSpaceIndex);
                        mTeamsListBox.Items.Add(team1);
                    }
                    int lastSpaceIndex = game.IndexOf(" at ") + 4;
                    if (lastSpaceIndex > 4)
                    {
                        String team2 = game.Substring(lastSpaceIndex);
                        mTeamsListBox.Items.Add(team2);
                    }
                    mScheduleListBox.Items.RemoveAt(index);
                }
            }
            else
            {
                MessageBox.Show("No game selected");
            }
        }

        private void mMoveTeamButton_Click(object sender, EventArgs e)
        {
            AddTeamToSchedule();
        }

        /// <summary>
        /// removes the selected team form the 'teams' list box, and adds it to the schedule listbox.
        /// </summary>
        private void AddTeamToSchedule()
        {
            int index = mTeamsListBox.SelectedIndex;
            if (index > -1)
            {
                String team = mTeamsListBox.SelectedItem.ToString();

                if (mScheduleListBox.Items.Count > 0 &&
                    mScheduleListBox.Items[mScheduleListBox.Items.Count - 1].ToString().IndexOf(" at ") == -1)
                {
                    String game = mScheduleListBox.Items[mScheduleListBox.Items.Count - 1].ToString() + "at " + team;
                    mScheduleListBox.Items.RemoveAt(mScheduleListBox.Items.Count - 1);
                    mScheduleListBox.Items.Add(game);
                }
                else
                {
                    mScheduleListBox.Items.Add(team + " " );
                }
                mTeamsListBox.Items.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("No team selected");
            }
        }

        private void mSelectAllGamesButton_Click(object sender, EventArgs e)
        {
            SelectAllGames();
        }

        /// <summary>
        /// Select all games in the schedule list box.
        /// </summary>
        private void SelectAllGames()
        {
            for (int i = 0; i < mScheduleListBox.Items.Count; i++)
            {
                mScheduleListBox.SelectedIndices.Add(i);
            }
        }

        /// <summary>
        /// returns the week string
        /// </summary>
        public override string ToString()
        {
            return GetWeekString();
        }

        /// <summary>
        /// returns the data for this week.
        /// </summary>
        /// <returns> returns the data for this week.</returns>
        public string GetWeekString()
        {
            StringBuilder builder = new StringBuilder(300);

            for (int i = 0; i < mScheduleListBox.Items.Count; i++)
            {
                builder.Append(mScheduleListBox.Items[i].ToString());
                builder.Append("\n");
            }
            builder.Append("\n");
            return builder.ToString();
        }

        private void mTeamsListBox_DoubleClick(object sender, EventArgs e)
        {
            AddTeamToSchedule();
        }

        private void mScheduleListBox_DoubleClick(object sender, EventArgs e)
        {
            RemoveSelectedGames();
        }

        /// <summary>
        /// Adds an entered string to the teams list
        /// </summary>
        private void mAddTeamButton_Click(object sender, EventArgs e)
        {
            String newTeam = StringInputDlg.GetString("Add a Team", "Enter a string to add a team to the List box", "");
            if (!String.IsNullOrEmpty(newTeam))
            {
                mTeamsListBox.Items.Add(newTeam);
            }
        }
    }
}
