using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace PlayProto
{
	/// <summary>
	/// Summary description for TilePanel.
	/// This panel hosts all of the play tiles.
	/// </summary>
	public class TilePanel : System.Windows.Forms.Panel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ImageGuy[][] mTiles;
		private Hashtable mTileHash; // used for fast lookup.

		private int mCurrentX = 0;
		private int mCurrentY = 0;

		private int mColumns = 8;
		private int mRows    = 15;
		private int mTileScale = 3;


		public TilePanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			mTiles = new ImageGuy[mColumns][];
			for(int i = 0; i < mTiles.Length; i++)
			{
				mTiles[i] = new ImageGuy[mRows];
			}
			mTileHash = new Hashtable(200);
			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary>
		/// Gets an ImageGuy by name.
		/// </summary>
		/// <param name="name">The 'FileNameNoExt' property of an ImageGuy.</param>
		/// <returns></returns>
		public ImageGuy GetImageGuy(string name)
		{
			ImageGuy ret = null;
			if( mTileHash.Contains(name) )
			{
				ret = mTileHash[name] as ImageGuy;
			}
			return ret;
		}

		/// <summary>
		/// Gets the ImageGuy at location x,y on this control.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public ImageGuy GetImageGuy( int x, int y)
		{
			ImageGuy ret = null;

			try
			{
				int xSpot = 0;
				int ySpot = 0;
				xSpot = x/(mTileScale*mTiles[0][1].TheImage.Width);
				ySpot = y/(mTileScale*mTiles[0][1].TheImage.Height);
				ret = mTiles[xSpot][ySpot];
			}
			catch{} 
			return ret;
		}

		/// <summary>
		/// Adds the tiles to this control.
		/// </summary>
		/// <param name="fileNames">The file names for the tiles.</param>
		public void AddTiles(string[] fileNames)
		{
			if( fileNames == null || fileNames.Length < 1 )
				return;

			mCurrentX = 0;
			mCurrentY = 0;
			foreach( string file in fileNames )
			{
				ImageGuy img = new ImageGuy( file );
				AddImage(img);
			}
			this.Invalidate();
		}

		private void AddImage(ImageGuy img)
		{
			if( mCurrentY >= mTiles[1].Length  )
			{
				mCurrentX ++;
				mCurrentY = 0;
			}
			if( mCurrentX < mTiles.Length && mCurrentY < mTiles[0].Length )
			{
				mTiles[mCurrentX][mCurrentY] = img;
				mCurrentY++;
			}
			mTileHash.Add(img.FileNameNoExt, img);
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

		
		protected override void OnPaint(PaintEventArgs e)
		{
			int sizeX =8*mTileScale, sizeY = 8*mTileScale;

			for(int x=0; x < mTiles.Length; x++ )	
			{
				for( int y = 0; y < mTiles[x].Length; y++)
				try
				{
					Image img = mTiles[x][y].TheImage;
					Rectangle r = new Rectangle(x*sizeX, y*sizeY,img.Width*mTileScale, img.Height*mTileScale);
					e.Graphics.DrawImage(img, r);
				}
				catch{ break; }
			}
			
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
