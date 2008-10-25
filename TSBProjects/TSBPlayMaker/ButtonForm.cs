using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlayProto
{
	/// <summary>
	/// Summary description for ButtonForm.
	/// </summary>
	public class ButtonForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label m_InfoLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button mCancelButton;

		private string m_Result = null;

		public string Result 
		{
			get{ return m_Result; }
			set{ m_Result = value; }
		}

		private string[] m_ButtonValues = null;
		public string[] ButtonValues
		{
			get{ return m_ButtonValues; }
			set
			{ 
				m_ButtonValues = value;
				SetupButtons();
			}
		}

		public string Title
		{
			get{ return this.Text;}
			set{ this.Text = value;}
		}

		public string Info
		{
			get{ return this.m_InfoLabel.Text;}
			set{ this.m_InfoLabel.Text = value;}
		}

		public int ButtonWidth = 40;

		public ButtonForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		private void SetupButtons()
		{
			int currentX = 8;
			int currentY = 80;
			int tabIndex = 1;
			foreach( string str in m_ButtonValues)
			{
				Button b = new Button();
				b.Text = str;
				b.Height = 23;
				b.Width  = ButtonWidth;
				b.TabIndex = tabIndex++;
				if( currentX + 8 + b.Width > this.Width )
				{
					currentX = 8;
					currentY += (8+b.Height);
					b.Location = new Point(currentX, currentY);
					currentX += (8 + b.Width);
				}
				else
				{
					b.Location = new Point(currentX, currentY);
					currentX += (8 + b.Width);
				}
				
				b.Click += new EventHandler(Button_Click);
				this.Controls.Add(b);
			}
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
			this.m_InfoLabel = new System.Windows.Forms.Label();
			this.mCancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_InfoLabel
			// 
			this.m_InfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_InfoLabel.Location = new System.Drawing.Point(0, 8);
			this.m_InfoLabel.Name = "m_InfoLabel";
			this.m_InfoLabel.Size = new System.Drawing.Size(480, 60);
			this.m_InfoLabel.TabIndex = 0;
			// 
			// mCancelButton
			// 
			this.mCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mCancelButton.Location = new System.Drawing.Point(332, 176);
			this.mCancelButton.Name = "mCancelButton";
			this.mCancelButton.TabIndex = 1;
			this.mCancelButton.Text = "Cancel";
			this.mCancelButton.Click += new System.EventHandler(this.mCancelButton_Click);
			// 
			// ButtonForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 206);
			this.Controls.Add(this.mCancelButton);
			this.Controls.Add(this.m_InfoLabel);
			this.Name = "ButtonForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ButtonForm";
			this.ResumeLayout(false);

		}
		#endregion


		private void Button_Click(object sender, System.EventArgs e)
		{
			Button b = sender as Button;
			if( b != null )
			{
				Result = b.Text;
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
			}
		}

		private void mCancelButton_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
