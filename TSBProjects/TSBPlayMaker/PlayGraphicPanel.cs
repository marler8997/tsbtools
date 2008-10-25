using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace PlayProto
{
	/// <summary>
	/// Summary description for PlatGraphicPanel.
	/// </summary>
	public class PlayGraphicPanel : System.Windows.Forms.Panel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ImageGuy[][] mImageGuys;

		public PlayGraphicPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.BackColor = Color.Gainsboro;
			this.AllowDrop = true;

			mImageGuys = new ImageGuy[6][];
			for(int i =0; i < mImageGuys.Length; i++)
			{
				mImageGuys[i] = new ImageGuy[7];
			}
		}

		/// <summary>
		/// Returns the ImageGuy at row =j, col = i
		/// </summary>
		/// <param name="row">the row</param>
		/// <param name="col">the col</param>
		/// <returns></returns>
		public ImageGuy GetImageGuy(int row, int col )
		{
			ImageGuy ret = null;
			if( row < mImageGuys.Length && col < mImageGuys[row].Length )
			{
				ret = mImageGuys[row][col];
			}
			else
			{
				System.Diagnostics.Debug.WriteLine(string.Format("No Image Guy at {0}, {1}",row, col));
			}
			return ret;
		}

		public void FillWithImage(ImageGuy guy)
		{
			for(int i = 0; i < mImageGuys.Length; i++)
			{
				for(int j = 0; j < mImageGuys[i].Length; j++)
				{
					mImageGuys[i][j] = guy;
				}
			}
			this.Invalidate();
		}

		/// <summary>
		/// Sets guy to be the image in row,col.
		/// </summary>
		/// <param name="guy"></param>
		/// <param name="row"></param>
		/// <param name="col"></param>
		public void SetImageGuy(ImageGuy guy, int row, int col)
		{
			if( row < mImageGuys.Length && col < mImageGuys[row].Length)
			{
				mImageGuys[row][col] = guy;
				this.Invalidate(new Rectangle(row*24, col*24,24,24));
			}
		}

		/// <summary>
		/// Adds ImageGuy guy to the square that contains x,y.
		/// </summary>
		/// <param name="guy"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void AddImageGuy(ImageGuy guy, int x, int y )
		{
			int i = (int)x/24;
			int j = (int)y/24;
			if( i < mImageGuys.Length && j < mImageGuys[i].Length )
			{
				mImageGuys[i][j] = guy;
			}
			this.Invalidate(new Rectangle(i*24, j*24,24,24));
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

		private void DrawGrid(PaintEventArgs e)
		{
			int x=0,y=0;
			int gap = 8*3;
			Pen pen = new Pen(Color.White, 1);

			for( x=0; x < this.Width; x+= gap )
			{
				e.Graphics.DrawLine(pen, new Point(x, 0),new Point(x, this.Height));
			}
			for( y=0; y < this.Height; y+= gap )
			{
				e.Graphics.DrawLine(pen, new Point(0, y),new Point(this.Width,y ));
			}
		}

		private int mTileScale = 3;

		protected override void OnPaint(PaintEventArgs e)
		{
			int sizeX =8*mTileScale, sizeY = 8*mTileScale;

			for(int i = 0; i < mImageGuys.Length; i++)
			{
				for(int j = 0; j <mImageGuys[i].Length; j++)
				{
					if( mImageGuys[i][j] != null )
					{
						Image img = mImageGuys[i][j].TheImage;
						Rectangle r = new Rectangle(i*sizeX, j*sizeY,img.Width*mTileScale, img.Height*mTileScale);
						e.Graphics.DrawImage(img, r);
					}
				}
			}
			DrawGrid(e);
			base.OnPaint (e);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
