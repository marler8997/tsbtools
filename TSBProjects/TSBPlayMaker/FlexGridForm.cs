using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlayProto
{
	/// <summary>
	/// Summary description for FlexGridForm.
	/// </summary>
	public class FlexGridForm : System.Windows.Forms.Form
	{
		private AxMSFlexGridLib.AxMSFlexGrid m_FlexGrid;
		private System.Windows.Forms.Button m_OkButton;
		private System.Windows.Forms.Button m_CancelButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		
		public FlexGridForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public int SelectedRow
		{
			get
			{
				return m_FlexGrid.RowSel;
			}
		}

		public int SelectedCol
		{
			get
			{
				return m_FlexGrid.ColSel;
			}
		}

		public void SetColumnHeadders(string[] headders)
		{
			if( headders != null )
			{
				m_FlexGrid.Cols = headders.Length;
				for(int i =1; i < m_FlexGrid.Cols; i++)
				{
					m_FlexGrid.Row = 0;
					m_FlexGrid.Col = i;
					m_FlexGrid.Text = headders[i];
				}
			}
		}

		public void SetRowTitles(string[] titles)
		{
			if( titles != null )
			{
				m_FlexGrid.Cols = titles.Length;
				for(int i =1; i < m_FlexGrid.Cols; i++)
				{
					m_FlexGrid.Row = i;
					m_FlexGrid.Col = 0;
					m_FlexGrid.Text = titles[i];
					m_FlexGrid.CellAlignment = 1;
				}
			}
		}

		public void PopulateData(string[][] data)
		{
			int gomes = m_FlexGrid.CellWidth;
			// = gomes*2;

			m_FlexGrid.Cols = data.Length +1;
			m_FlexGrid.Rows = data[0].Length+1;
			for(int i =0; i < m_FlexGrid.Cols; i++)
			{
				m_FlexGrid.set_ColWidth(i, gomes*2);
			}

			string current = "";
			if( data != null )
			{
				for( int i = 0; i < data.Length; i++)
				{
					for( int j = 0; j < data[i].Length; j++)
					{
						m_FlexGrid.Row = j+1;
						m_FlexGrid.Col = i+1;
						current = data[i][j];
						if( current == null )
						{
							current = "";
						}
						m_FlexGrid.CellAlignment = 1;
						m_FlexGrid.Text = current;
						
						//m_SpreadSheet.ActiveSheet.Cells[j+adjustment,i+1] = current;
						//current = "";
					}
				}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FlexGridForm));
			this.m_FlexGrid = new AxMSFlexGridLib.AxMSFlexGrid();
			this.m_OkButton = new System.Windows.Forms.Button();
			this.m_CancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.m_FlexGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// m_FlexGrid
			// 
			this.m_FlexGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_FlexGrid.Location = new System.Drawing.Point(0, 0);
			this.m_FlexGrid.Name = "m_FlexGrid";
			this.m_FlexGrid.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("m_FlexGrid.OcxState")));
			this.m_FlexGrid.Size = new System.Drawing.Size(608, 424);
			this.m_FlexGrid.TabIndex = 0;
			// 
			// m_OkButton
			// 
			this.m_OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_OkButton.Location = new System.Drawing.Point(528, 428);
			this.m_OkButton.Name = "m_OkButton";
			this.m_OkButton.TabIndex = 4;
			this.m_OkButton.Text = "&OK";
			// 
			// m_CancelButton
			// 
			this.m_CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_CancelButton.Location = new System.Drawing.Point(432, 428);
			this.m_CancelButton.Name = "m_CancelButton";
			this.m_CancelButton.TabIndex = 3;
			this.m_CancelButton.Text = "&Cancel";
			// 
			// FlexGridForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 454);
			this.Controls.Add(this.m_OkButton);
			this.Controls.Add(this.m_CancelButton);
			this.Controls.Add(this.m_FlexGrid);
			this.Name = "FlexGridForm";
			this.Text = "FlexGridForm";
			((System.ComponentModel.ISupportInitialize)(this.m_FlexGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
