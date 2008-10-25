using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;


namespace PlayProto
{
	/// <summary>
	/// Summary description for Pattern.
	/// </summary>
	public class Pattern
	{
		public static ArrayList[] Commands = null;
		public Pattern()
		{
			if( Commands == null )
			{
				ReadCommands();
			}
		}

		public string GetPattern( int location, string cmdList )
		{
			string ret = "";
			byte command = MainForm.TheROM[location];
			ArrayList list = Commands[command] as ArrayList;
			int lo, hi, offset=0;

			if( list != null )
			{
				if( cmdList == "" )
				{
					ret += string.Format("{0:x6};",location); 
				}
				ret += string.Format("{0:x2}:",command);  // command hex
				ret += list[0];             // name or command
				int args = Int32.Parse(list[1].ToString());
				
				switch( args )
				{
					case 1:
						ret += string.Format("({0:x2});",MainForm.TheROM[location+1]);
						break;
					case 2:
						if( list[2].ToString() == "lo")
						{
							lo = MainForm.TheROM[location+1];
							hi = MainForm.TheROM[location+2];
							offset = lo + (hi * 256) - 0x2000 + 0x10;
							//ret += string.Format("({0:x2}{1:x2});",hi,lo );
							ret += string.Format("({0:x4});",offset );
						}
						else
						{
							ret += string.Format("({0:x2},{1:x2});",
								MainForm.TheROM[location+1],
								MainForm.TheROM[location+2]);
						}
						break;
					case 3:
						if( list[2].ToString() == "lo")
						{
							lo = MainForm.TheROM[location+1];
							hi = MainForm.TheROM[location+2];
							offset = lo + (hi * 256) - 0x2000 + 0x10;
							//ret += string.Format("({0:x2}{1:x2}, {2:x2});",
							//	hi,
							//	lo,
							//  MainForm.TheROM[location+2] );
							ret += string.Format("({0:x4}, {1:x2});",
								offset,
								MainForm.TheROM[location+2] );
						}
						else
						{
							ret += string.Format("({0:x2}, {1:x2}, {2:x2});",
								MainForm.TheROM[location+1],
								MainForm.TheROM[location+2],
								MainForm.TheROM[location+3] );
						}
						break;
					case 4:
						lo = MainForm.TheROM[location+3];
						hi = MainForm.TheROM[location+4];
						offset = lo + (hi * 256) + 0x10; /* Calculate the offset */

						/*ret = string.Format("{0:x2}-Pass({1:x2} {2:x2}) Branch-{3:x2}{4:x2}:",
							command,
							MainForm.TheROM[location+1],
							MainForm.TheROM[location+2],
							MainForm.TheROM[location+3],
							MainForm.TheROM[location+4]);*/
						ret = string.Format("{0:x2}-Pass({1:x2} {2:x2}) Branch-{3:x4}:",
							command,
							MainForm.TheROM[location+1],
							MainForm.TheROM[location+2],
							offset);
						break;
					case 5:
						lo = MainForm.TheROM[location+4];
						hi = MainForm.TheROM[location+5];
						offset = lo + (hi * 256) + 0x10; /* Calculate the offset */

						/*ret = string.Format("{0:x2}-Pass({1:x2} {2:x2} {3:x2} ) Branch-{4:x2}{5:x2}:",
							command,
							MainForm.TheROM[location+1],
							MainForm.TheROM[location+2],
							MainForm.TheROM[location+3],
							MainForm.TheROM[location+4],
							MainForm.TheROM[location+5]);*/
						ret = string.Format("{0:x2}-Pass({1:x2} {2:x2} {3:x2} ) Branch-{4:x4}:",
							command,
							MainForm.TheROM[location+1],
							MainForm.TheROM[location+2],
							MainForm.TheROM[location+3],
							offset);
						break;
					case 6:
						lo = MainForm.TheROM[location+5];
						hi = MainForm.TheROM[location+6];
						offset = lo + (hi * 256) + 0x10; /* Calculate the offset */

						/*ret = string.Format("{0:x2}-Pass({1:x2} {2:x2} {3:x2} {4:x2} ) Branch-{5:x2}{6:x2}:",
							command,
							MainForm.TheROM[location+1],
							MainForm.TheROM[location+2],
							MainForm.TheROM[location+3],
							MainForm.TheROM[location+4],
							MainForm.TheROM[location+5],
							MainForm.TheROM[location+6]);*/
						ret = string.Format("{0:x2}-Pass({1:x2} {2:x2} {3:x2} {4:x2} ) Branch-{5:x4}:",
							command,
							MainForm.TheROM[location+1],
							MainForm.TheROM[location+2],
							MainForm.TheROM[location+3],
							MainForm.TheROM[location+4],
							offset);
						break;
					case 7:
						lo = MainForm.TheROM[location+6];
						hi = MainForm.TheROM[location+7];
						offset = lo + (hi * 0x100) + 0x10; /* Calculate the offset */

						/*ret = string.Format("{0:x2}-Pass({1:x2} {2:x2} {3:x2} {4:x2} {5:x2}) Branch-{6:x2}{7:x2}:",
							command,
							MainForm.TheROM[location+1],
							MainForm.TheROM[location+2],
							MainForm.TheROM[location+3],
							MainForm.TheROM[location+4],
							MainForm.TheROM[location+5],
							MainForm.TheROM[location+6],
							MainForm.TheROM[location+7]);*/
						ret = string.Format("{0:x2}-Pass({1:x2} {2:x2} {3:x2} {4:x2} {5:x2}) Branch-{6:x4}:",
							command,
							MainForm.TheROM[location+1],
							MainForm.TheROM[location+2],
							MainForm.TheROM[location+3],
							MainForm.TheROM[location+4],
							MainForm.TheROM[location+5],
							offset);
						break;
				} // end switch

				if( command == 0xFE )
				{
					byte arg = MainForm.TheROM[location+1];

					if( arg >= 0x80 )
					{
						//byte back = (byte)~arg;
						byte back = (byte)(0xFF - arg +1);
						ret = string.Format("FE:LoopBack({0:x2});",back);
						offset = location - back;
					}
					else
					{
						// still needs work
						ret = string.Format("FE:LoopForward({0:x2});",arg);
						offset = location+arg;
					}
				}
				// we branch out.
				

				int locNext = 0;
				/*string branch = "";
				if( IsConditionalGotoCommand( command ) )
				{
					branch = string.Format("<branch>({0:x4},{1:x2}){2}</branch>",offset,command ,GetPattern(offset, ""));
				}*/

				if( offset == 0 )
					locNext = 1 + location + args;
				else
					locNext = offset;

				// not a goto, not a loopback/loopforward
				if( !(command == 0xFE || IsGotoCommand(command)) )
			    {
					ret = GetPattern( locNext, cmdList + ret );
				}
				else
				{
					if( InfiniteLoop(cmdList, locNext) )
					{
						ret = cmdList+ret;
					}
					else
					{
						ret = GetPattern(locNext, cmdList+ret);
					}
				}
				return ret;
			}
			return cmdList;
			// t = ret.IndexOf(test) 
			// if( (command == goto || command == loopback )&& t > -1)
			// {
			//    ret = ret.Substring(0, t);
			//    // end condition
			//    return ret;
			// }
			// work on the termination of patterns, string formatting for pointers.
		}

		private bool IsGotoCommand( int cmd )
		{
			bool ret = false;
			switch ( cmd )
			{
				case 0xFF: case 0xC7: case 0xC8: case 0x20: case 0x21: case 0x22: 
				case 0x23: case 0x24: case 0x25: case 0x26: case 0x27: case 0x28:
				case 0x29: case 0x2A: case 0x2B: case 0x2C: case 0x2D: case 0x2F:
					ret = true;
					break;
			}
			return ret;
		}

		private bool IsConditionalGotoCommand( int cmd )
		{
			bool ret = false;
			switch ( cmd )
			{
				case 0xC7: case 0xC8: 
				case 0x20: case 0x21: case 0x22: 
				case 0x23: case 0x24: case 0x25: 
				case 0x26: case 0x27: case 0x28:
				case 0x29: case 0x2A: case 0x2B: 
				case 0x2C: case 0x2D: case 0x2F:
					ret = true;
					break;
			}
			return ret;
		}

		public static void ReadCommands()
		{
			string fileName = "Command-List.csv";
			Commands = new ArrayList[256];
			StreamReader sr = null;
			int lineNo = 0;
			int index = 0;
			try
			{
				sr = new StreamReader(fileName);
				string line = null;
				string[] parts;
				string part;
				char[] seps = { ',',' ' };
				while( (line = sr.ReadLine()) != null )
				{
					parts = line.Split(seps);
					ArrayList list = new ArrayList(parts.Length);
					for(int i = 0; i < parts.Length; i++ )
					{
						part = parts[i];
						if( parts[i].Length > 0 )
						{
							if( i == 0 )
							{
								part = part.Substring(2);
								index =  Int32.Parse( part,System.Globalization.NumberStyles.AllowHexSpecifier );
							}
							else
								list.Add(part);
						}
					}
					Commands[index] = list ;
					lineNo++;
				}
				
			}
			catch(Exception e )
			{
				MessageBox.Show(lineNo+ ": ERROR! Reading File '"+ fileName+"' " + e.Message);
				return;
			}
			finally
			{
				if( sr != null )
					sr.Close();
			}
		}

		
		string[] mOPositions = 
		  { "QB", "HB", "FB", "WR1", "WR2", "TE", "OC", "LG", "RG", "LT", "RT" };

		/// <summary>
		/// Get the offensive patterns for a play.
		/// </summary>
		/// <param name="playIndex"></param>
		/// <returns></returns>
		public string[] GetOffensivePatterns(int playIndex)
		{
			string[] ret = new string[11];
			int playNumber = GetPlayNumber(playIndex);
			int location = 0x4410 + (playNumber * 0x16);
			int pointsTo = 0;
			byte hi, lo;

			for(int i =0; i < ret.Length; i++ )
			{
				lo = MainForm.TheROM[location];
				hi = MainForm.TheROM[location+1];
				pointsTo = (hi * 0x100) + lo - 0x2000 + 0x10;
				ret[i] = string.Format("{0}->{1}",
					mOPositions[i], 
					GetPattern(pointsTo, ""));
				location+=2;
			}
			return ret;
		}

		/// <summary>
		/// Returns true when 'location' represents some point in the cmdList.
		/// </summary>
		/// <param name="cmdList"></param>
		/// <param name="location"></param>
		/// <returns></returns>
		public bool InfiniteLoop(string cmdList, int location)
		{
			if( cmdList.Length < 1 )
				return false;

			int testCmd = 0;
			int testLoc = 0;
			int test = 0;
			int oParen = 0;
			int cParen = 0;
			int commaLoc = 0;
			string command= "";
			char[] seps = {';'};
			string[] commands = cmdList.Split(seps);

			for(int i = 0; i < commands.Length; i ++)
			{
				command = commands[i];
				if( i == 0 )
				{
					testLoc += Int32.Parse(commands[i], System.Globalization.NumberStyles.AllowHexSpecifier);
				}
				else if( commands[i].Length > 0)
				{
					testCmd = Int32.Parse(command.Substring(0,2), System.Globalization.NumberStyles.AllowHexSpecifier);
					switch(testCmd)
					{
						// Jump commands
						case 0xFF: case 0xC7: case 0xC8: case 0x20: case 0x21: case 0x22: 
						case 0x23: case 0x24: case 0x25: case 0x26: case 0x27: case 0x28:
						case 0x29: case 0x2A: case 0x2B: case 0x2C: case 0x2D: case 0x2F:
							oParen = command.IndexOf('(');
							cParen = command.IndexOf(')');
							commaLoc = command.IndexOf(',');
							if( commaLoc > -1 )
								cParen = commaLoc;
							testLoc = Int32.Parse(command.Substring(oParen+1,cParen-oParen-1), System.Globalization.NumberStyles.AllowHexSpecifier);
							break;
						// Loop command
						case 0xFE:
							oParen = command.IndexOf('(');
							cParen = commands[i].IndexOf(')');
							test = Int32.Parse(command.Substring(oParen+1,cParen-oParen-1), System.Globalization.NumberStyles.AllowHexSpecifier);
							if( commands[i].IndexOf("Back") > -1 )
								test *= -1;
							testLoc += test;
							break;
						// The rest of the commands
						default:
							ArrayList list = Commands[testCmd] as ArrayList;
							if(list != null )
							{
								test = Int32.Parse(list[1].ToString() );
								testLoc += (test+1);
							}
							break;
					}
				}
				if( testLoc == location )
					return true;
			}
			return false;
		}



		int GetPlayNumber(int playIndex)
		{
			return 0x11;
		}

	}
}
