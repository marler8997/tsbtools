using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for Player.
	/// </summary>
	public class Player
	{
		protected InputReader reader;
		private static Hashtable faceHash = null;

		// interesting stats
		public bool    HOF;
		public string  description,position, fname, lname;
		public int     skinColor,jerseyNumber,speed,ranking,seasons;
		public int     intTD, fumbleTD, tackles, fumblesforced, ints;
		public int     receptions,rushes,rushYards,recYards,td; 
		public int     puntRet, puntRetYards, kickRet, kickRetYards;
		public double  sacks, passerRating;
		public int     passingYards, passTD, passAtt, passComp, passInt;
		public Team team;
		private static string dataDir = "TSB_Data"+Path.DirectorySeparatorChar;

		public double fumblepercent;

		private Regex descriptionRegex = null;
		private static Hashtable regexHash= new Hashtable();
		private static Hashtable numberHash= null;
		private static Hashtable attrHash = new Hashtable();

		public Player(string line, Team t)
		{
			ranking = 26;
			this.team = t;
			init(line);
		}

		private void init(string line)
		{
			char [] seps = ",\r\n".ToCharArray();
			string[] attributes = line.Split(seps);

			position = attributes[0];
			lname    = attributes[1];
			fname    = attributes[2];
			jerseyNumber = Int32.Parse(attributes[5].Replace("#",""));
			skinColor = GetInt("skinColor\\s*=\\s*([0-9])",line);
			if(line.ToLower().IndexOf("hof=true") > -1)
				HOF = true;
			description = GetDescription(line);
			ranking = GetInt("ranking\\s*=\\s*([0-9]+)",line);
			seasons = GetInt("seasons\\s*=\\s*([0-9]+)",line);
			//defensive stats
			sacks = GetDouble("sacks\\s*=\\s*([0-9\\.]+)",line); 
			tackles = GetInt("tackles\\s*=\\s*([0-9]+)",line); 
			intTD  = GetInt("intTD\\s*=\\s*([0-9]+)",line);
			fumbleTD = GetInt("fumbleTD\\s*=\\s*([0-9]+)",line);
			fumblesforced = GetInt("fumblesForced\\s*=\\s*([0-9]+)",line);
			ints = GetInt("ints\\s*=\\s*([0-9]+)",line);
			// offensive yards
			fumblepercent = GetDouble("fumblepercent\\s*=\\s*([0-9\\.]+)",line);
			receptions = GetInt("rec\\s*=\\s*([0-9]+)",line);
			rushes = GetInt("rushes\\s*=\\s*([0-9]+)",line);
			recYards = GetInt("recYards\\s*=\\s*(-?[0-9]+)",line);
			rushYards = GetInt("rushYards\\s*=\\s*(-?[0-9]+)",line);
			td = GetInt("td\\s*=\\s*([0-9]+)",line);
			// passing stats
			passerRating = GetDouble("passRat\\s*=\\s*([0-9\\.]+)",line);
			passingYards = GetInt("passYards\\s*=\\s*([0-9]+)",line);
			passAtt = GetInt("passAtt\\s*=\\s*([0-9]+)",line);
			passComp = GetInt("passComp\\s*=\\s*([0-9]+)",line);
			passTD = GetInt("passTD\\s*=\\s*([0-9]+)",line);
			passInt = GetInt("passInt\\s*=\\s*([0-9]+)",line);
			//special teams stats
			kickRet = GetInt("kickRet\\s*=\\s*([0-9]+)",line);
			kickRetYards =  GetInt("kickRetYards\\s*=\\s*([0-9]+)",line);
			puntRet = GetInt("puntRet\\s*=\\s*([0-9]+)",line);
			puntRetYards =  GetInt("puntRetYards\\s*=\\s*([0-9]+)",line);
		}


		private static Regex GetRegex(string r)
		{
			Regex ret;
			if (regexHash.ContainsKey(r))
				ret = (Regex) regexHash[r];
			else{
				ret = new Regex(r);
				regexHash.Add(r,ret);
			}
			return ret;
		}

		/// <summary>
		/// returns -9999 on error.
		/// </summary>
		/// <param name="regex_str"></param>
		/// <returns></returns>
		private int GetInt(string regex_str, string line)
		{
			int ret = -9999;
			Regex r = GetRegex(regex_str);
			Match m = r.Match(line);
			string j = m.Groups[1].ToString();
			if(j.Length > 0)
				ret = Int32.Parse(j);
			return ret;
		}

		private double GetDouble(string regex_str, string line)
		{
			double ret = -9999;
			Regex r = GetRegex(regex_str);
			Match m = r.Match(line);
			string j = m.Groups[1].ToString();
			if(j.Length > 0)
				ret = Double.Parse(j);
			return ret;
		}

		private string GetDescription(string line)
		{
			string ret = "";
			if(descriptionRegex==null)
				descriptionRegex= new Regex("description\\s*=\\s*'(.*)'");
			Match m = descriptionRegex.Match(line);
			
			ret = m.Groups[1].ToString();
			return ret;
		}

		//Random random = new Random();

		// faces for only qb,wr,qb,p,k
		private static int[] wr_qb_pk_only = {0x4a, 0x07, 0x10, 0x11, 0x22, 0x23, 0x21,
		                                      0x25, 0x26, 0x42, 0x85, 0xa0, 0xab, 0xb3, 0xb6 };
		// faces for only lineman,fb,te
		private static int[] fb_lm_te_only = {0x1c, 0x8c, 0x24, 0x35, 0x38, 0x45, 0x46,
		                                      0x47, 0x80, 0x82, 0x83, 0xa7, 0xa8, 0xac,
		                                      0xae, 0xaf, 0xb , 0xc,  0xb4, 0xbb ,0xbd, 0xbe, 
											  0xbf, 0xc5, 0xc6, 0xce  };
		// don't use these faces (they look dumb)
		private static int[] dont_use = {0x00, 0xd, 0x1c, 0x52, 0x53, 0x36, 0x49, 0x05, 0x83, 0xa9, 0xd4 };

		static bool IN(int i_, int[] set_)
		{
			for(int i = 0; i < set_.Length; i++)
				if( i_ == set_[i])
					return true;
			return false;
		}

		private static string GetPositionForSearch(string p)
		{
			string ret = "";
			string pos = p .ToLower();

			if(pos.StartsWith("rb")|| pos.StartsWith("fb") || pos.StartsWith("hb") || pos.StartsWith("tb") )
				ret = "rb:";
			else if(pos.StartsWith("wr") || pos.StartsWith("e") )
				ret= "wr:";
			else if( pos.StartsWith("te") )
				ret = "te:";
			else if( pos.StartsWith("c") || pos.IndexOf("g") > -1 || pos.StartsWith("rt") || pos.StartsWith("lt") || pos.StartsWith("t") )
				ret = "ol:";
			else if(pos.StartsWith("db") || pos.IndexOf("cb") > -1|| pos.StartsWith("s") || pos.StartsWith("ss") || pos.StartsWith("fs"))
				ret = "db:";
			else if( pos.StartsWith("k")|| pos.StartsWith("p") )
				ret = "k:";
			else if( pos.IndexOf("qb") > -1 )
				ret = "qb:";
			
			return ret;
		}

		/// <summary>
		/// If name is not in face hash, a face will be randomly assigned based on
		/// skin color.
		/// </summary>
		/// <returns></returns>
		public string GetFace()
		{
			string name = string.Format("{0} {1}", fname.ToLower(),lname.ToLower() );
			string name2 = string.Format("{0}. {1}", fname.ToLower()[0],lname.ToLower() );

			string ret;
			if(faceHash == null)
				PopulateFaceHash();

			name  = GetPositionForSearch(this.position) + name;
			name2 = GetPositionForSearch(this.position) + name2;

			if(faceHash.ContainsKey(name))
			{
				ret = (string)faceHash[name];
				return ret;
			}
			else if( faceHash.ContainsKey(name2))
			{
				ret = (string)faceHash[name2];
				return ret;
			}

			//skin color guess
			// if the player is a QB with a '0' skin color (unknown) we will
			// assume '1' (white)
			if(skinColor == 0 && (position == "QB"|| position == "K" || 
				                  position == "P" ))
				skinColor = 1;
			else if(skinColor == 0)
				skinColor = 4;

			int	min = 0x1;
			int max = 0x51;

			if(skinColor > 2)
			{
				min = 0x80;
				max = 0xd3;
			}
			int f = min + fname.Length+lname.Length;
			switch(position)
			{
				case "QB": case "WR": case "K":	case "P":
					while( IN(f,fb_lm_te_only) || IN(f,dont_use) )
					{
						f++;
						if( f >= max)
							f=min;
					}
				break;
				case "RB": case "HB":
				case "CB": case "DB": case "S": case "SS": case "FS":
				case "LB":
					while( IN(f,fb_lm_te_only) || IN(f,dont_use) || IN(f,wr_qb_pk_only) )
					{
						f++;
						if( f >= max)
							f=min;
					}
				break;				
				
				case "FB": case "TE":
				case "G":  case "T": case "C":
				case "DE": case "DT": case "NT":
					while( IN(f,dont_use) || IN(f,wr_qb_pk_only) )
					{
						f++;
						if( f >= max)
							f=min;
					}
				break;
			}

			if( lname.ToUpper() == "NOBODY")
			{
				if( skinColor == 4 )
					f = 0xd4;
				else
					f = 0x52;
			}
			ret = string.Format("{0:x}",f);
			//ret = string.Format("{0:x}",random.Next(min,max));
			return ret;
		}

		public string GetNumberAlt()
		{
			string name = string.Format("{0} {1}", fname.ToLower(),lname.ToLower() );
			string ret = null;
			if(numberHash == null)
				PopulateNumberHash();

			if(position.ToLower().StartsWith("rb")|| position.ToLower().StartsWith("fb"))
				name = GetPositionForSearch(this.position) + name;
			else if(position.ToLower().StartsWith("wr"))
				name = GetPositionForSearch(this.position) + name;
			
			if(numberHash.ContainsKey(name))
				ret = (string)numberHash[name];

			return ret;
		}


		private static void PopulateFaceHash()
		{
			faceHash = new Hashtable(5000);
			string faceFile =  dataDir+ "NumberFaceData.txt";
			if(!File.Exists(faceFile))
				return;
			string name, face,line;
			try
			{
				StreamReader sr = new StreamReader(faceFile);
				string data = sr.ReadToEnd();
				sr.Close();
				data = data.ToLower();
				
				char[] seps = "\r\n".ToCharArray();
				string[] lines = data.Split(seps);
				Regex faceRegex = new Regex("face\\s*=\\s*0x([a-z0-9]+)");
				Regex nameRegex = new Regex("^[a-z 1-4]+,\\s*([a-z \\.]+)");
				Match m1,m2;
				string pos="";
				for(int i =0; i < lines.Length; i++)
				{
					line = lines[i];
					m1 = faceRegex.Match(line);
					if(m1 != Match.Empty)
					{
						m2 = nameRegex.Match(line);
						if(m2 != Match.Empty)
						{
							name = m2.Groups[1].ToString();
							face = m1.Groups[1].ToString();
							pos = line.Substring(0, line.IndexOf(','));
							name = GetPositionForSearch(pos) + name;
							/*
							if(line.StartsWith("rb"))
								name = "rb:"+name;
							else if(line.StartsWith("wr"))
								name = "wr:"+name;*/
							if(!faceHash.ContainsKey(name))
								faceHash.Add(name,face);
						}
					}
				}
			}
			catch(Exception e)
			{
				Console.Error.WriteLine("Warning! Face File ({0}) not present.\n{1}\n{2}", 
					faceFile,e.StackTrace,e.Message);
			}
		}

		private static void PopulateNumberHash()
		{
			numberHash = new Hashtable(5000);
			string faceFile = dataDir+ "NumberFaceData.txt";
			if(!File.Exists(faceFile))
				return;
			string name, number,line;
			try
			{
				StreamReader sr = new StreamReader(faceFile);
				string data = sr.ReadToEnd();
				sr.Close();
				data = data.ToLower();
				
				char[] seps = "\r\n".ToCharArray();
				string[] lines = data.Split(seps);
				Regex numberRegex = new Regex("#([0-9]+)");
				Regex nameRegex = new Regex("^[a-z 1-4]+,\\s*([a-z \\.]+)");
				
				Match m1,m2;
				string pos;
				for(int i =0; i < lines.Length; i++)
				{
					line = lines[i];
					m1 = numberRegex.Match(line);
					if(m1 != Match.Empty)
					{
						m2 = nameRegex.Match(line);
						if(m2 != Match.Empty)
						{
							name = m2.Groups[1].ToString();
							number = m1.Groups[1].ToString();
							pos = line.Substring(0, line.IndexOf(','));
							name = GetPositionForSearch(pos) + name;

							/*if(line.StartsWith("rb"))
								name = "rb:"+name;
							else if(line.StartsWith("wr"))
								name = "wr:"+name;*/
							if(!numberHash.ContainsKey(name))
								numberHash.Add(name,number);
						}
					}
				}
			}
			catch(Exception e)
			{
				Console.Error.WriteLine("Warning! File ({0}) not present.\n{1}\n{2}", 
					faceFile,e.StackTrace,e.Message);
			}
		}

		protected static string GetAttributesFromMap(string playerName, string position)
		{
			string test = GetPositionForSearch(position) + playerName.ToLower();

			playerName = playerName.ToLower();
			if(attrHash == null || attrHash.Count == 0)
				PopulateAttrHash();
			if(attrHash.ContainsKey(test))
				return (string)attrHash[test];
			return null;
		}

		protected static void PopulateAttrHash()
		{
			string fileName =  dataDir+"AttributeMap.txt";
			if(!File.Exists(fileName))
				return;
			try
			{
				StreamReader sr = new StreamReader(fileName);
				string data = sr.ReadToEnd();
				sr.Close();
				data = data.ToLower();
				
				char[] seps = "\r\n".ToCharArray();
				string[] lines = data.Split(seps);
				//Regex numberRegex = new Regex("");
				Regex nameRegex = new Regex("^[a-z 1-4]+,\\s*([a-z \\.]+)");
				int index = 0;
				Match m2;
				string line, attrs,name, pos;
				for(int i =0; i < lines.Length; i++)
				{
					line = lines[i];
				    
					m2 = nameRegex.Match(line);
					index = m2.Groups[1].Index+m2.Groups[1].Length;
					index = line.IndexOf(",",index);
					attrs = line.Substring(index+1);
					if(m2 != Match.Empty)
					{
						name = m2.Groups[1].ToString();
						//number = m1.Groups[1].ToString();
						pos = line.Substring(0, line.IndexOf(','));
						name = GetPositionForSearch(pos) + name;

						/*if(line.StartsWith("rb"))
							name = "rb:"+name;
						else if(line.StartsWith("wr"))
							name = "wr:"+name;*/
						if(!attrHash.ContainsKey(name))
							attrHash.Add(name,attrs);
					}
				}
			}
			catch(Exception e)
			{
				Console.Error.WriteLine("Warning! File ({0}) not present.\n{1}\n{2}", 
					fileName,e.StackTrace,e.Message);
			}
		}

		private static Random r = new Random();

		public virtual string GetAttributes(bool autoCompensate)
		{
			int index = ranking;
			string ret,test = null;
			int retAttrInt=0, attrInt=0;

			test = GetAttributesFromMap(string.Format("{0} {1}", fname,lname),position);
			switch(position)
			{
				case "QB":
					if(HOF && ranking > 15)
						index = 14;
					else if(ranking > 20 && passerRating > 84 && passingYards > 500)
						index = 14;

					if( (this.team.SuperBowlLoser || this.team.SuperBowlWinner) && passingYards > 2000)
						index =  (fname.Length+lname.Length) % 5; 
					
					ret =  TSBData.GetQB(index);
					if(this.passAtt > 0 )
					{
						double d = ( (1.0*this.passComp) / (1.0*this.passAtt)) ;
						int compPct = (int) (100.0 * d);

						if( TSBData.QB_PC != null)
							ret = SetOptions(TSBData.QB_PC, 5, compPct, ret );
					}
					break;
				case "RB": case "HB":
					if(HOF && ranking > 20)
						index = 20;
					index = ranking;
					ret = TSBData.GetRB(index);
					
					if( TSBData.RB_REC != null ){
						ret = SetOptions(TSBData.RB_REC, 5, receptions, ret);
					}

					
					if( TSBData.RETURN_MAN_MS != null && (puntRetYards+kickRetYards) > 150 )
					{
						string tmp = ret; 
						ret = SetOptions(TSBData.RETURN_MAN_MS, 2, (puntRetYards+kickRetYards), ret);
						if( tmp != ret )
							ret = ReplaceAttribute(ret,3,25); //HP make sure he doesn't get lots of HP.
						retAttrInt = GetAttrInt(ret,2);
						/*if( MainClass.AutoCorrectDefenseSimData )
						{
							switch(retAttrInt)
							{
								case 100: case 88: case 81: 
								case 75: case 69: case 63:
									ret = ReplaceAttribute(ret,9,15);
									ret = ReplaceAttribute(ret,10,15);
									break;
								case 56:
									ret = ReplaceAttribute(ret,9,12);
									ret = ReplaceAttribute(ret,10,12);
									break;
								case 50:
									ret = ReplaceAttribute(ret,9,10);
									ret = ReplaceAttribute(ret,10,10);
									break;
								case 44:
									ret = ReplaceAttribute(ret,9,8);
									ret = ReplaceAttribute(ret,10,8);
									break;
								case 38:
									ret = ReplaceAttribute(ret,9,4);
									ret = ReplaceAttribute(ret,10,4);
									break;
								case 31:
									ret = ReplaceAttribute(ret,9,2);
									ret = ReplaceAttribute(ret,10,2);
									break;
								default:
									ret = ReplaceAttribute(ret,9,1);
									ret = ReplaceAttribute(ret,10,1);
									break;
							}
						}*/
					}
					if( TSBData.RB_MS != null ){
						ret = SetOptions(TSBData.RB_MS, 2, (rushYards+recYards), ret);
						attrInt = GetAttrInt(ret,2);
					}
					if( retAttrInt > attrInt ) // give him the max of the MS 
						ret = ReplaceAttribute(ret,2,retAttrInt);
					
					// get from map (if there)
					break;
				case "FB":
					if(HOF && ranking > 10)
						index = 10;
					
					ret = TSBData.GetFB(index);
					
					if( TSBData.RB_REC != null ){
						ret = SetOptions(TSBData.RB_REC, 5, receptions, ret);
					}
					if( TSBData.RB_MS != null ){
						ret = SetOptions(TSBData.RB_MS, 2, (rushYards+recYards), ret);
					}

					break;
				case "TE":
					if(HOF && ranking > 10)
						index = 10;
					ret= TSBData.GetTE(index);

					if( TSBData.TE_REC != null ) 
						ret = SetOptions(TSBData.TE_REC, 5, receptions, ret);
					
					if( TSBData.TE_MS != null )
						ret = SetOptions(TSBData.TE_MS, 2, recYards, ret);
					
					break;
				case "WR":
					if(HOF && ranking > 20)
						index = 20;
					ret= TSBData.GetWR(index);

					if( TSBData.WR_REC != null ){
						ret = SetOptions(TSBData.RB_REC, 5, receptions, ret);
					}
					if( TSBData.RETURN_MAN_MS != null && (puntRetYards+kickRetYards) > 150){
						string tmp = ret; 
						ret = SetOptions(TSBData.RETURN_MAN_MS, 2, (puntRetYards+kickRetYards), ret);
						retAttrInt = GetAttrInt(ret,2);
					}
					if( TSBData.WR_MS != null ){
						ret = SetOptions(TSBData.WR_MS, 2, (rushYards+recYards), ret);
						attrInt = GetAttrInt(ret,2);
					}
					if( retAttrInt > attrInt ) // give him the max of the MS 
						ret = ReplaceAttribute(ret,2,retAttrInt);
					
					break;
				case "CB": case "DB": case "S": case "SS": case "FS":
					ret = GetDBAttributes(autoCompensate);
					break;
				case "DE": case "DT": case "NT":
					ret = GetDLAttributes();
					if( TSBData.DL_LB_PI != null )
						ret = SetOptions(TSBData.DL_LB_PI, 4, ints, ret);

					break;
				case "LB":
					ret = GetLBAttributes();
					break;
				case "K":
					ret= TSBData.GetKicker(index);
					break;
				case "P":
					ret= TSBData.GetPunter(index); 
					break;
				default:
						return "";
			}
			if(test != null )
			{
				if(test.IndexOf("?") > -1)
					ret = CombineAttrs(test,ret);
				else
					ret = test;
			}
					
			return ret;
		}

		/// <summary>
		/// This function will replace the '?'s in the withQ_marks string,
		/// and replace them with the values at the same positions in the
		/// withOutQ_marks string.
		/// </summary>
		/// <param name="withQ_marks"></param>
		/// <param name="withOutQ_marks"></param>
		/// <returns></returns>
		protected string CombineAttrs(string withQ_marks, string withOutQ_marks)
		{
			StringBuilder sb = new StringBuilder();
			int commaCount =0;
			string num;
			char c;
			for(int i =0; i < withQ_marks.Length; i++)
			{
				c = withQ_marks[i];
				if(c == ',')
				{
					commaCount++;
					sb.Append(c);
				}
				else if(c == '?')
				{
					num = GetStringAfterComma(commaCount,withOutQ_marks);
					sb.Append(num);
				}
				else
					sb.Append(c);
			}
			return sb.ToString();
		}

		protected string GetStringAfterComma(int comma, string s)
		{
			int count=0;
			char c;
			StringBuilder sb = new StringBuilder();
			for(int i =0; i < s.Length; i++)
			{
				c = s[i];
				if(c == ',')
					count++;
				else if(c == '[' || c == ']')
					;
				else if(count == comma)
					sb.Append(c);
			}
			return sb.ToString();
		}

		protected virtual string GetLBAttributes()
		{
			int defRank = this.team.defenseRank;
			
			int index = ranking;

			//if(ranking >= TSBData.lbs.Length)
			//	index = TSBData.lbs.Length-10;
			
			if(ranking > 40)
			{
				switch(defRank )
				{
					case 1: 
						index = 37;
						break;
					case 2: case 3: 
						index -= 40;
						break;
					case 4: case 5:
						index -= 35;
						break;
					case 6: case 7: case 8: 
					case 9: case 10:
						if( index > 71)
							index = 71;
						//index -= 25;
						break;
					case 11: case 12: case 13: 
					case 14: case 15:
						if( index > 81 )
							index = 81;
						//index -= 20;
						break;
					case 16: case 17: case 18: 
						//index -= 10;
						//break;
					case 19: case 20: case 21:
						break;
					case 22: case 23: case 24: //we make them worse here.
						index += 10;
						break;
					case 25: case 26: case 27:
					case 28: case 29: case 30:
					case 31: case 32: 
						index += 20;
						break;
					default:
						index +=20;
						break;
				}
				if( index > 80 && defRank < 16 )
					index = 80;

				if(index < 40 && defRank > 4)
					index =40; // can't have them be too good.
			}
			else
			{
				if( index > 10 && defRank > 21 )
				{
					index += /*(int)(index * 0.5)+ */defRank;
				}
			}
			string ret = TSBData.GetLB(index); //TSBData.lbs[index];
			
			if( TSBData.DL_LB_PI != null )
			{
				ret = SetOptions(TSBData.DL_LB_PI, 4, ints, ret);
			}
			return ret;
		}

		protected virtual string GetDBAttributes(bool autoCompensate)
		{
			int defRank = this.team.defenseRank;
			int index = ranking;

			//if(ranking >= TSBData.dbs.Length)
			//	index = TSBData.dbs.Length-10;
			
			if(ranking > 40 && autoCompensate ) // should this be configureable?
			{
				switch(defRank )
				{
					case 1:
						index = 40;
						break;
					case 2: case 3: 
						index -= 40;
						break;
					case 4: case 5:
						index -= 35;
						break;
					case 6: case 7: case 8: case 9: case 10:
						index -= 30;
						break;
					case 11: case 12: case 13: case 14: case 15:
						index -= 20;
						break;
					case 16: case 17: case 18: 
						index -= 10;
						break;
					case 19: case 20: case 21:
			  			break;
					case 22: case 23: case 24:
						index += 10;
						break;
					case 25: case 26: case 27: //we make them worse here.
					case 28: case 29: case 30:
					case 31: case 32: 
						index += 20;
						break;
					default:
						index +=20;
						break;
				}
				if( index > 44 && defRank < 3)
					index = 44;
				else if( index > 64 && defRank < 16 )
					index = 64;
				if(index < 30 && defRank > 10)
					index = 44; // can't have them be too good.
			}
			else
			{
				if( index > 10 && defRank > 21 )
				{
					index += defRank;
					if( team.dPassRank < 22 && index > 81)
						index = 81;
				}
			}
			string ret =  TSBData.GetDB(index);

			if( TSBData.DB_PI != null )
			{
				ret = SetOptions(TSBData.DB_PI, 4, ints, ret);
			}
			return ret;
		}

		protected virtual string GetDLAttributes()
		{
			int defRank = this.team.defenseRank;
			int index = ranking;

			//if(ranking >= TSBData.dls.Length)
			//	index = TSBData.dls.Length-10;
			
			if(ranking > 34)
			{
				switch(defRank )
				{
					case 1: 
						index = 30;
						break;
					case 2: case 3: 
						index -= 30;
						break;
					case 4: case 5:
						index -= 20;
						break;
					case 6: case 7: case 8: case 9: case 10:
						index -= 15;
						break;
					case 11: case 12: case 13: case 14: case 15:
						index -= 10;
						break;
					case 16: case 17: case 18: 
						index -= 5;
						break;
					case 19: case 20: case 21:
						break;
					case 22: case 23: case 24:
						index += 10;
						break;
					case 25: case 26: case 27: //we make them worse here.
					case 28: case 29: case 30:
					case 31: case 32: 
						index += 15;
						break;
					default:
						index +=20;
						break;
				}
				if( index > 38 && defRank < 14 )
					index = 38;
				if(index < 30 && defRank > 10)
					index =30; // can't have them be too good.
			}

			return TSBData.GetDL(index); // TSBData.dls[index];
		}

		public static string SetOptions(ArrayList options, int pos, int stat, string tecmoList )
		{
			if( options != null )
			{
				int tmp =0;
				int ability = 6;
				for(int i = 0; i < options.Count; i+= 2)
				{
					tmp = (int) options[i];
					ability = (int) options[i+1];
					if( stat > tmp )
					{
						tecmoList = ReplaceAttribute(tecmoList, pos, ability );
						break;
					}
				}
			}
			return tecmoList;
		}

		public static string ReplaceAttribute(string attrList, int pos, int newNumber)
		{
			int spot1=-1, spot2=0, commaCount=0;
			string ret = attrList;

			for(int i=0; i < attrList.Length; i++)
			{
				if(attrList[i] == ',')
					commaCount++;
				if(commaCount == pos && spot1 ==-1)
					spot1=i;
				else if(commaCount == pos+1 && spot2 == 0)
				{
					spot2=i;
					break;
				}
			}
			if(spot1 > 0 && spot2 > 0)
			{
				ret = attrList.Substring(0,spot1+1) + newNumber +
					  attrList.Substring(spot2);
			}
			return ret;
		}

		/// <summary>
		/// GetAttrInt("50,56,75,63,69,[44, 55]", 1); // returns 56
		/// </summary>
		/// <param name="attrList">The attribute list like "50,56,75,63,69,[44, 55]"</param>
		/// <param name="n">the index you want.</param>
		/// <returns> an int representing the nth number in the list. </returns>
		public static int GetAttrInt(string attrList, int n)
		{
			Regex reg = new Regex("([0-9]{1,3})");
			MatchCollection mc = reg.Matches(attrList);
			string ret = "-1";

			if( n < mc.Count )
			{
				ret = mc[n].ToString();
			}
			int r = -1;
			try
			{
				r= Int32.Parse(ret);
			}
			catch{}
			return r;
		}
	}
}
