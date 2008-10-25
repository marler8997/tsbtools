using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlayProto
{
	/// <summary>
	/// Summary description for FieldForm.
	/// </summary>
	public class FieldForm : System.Windows.Forms.Form
	{
		private PlayProto.FieldControl m_FieldControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FieldForm()
		{
			InitializeComponent();
		}

		public void AddGuy(int[] posData, string playerPosition)
		{
			m_FieldControl.AddGuy(posData, playerPosition);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_FieldControl = new PlayProto.FieldControl();
			this.SuspendLayout();
			// 
			// m_FieldControl
			// 
			this.m_FieldControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_FieldControl.BackColor = System.Drawing.Color.SeaGreen;
			this.m_FieldControl.Location = new System.Drawing.Point(0, 0);
			this.m_FieldControl.Name = "m_FieldControl";
			this.m_FieldControl.Size = new System.Drawing.Size(528, 448);
			this.m_FieldControl.TabIndex = 0;
			// 
			// FieldForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 446);
			this.Controls.Add(this.m_FieldControl);
			this.Name = "FieldForm";
			this.Text = "FieldForm";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
