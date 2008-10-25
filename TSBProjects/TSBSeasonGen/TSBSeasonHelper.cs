using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;


namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for TSBSeasonHelper.
	/// </summary>
	public class TSBSeasonHelper : IComparer
	{
		public TSBSeasonHelper()
		{}
		private static Hashtable regexHash = new Hashtable();
		private static TSBSeasonHelper helper = new TSBSeasonHelper();

		/*
		/// <summary>
		/// Extracts the players whom play positions defined in 'positions', sorts them
		/// based on how 'good' they are, and returns a string array of size 'count', with 
		/// the best players in it.
		/// </summary>
		/// <param name="players">The entire array list of a team's players.</param>
		/// <param name="positions">the positions you want to take from players.</param>
		/// <param name="count">the number of players you want returned in the array.</param>
		/// <returns>An array of strings with 'count' playersin it.</returns>
		string[] GetPlayers(ArrayList players, string[] positions, int count)
		{
			int count = 1;

			string[] ret = new string[count];
			for(int i =0; i < ret.Length; i++)
				ret[i]="";

			ArrayList myPlayers = new ArrayList();

			for(int i =0; i < players.Count; i++)
			{
				if(TestStartsWith(positions, players[i].ToString()))
					myPlayers.Add(players[i]);
			}

			myPlayers.Sort( helper );

			for(int i = 0 ; i <ret.Length; i++)
			{
				ret[i]=myPlayers[i];
			}
			return ret;
		}*/

		private bool TestStartsWith(string[] s, string test)
		{
			for(int i=0; i < s.Length; i++)
				if(s[i].StartsWith(test))
					return true;
			return false;
		}

		/// <summary>
		/// returns -9999 on error.
		/// </summary>
		/// <param name="regex_str"></param>
		/// <returns></returns>
		private int GetInt(string regex_str, string line)
		{
			int ret = 0;
			Regex r = GetRegex(regex_str);
			Match m = r.Match(line);
			string j = m.Groups[1].ToString();
			if(j.Length > 0)
				ret = Int32.Parse(j);
			return ret;
		}

		private static Regex GetRegex(string r)
		{
			Regex ret;
			if (regexHash.ContainsKey(r))
				ret = (Regex) regexHash[r];
			else
			{
				ret = new Regex(r);
				regexHash.Add(r,ret);
			}
			return ret;
		}
		#region IComparer Members

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(object x, object y)
		{
			string x_str = x.ToString();
			string y_str = y.ToString();
			int index = x_str.IndexOf(','); // first comma
			int statX, statY =0;

			string position = x_str.Substring(0,index);
			if(position.IndexOf("QB") > -1)
			{
				statX = GetInt("passYards\\s*=\\s*([0-9]+)",x_str);
				statY = GetInt("passYards\\s*=\\s*([0-9]+)",y_str);
				return statX - statY;
			}
			else if(position.Length == 1 && 
				( position.IndexOf("G") == 0 || 
				  position.IndexOf("T") == 0 ||
				  position.IndexOf("C") == 0)   )
			{
				statX = GetInt("seasons\\s*=\\s*([0-9]+)",x_str);
				statY = GetInt("seasons\\s*=\\s*([0-9]+)",y_str);
			}
			else 
			{
				statX = GetInt("ranking\\s*=\\s*([0-9]+)",x_str);
				statY = GetInt("ranking\\s*=\\s*([0-9]+)",y_str);
				return statX - statY;
			}
			return 0;
		}

		#endregion
	}
}
