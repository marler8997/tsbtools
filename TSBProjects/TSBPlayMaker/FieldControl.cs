using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace PlayProto
{
	/// <summary>
	/// Summary description for FieldControl.
	/// </summary>
	public class FieldControl : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ArrayList m_FieldNodes = null;
		

		public FieldControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			m_FieldNodes = new ArrayList(20);

//			AddGuy();
			// TODO: Add any initialization after the InitializeComponent call

		}

		private void AddGuy()
		{
			Button guy = new Button();
			guy.Location = new Point(50,50);
			guy.Size = new Size(20,20);
			guy.Text = "G";
			guy.BackColor = Color.Orange;
			
			Controls.Add(guy);
		}

		/* * 
		 * Need to be able to place objects in the control like the 
		 * following codes:
		 *    -- 0  1  2  3 --
				d0 f0 12 ee
				d0 d4 40 ea
				d0 0c 40 ea
				d1 36 14 ec
				d1 c0 0c ec
				d0 cc 0c ea
				d0 f0 06 d2
				d0 e4 0c ea
				d0 fc 0c ea
				d0 d8 0c ea
				d0 08 0c ea
			Where slot 0 holds the thyp of placement, slot 1 holds the Y coord, slot 2 the x coord
			and slot 3 holds the stance.
			d0 = Set Position From Hike
			d1 = Set Position From Middle of Field
		 * */

		public void AddGuy(int[] posData, string playerPosition)
		{
			if( posData != null && posData.Length > 3 && playerPosition != null )
			{
				FieldNode node = new FieldNode(posData);
				node.Name = playerPosition;
				AddNode(node);
			}
		}

		private void AddNode( FieldNode node)
		{
			m_FieldNodes.Add(node);
			Button b = new Button();
			b.Tag = node;
			b.Text = node.Name;
			b.Size = new Size(10,10);
			b.BackColor = Color.Orange;
			switch( node.Cmd )
			{
				case 0xd0:
					b.Location = new Point(node.X + this.Width/2 , node.Y);
					break;
				case 0xd1:
					b.Location = new Point(node.X + this.Width/2, node.Y + this.Height/2);
					break;
			}
			Controls.Add(b);
			Refresh();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// FieldControl
			// 
			this.BackColor = System.Drawing.Color.SeaGreen;
			this.Name = "FieldControl";
			this.Size = new System.Drawing.Size(408, 352);

		}
		#endregion
	}

	public class FieldNode
	{
		private int[] m_Data = null;
		public string Name = "";
		public int[] Data
		{
			get{ return m_Data; }
		}

		public int Cmd
		{
			get
			{
				if( m_Data != null )
					return m_Data[0];
				else
					return 0;
			}
		}

		public int Stance
		{
			get
			{
				if( m_Data != null )
					return m_Data[3];
				else
					return 0;
			}
		}

		public int X 
		{ 
			get
			{
				if( m_Data != null )
					return m_Data[2];
				else
					return 0;
			}
		}

		public int Y
		{ 
			get
			{
				if( m_Data != null )
					return m_Data[1];
				else
					return 0;
			}
		}

		public FieldNode(int[] data)
		{
			if( data != null && data.Length > 3 )
			{
				m_Data = data;
			}
		}
	}
}
