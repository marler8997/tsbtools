using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace TSBTool
{
	/// <summary>
	/// Summary description for ColorForm.
	/// </summary>
	public class ColorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox mPictureBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Bitmap mPictureBitmap;

		private System.Windows.Forms.Label mNumberLabel;

		private string mCurrentColorString = null;
		private System.Windows.Forms.Label mColorLabel;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

		private Color mCurrentColor;

		/// <summary>
		/// 
		/// </summary>
		public Color CurrentColor
		{
			get{ return mCurrentColor;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string CurrentColorString
		{
			get{ return mNumberLabel.Text;}
			set
			{
				string tmp = value.ToUpper();
				Color c = GetColor(tmp);
				if( c != Color.Empty )
				{
					mNumberLabel.Text = tmp;
					mNumberLabel.BackColor = c;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public ColorForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ColorForm));
			this.mPictureBox = new System.Windows.Forms.PictureBox();
			this.mNumberLabel = new System.Windows.Forms.Label();
			this.mColorLabel = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// mPictureBox
			// 
			this.mPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("mPictureBox.Image")));
			this.mPictureBox.Location = new System.Drawing.Point(0, 0);
			this.mPictureBox.Name = "mPictureBox";
			this.mPictureBox.Size = new System.Drawing.Size(536, 160);
			this.mPictureBox.TabIndex = 0;
			this.mPictureBox.TabStop = false;
			this.mPictureBox.DoubleClick += new System.EventHandler(this.mPictureBox_DoubleClick);
			this.mPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mPictureBox_MouseDown);
			// 
			// mNumberLabel
			// 
			this.mNumberLabel.BackColor = System.Drawing.Color.Black;
			this.mNumberLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mNumberLabel.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mNumberLabel.ForeColor = System.Drawing.Color.Lime;
			this.mNumberLabel.Location = new System.Drawing.Point(8, 168);
			this.mNumberLabel.Name = "mNumberLabel";
			this.mNumberLabel.Size = new System.Drawing.Size(40, 24);
			this.mNumberLabel.TabIndex = 2;
			this.mNumberLabel.Text = "00";
			this.mNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mColorLabel
			// 
			this.mColorLabel.BackColor = System.Drawing.SystemColors.Control;
			this.mColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mColorLabel.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.mColorLabel.ForeColor = System.Drawing.Color.Lime;
			this.mColorLabel.Location = new System.Drawing.Point(56, 168);
			this.mColorLabel.Name = "mColorLabel";
			this.mColorLabel.Size = new System.Drawing.Size(40, 24);
			this.mColorLabel.TabIndex = 3;
			this.mColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(360, 168);
			this.button1.Name = "button1";
			this.button1.TabIndex = 22;
			this.button1.Text = "&OK";
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(448, 168);
			this.button2.Name = "button2";
			this.button2.TabIndex = 23;
			this.button2.Text = "&Cancel";
			// 
			// ColorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 198);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.mColorLabel);
			this.Controls.Add(this.mNumberLabel);
			this.Controls.Add(this.mPictureBox);
			this.MaximumSize = new System.Drawing.Size(544, 232);
			this.MinimumSize = new System.Drawing.Size(544, 232);
			this.Name = "ColorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ColorForm";
			this.ResumeLayout(false);

		}
		#endregion

		private void mPictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if( mPictureBitmap == null )
			{
				mPictureBitmap = new Bitmap(mPictureBox.Image);
			}
			//mXYLabel.Text = string.Format("x= {0}, y= {1}",e.X,e.Y);
			int topNumber = (e.X - 32)/(32);
			int leftNumber = (e.Y - 32)/32;
			mCurrentColorString = string.Format("{0:X}{1:X}", leftNumber, topNumber );
			int x = topNumber*32 +32;
			int y = leftNumber*32 +32;

			mCurrentColor = mPictureBitmap.GetPixel(x, y);
			mColorLabel.BackColor = mCurrentColor;
			mNumberLabel.Text = mCurrentColorString;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hexStr"></param>
		/// <returns></returns>
		public Color GetColor(string hexStr)
		{
			Color ret = Color.Empty;
			if( hexStr != null && hexStr.Length == 2 )
			{
//				int left = hexStr[0] - 48;
//				int top  = hexStr[1] - 48;
				byte num = Byte.Parse(hexStr, System.Globalization.NumberStyles.AllowHexSpecifier);
				int left = num >> 4;
				int top = num & 0x0F;

				if( left < 4 && left > -1 && top > -1 && top < 0x10 )
				{
					int y = left*32+32;
					int x = top *32+32;
					Bitmap bmp = new Bitmap(mPictureBox.Image);
					ret = bmp.GetPixel(x,y);
				}
			}
			return ret;
		}

		private void mPictureBox_DoubleClick(object sender, System.EventArgs e)
		{
			EnterCurrentColor();
		}

		private void EnterCurrentColor()
		{
		}

	}
}
