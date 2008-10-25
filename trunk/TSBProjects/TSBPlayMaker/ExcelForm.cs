using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlayProto
{
	/// <summary>
	/// Summary description for ExcelForm.
	/// </summary>
	public class ExcelForm : System.Windows.Forms.Form
	{/*
		private AxMicrosoft.Office.Interop.Owc11.AxSpreadsheet m_SpreadSheet;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.Button m_CancelButton;
		private System.Windows.Forms.Button m_OkButton;

		public int SelectedRow
		{
			get
			{ 
				return m_SpreadSheet.ActiveCell.Row;
				//return m_SelectedRow;
			}
		}

		public int SelectedCol
		{
			get
			{ 
				return m_SpreadSheet.ActiveCell.Column;
				//return m_SelectedCol; 
			}
		}


		public ExcelForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		public void SetColumnTitles(string[] titles)
		{
			//m_SpreadSheet.ActiveSheet.Columns[
		}

		public void PopulateData(string[] headder, string[][] data)
		{
			//m_SpreadSheet.ActiveCell.set_Value(Type.Missing, "Hello"); // works
			//Microsoft.Office.Interop.Owc11.Range r = m_SpreadSheet.ActiveSheet.Cells;
			// m_SpreadSheet.ActiveSheet.Cells[2,2] = "World"; // works
			int adjustment = 1;
			if( headder != null )
			{
				adjustment = 2;
				for(int i = 0; i < headder.Length; i++)
				{
					m_SpreadSheet.ActiveSheet.Cells[1,i+1] = headder[i];
				}
				//m_SpreadSheet.ActiveSheet.ProtectContents= true;
				
				m_SpreadSheet.ActiveWindow.FreezePanes = true;
			}
			string current = "";
			if( data != null )
			{
				for( int i = 0; i < data.Length; i++)
				{
					for( int j = 0; j < data[i].Length; j++)
					{
						current = data[i][j];
						if( current == null )
						{
							current = "";
						}
						m_SpreadSheet.ActiveSheet.Cells[j+adjustment,i+1] = current;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExcelForm));
			this.m_SpreadSheet = new AxMicrosoft.Office.Interop.Owc11.AxSpreadsheet();
			this.m_CancelButton = new System.Windows.Forms.Button();
			this.m_OkButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.m_SpreadSheet)).BeginInit();
			this.SuspendLayout();
			// 
			// m_SpreadSheet
			// 
			this.m_SpreadSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_SpreadSheet.DataSource = null;
			this.m_SpreadSheet.Enabled = true;
			this.m_SpreadSheet.Location = new System.Drawing.Point(0, 0);
			this.m_SpreadSheet.Name = "m_SpreadSheet";
			this.m_SpreadSheet.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("m_SpreadSheet.OcxState")));
			this.m_SpreadSheet.Size = new System.Drawing.Size(616, 454);
			this.m_SpreadSheet.TabIndex = 0;
			// 
			// m_CancelButton
			// 
			this.m_CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_CancelButton.Location = new System.Drawing.Point(432, 464);
			this.m_CancelButton.Name = "m_CancelButton";
			this.m_CancelButton.TabIndex = 1;
			this.m_CancelButton.Text = "&Cancel";
			// 
			// m_OkButton
			// 
			this.m_OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_OkButton.Location = new System.Drawing.Point(528, 464);
			this.m_OkButton.Name = "m_OkButton";
			this.m_OkButton.TabIndex = 2;
			this.m_OkButton.Text = "&OK";
			// 
			// ExcelForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 494);
			this.Controls.Add(this.m_OkButton);
			this.Controls.Add(this.m_CancelButton);
			this.Controls.Add(this.m_SpreadSheet);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ExcelForm";
			this.Text = "ExcelForm";
			((System.ComponentModel.ISupportInitialize)(this.m_SpreadSheet)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
*/
	}
}
