using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace PlayProto
{
	/// <summary>
	/// Summary description for CurveControl.
	/// </summary>
	public class CurveControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TextBox m_InputTextBox;
		private System.Windows.Forms.Button mUpButton;
		private System.Windows.Forms.Button mDownButton;
		private System.Windows.Forms.Button mLeftButton;
		private System.Windows.Forms.Button mRightButton;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.CheckBox m_DrawPointsCheckBox;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CurveControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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

		private string[] GetArgList_s()
		{
			string[] args_s = null;
			if( m_InputTextBox.Text.Length > 0 )
			{
				char[] seps = {','};
				string stuff = m_InputTextBox.Text.Trim(seps).Replace(" ","");
				args_s = stuff.Split(seps);
			}
			return args_s;
		}

		private int[] GetArgList_i()
		{
			int[] args = null;
			string[] list = GetArgList_s();
			if( list != null && list.Length > 0 )
			{
				args = new int[list.Length];
				try
				{
					for( int i =0; i < list.Length; i++)
					{
						args[i] = Int32.Parse(list[i]);
					}
				}
				catch(Exception e )
				{
					//error = "You need to enter an int!";
					Console.WriteLine(e.Message);
				}
			}
			return args;
		}

		private Pen drawingPen = null;
		private int[] mCurveData = null;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			if( drawingPen == null )
			{
				drawingPen = new Pen(Color.Orange);
				drawingPen.Width = 3;
			}
			
			if( mCurveData == null )
			{
				e.Graphics.DrawString("not enough numbers!",
					this.Font,System.Drawing.Brushes.Red, 10,10);
			}
			else
			{
				if( m_DrawPointsCheckBox.Checked )
					DrawPoints(e);
				else
					DrawLines(e);
			}
		}

		private void DrawLines(PaintEventArgs e)
		{
			if( mCurveData != null && mCurveData.Length > 3)
			{
				Point current;
				Point prev = new Point(0,0);

				bool firstOne = true;
				for(int i = 0; i+1 < mCurveData.Length; i+= 2)
				{
					current = new Point(mCurveData[i], mCurveData[i+1]);
					if( !firstOne )
					{
						e.Graphics.DrawLine(drawingPen,prev,current);
					}
					prev = current;
					firstOne = false;
				}
			}
		}


		private void DrawPoints( PaintEventArgs e)
		{
			if( mCurveData != null )
			{
				for(int i = 0; i+1 < mCurveData.Length; i+=2)
				{
					e.Graphics.DrawEllipse(drawingPen,
						mCurveData[i],mCurveData[i+1],4,4);
				}
			}
		}

		private void DrawCurve(PaintEventArgs e)
		{
			if( mCurveData != null &&  mCurveData.Length > 7 )
			{
				e.Graphics.DrawBezier(drawingPen, 
					mCurveData[0],mCurveData[1],
					mCurveData[6],mCurveData[7],
					mCurveData[2],mCurveData[3],
					mCurveData[4],mCurveData[5]);
			}
		}


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_InputTextBox = new System.Windows.Forms.TextBox();
			this.mUpButton = new System.Windows.Forms.Button();
			this.mDownButton = new System.Windows.Forms.Button();
			this.mLeftButton = new System.Windows.Forms.Button();
			this.mRightButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.m_DrawPointsCheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// m_InputTextBox
			// 
			this.m_InputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_InputTextBox.Location = new System.Drawing.Point(8, 360);
			this.m_InputTextBox.Name = "m_InputTextBox";
			this.m_InputTextBox.Size = new System.Drawing.Size(288, 31);
			this.m_InputTextBox.TabIndex = 0;
			this.m_InputTextBox.Text = "";
			this.m_InputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_InputTextBox_KeyDown);
			// 
			// mUpButton
			// 
			this.mUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mUpButton.Location = new System.Drawing.Point(352, 344);
			this.mUpButton.Name = "mUpButton";
			this.mUpButton.Size = new System.Drawing.Size(24, 23);
			this.mUpButton.TabIndex = 1;
			this.mUpButton.Text = "/\\";
			this.mUpButton.Click += new System.EventHandler(this.mDownButton_Click);
			// 
			// mDownButton
			// 
			this.mDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mDownButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mDownButton.Location = new System.Drawing.Point(352, 368);
			this.mDownButton.Name = "mDownButton";
			this.mDownButton.Size = new System.Drawing.Size(24, 23);
			this.mDownButton.TabIndex = 2;
			this.mDownButton.Text = "\\/";
			this.mDownButton.Click += new System.EventHandler(this.mDownButton_Click);
			// 
			// mLeftButton
			// 
			this.mLeftButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mLeftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mLeftButton.Location = new System.Drawing.Point(328, 356);
			this.mLeftButton.Name = "mLeftButton";
			this.mLeftButton.Size = new System.Drawing.Size(24, 23);
			this.mLeftButton.TabIndex = 3;
			this.mLeftButton.Text = "<";
			this.mLeftButton.Click += new System.EventHandler(this.mDownButton_Click);
			// 
			// mRightButton
			// 
			this.mRightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mRightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mRightButton.Location = new System.Drawing.Point(376, 356);
			this.mRightButton.Name = "mRightButton";
			this.mRightButton.Size = new System.Drawing.Size(24, 23);
			this.mRightButton.TabIndex = 4;
			this.mRightButton.Text = ">";
			this.mRightButton.Click += new System.EventHandler(this.mDownButton_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.Location = new System.Drawing.Point(416, 344);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(24, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "+";
			this.button1.Click += new System.EventHandler(this.mDownButton_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button2.Location = new System.Drawing.Point(416, 376);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(24, 23);
			this.button2.TabIndex = 6;
			this.button2.Text = "-";
			this.button2.Click += new System.EventHandler(this.mDownButton_Click);
			// 
			// m_DrawPointsCheckBox
			// 
			this.m_DrawPointsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_DrawPointsCheckBox.Location = new System.Drawing.Point(424, 312);
			this.m_DrawPointsCheckBox.Name = "m_DrawPointsCheckBox";
			this.m_DrawPointsCheckBox.Size = new System.Drawing.Size(16, 24);
			this.m_DrawPointsCheckBox.TabIndex = 7;
			// 
			// CurveControl
			// 
			this.BackColor = System.Drawing.Color.ForestGreen;
			this.Controls.Add(this.m_DrawPointsCheckBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.mRightButton);
			this.Controls.Add(this.mLeftButton);
			this.Controls.Add(this.mDownButton);
			this.Controls.Add(this.mUpButton);
			this.Controls.Add(this.m_InputTextBox);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "CurveControl";
			this.Size = new System.Drawing.Size(448, 400);
			this.ResumeLayout(false);

		}
		#endregion

		private void m_InputTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if( e.KeyCode == Keys.Enter )
			{
				int[] tmp = GetArgList_i();
				if( tmp != null && tmp.Length > 7 )
				{
					for(int i =0; i < tmp.Length; i++ )
					{
						tmp[i] = tmp[i];// +(this.Width/2);
					}
					mCurveData = tmp;
					Refresh();
				}
			}
		}


		private void mDownButton_Click(object sender, System.EventArgs e)
		{
			Button b = sender as Button;
			if( b != null && mCurveData != null)
			{
				switch(b.Text)
				{
					case @"/\":
						for(int i = 1; i < mCurveData.Length; i+=2 )
							mCurveData[i]-= 10;
						break;
					case @"\/":
						for(int i = 1; i < mCurveData.Length; i+=2 )
							mCurveData[i]+= 10;
						break;
					case "<":
						for(int i = 0; i < mCurveData.Length; i+=2 )
							mCurveData[i]-= 10;
						break;
					case ">":
						for(int i = 0; i < mCurveData.Length; i+=2 )
							mCurveData[i]+= 10;
						break;
					case "+":
						for(int i = 1; i < mCurveData.Length; i++ )
							mCurveData[i] = mCurveData[i]*2;
						break;
					case "-":
						for(int i = 0; i < mCurveData.Length; i++ )
						{
							if( mCurveData[i] > 2 )
								mCurveData[i] = mCurveData[i]/2;
						}
						break;
				}
				Refresh();
			}
		}
	}
}
