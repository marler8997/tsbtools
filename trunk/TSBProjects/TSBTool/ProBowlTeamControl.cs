using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TSBTool
{
    public partial class ProBowlTeamControl : UserControl
    {
        List<ProBowlLabel> mOffensivePlayers = new List<ProBowlLabel>(22);
        List<ProBowlLabel> mDefensivePlayers = new List<ProBowlLabel>(22);

        private Conference mConference = Conference.AFC;

        private String mData = "";


        public ProBowlTeamControl()
        {
            InitializeComponent();

            SuspendLayout();
            AddLabels();
            ResumeLayout();
        }

        public String Data
        {
            get { return mData; }
            set
            {
                mData = value;
                OnDataChanged();
            }
        }


        /// <summary>
        /// The conference for this control
        /// </summary>
        public Conference Conference
        {
            get { return mConference; }
            set
            {
                if (Conference != value)
                {
                    mConference = value;
                    OnConferenceChanged();
                }
            }
        }

        private void OnConferenceChanged()
        {
        }


        private void OnDataChanged()
        {

        }


        private void AddLabels()
        {
            int offset = 10;
            int labelHeight = 20;
            int col2Loc = 45;
            //offense
            for (int i = 0; i < 17; i++)
            {
                Label posLabel = new Label();
                posLabel.AutoSize = false;
                posLabel.Size = new Size(40, labelHeight);
                posLabel.Top = offset + labelHeight * i;
                posLabel.Parent = this;

                ProBowlLabel nameLabel = new ProBowlLabel();
                nameLabel.AutoSize = false;
                nameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                nameLabel.Size = new Size(80, labelHeight);
                nameLabel.Top = offset + labelHeight * i;
                nameLabel.Left = col2Loc;
                nameLabel.ConferencePosition = (TSBPlayer)i;
                nameLabel.Click += new EventHandler(nameLabel_Click);
                nameLabel.Parent = this;


            }
        }

        void nameLabel_Click(object sender, EventArgs e)
        {
            ProBowlLabel label = sender as ProBowlLabel;
            //show chooser form
            
        }


    }

    public class ProBowlLabel : Label
    {
        public TSBPlayer ConferencePosition { get; set; }

        public TSBPlayer TeamPosition { get; set; }

        public string Team { get; set; }
    }
}
