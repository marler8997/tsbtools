using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for ConfigFileParser.
	/// </summary>
	public class ConfigFileUtil
	{
		public ConfigFileUtil()
		{
		}

		public static ArrayList GetConfigFileElements(string configFileName)
		{
			string[] lines = InputReader.GetLines(configFileName);
			ArrayList elements = new ArrayList();

			string line,
				   teamFileName ="";
			string baseDir = MainClass.LeagueFolder +Path.DirectorySeparatorChar;

			ConfigFileElement current;

			Regex schedule    = new Regex("schedule(\\s*)=(\\s*)([0-9]+)");
			Regex team        = new Regex("([0-9]+)\\s+([49a-z]+)");
			Regex teamSub     = new Regex("([49a-z]+)\\s*=\\s*([0-9]+)\\s+([49a-z]+)");
			Regex teamFileSub = new Regex("([49a-z]+)\\s*=\\s*([0-9a-z\\\\ \\.\\)\\(:_]+)");
			Match teamMatch, subMatch, scheduleMatch, teamFileMatch;

			for(int i = 0; i < lines.Length; i++)
			{
				line = lines[i].ToLower().Trim();
				teamMatch = team.Match(line);
				subMatch = teamSub.Match(line);
				teamFileMatch = teamFileSub.Match(line);
				scheduleMatch = schedule.Match(line);

				current = null;

				if(line.StartsWith("#") || line == "")
				{
					// do nothing
				}
				else if(subMatch != Match.Empty)
				{
					// t_loc,   sub_year,   t_sub
					// rams   = 1985        bears
					string t_loc    = subMatch.Groups[1].ToString();
					string sub_year = subMatch.Groups[2].ToString();
					string t_sub    = subMatch.Groups[3].ToString();
					teamFileName    = string.Format("{0}{1}\\{2}.txt",baseDir,sub_year,t_sub);
					
					current              = new ConfigFileElement();
					current.Type         = ConfigFileElementType.TeamFromYear;
					//current.InRomTeam    = Season.GetTeamName( t_loc);
					current.InRomTeam    = t_loc;
					current.RealTeam     = t_sub;
					current.Year         = Int32.Parse(sub_year);
					current.TeamFileName = teamFileName;
				}
				else if(scheduleMatch != Match.Empty)
				{
					current       = new ConfigFileElement();
					current.Type  = ConfigFileElementType.Schedule;
					current.Year  = Int32.Parse( scheduleMatch.Groups[3].ToString());
				}
				else if( teamFileMatch != Match.Empty )
				{   // NEW!
					string t_loc    = teamFileMatch.Groups[1].Value;
					teamFileName    = teamFileMatch.Groups[2].Value;
					if( GetTeam(t_loc, elements) != null )
					{
						current              = new ConfigFileElement();
						current.Type         = ConfigFileElementType.TeamFromFile;
						//current.InRomTeam    = Season.GetTeamName( t_loc);
						current.InRomTeam    =  t_loc;
						current.TeamFileName = teamFileName;
					}
				}
				else if( teamMatch != Match.Empty )
				{
					string _year = teamMatch.Groups[1].ToString();
					string _team = teamMatch.Groups[2].ToString();

					string _realTeam = Season.GetTeamName(_team);
					//_team      =  Season.GetTeamName(_team);
					_team        =  _team;
					teamFileName = string.Format("{0}{1}\\{2}.txt",baseDir,_year,_realTeam);
					
					current              = new ConfigFileElement();
					current.Type         = ConfigFileElementType.TeamFromYear;
					current.InRomTeam    = _team;
					current.RealTeam     = _team;
					current.TeamFileName = teamFileName;
					current.Year         = Int32.Parse(_year);
					if(! File.Exists(teamFileName) )
					{
						MainClass.AddError(string.Format(
                           "ERROR! No data for team '{0} {1}'. Did they exist in this year?",
							current.Year,current.RealTeam));
					}
				}

				if( current != null )
				{
					elements.Add(current);
				}
			}
			return elements;
		}

		public static string GetTeam(string t, ArrayList teams)
		{
			for(int i = 0; i < Season.team_names.Length; i++)
			{
				if(t.ToLower().IndexOf(Season.team_names[i]) > -1)
				{
					return Season.team_names[i];
				}
			}
			return null;
		}
	}

	public enum ConfigFileElementType
	{
		Schedule,
		TeamFromYear,
		TeamFromFile
	}


	public class ConfigFileElement
	{
		public ConfigFileElementType Type;
		public string                InRomTeam;
		public string                RealTeam;
		public int                   Year;
		public string                TeamFileName;
	}
}
