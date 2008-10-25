using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.IO;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for Team.
	/// </summary>
	public class Team : IComparer
	{
		public ArrayList players;
		public string teamName;
		public int offenseRank, defenseRank, oRushRank, dRushRank, oPassRank, dPassRank,
			       pointsScored, pointsAllowed;
		public bool SuperBowlWinner, SuperBowlLoser;

		public int offensePreference = 2; // 0-3 
		//"#     0 = Little more rushing, 1 = Heavy Rushing,\n"+
		//"#     2 = little more passing, 3 = Heavy Passing.\n"+

		public int Year = 0;

		public Team(string filename)
		{
			players = new ArrayList();
			SetTeamName(filename);
			SetYear(filename);
			string[] lines = InputReader.GetLines(filename);
			if( lines ==null || lines.Length < 1)
			{
				MainClass.AddError(string.Format(
                  "Error! No Data. Does file '{0}' exist?",filename));
				return;
			}
			SetTeamStats(lines[0]);
			players = new ArrayList(60);
			Player p;
			string line;
			for(int i =1; i < lines.Length; i++)
			{
				line= lines[i];
				if(!line.Trim().StartsWith("#") && line.Trim() != "")
				{
					try
					{
						p = new Player(line,this);
						players.Add(p);
					}
					catch(Exception e)
					{
						MainClass.AddError(string.Format(
                          "Error! Could not Add Player. Line '{0}', File='{1}',",
							 line, filename));
					}
				}
			}
			players.Sort(this);
		}

		public int CountPositions(string pos)
		{
			int ret=0;
			Player p;
			for(int i =0; i < players.Count; i++)
			{
				p= (Player)players[i];
				if(p.position == pos)
					ret++;
			}
			
			return ret;
		}

		/// <summary>
		/// Returns the player at depth 'depth', playing at pos1-pos4.
		/// </summary>
		/// <param name="pos1"></param>
		/// <param name="pos2"></param>
		/// <param name="pos3"></param>
		/// <param name="pos4"></param>
		/// <param name="depth"></param>
		/// <returns></returns>
		public Player GetPlayer(string pos1, string pos2, string pos3, string pos4, int depth)
		{
			int d = 0;
			Player p;
			for(int i =0; i < players.Count; i++)
			{
				p = (Player)players[i];
				if(p.position == pos1 || p.position == pos2 || 
				   p.position == pos3 || p.position == pos4   )
				{
					d++;
					if(d == depth)
						return p;
				}
			}
			return new Player("X,Nobody,Joe,skinColor=0,HOF=False,#0,description='',seasons=0",this);
		}

		public Player GetTopReturnMan()
		{
			Player returnMan = null;
			if( players.Count > 0 )
			{
				returnMan = (Player)players[0];

				foreach( Player p in players )
				{
					if(p.kickRetYards + p.puntRetYards > returnMan.puntRetYards+ returnMan.kickRetYards)
						returnMan = p;
				}
			}
			return returnMan;
		}

		/// <summary>
		/// Gets the QB at depth 'depth'.
		/// </summary>
		/// <param name="depth"></param>
		/// <returns></returns>
		public Player GetQBPlayer( int depth)
		{
			ArrayList qbs = new ArrayList(10);
			Player p;

			for(int i =0; i < players.Count; i++)
			{
				p = (Player)players[i];
				if(p.position == "QB")
					qbs.Add(p);
			}
			qbs.Sort(this);
			if(depth > qbs.Count || depth < 1)
				p = new Player("X,Nobody,Joe,skinColor=0,HOF=False,#0,description='',seasons=0",this);
			else
				p = (Player)qbs[depth-1];
			return p;
		}

		private void SetTeamName(string file)
		{
			/*int slash = file.LastIndexOf("\\");
			if(slash < 0 )
				slash = file.LastIndexOf("/");*/

			int slash = file.LastIndexOf(Path.DirectorySeparatorChar);
			int dot = file.LastIndexOf(".");
			if(slash > 0 && dot > 0)
				teamName = file.Substring(slash+1,dot-slash-1);
			else
			{
				MainClass.AddError(string.Format(
					"Error! Could not extract team name from '{0}'",file));
			}
		}
		private static Regex yearRegex = null;

		private void SetYear( string fileName)
		{
			if( yearRegex == null)
				yearRegex = new Regex("([0-9]{4})");
			
			Match m = yearRegex.Match(fileName);
			if( m != Match.Empty )
			{
				try
				{
					Year = Int32.Parse(m.Groups[1].ToString());
				}
				catch{}
			}
		}

		private void SetTeamStats(string line)
		{
			this.defenseRank = GetInt("Defense\\s*=\\s*([0-9]+)",  line);
			this.offenseRank = GetInt("Offense\\s*=\\s*([0-9]+)",  line);
			this.oPassRank   = GetInt("O_Passing\\s*=\\s*([0-9]+)",line);
			this.oRushRank   = GetInt("O_Rushing\\s*=\\s*([0-9]+)",line);
			this.dPassRank   = GetInt("D_Passing\\s*=\\s*([0-9]+)",line);
			this.dRushRank   = GetInt("D_Rushing\\s*=\\s*([0-9]+)",line);
			this.pointsAllowed = GetInt("PA\\s*=\\s*([0-9]+)",line);
			this.pointsScored = GetInt("PF\\s*=\\s*([0-9]+)",line);
			if(line.IndexOf("SuperBowlWinner") > -1)
				this.SuperBowlWinner=true;
			if(line.IndexOf("SuperBowlLoser") > -1)
				this.SuperBowlLoser=true;
			//     0 = Little more rushing, 1 = Heavy Rushing,
			//     2 = little more passing, 3 = Heavy Passing.
			int difference = oPassRank - oRushRank;
			if( difference < 0 )
				difference = -difference;

			if( oPassRank < oRushRank)
			{
				if( difference > TSBData.HEAVY_RUN_PASS_MARGIN )
					offensePreference = 3; // heavy pass
				else
					offensePreference = 2; // light pass
			}
			else
			{
				if( difference > TSBData.HEAVY_RUN_PASS_MARGIN )
					offensePreference = 1; // heavy rush
				else
					offensePreference = 0; // light rush
			}
		}

		/// <summary>
		/// Iterates through our list of players returning the players who play position
		/// 'position'.
		/// </summary>
		/// <param name="position">The position the player plays.</param>
		/// <returns>An ArrayList of strings.</returns>
		public ArrayList GetPlayersAtPosition(string position)
		{
			string player="";
			ArrayList ret = new ArrayList(12);

			for(int i =0; i < players.Count; i++)
			{
				player = (string)players[i];
				if(player.StartsWith(position))
					ret.Add(player);
			}
			return ret;
		}

		/*
		/// <summary>
		/// Gets the player at position 'position' at depth 'depth'.
		/// Example: GrtPlayer("QB", 2); //==> gets 2nd string QB.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="depth">1,2,3,4 Do not pass 0.</param>
		/// <returns></returns>
		public Player GetPlayer(string position, int depth)
		{

			ArrayList players = GetPlayersAtPosition(position);
			players.Sort(this);
			if(depth > players.Count|| depth == 0)
				return null;
			return players[depth-1];
		}*/


		/// <summary>
		/// returns 0 on error.
		/// </summary>
		/// <param name="regex_str"></param>
		/// <returns></returns>
		private int GetInt(string regex_str, string line)
		{
			int ret = 0;
			Regex r = new Regex(regex_str);
			Match m = r.Match(line);
			string j = m.Groups[1].ToString();
			try
			{
				if(j.Length > 0)
					ret = Int32.Parse(j);
			}
			catch//(Exception e)
			{
				MainClass.AddError(string.Format(
                  "Error for Team {0}, line ='{1}'.",teamName,line));
			}
			return ret;
		}
		#region IComparer Members
		private static string[] kr_positions = {"WR","RB","HB"};
		private static ArrayList krPositions =null;
   

		public int Compare(object x, object y)
		{
			if(krPositions == null)
				 krPositions = new ArrayList(kr_positions );

			Player xp =(Player)x;
			Player yp =(Player)y;

			int statX = xp.ranking;
			int statY = yp.ranking;

			string position = xp.position;
			if( xp.position == "QB" && xp.position == yp.position )
			{
				statX = xp.passingYards;
				statY = yp.passingYards;
				return statY - statX; // because here, more is better.
			}
			else if(xp.position.Length == 1 && yp.position.Length == 1 && 
				( position.IndexOf("G") == 0 || 
				position.IndexOf("T") == 0 ||
				position.IndexOf("C") == 0)   )
			{
				// if they they both have or both don't have a jersey number
				// use seasons played as a sort criteria.
				if( ( xp.jerseyNumber  > 0 && yp.jerseyNumber  > 0)||
					( xp.jerseyNumber == 0 && yp.jerseyNumber == 0)  )
				{
					statX = -xp.seasons;
					statY = -yp.seasons;
				}
					// otherwise use a jersey number
				else //if(xp.jerseyNumber > 0)
				{   
					statX = xp.jerseyNumber;
					statY = xp.jerseyNumber;
				}
			}
			/*else if( krPositions.Contains(xp.position) && krPositions.Contains(yp.position) )
			{
				if(xp.position == yp.position && yp.position == "WR")
				{
					if(yp.recYards < 300 && xp.recYards < 300)
					{
						statX = xp.recYards + xp.puntRetYards + xp.kickRetYards;
						statY = yp.recYards + yp.puntRetYards + yp.kickRetYards;
					}
				}
				if(xp.position == yp.position && yp.position == "RB" || yp.position=="HB")
				{
					if(yp.recYards < 200 && xp.recYards < 200)
					{
						statX = xp.rushYards + xp.puntRetYards + xp.kickRetYards;
						statY = yp.rushYards + yp.puntRetYards + yp.kickRetYards;
					}
				}
			}*/
			else 
			{
				statX = xp.ranking;
				statY = yp.ranking;
			}
			// check this to make sure order is correct.
			int ret =  statX - statY;
			return ret;
		}

		#endregion
	}
}
