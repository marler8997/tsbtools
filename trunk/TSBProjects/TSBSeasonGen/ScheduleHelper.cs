using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for ScheduleHelper.
	/// </summary>
	public class ScheduleHelper
	{
		private string[][] weeks;
		private string schedule;
		private ArrayList extraGames, funkyTeams;

		private int[] gamesPerWeek = 
			{14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14};
			//{14,14,14,14,12,12,11,12,12,12,14,14,14,13,14,14,14};
		/// <summary>
		/// 
		/// </summary>
		/// <param name="schedule">The text of a schedule file.</param>
		public ScheduleHelper(string schedule)
		{
			string[]fteams ={ "ravens", "panthers", "jaguars", "titans", "texans"};
			funkyTeams = new ArrayList( fteams);
			extraGames = new ArrayList(30);
			this.schedule = schedule;
			int i =0;
			weeks = new string[17][];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			weeks[i]  = new string[gamesPerWeek[i++]];
			FillSchedule();
		}

		private Stack teamStack = null;

		/// <summary>
		/// Pushes a team onto a stack.
		/// </summary>
		/// <param name="team"></param>
		private void PushTeam(string team)
		{
			if(teamStack == null)
				teamStack = new Stack();
			teamStack.Push(team);
		}

		/// <summary>
		/// Returns the top team on the stack.
		/// </summary>
		/// <returns></returns>
		private string PopTeam()
		{
			string ret = null;
			if(teamStack == null || teamStack.Count == 0)
				ret = null;
			else
				ret = teamStack.Pop().ToString();
			return ret;
		}

		/// <summary>
		/// This Function takes the entire season schedule and schedules all the games.
		/// </summary>
		private void FillSchedule()
		{
			char[] seps = "\r\n".ToCharArray();
			string[] lines = schedule.Split(seps);
			string line,g,team1,team2;
			Regex game = new Regex("([49A-Za-z \\.]+) vs. ([49A-Za-z \\.]+)");
			Match m;
			int week = -1;
			int gameOfWeek = 0;
			for(int i = 0; i < lines.Length; i++)
			{
				line = lines[i];
				if(line.ToUpper().IndexOf("WEEK") > -1)
				{
					week++;
					gameOfWeek = 0;
				}
				else if(line.Trim() == "")
					;//	result.Append("\r\n");
				else // games
				{
					m = game.Match(line);
					if( m!= Match.Empty )
					{
						team1 = Season.GetTeam(m.Groups[1].ToString());
						team2 = Season.GetTeam(m.Groups[2].ToString());

						g = GetGameString(team1,team2);
						if(g != null)
						{
							if(week < weeks.Length && gameOfWeek < gamesPerWeek[week])
								weeks[week][gameOfWeek]=g;
							else
							{
								extraGames.Add(g);
							}
							gameOfWeek++;
						}
					}
					else
					{
						MainClass.AddError(string.Format("Error! Line '{0}'",line));
					}
				}
			}
			AddExtraGames();
			EnsureAtLeast1Game();
		}

		/// <summary>
		/// In most cases, will return a string like "team1 at. team2",
		/// but if one of the teams are the any of the following 
		/// { "ravens", "panthers", "jaguars", "titans", "texans"} the
		/// string returned may be null, or may be different.
		/// </summary>
		/// <param name="team1"></param>
		/// <param name="team2"></param>
		/// <returns></returns>
		private string GetGameString(string team1, string team2)
		{
			string t1, t2, ret;
			ret = null;
			
			t1 = GetTeamName(team1);
			t2 = GetTeamName(team2);

			if(t1 == null && t2 == null)
			{
				ret = null;
			}
			else if(t1 == null)
			{
				t1 = PopTeam();
				if(t1 == null)
					PushTeam(t2);
				else
					ret = string.Format("{0} at {1}",t1,t2);
			}
			else if(t2 == null)
			{
				t2 = PopTeam();
				if(t2 == null)
					PushTeam(t1);
				else
					ret = string.Format("{0} at {1}",t1,t2);
			}
			else 
			{
				ret = string.Format("{0} at {1}",t1,t2);
			}
			return ret;
			//string g = string.Format("{0} at {1}",team1,team2);
			//return g;
		}

		private string GetTeamName(string teamName)
		{
			string ret = teamName;
			if(funkyTeams.Contains(teamName))
			{
				switch(teamName)
				{
					case "titans": ret = "oilers"; break;
					case "ravens":
						if(this.schedule.IndexOf("Browns") == -1 && this.schedule.IndexOf("browns") == -1)
							ret = "browns";
						else
							ret = null;
						break;
					case "panthers":
					case "jaguars": 
					case "texans": ret = null; break;
				}
			} 
			return ret;
		}
		

		public string GetSchedule()
		{
			StringBuilder sb = new StringBuilder(17*15*20);
			string g = "";
			for(int week = 0; week < weeks.Length; week++)
			{
				sb.Append(string.Format("\nWEEK {0}\n",week+1));
				for(int game = 0; game < weeks[week].Length; game++)
				{
					g  = weeks[week][game]; 
					if(  g != null && g != "" )
						sb.Append(string.Format("{0}\n",g));
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Not all schedules have the same amount of games per week as the 1991 schedule.
		/// This function will take the games that we postponed scheduling, and find an empty 
		/// slot in the schedule to play it.
		/// </summary>
		private void AddExtraGames()
		{
			string game;
			int week, gameOfWeek;
			for(int i = 0; i < extraGames.Count; i++)
			{
				game = (string)extraGames[i];
				week = GetFreeGameWeek();
				gameOfWeek = GetFreeGame(week);
				if(week < 0 || gameOfWeek < 0)
					return;
				else
				{
					weeks[week][gameOfWeek] = game;
					Console.Error.WriteLine("Moved game '{0}' to game {1} of week {2}",
						game,gameOfWeek,week+1);
				}
			}
		}

		/// <summary>
		/// Makes sure that at least 1 game is in each week.
		/// </summary>
		private void EnsureAtLeast1Game()
		{
			int i=0, lastWeek = 18;
			string s;
			ArrayList tmp = new ArrayList(14);
			// find last week with a game
			for(  i = 0; i < weeks.Length ; i++)
			{
				if( weeks[i][0] == null || weeks[i][0] == "")
				{
					lastWeek = i - 1;
					for( int j = 0; j < weeks[lastWeek].Length; j ++)
					{ // fill up the last week;
						s = weeks[lastWeek][j];
						if( s != null && s != "")
						{
							tmp.Add(s);
							weeks[lastWeek][j] = null;
						}
					}
					break;
				}
			}
			object g;
			for(  int j = weeks.Length-1; j > lastWeek; j-- )
			{
				g = tmp[tmp.Count-1];
				weeks[j][0] = g.ToString();
				tmp.Remove(g);
			}
			for( int j = 0; j < tmp.Count; j++)
			{
				weeks[lastWeek][j] = tmp[j].ToString();
			}

		}

		/// <summary>
		/// Returns the first week (number, 0-16) week that has a free slot for a game.
		/// Returns -1 on error.
		/// </summary>
		/// <returns></returns>
		private int GetFreeGameWeek()
		{
			int week;
			for(int i=0; i < weeks.Length; i++)
			{
				week = GetFreeGame(i);
				if(week > -1)
					return i;
			}
			return -1;
		}

		/// <summary>
		/// Returns the game number of week 'week' that is still free.
		/// Returns -1 on error.
		/// </summary>
		/// <param name="week"></param>
		/// <returns></returns>
		private int GetFreeGame(int week)
		{
			for(int i =0; i< weeks[week].Length;i++)
			{
				if(weeks[week][i] == null)
					return i;
			}
			return -1;
		}

	}
}
