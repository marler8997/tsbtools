using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for InputReader.
	/// </summary>
	public class InputReader
	{
		
		public InputReader()
		{

		}

		public static string[] GetLines(string fileName)
		{ 
			string[] lines = null;
			try
			{
				StreamReader sr = new StreamReader(fileName);
				string contents = sr.ReadToEnd();
				sr.Close();
				char[] seps = "\r\n".ToCharArray();
				lines = contents.Split(seps);
			}
			catch(Exception e)
			{
				MainClass.AddError(string.Format(
                   "ERROR! {1}\nproblem reading file {0}\n.",
					fileName, e.Message));
			}
			return lines;
		}

		/// <summary>
		/// Get The players that play at 'position'.
		/// </summary>
		/// <param name="position">The position the player plays (ie "QB", "RB", "T","C","WR"...).</param>
		/// <returns>An ArrayList of strings in the NFLData form.</returns>
		public ArrayList GetPlayers(string position, string[] lines)
		{
			ArrayList ret = new ArrayList();
			string line;
			for(int i = 0; i < lines.Length; i++)
			{
				line = lines[i];
				if(line.StartsWith(position))
					ret.Add(line);
			}
			return ret;
		}
	}
}
