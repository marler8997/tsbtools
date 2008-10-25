using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;

namespace PlayProto
{
	/// <summary>
	/// Summary description for CommandTree.
	/// </summary>
	public class CommandTree
	{

		public CommandTree Parent = null;

		private ArrayList mBranches = new ArrayList();

		/// <summary>
		/// The various branches (of CommandTrees) that the execution path can go down.
		/// </summary>
		public ArrayList Branches
		{
			get{ return mBranches; }
		}

		/// <summary>
		/// Adds a Branch to the Tree
		/// </summary>
		/// <param name="branch"></param>
		public void AddBranch( CommandTree branch)
		{
			if( branch != null )
			{
				mBranches.Add(branch);
			}
		}

		private string mMainExecutionPath="";

		/// <summary>
		/// Represents the execution path of a pattern without any
		/// conditional paths taken.
		/// </summary>
		public string MainExecutionPath
		{
			get{ return mMainExecutionPath; }
			set{ mMainExecutionPath = value; }
		}

		public CommandTree(string cmd)
		{
			this.mMainExecutionPath = cmd;
		}

		/// <summary>
		/// Returns the Level of the 
		/// </summary>
		internal int Level
		{
			get
			{
				int ret = 0;
				CommandTree test = Parent;

				while( test != null )
				{
					ret ++;
					test = test.Parent;
				}
				return ret;
			}
		}

		/// <summary>
		/// Returns the indent string.
		/// 4 spaces * level
		/// </summary>
		public string Indent
		{
			get
			{
				string indent = new string(' ', 4 * Level);
				return indent;
			}
		}

		public void Append( string cmd )
		{
			mMainExecutionPath += cmd;
		}

		public void Append( CommandTree cmd )
		{
			mMainExecutionPath += cmd.ToString();
		}

		/// <summary>
		/// Provides 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(300);
			
			string tmp = "";
//			string indent = Indent;

//			sb.Append(indent);
			sb.Append(mMainExecutionPath);

			foreach( CommandTree cTree in mBranches )
			{
//				sb.Append(indent);
//				sb.Append("\r\n{\r\n");
				tmp = cTree.ToString();
				sb.Append( tmp );
				sb.Append("//");
//				sb.Append("\r\n");
//				sb.Append(indent);
//				sb.Append("}");
			}
			tmp = sb.ToString();
			return tmp;
		}

	}
}
