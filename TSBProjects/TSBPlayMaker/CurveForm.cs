using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlayProto
{
	/// <summary>
	/// Summary description for CurveForm.
	/// </summary>
	public class CurveForm : System.Windows.Forms.Form
	{
		private PlayProto.CurveControl m_CurveControl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CurveForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.m_CurveControl = new PlayProto.CurveControl();
			this.SuspendLayout();
			// 
			// m_CurveControl
			// 
			this.m_CurveControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_CurveControl.BackColor = System.Drawing.Color.ForestGreen;
			this.m_CurveControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.m_CurveControl.Location = new System.Drawing.Point(0, 0);
			this.m_CurveControl.Name = "m_CurveControl";
			this.m_CurveControl.Size = new System.Drawing.Size(448, 400);
			this.m_CurveControl.TabIndex = 0;
			// 
			// CurveForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 398);
			this.Controls.Add(this.m_CurveControl);
			this.Name = "CurveForm";
			this.Text = "CurveForm";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
