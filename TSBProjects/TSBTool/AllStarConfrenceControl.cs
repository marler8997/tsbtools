using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TSBTool
{
    public partial class AllStarConfrenceControl : UserControl
    {
        public AllStarConfrenceControl()
        {
            InitializeComponent();
        }

        public void ReInitialize()
        {
            foreach (Control ctrl in Controls)
            {
                AllstarPlayerControl asp = ctrl as AllstarPlayerControl;
                {
                    if (asp != null)
                        asp.ReInitialize();
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddPlayerControls();
        }

        public Conference Conference { get; set; }

        private Control[] mPlayerControls = new Control[33];

        private void AddPlayerControls()
        {
            TSBPlayer player;
            for (int i = 0; i < 30; i++)
            {
                player = (TSBPlayer)i;
                AllstarPlayerControl aspc = new AllstarPlayerControl();
                aspc.Top = i * aspc.Height;
                aspc.Width = Width - 3;
                aspc.PlayerPosition = player;
                aspc.Name = player.ToString();
                aspc.Conference = this.Conference;
                aspc.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
                aspc.Data = this.Data;
                mPlayerControls[i] = aspc;
            }
            
            if (Data.IndexOf("AFC,RET1") > 0)
            {
                // SNES version , Add controls for the return team
                SnesReturnTeamPlayerControl ret1 = new SnesReturnTeamPlayerControl("RET1");
                ret1.Conference = this.Conference;
                ret1.Width = Width - 3;
                ret1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
                ret1.Data = this.Data;
                ret1.Top = ((Control)mPlayerControls[29]).Bottom + 1;
                mPlayerControls[30] = ret1;

                SnesReturnTeamPlayerControl ret2 = new SnesReturnTeamPlayerControl("RET2");
                ret2.Conference = this.Conference;
                ret2.Width = Width - 3;
                ret2.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
                ret2.Data = this.Data;
                ret2.Top = ((Control)mPlayerControls[30]).Bottom + 1;
                mPlayerControls[31] = ret2;

                SnesReturnTeamPlayerControl ret3 = new SnesReturnTeamPlayerControl("RET3");
                ret3.Conference = this.Conference;
                ret3.Width = Width - 3;
                ret3.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
                ret3.Data = this.Data;
                ret3.Top = ((Control)mPlayerControls[31]).Bottom + 1;
                mPlayerControls[32] = ret3;
            }

            SuspendLayout();
            Controls.AddRange(mPlayerControls);
            ResumeLayout();
        }

        /// <summary>
        /// The Text / player data from main gui
        /// </summary>
        public String Data { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(2000);
            builder.Append("# ");
            builder.Append(Conference.ToString());
            builder.Append(" ProBowl players\r\n");
            for (int i = 0; i < mPlayerControls.Length; i++)
            {
                if (mPlayerControls[i] != null)
                {
                    builder.Append(mPlayerControls[i].ToString());
                    builder.Append("\r\n");
                }
            }
            return builder.ToString();
        }
    }
}
