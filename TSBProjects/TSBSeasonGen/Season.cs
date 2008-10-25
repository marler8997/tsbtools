using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.IO;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for Season.
	/// </summary>
	public class Season
	{
		protected ArrayList teams;
		private int year=1991;
		private string baseDir = MainClass.LeagueFolder +Path.DirectorySeparatorChar;

		/// <summary>
		/// Generates a season for the given year.
		/// </summary>
		/// <param name="year"> The season you want to generate</param>
		public Season(int year)
		{
			Init();
			this.year = year;
			string dir = baseDir+ year;
			if(!Directory.Exists(dir))
			{
				MainClass.AddError(string.Format(
                    "ERROR!!!\n'{0}' was not found!!",dir));
				Environment.Exit(1);
			}
			string[] teamFiles = Directory.GetFiles(dir);
			Team t;
			for(int i = 0; i < teamFiles.Length; i++)
			{
				if(teamFiles[i].ToString().ToLower().IndexOf("schedule") < 0 &&
					teamFiles[i].EndsWith(".txt") )
				{
					t = new Team(teamFiles[i]);
					t.Year = year;
					SetTeamName( t );
					teams.Add(t); 
				}
			}
		}

		public Season(string configFileName)
		{
			if(File.Exists(configFileName))
			{
				Init();
				string[] lines = InputReader.GetLines(configFileName);
				string line,teamFileName ="";
				Team team_t;
				Regex team        = new Regex("([0-9]+)\\s+([49a-z]+)");
				Regex teamSub     = new Regex("([49a-z]+)\\s*=\\s*([0-9]+)\\s+([49a-z]+)");
				Regex teamFileSub = new Regex("([49a-z]+)\\s*=\\s*([0-9a-z\\\\ \\.\\)\\(:_]+)");
				Regex schedule    = new Regex("schedule(\\s*)=(\\s*)([0-9]+)");
				Match teamMatch, scheduleMatch, subMatch, teamFileMatch;

				for(int i = 0; i < lines.Length; i++)
				{
					line = lines[i].ToLower().Trim();
					teamMatch = team.Match(line);
					scheduleMatch = schedule.Match(line);
					subMatch = teamSub.Match(line);
					teamFileMatch = teamFileSub.Match(line);

					if(line.StartsWith("#") || line == "")
					{
						// do nothing
					}
					else if(scheduleMatch != Match.Empty)
					{
						this.year = Int32.Parse( scheduleMatch.Groups[3].ToString());
					}
					else if(subMatch != Match.Empty)
					{
						// t_loc,   sub_year,   t_sub
						// rams   = 1985        bears
						string t_loc    = subMatch.Groups[1].ToString();
						string sub_year = subMatch.Groups[2].ToString();
						string t_sub    = subMatch.Groups[3].ToString();
						teamFileName    = string.Format("{0}{1}\\{2}.txt",baseDir,sub_year,t_sub);
						team_t          = new Team(teamFileName);
						team_t.teamName = t_loc;
						teams.Add(team_t);
					}
					else if( teamFileMatch != Match.Empty )
					{   // NEW!
						string t_loc    = teamFileMatch.Groups[1].Value;
						teamFileName    = teamFileMatch.Groups[2].Value;
						if( GetTeam(t_loc) != null )
						{
							team_t          = new Team(teamFileName);
							team_t.teamName = t_loc;
							teams.Add(team_t);
						}
					}
					else if( teamMatch != Match.Empty)
					{
						string y = teamMatch.Groups[1].ToString();
						string t = teamMatch.Groups[2].ToString();
						//t = GetTeamName(t);
						teamFileName = string.Format("{0}{1}\\{2}.txt",baseDir,y,t);
						team_t = new Team(teamFileName);
						teams.Add(team_t);
					}
				}
			}
			else
			{
				MainClass.AddError(string.Format(
                    "File '{0}' does not exist.\n",
					configFileName));
			}
			
		}

		public static string GetTeamName(string teamName)
		{
			string ret = teamName;
			
			switch(teamName)
			{
				case "titans": ret = "oilers"; break;
				case "ravens": ret = "browns"; break;
				case "panthers":
				case "jaguars": 
				case "texans": ret = null; break;
			}
			 
			return ret;
		}

		private void Init()
		{
			teams = new ArrayList(40);
			TSBData.InitAttributesFromFiles();
			//TSBData.InitOptions();
		}

		/// <summary>
		/// Generates a season with the given ArrayList of teams passed.
		/// </summary>
		/// <param name="teams">the teams to use in the season.</param>
		public Season(ArrayList teams)
		{
			this.teams = teams;
		}

		/// <summary>
		/// This is where the magic happens.
		/// This is the top level method you call to generate a season of TSB (It writes text that
		/// gets fed into TSBTool.exe).
		/// </summary>
		public virtual string GenerateSeason()
		{
			string ret = "";
			if(teams.Count > 0)
			{
				StringBuilder sb = new StringBuilder(33*35*100);

				sb.Append(string.Format("YEAR = {0}\r\n\r\n",year));

				for(int i =0; i < teams.Count; i++)
				{
					sb.Append(GetTeamString((Team)teams[i])+"\r\n" );
				}
				string schedule;
				string fileName = string.Format("{0}{1}{2}Schedule{1}.txt",baseDir,year,Path.DirectorySeparatorChar);
			
				if(!File.Exists(fileName))
				{
					MainClass.AddError(string.Format(
                     "ERROR!!\nFile '{0}' does not exist!!!",fileName));
					ret = sb.ToString();
				}
				else
				{
					if( MainClass.GenerateSchedule )
					{
						string sch = GetContents(fileName);
						sch = sch.Replace("New York Titans", "New York Jets");
						sch = sch.Replace("Dallas Texans", "Kansas City Chiefs");

						ScheduleHelper helper = new ScheduleHelper(sch);
						schedule = helper.GetSchedule();

						sb.Append(schedule);
					}
					ret = sb.ToString();
				}
			}
			MainClass.ShowErrors();
			return ret;
		}

		string GetContents(string fileName)
		{
			string contents = null;
			try
			{
				StreamReader reader = new StreamReader(fileName);
				contents = reader.ReadToEnd();
				reader.Close();
			}
			catch(Exception e)
			{
				MainClass.AddError(
                 string.Format("ERROR! Season.GetContents() {0}\n",
					e.Message));
			}
			return contents;
		}

		public virtual string GetSchedule(string fileName)
		{
			string contents =null;
			string[] lines= null;
			try
			{
				StreamReader reader = new StreamReader(fileName);
				contents = reader.ReadToEnd();
				reader.Close();
				char[] seps = "\r\n".ToCharArray();
				lines = contents.Split(seps);
			}
			catch(Exception e)
			{
				MainClass.AddError("ERROR! Season.GetSchedule()\n"+e.Message);
			}
			if(lines == null)
			{
				MainClass.AddError("Schedule not generated");
				return null;
			}
			StringBuilder result = new StringBuilder();
			string line ="";
			string team1, team2, g ;
			Regex game = new Regex("([49A-Za-z \\.]+) vs. ([49A-Za-z \\.]+)");
			Match m;
			for(int i = 0; i < lines.Length; i++)
			{
				line = lines[i];
				if(line.ToLower().Trim().StartsWith("week"))
					result.Append(string.Format("\r\n{0}\r\n",line));
				else if(line.Trim() == "")
				;//	result.Append("\r\n");
				else
				{
					m = game.Match(line);
					team1 = GetTeam(m.Groups[1].ToString());
					team2 = GetTeam(m.Groups[2].ToString());
					g = string.Format("{0} at {1}\r\n",team1,team2);
					result.Append(g);
				}
			}
			return result.ToString();
		}
 
		public static string GetTeam(string t)
		{
			for(int i = 0; i < team_names.Length; i++)
			{
				if(t.ToLower().IndexOf(team_names[i]) > -1)
				{
					return team_names[i];
				}
			}
			return null;
		}

		public static string[] team_names =
		{
			"bills",   "colts",  "dolphins", "patriots",  "jets",
			"bengals", "browns", "oilers",   "steelers",
			"broncos", "chiefs", "raiders",  "chargers",  "seahawks",
			"redskins","giants", "eagles",   "cardinals", "cowboys",
			"bears",   "lions",  "packers",  "vikings",   "buccaneers",
			"49ers",   "rams",   "saints",   "falcons", 
            "panthers", "jaguars", "ravens", "titans",  "texans"
		};

		public virtual string GetTeamString(Team team)
		{
			string teamName = team.teamName.ToLower();
			bool stop = false;

			switch(teamName)
			{
				case "titans": teamName = "oilers"; break;
				case "ravens":
					bool browns = false;
					Team t;
					for(int i = 0; i < teams.Count; i++)
					{
						t = (Team)teams[i];
						if(t.teamName.ToLower() == "browns")
							browns = true;
					}
					if(!browns)
						teamName = "browns";
					else
						stop = true;
					break;
				case "panthers":
				case "jaguars": 
				case "texans": stop = true; break;
			}

			if(!stop)
			{
				StringBuilder sb = new StringBuilder(40*100);
				string simData = GetTeamSimData(team);
				sb.Append(string.Format("TEAM={0} SimData={1}\r\n",teamName,simData));
				string [] offense_st = GetOffensiveSkillPlayerString(team);
				sb.Append(offense_st[0]);
				sb.Append(GetOLineString(team));
				sb.Append(GetDefenseString(team));
				sb.Append(GetKickerPunterString(team));
				sb.Append(offense_st[1]);
				sb.Append("\r\n");
				return sb.ToString();
			}
			return "\r\n";
		}

		/// <summary>
		/// Returns an array of 2 strings.
		/// ret[0] = the offensive string.
		/// ret[1] = the KR_PR string.
		/// </summary>
		/// <param name="team"></param>
		/// <returns></returns>
		public string[] GetOffensiveSkillPlayerString(Team team)
		{
			Player qb1,qb2,rb1,rb2,rb3,rb4,wr1,wr2,wr3,wr4,te1,te2=null;
			rb1=rb2=rb3=rb4=null;
			int fb_count = team.CountPositions("FB");
			if(fb_count > 2)
				fb_count =2;
			qb1 = team.GetQBPlayer(1);
			qb2 = team.GetQBPlayer(2);
			
			if(qb2.ranking < qb1.ranking && qb1.passingYards > qb2.passingYards)
			{
				qb2.ranking = qb1.ranking + 5;
			}

			if(qb2 == null)
				rb1=null;
			rb1 = team.GetPlayer("RB","HB",null,null,1);
			switch(fb_count)
			{
				case 0:
					rb2 = team.GetPlayer("RB","HB",null,null,2);
					rb3 = team.GetPlayer("RB","HB",null,null,3);
					rb4 = team.GetPlayer("RB","HB",null,null,4);
					break;
				case 1:
					rb2 = team.GetPlayer("FB",null,null,null,1);
					rb3 = team.GetPlayer("RB","HB",null,null,2);
					rb4 = team.GetPlayer("RB","HB",null,null,3);
					break;
				case 2:
					rb2 = team.GetPlayer("FB",null,null,null,1);
					rb3 = team.GetPlayer("RB","HB",null,null,2);
					rb4 = team.GetPlayer("FB",null,null,null,2);
					break;
			}
			int wr_count = 1;
			wr1 = team.GetPlayer("WR",null,null,null,wr_count++);
			wr2 = team.GetPlayer("WR",null,null,null,wr_count++);
			wr3 = team.GetPlayer("WR",null,null,null,wr_count++);
			wr4 = team.GetPlayer("WR",null,null,null,wr_count++);
			te1 = team.GetPlayer("TE",null,null,null,1);
			te2= team.GetPlayer("TE",null,null,null,2);

			//Take care of run+ shoot teams
			if(rb2.lname.ToLower() == "nobody"){
				rb2 = team.GetPlayer("WR",null,null,null,wr_count++);
				rb3 = team.GetPlayer("WR",null,null,null,wr_count++);
				rb4 = team.GetPlayer("WR",null,null,null,wr_count++);
			}
			else if(rb3.lname.ToLower() == "nobody"){
				rb3 = team.GetPlayer("WR",null,null,null,wr_count++);
				rb4 = team.GetPlayer("WR",null,null,null,wr_count++);
			}
			else if(rb4.lname.ToLower() == "nobody"){
				rb4 = team.GetPlayer("WR",null,null,null,wr_count++);
			}

			// take care of team with no or few tightends
			if(te1.lname.ToLower() == "nobody"){
				te1 = team.GetPlayer("WR",null,null,null,wr_count++);
				te2 = team.GetPlayer("WR",null,null,null,wr_count++);
			}
			else if(te2.lname.ToLower() == "nobody"){
				te2 = team.GetPlayer("WR",null,null,null,wr_count++);
			}

			// make sure the guy with the most Rush yards starts at RB
			if( rb1.rushYards < rb3.rushYards )
			{   // swap players
				Player tmp;
				tmp = rb1;
				rb1 = rb3;
				rb3 = tmp;
			}

			Player returnMan = team.GetTopReturnMan();
			if( returnMan!= null && returnMan.kickRetYards + returnMan.puntRetYards > 500 )
			{
				if( returnMan != rb1 && returnMan != rb2 && returnMan != rb3 && returnMan != rb4 &&
					returnMan != wr1 && returnMan != wr2 && returnMan != wr3 && returnMan != wr4 &&
					returnMan != te1 && returnMan != te2                                            )
				{
					if(wr4.recYards+ wr4.rushYards < rb4.rushYards+rb4.recYards)
						wr4 = returnMan;
					else
						rb4=returnMan;
				}
			}

			string ret = //string.Format(
				//"{0}(1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}",
				GetPlayerString(qb1,"QB1")+
				GetPlayerString(qb2,"QB2")+
				GetPlayerString(rb1,"RB1")+
				GetPlayerString(rb2,"RB2")+
				GetPlayerString(rb3,"RB3")+
				GetPlayerString(rb4,"RB4")+
				GetPlayerString(wr1,"WR1")+
				GetPlayerString(wr2,"WR2")+
				GetPlayerString(wr3,"WR3")+
				GetPlayerString(wr4,"WR4")+
				GetPlayerString(te1,"TE1")+
				GetPlayerString(te2,"TE2");

			
			string KR_PR = getKR_PR(rb1,rb2,rb3,rb4,wr1,wr2,wr3,wr4,te1,te2);

			string[] retArray = new string[2];
			retArray[0] = ret;
			retArray[1] = KR_PR;
			return retArray;
			//return KR_PR +"\r\n"+ ret ;
		}

		public string getKR_PR(Player rb1,Player rb2,Player rb3,Player rb4,
			Player wr1,Player wr2,Player wr3,Player wr4, Player te1, Player te2)
		{
			string kr_ = "RB3";
			string pr_ = "RB3";

			Player kr, pr;
			kr = rb3;
			pr = rb3;
			
			if( rb1.kickRetYards > kr.kickRetYards )
				kr  = rb1;
			if( rb4.kickRetYards > kr.kickRetYards )
				kr = rb4;
			if( wr1.kickRetYards > kr.kickRetYards )
				kr = wr1;
			if( wr2.kickRetYards > kr.kickRetYards )
				kr = wr2;
			if( wr3.kickRetYards > kr.kickRetYards )
				kr = wr3;
			if( wr4.kickRetYards > kr.kickRetYards )
				kr = wr4;
			if( te1.kickRetYards > kr.kickRetYards )
				kr = te1;
			if( te2.kickRetYards > kr.kickRetYards )
				kr = te2;
			
			if( rb1.puntRetYards > pr.puntRetYards )
				pr  = rb1;
			if( rb4.puntRetYards > pr.puntRetYards )
				pr = rb4;
			if( wr1.puntRetYards > pr.puntRetYards )
				pr = wr1;
			if( wr2.puntRetYards > pr.puntRetYards )
				pr = wr2;
			if( wr3.puntRetYards > pr.puntRetYards )
				pr = wr3;
			if( wr4.puntRetYards > pr.puntRetYards )
				pr = wr4;
			if( te1.puntRetYards > pr.puntRetYards )
				kr = te1;
			if( te2.puntRetYards > pr.puntRetYards )
				kr = te2;

			if( kr == rb1 )
				kr_ = "RB1";
			else if( kr == rb2 )
				kr_ = "RB2";
			else if( kr == rb3)
				kr_ = "RB3";
			else if( kr == rb4 )
				kr_ = "RB4";
			else if( kr == wr1 )
				kr_ = "WR1";
			else if( kr == wr2 )
				kr_ = "WR2";
			else if( kr == wr3 )
				kr_ = "WR3";
			else if( kr == wr4 )
				kr_ = "WR4";
			else if( kr == te1 )
				kr_ = "TE1";
			else if( kr == te2 )
				kr_ = "TE2";


			if( pr == rb1 )
				pr_ = "RB1";
			else if( pr == rb2 )
				pr_ = "RB2";
			else if( pr == rb3)
				pr_ = "RB3";
			else if( pr == rb4 )
				pr_ = "RB4";
			else if( pr == wr1 )
				pr_ = "WR1";
			else if( pr == wr2 )
				pr_ = "WR2";
			else if( pr == wr3 )
				pr_ = "WR3";
			else if( pr == wr4 )
				pr_ = "WR4";
			else if( pr == te1 )
				pr_ = "TE1";
			else if( pr == te2 )
				pr_ = "TE2";

			string ret = string.Format("KR,{0}\r\nPR,{1}",kr_,pr_);
			return ret;
		}

		public string GetOLineString(Team team)
		{
			Player lt,lg,c,rg,rt;
			int t_count = team.CountPositions("T");
			int c_count = team.CountPositions("C");
			int g_count = team.CountPositions("G");
			if(t_count < 2|| g_count < 2|| c_count < 1)
			{
				lt = team.GetPlayer("T","G","C",null,1);
				rt = team.GetPlayer("T","G","C",null,4);
				lg = team.GetPlayer("T","G","C",null,3);
				rg = team.GetPlayer("T","G","C",null,5);
				c  = team.GetPlayer("T","G","C",null,2);
			}
			else
			{
				lt = team.GetPlayer("T",null,null,null,1);
				rt = team.GetPlayer("T",null,null,null,2);
				lg = team.GetPlayer("G",null,null,null,2);
				rg = team.GetPlayer("G",null,null,null,1);
				c  = team.GetPlayer("C",null,null,null,1);
			}
			int max, min; max= min=0;
			if(team.offenseRank < 5){
				min = 0;
				max = 15;
			}
			else if(team.offenseRank < 10){
				min = 10;
				max = 25;
			}
			else if( team.offenseRank < 15){
				min = 20;
				max = 35;
			}
			else if(team.offenseRank < 20){
				min = 30;
				max = 45;
			}
			else if(team.offenseRank < 28){
				min = 40;
				max =  5*28;// TSBData.OLmen.Length;
			}
			int cd,ltd,rtd,lgd,rgd;
			/*cd = r.Next(min,max);
			ltd = r.Next(min,max);
			rtd = r.Next(min,max);
			rgd = r.Next(min,max);
			lgd = r.Next(min,max);*/
			cd  = min + (((c.fname.Length  + c.lname.Length ) * 3 ) % 15 );
			ltd = min + (((lt.fname.Length + lt.lname.Length) * 3 ) % 15 );
			rtd = min + (((rt.fname.Length + rt.lname.Length) * 3 ) % 15 );
			rgd = min + (((rg.fname.Length + rg.lname.Length) * 3 ) % 15 );
			lgd = min + (((lg.fname.Length + lg.lname.Length) * 3 ) % 15 );
			string ret = string.Format(
			"{0}{1}{2}{3}{4}",
			GetOLPlayerString(c,"C",cd),
			GetOLPlayerString(lg,"LG",lgd),
			GetOLPlayerString(rg,"RG",rgd),
			GetOLPlayerString(lt,"LT",ltd),
			GetOLPlayerString(rt,"RT",rtd) );
			return ret;
		}


		public string GetDefenseString(Team team)
		{
			Player re,nt,le,lolb,lilb,rolb,rilb,rcb,lcb,fs,ss,db1,db2;
			rcb=lcb=fs=ss=db1=db2=null;
			le = team.GetPlayer("DE","DT","NT",null,1);
			re = team.GetPlayer("DE","DT","NT",null,2);
			nt = team.GetPlayer("DE","DT","NT",null,3);
			rolb = team.GetPlayer("LB",null,null,null,1);
			lolb = team.GetPlayer("LB",null,null,null,2);
			lilb = team.GetPlayer("LB",null,null,null,3);
			rilb = team.GetPlayer("LB",null,null,null,4);

			int s_count = team.CountPositions("S") + 
				          team.CountPositions("SS")+ 
				          team.CountPositions("FS");
			if(s_count > 3 )
				s_count = 3;
			switch(s_count)
			{
				case 0:
					rcb = team.GetPlayer("CB","DB",null,null,1);
					fs  = team.GetPlayer("CB","DB",null,null,3);
					ss  = team.GetPlayer("CB","DB",null,null,2);
					lcb = team.GetPlayer("CB","DB",null,null,4);
					db1 = team.GetPlayer("CB","DB",null,null,5);
					db2 = team.GetPlayer("CB","DB",null,null,6);
					break;
				case 1:
					rcb = team.GetPlayer("CB","DB",null,null,1);
					fs  = team.GetPlayer("CB","DB",null,null,3);
					ss  = team.GetPlayer("S","SS","FS", null,1);
					lcb = team.GetPlayer("CB","DB",null,null,2);
					db1 = team.GetPlayer("CB","DB",null,null,4);
					db2 = team.GetPlayer("CB","DB",null,null,5);
					break;
				case 2:
					rcb = team.GetPlayer("CB","DB",null,null,1);
					fs  = team.GetPlayer("S","SS","FS", null,2);
					ss  = team.GetPlayer("S","SS","FS", null,1);
					lcb = team.GetPlayer("CB","DB",null,null,2);
					db1 = team.GetPlayer("CB","DB",null,null,3);
					db2 = team.GetPlayer("CB","DB",null,null,4);
					break;
				case 3:
					rcb = team.GetPlayer("CB","DB",null,null,1);
					fs  = team.GetPlayer("S","SS","FS", null,2);
					ss  = team.GetPlayer("S","SS","FS", null,1);
					lcb = team.GetPlayer("CB","DB",null,null,2);
					db1 = team.GetPlayer("S","SS","FS",null,3);
					db2 = team.GetPlayer("CB","DB",null,null,3);
					break;
			}
			if(ss.position == "CB"&& rcb.position == "DB"){
				Player tmp = ss;
				ss = rcb;
				ss=tmp;
			}
			if(ss.position == "CB"&& lcb.position == "DB"){
				Player tmp = ss;
				ss = lcb;
				ss=tmp;
			}
			if(fs.position == "CB"&& lcb.position == "DB"){
				Player tmp = fs;
				fs = lcb;
				fs=tmp;
			}
			if(fs.position == "CB"&& rcb.position == "DB"){
				Player tmp = fs;
				fs = lcb;
				fs=tmp;
			}
			string ret = string.Format(
            "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
				GetPlayerString(re,"RE"),
				GetPlayerString(nt,"NT"),
				GetPlayerString(le,"LE"),
				GetPlayerString(rolb,"ROLB"),
				GetPlayerString(rilb,"RILB"),
				GetPlayerString(lilb,"LILB"),
				GetPlayerString(lolb,"LOLB"),
				GetPlayerString(rcb,"RCB"),
				GetPlayerString(lcb,"LCB"),
				GetPlayerString(fs,"FS"),
				GetPlayerString(ss,"SS")); 
			
			if( MainClass.AutoCorrectDefenseSimData )
			{
				ret = AdjustSimPassRush(
					re.sacks,   nt.sacks,   le.sacks,  rolb.sacks, 
					rilb.sacks, lilb.sacks, lolb.sacks, ret).Trim()+"\n";

				ret = AdjustSimPassDefense(
					rolb.ints, rilb.ints,  lilb.ints, lolb.ints, 
					rcb.ints, lcb.ints, fs.ints, ss.ints, ret ).Trim()+"\n";
			}

			if( MainClass.GenerateForSNES )
			{
				string db1Str = GetPlayerString(db1,"DB1");
				string db2Str = GetPlayerString(db2,"DB2");
				int index = db1Str.IndexOf("[");
				
				if( index > -2 )
					db1Str = db1Str.Substring(0, index);
				index = db2Str.IndexOf("[");
				
				if( index > -2 )
					db2Str = db2Str.Substring(0, index);
				
				ret += db1Str + "\n";
				ret += db2Str + "\n";
			}
			return ret;
		}

		private string AdjustSimPassRush( 
			double reSacks,   double ntSacks,   double leSacks,  double rolbSacks,
			double rilbSacks, double lilbSacks, double lolbSacks, 
			string playerData )
		{
			string ret = playerData;
			double totalSacks = reSacks + ntSacks + leSacks + rolbSacks + rilbSacks + lilbSacks + lolbSacks;
			
			int totalSimPoints = TSBData.FRONT_7_SIM_POINT_POOL;
			int minPr          = TSBData.FRONT_7_MIN_SIM_PASS_RUSH;

			int rePoints, ntPoints, lePoints, rolbPoints, rilbPoints, lilbPoints, lolbPoints, ssPoints;
			int dbPoints = 0;
			int cbPoints = 0;
			int front7Points = 0;

			if( totalSacks == 0 )
			{
				rePoints= ntPoints= lePoints= rolbPoints= rilbPoints= lilbPoints= lolbPoints= ssPoints=31;
				rePoints += 4;
			}
			else
			{
				rePoints   = Math.Max( (int)((reSacks   / totalSacks )* totalSimPoints), minPr);
				lePoints   = Math.Max( (int)((leSacks   / totalSacks )* totalSimPoints), minPr);
				ntPoints   = Math.Max( (int)((ntSacks   / totalSacks )* totalSimPoints), minPr);
				rolbPoints = Math.Max( (int)((rolbSacks / totalSacks )* totalSimPoints), minPr);
				rilbPoints = Math.Max( (int)((rilbSacks / totalSacks )* totalSimPoints), minPr);
				lilbPoints = Math.Max( (int)((lilbSacks / totalSacks )* totalSimPoints), minPr);
				lolbPoints = Math.Max( (int)((lolbSacks / totalSacks )* totalSimPoints), minPr);

				front7Points = rePoints + lePoints + ntPoints + rolbPoints + 
					rilbPoints + lilbPoints + lolbPoints;

				dbPoints = (int)(255 - front7Points);
				
				cbPoints = dbPoints / 4 ;
				ssPoints = (int) (255 - ((3*cbPoints) + front7Points)  );
			}

			ret = ReplaceInt( 0, 2 ,rePoints, ret);
			ret = ReplaceInt( 1, 2 ,ntPoints, ret);
			ret = ReplaceInt( 2, 2 ,lePoints, ret);
			ret = ReplaceInt( 3, 2 ,rolbPoints, ret);
			ret = ReplaceInt( 4, 2 ,rilbPoints, ret);
			ret = ReplaceInt( 5, 2 ,lilbPoints, ret);
			ret = ReplaceInt( 6, 2 ,lolbPoints, ret);
			ret = ReplaceInt( 7, 2 ,cbPoints, ret);
			ret = ReplaceInt( 8, 2 ,cbPoints, ret);
			ret = ReplaceInt( 9, 2 ,cbPoints, ret);
			ret = ReplaceInt(10, 2 ,ssPoints, ret);
			
			return ret;
		}

		private string ReplaceInt(int lineNo, int position, int replacement, string playerData)
		{
			string ret ="";
			string[] lines = playerData.Split("\n".ToCharArray());

			if( lineNo > lines.Length - 1 || lineNo < 0 )
			{
				MainClass.AddError(string.Format(
                  "ERROR! ReplaceInt lineNo ={0}, lines={1}",
					lineNo, lines.Length));
				return playerData;
			}

			string target = lines[lineNo];
			target = ReplaceInt(target, position, replacement);

			for( int i = 0; i < lines.Length; i ++)
			{
				if( i != lineNo )
					ret += lines[i]+"\n";
				else
					ret += target+"\n";
			}
			return ret;
		}

		private string ReplaceInt( string line, int positionFromEnd, int replacement)
		{
			string ret ="";
			Regex nums = new Regex("([0-9]+)");
			MatchCollection mc = nums.Matches(line);

			int position = mc.Count - positionFromEnd;
			if(mc[position].Value.Length > 0 )
			{
				int start = mc[position ].Index;
				int end   = start + mc[position].Value.Length;
				ret = line.Substring(0,start);
				ret += replacement + line.Substring(end);
			}
			return ret;
		}

		private string AdjustSimPassDefense(
			int rolbInts, int rilbInts, int lilbInts, int lolbInts,
			int rcbInts,  int lcbInts,  int fsInts,   int ssInts,
			string playerData )
		{
			string ret = playerData;
			double totalInts = rolbInts + rilbInts + lilbInts + lolbInts + rcbInts + lcbInts + fsInts +ssInts;
			double totalSimPoints = 254; 
			int rolbPoints, rilbPoints, lilbPoints, lolbPoints, rcbPoints, lcbPoints, fsPoints, ssPoints;

			rolbPoints   = (int)((rolbInts / totalInts )* totalSimPoints);
			rilbPoints   = (int)((rilbInts / totalInts )* totalSimPoints);
			//lilbPoints = (int)((lilbInts / totalInts )* totalSimPoints);
			lolbPoints   = (int)((lolbInts / totalInts )* totalSimPoints);
			rcbPoints    = (int)((rcbInts  / totalInts )* totalSimPoints);
			lcbPoints    = (int)((lcbInts  / totalInts )* totalSimPoints);
			fsPoints     = (int)((fsInts   / totalInts )* totalSimPoints);
			ssPoints     = (int)((ssInts   / totalInts )* totalSimPoints);

			lilbPoints = 1 + (int)(totalSimPoints 
				- 
				(rcbPoints + lcbPoints + fsPoints + rolbPoints + ssPoints +
				rilbPoints +  lolbPoints));

			ret = ReplaceInt( 0, 1 ,0, ret);
			ret = ReplaceInt( 1, 1 ,0, ret);
			ret = ReplaceInt( 2, 1 ,0, ret);
			ret = ReplaceInt( 3, 1 ,rolbPoints, ret);
			ret = ReplaceInt( 4, 1 ,rilbPoints, ret);
			ret = ReplaceInt( 5, 1 ,lilbPoints, ret);
			ret = ReplaceInt( 6, 1 ,lolbPoints, ret);
			ret = ReplaceInt( 7, 1 ,rcbPoints, ret);
			ret = ReplaceInt( 8, 1 ,lcbPoints, ret);
			ret = ReplaceInt( 9, 1 ,fsPoints, ret);
			ret = ReplaceInt(10, 1 ,ssPoints, ret);
			
			return ret;
		}

		public string GetKickerPunterString(Team team)
		{
			Player k, p;
			k = team.GetPlayer("K",null,null,null,1);
			if(k.position == "X")
				k.skinColor = 1;
			p = team.GetPlayer("P",null,null,null,1);
			if(p.position == "X")
				p.skinColor = 1;
			string k_str = GetPlayerString(k,"K");
			string p_str = GetPlayerString(p,"P");
			// if we don't have a kicker, but do have a punter
			/*if(k_str.IndexOf("NOBODY") > -1 && p_str.IndexOf("NOBODY") == -1)
			{
				k_str = GetPlayerString(k,"P");
			}
			// we don't have a punter, but do have a kicker.
			else if( k_str.IndexOf("NOBODY") == -1 && p_str.IndexOf("NOBODY") > -1 )
			{
				p_str = GetPlayerString(k,"P");
			}*/
			return k_str + p_str;
		}

		private string GetTeamSimData(Team t)
		{
			string ret = "";
			int offense = t.offenseRank;
			int defense = t.defenseRank;
			int offensePref = t.offensePreference;

			//shake things up a bit for those whom suck.
			if(offense > 28)
				//offense = r.Next(22, offenseRanks.Length-1);
				offense = 28;
			if(defense > 28)
				//defense = r.Next(22, offenseRanks.Length-1);
				defense = 28;

			ret = string.Format("{0}{1}{2}", offenseRanks[offense],defensiveRanks[defense],offensePref);

			return ret;
		}

		public virtual string GetPlayerString(Player p, string tecmoPosition)
		{
			string ret ="";
			string jerseyNumber="0";

			bool autoCompensate = true;
			if( tecmoPosition.IndexOf("DB")> -1 )
				autoCompensate = false;

			jerseyNumber = p.GetNumberAlt();
			if(jerseyNumber == null || jerseyNumber =="")
				jerseyNumber = ""+p.jerseyNumber;
			//if(jerseyNumber == null || jerseyNumber =="")
			//	jerseyNumber="0";

			ret = string.Format("{0}, {1} {2}, Face=0x{3}, #{4}, {5}\r\n",
				tecmoPosition, p.fname.ToLower(),p.lname.ToUpper(),p.GetFace(),jerseyNumber,p.GetAttributes(autoCompensate));
			return ret;
		}

		public virtual string GetOLPlayerString(Player p, string tecmoPosition, int attribIndex)
		{
			string ret ="";
			string jerseyNumber="0";
			if(p.jerseyNumber == 0)
				jerseyNumber = p.GetNumberAlt();
			else
				jerseyNumber = ""+p.jerseyNumber;
			if(jerseyNumber == null || jerseyNumber =="")
				jerseyNumber="0";

			ret = string.Format("{0}, {1} {2}, Face=0x{3}, #{4}, {5}\r\n",
				tecmoPosition, p.fname.ToLower(),p.lname.ToUpper(),p.GetFace(),
				jerseyNumber,TSBData.GetOL(attribIndex));
			return ret;
		}

		
/*
		/// <summary>
		/// Returns an arraylist of players that should play on Super Tecmo Bowl.
		/// </summary>
		/// <param name="team"></param>
		/// <returns></returns>
		public virtual ArrayList GetPlayers(Team team)
		{
			ArrayList ret = new ArrayList(40);
			string[] qb = {"QB"}; string[] fb = {"FB"}; string[] rb  = {"RB","HB"};
			string[] te = {"TE"}; string[] wr = {"WR"}; string[] ol  = {"C","G","T"};
			string[] db = {"CB","DB"};                  string[] s   = {"FS","SS","S"};
			string[] dl = {"NT","DE","DT"};             string[] lb  = {"LB"};
			string[] k  = {"K"};                        string[] p   = {"P"};
			string[] qbs = TSBSeasonHelper.GetPlayers(team.players,qb,2); 
			string[] fbs = TSBSeasonHelper.GetPlayers(team.players,pos,2);
			int count =2;
			if(fbs[0] == "")
				count = 4;
			else if(fbs[1]=="")
				count = 3;
			string[] rbs = TSBSeasonHelper.GetPlayers(team.players,rb,count);
			string[] wrs = TSBSeasonHelper.GetPlayers(team.players,wr,4);
			string[] tes = TSBSeasonHelper.GetPlayers(team.players,te,2);
			string[] ols = TSBSeasonHelper.GetPlayers(team.players,wr,5);
			count=2;
			string[] ss =  TSBSeasonHelper.GetPlayers(team.players,s,2);
			if(ss[0] == "")
				count = 4;
			else if(ss[1]=="")
				count = 3;
			string[] dbs = TSBSeasonHelper.GetPlayers(team.players,db,count);
			string[] dls = TSBSeasonHelper.GetPlayers(team.players,dl,3);
			string[] lbs = TSBSeasonHelper.GetPlayers(team.players,lb,4);
			string[] ks   = TSBSeasonHelper.GetPlayers(team.players,k,1);
			string[] ps   = TSBSeasonHelper.GetPlayers(team.players,p,1);
			AddFromArray(ret,qbs);
			if(fbs[1] == "" && fbs[0] == "")
				AddFromArray(ret,rbs);
			else if(fbs[1] =="")
			{
				ret.Add(rbs[0]);
				ret.Add(fb[0]);
				ret.Add(rbs[1]);
				ret.Add(rbs[2]);
			}
			else
			{
				ret.Add(rbs[0]);
				ret.Add(fb[0]);
				ret.Add(rbs[1]);
				ret.Add(fbs[1]);
			}
			//AddFromArray(ret,fbs);
			//AddFromArray(ret,rbs);
			AddFromArray(ret,wrs);
			AddFromArray(ret,tes);
			AddFromArray(ret,ols);
			AddFromArray(ret,dls);
			AddFromArray(ret,lbs);
			AddFromArray(ret,dbs);
			AddFromArray(ret,ss);
			ret.Add(ks[0]);
			ret.Add(ps[0]);
			return ret;
		}*/

		private void AddFromArray(ArrayList list, string[] stuff)
		{
			for(int i=0; i < stuff.Length; i++)
			{
				if(stuff[i] != "")
					list.Add(stuff[i]);
			}
		}

		private void SetTeamName( Team t )
		{
			switch(t.teamName )
			{
				case "Texans":
					if( t.Year < 2000 )
						t.teamName = "Chiefs";
					break;
				case "Titans":
					if( year < 1968 )
						t.teamName = "Jets";
					else if( t.Year > 1998 )
						t.teamName = "Oilers";
					break;
				case "Ravens":
					if( year < 1999)
						t.teamName = "Browns";
					break;

			}
		}


		private string[] offenseRanks= {
		"0xf", 
        "0xf", "0xe", "0xd", "0xc", "0xb", "0xa", "0xa", 
        "0x9", "0x8", "0x8", "0x8", "0x8", "0x7", "0x7", 
        "0x7", "0x6", "0x6", "0x6", "0x5", "0x5", "0x4", 
        "0x4", "0x3", "0x2", "0x2", "0x1", "0x1", "0x0" };

		private string[] defensiveRanks ={
		"f", 
        "f", "e", "d", "c", "c", "b", "b",
		"b", "a", "a", "8", "8", "8", "8",
		"8", "7", "7", "6", "6", "5", "4",
		"4", "4", "4", "3", "3", "1", "0" };
	}
}
