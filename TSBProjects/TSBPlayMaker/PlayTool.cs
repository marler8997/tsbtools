using System;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;


namespace PlayProto
{
// note: quarter length loaction = 0x2224B 
	public enum OPlayer
	{
		QB=0, RB1, RB2, WR1, WR2, TE, OC, LG, RG, LT, RT
	}

	public enum DPlayer
	{
		RE=0, NT, LE, ROLB, RILB, LILB, LOLB, RCB, LCB, FS, SS
	}

	/// <summary>
	/// Summary description for PlayTool.
	/// </summary>
	public class PlayTool
	{
		public static int PLAY_NAME_STARTING_INDEX
		{
			get 
			{
				int ret = -1111111111;
				switch(MainForm.TheGameType)
				{
					case GameType.NES_TSB:   ret = 0x1D410; break;
					case GameType.SNES_TSB:  break;
					case GameType.SNES_TSB3: break;
				}
				return ret;
			}
		}
		// PLAY_NAME_STARTING_INDEX + (0x18 * playIndex) +0x10
		
		public static int DEFENSIVE_REACTION_STARTING_INDEX
		{
			get
			{
				int ret = -1111111111;
				switch(MainForm.TheGameType)
				{
					case GameType.NES_TSB:   ret = 0x1DC10; break;
					case GameType.SNES_TSB:  break;
					case GameType.SNES_TSB3: break;
				}
				return ret;
			}
	    }

		// OFFENSIVE_PLAY_POINTERS + (playNumber * 22)
		public static int OFFENSIVE_PLAY_POINTERS
		{
			get
			{
				int ret = -1111111111;
				switch(MainForm.TheGameType)
				{
					case GameType.NES_TSB:   ret  = 0x4410; break;
					case GameType.SNES_TSB:  break;
					case GameType.SNES_TSB3: break;
				}
				return ret;
			}
		}

		// DEFENSIVE_REACTION_POINTERS
		public static int DEFENSIVE_REACTION_POINTERS
		{
			get
			{
				int ret = -1111111111;
				switch(MainForm.TheGameType)
				{
					case GameType.NES_TSB:   ret  = 0x6010; break;
					case GameType.SNES_TSB:  break;
					case GameType.SNES_TSB3: break;
				}
				return ret;
			}
		}

		public static int FORMATION_POINTER_STARTING_INDEX
		{
			get 
			{
				int ret = -1111111111;
				switch(MainForm.TheGameType)
				{
					case GameType.NES_TSB:   ret = 0x4010; break;
					case GameType.SNES_TSB:  break;
					case GameType.SNES_TSB3: break;
				}
				return ret;
			}
		}

		public PlayTool()
		{
			
			//
			// TODO: Add constructor logic here
			//
		}


		/// <summary>
		/// Gets the 2 bytes that comprise a player's pattern pointer (relative address).
		/// [ loByte, hiByte  ]
		/// </summary>
		/// <param name="p"></param>
		/// <param name="playNumber"></param>
		/// <returns></returns>
		public byte[] GetOPatternLocationRelative(OPlayer p, int playNumber)
		{
			int playerPointer = OFFENSIVE_PLAY_POINTERS + (playNumber*22) + ((int)p * 2);
			byte lowByte, hiByte;
			
			lowByte = MainForm.TheROM[playerPointer];
			hiByte = MainForm.TheROM[playerPointer+1];
			byte[] ret = new byte[2];
//			ret[0] = lowByte;
//			ret[1] = hiByte;
			ret[0] = hiByte;
			ret[1] = lowByte;

			return ret;
			//byte ret = MainForm.TheROM[playerPointer];
			//return ret;
		}

		/// <summary>
		/// Gets a string representation of the pointer for the given player on
		/// the given play.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="playNumber"></param>
		/// <returns></returns>
		public string GetOPatternLocationRelativeStr(OPlayer p, int playNumber)
		{
			string result = null;
			byte[] pointer = GetOPatternLocationRelative(p,playNumber);
			if( pointer != null && pointer.Length > 1)
			{
				result = string.Format("{0:x2}{1:x2}",pointer[0],pointer[1]);
			}
			return result;
		}


		/// <summary>
		/// Sets the pointer for player p on 'playNumber' to 'pointer'.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="playNumber"></param>
		/// <param name="pointer"></param>
		public void SetOPatternLocationRelative(OPlayer p, int playNumber, string pointer)
		{
			try
			{
				int stuff = Int32.Parse(pointer,System.Globalization.NumberStyles.AllowHexSpecifier);
				byte[] bytes = new byte[2];
				byte hi  = (byte)(stuff >> 8);
				byte lo  = (byte)(stuff & 0x00ff);

				bytes[1] = hi;
				bytes[0] = lo;
//				ret[0] = hiByte;
//				ret[1] = lowByte;
				SetOPatternLocationRelative(p, playNumber, bytes);
			}
			catch(Exception ex )
			{
				MessageBox.Show(string.Format(
                    "ERROR! {0}\nCould not set pointer on play {1} for position {2}\n{3}",
					ex.Message,
					playNumber,
					p,
					ex.StackTrace));
			}
		}

		/// <summary>
		/// Sets the pointer for player p on 'playNumber' to 'pointer'.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="playNumber"></param>
		/// <param name="pointer"></param>
		public void SetOPatternLocationRelative(OPlayer p, int playNumber, byte[] pointer)
		{
			if( pointer != null && pointer.Length > 1 )
			{
				int playerPointer = OFFENSIVE_PLAY_POINTERS + (playNumber*22) + ((int)p * 2);
				MainForm.TheROM[playerPointer]   = pointer[0];
				MainForm.TheROM[playerPointer+1] = pointer[1];
			}
			else
			{
				MessageBox.Show("Argument Error with SetOPatternLocationRelative 'byte[]'");
			}
		}

		/// <summary>
		/// Gets the pointer location for a player's pattern.
		/// Result is absolute location in ROM (not relative).
		/// </summary>
		/// <param name="p"></param>
		/// <param name="playNumber"></param>
		/// <returns></returns>
		public int GetOPatternLocation( OPlayer p, int playNumber)
		{
			int playerPointer = OFFENSIVE_PLAY_POINTERS + (playNumber*22) + ((int)p*2);
			byte lowByte, hiByte;
			
			lowByte = MainForm.TheROM[playerPointer];
			hiByte = MainForm.TheROM[playerPointer+1];

			int patternLoc =  lowByte + (hiByte * 255 ) - 0x2000 + 0x10;
			return patternLoc;
		}

		/// <summary>
		/// Gets the pattern for palyer 'p' on 'playnumber'.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="playNumber"></param>
		/// <returns></returns>
		public string GetOPattern( OPlayer p, int playNumber)
		{
			int patternLoc = GetOPatternLocation(p, playNumber);
			//TODO: write this function
			return "";
		}

		/// <summary>
		/// Gets the location of d's defensive pattern.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="playNumber"></param>
		/// <returns></returns>
		public int GetDPatternPointer(DPlayer d, int playNumber)
		{
			int playerPointer = DEFENSIVE_REACTION_POINTERS + (playNumber*22) +((int)d*2);
			byte lowByte, hiByte;
			
			lowByte = MainForm.TheROM[playerPointer];
			hiByte = MainForm.TheROM[playerPointer+1];

			int patternLoc =  lowByte + (hiByte * 255 ) - 0x2000 + 0x10;
			return patternLoc;
		}

		/// <summary>
		/// Gets the location of d's defensive pattern.
		/// Returns an array of 2 bytes.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="playNumber"></param>
		/// <returns></returns>
		public byte[] GetDPatternPointerRelative(DPlayer d, int playNumber)
		{
			int playerPointer = DEFENSIVE_REACTION_POINTERS + (playNumber*22) +((int)d*2);
			byte lowByte, hiByte;
			byte[] ret = new byte[2];
			lowByte = MainForm.TheROM[playerPointer]; 
			hiByte = MainForm.TheROM[playerPointer+1]; 
			ret[0] = hiByte;
			ret[1] = lowByte;

			return ret;
		}

		/// <summary>
		/// returns the Pointer for player 'd' in string format.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="playNumber"></param>
		/// <returns></returns>
		public string GetDPatternPointerRelativeStr(DPlayer d, int playNumber)
		{
			string  ret = null;
			byte[] pointer = GetDPatternPointerRelative( d, playNumber);
			if( pointer != null && pointer.Length > 1)
			{
				ret = string.Format("{0:x2}{1:x2}",pointer[0], pointer[1]);
			}
			return ret;
		}

		/// <summary>
		/// Sets the pointer for player p on 'playNumber' to 'pointer'.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="playNumber"></param>
		/// <param name="pointer"></param>
		public void SetDPatternPointerRelative(DPlayer d, int playNumber, string pointer)
		{
			try
			{
				int stuff = Int32.Parse(pointer,System.Globalization.NumberStyles.AllowHexSpecifier);
				byte[] bytes = new byte[2];
				byte hi  = (byte)(stuff >> 8);
				byte lo  = (byte)(stuff & 0x00ff);

				bytes[0] = lo;
				bytes[1] = hi;
				SetDPatternPointerRelative(d, playNumber, bytes);
			}
			catch(Exception ex )
			{
				MessageBox.Show(string.Format(
					"ERROR! {0}\nCould not set pointer on play {1} for position {2}\n{3}",
					ex.Message,
					playNumber,
					d,
					ex.StackTrace));
			}
		}

		public void SetDPatternPointerRelative(DPlayer d, int playNumber, byte[] pointer)
		{
			if( pointer != null && pointer.Length > 1 )
			{
				int playerPointer = DEFENSIVE_REACTION_POINTERS + (playNumber*22) + ((int)d * 2);
				MainForm.TheROM[playerPointer]   = pointer[0];
				MainForm.TheROM[playerPointer+1] = pointer[1];
			}
			else
			{
				MessageBox.Show("Argument Error with SetOPatternLocationRelative 'byte[]'");
			}
		}



		public string GetDPattern( DPlayer d, int playNumber)
		{
			//TODO: write this function
			return "";
		}

		/// <summary>
		/// Returns the index of a given play.
		/// </summary>
		/// <param name="r_p">Can be either 'r'(run) or 'p'(pass)</param>
		/// <param name="slot">The slot number (0-3)</param>
		/// <param name="number">The play number (0-7)</param>
		/// <returns></returns>
		public int GetPlayIndex( char r_p, int slot, int number )
		{
			int ret = 0;

			if( slot > 3 || number > 7 )
			{
				MessageBox.Show("ERROR!!! GetPlayIndex: Slot max =3, number max = 7");
				return ret;
			}
			switch( r_p )
			{
				case 'r':
					ret = slot * 8 + number;
					break;
				case 'p':
					ret = 32 + (slot * 8 + number);
					break;
				default:
					MessageBox.Show("Invalid character passed to GetPlayIndex > '"+r_p+"'");
					break;
			}
			return ret;
		}

		/// <summary>
		/// Returns the name of the specified play.
		/// </summary>
		/// <param name="playIndex"></param>
		/// <returns></returns>
		public string GetPlayName(int playIndex)
		{
			string errStr = null;

			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! GetPlayName: Invalid playIndex";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";

			string ret = null;
			if( errStr == null )
			{
				int loc = (playIndex * 24) + PLAY_NAME_STARTING_INDEX;
				int end = loc+15;

				if( MainForm.TheROM != null )
				{
					StringBuilder sb = new StringBuilder(15);
					char c = ' ';
					for(long i =loc; i < end; i++)
					{
						c = (char) MainForm.TheROM[i];
						sb.Append(c);
					}
					ret = sb.ToString();
				}
			}
			else
			{
				MessageBox.Show(errStr);
			}
			return ret;
		}
		
		/// <summary>
		/// Returns one of the following "04","05","06","07","08","09","0A","0B",
		/// "0C","0D", "0E","0F","10","11","12","13","14","15".
		/// </summary>
		/// <param name="playIndex">the index of the play.</param>
		/// <returns></returns>
		public byte GetPlayFormation( int playIndex)
		{
			string errStr = null;

			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! GetPlayFormation: Invalid playIndex";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";

			byte ret = 4;
			if( errStr == null )
			{
				int loc = (playIndex * 24) + PLAY_NAME_STARTING_INDEX+15;

				if( MainForm.TheROM != null )
					ret = MainForm.TheROM[loc];
			}
			else
			{
				MessageBox.Show(errStr);
			}
			return ret;
		}

		/// <summary>
		/// Sets the formation of a play.
		/// </summary>
		/// <param name="playIndex">the play index</param>
		/// <param name="formation"> 0x04 - 0x15 </param>
		public void SetPlayFormation( int playIndex, byte formation)
		{
			string errStr = null;

			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! SetPlayFormation: Invalid playIndex";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";
			else if( playIndex > 64 || formation > 0x15 || formation < 0x04)
				errStr = "ERROR!!! SetPlayFormation, playIndex values = 0-63, formation = (0x04-0x15)";

			if( errStr == null)
			{
				int loc = (playIndex * 24) + PLAY_NAME_STARTING_INDEX+15;

				if( MainForm.TheROM != null )
				{
					MainForm.TheROM[loc] = formation;
				}
			}
			else
			{
				MessageBox.Show(errStr);
			}
		}

		/// <summary>
		/// Set the name of the specified play.
		/// </summary>
		/// <param name="playIndex"></param>
		/// <param name="name">the name to set.</param>
		public void SetPalyName(int playIndex, string name)
		{
			string errStr = null;
			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! SetPalyName: Invalid playIndex";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";
			else if( name != null && name.Length > 15 )
				errStr = "ERROR! SetPalyName:  Play name can be no more than 15 characters.";

			if( errStr == null )
			{
				// pad spaces to make name 15 characters
				if( name.Length < 15 )
				{
					bool back = true;
					int spaces = 15-name.Length;
					for(int i = 0; i < spaces; i++)
					{
						if( back )
							name += " ";
						else
							name = " " + name;
						back = !back;
					}
				}
				int loc = (playIndex * 24) + PLAY_NAME_STARTING_INDEX;
				byte b = 0;
				
				for( int i = 0; i < name.Length; i++)
				{
					b = (byte) name[i];
					MainForm.TheROM[ loc + i ] = b;
				}
			}
			else
			{
				MessageBox.Show(errStr);
			}
		}

		/// <summary>
		/// Returns an array of 8 bytes for the defensive reaction numbers.
		/// </summary>
		/// <param name="playIndex"></param>
		/// <returns></returns>
		public byte[] GetDefensiveReactions(int playIndex)
		{
			byte[] ret = null;

			string errStr = null;
			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! GetDefensiveReactions: Invalid playIndex";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";

			if( errStr == null )
			{
				int loc = DEFENSIVE_REACTION_STARTING_INDEX + ( 8 * playIndex );
				ret = new byte[8];
				for(int i = 0; i < ret.Length; i++)
				{
					ret[i] = MainForm.TheROM[loc+i];
				}
			}
			else
			{
				MessageBox.Show(errStr);
			}
			return ret;
		}

		public byte[] GetOffensivePlayToCall(int playIndex)
		{
			byte[] ret = null;

			string errStr = null;
			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! GetOffensivePlayToCall: Invalid playIndex";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";

			if( errStr == null )
			{
				int loc = PLAY_NAME_STARTING_INDEX + (0x18 * playIndex) + 0x10;
				ret = new byte[8];
				for(int i = 0; i < ret.Length; i++)
				{
					ret[i] = MainForm.TheROM[loc+i];
				}
			}
			else
			{
				MessageBox.Show(errStr);
			}
			return ret;
		}

		/// <summary>
		/// Set the defensive reactions for a given playIndex.
		/// </summary>
		/// <param name="playIndex">the index of the play</param>
		/// <param name="reactions">the bytes to use.</param>
		public void SetDefensiveReactions(int playIndex, byte[] reactions)
		{
			string errStr = null;
			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! SetDefensiveReactions: Invalid playIndex";
			else if( reactions == null || reactions.Length < 8 )
				errStr = "ERROR! SetDefensiveReactions: param 'reactions' invalid";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";

			if( errStr == null )
			{
				int loc = DEFENSIVE_REACTION_STARTING_INDEX + ( 8 * playIndex );
				for(int i = 0; i < reactions.Length; i++)
				{
					MainForm.TheROM[loc+i] = reactions[i];
				}
			}
			else
			{
				MessageBox.Show(errStr);
			}
		}

		public void SetOffensivePlayToCall(int playIndex, byte[] plays)
		{
			string errStr = null;
			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! SetOffensivePlayToCall: Invalid playIndex";
			else if( plays == null || plays.Length < 8 )
				errStr = "ERROR! SetOffensivePlayToCall: param 'plays' invalid";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";

			if( errStr == null )
			{
				int loc = PLAY_NAME_STARTING_INDEX + (0x18 * playIndex) + 0x10;
				for(int i = 0; i < plays.Length; i++)
				{
					MainForm.TheROM[loc+i] = plays[i];
				}
			}
			else
			{
				MessageBox.Show(errStr);
			}
		}

		/// <summary>
		/// Get the text reprensentation of the defensive reactions
		/// for play at 'playIndex'.
		/// </summary>
		/// <param name="playIndex"></param>
		/// <returns></returns>
		public string GetDefensiveReactionText(int playIndex)
		{
			string errStr = null;

			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! GetDefensiveReactionText: Invalid playIndex";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";

			string ret = null;
			if( errStr == null )
			{
				byte[] reactions = GetDefensiveReactions(playIndex);

				StringBuilder sb = new StringBuilder(50);
				string b = "";
				for(int i = 0; i < reactions.Length; i++)
				{
					b = string.Format("{0:x2}",reactions[i]);
					sb.Append(b);
					if( i != reactions.Length-1 )
						sb.Append(", ");
				}
				ret = sb.ToString();
			}
			return ret;
		}

		public string GetPlayToCallText(int playIndex)
		{
			string errStr = null;

			if( playIndex > 63 || playIndex < 0)
				errStr = "ERROR! GetPlayToCallText: Invalid playIndex";
			else if( MainForm.TheROM == null )
				errStr = "You must open a rom first.";

			string ret = null;
			if( errStr == null )
			{
				byte[] reactions = GetOffensivePlayToCall(playIndex);

				StringBuilder sb = new StringBuilder(50);
				string b = "";
				for(int i = 0; i < reactions.Length; i++)
				{
					b = string.Format("{0:x2}",reactions[i]);
					sb.Append(b);
					if( i != reactions.Length-1 )
						sb.Append(", ");
				}
				ret = sb.ToString();
			}
			return ret;
		}
		/// <summary>
		/// Sets the defensive reactions given a string like:
		/// "5B, 5A, 5A, 58, 58, 59, 59, 59"
		/// </summary>
		/// <param name="text"></param>
		public void SetDefensivereactionsFromText(string text, int playIndex)
		{
			string errStr = null;

			if( MainForm.TheROM == null)
				errStr = "You must open a rom first.";
			else if( text == null )
				errStr = "ERROR!!! SetDefensivereactionsFromtext, text == null";

			if( errStr == null )
			{
				ArrayList stuff = GetItems(text, " \r\n,\t");
				byte[] bytes = new byte[8];
				string item ="";
				for(int i =0;i < bytes.Length && i < stuff.Count; i++)
				{
					item = stuff[i].ToString();
					bytes[i] = Byte.Parse(item,System.Globalization.NumberStyles.AllowHexSpecifier);
				}
				SetDefensiveReactions( playIndex, bytes);
			}
			else
			{
				MessageBox.Show(errStr);
			}
		}

		public void SetOffensivePlayToCallFromText(string text, int playIndex)
		{
			string errStr = null;

			if( MainForm.TheROM == null)
				errStr = "You must open a rom first.";
			else if( text == null )
				errStr = "ERROR!!! SetOffensivePlayToCallFromText, text == null";

			if( errStr == null )
			{
				ArrayList stuff = GetItems(text, " \r\n,\t");
				byte[] bytes = new byte[8];
				string item ="";
				for(int i =0;i < bytes.Length && i < stuff.Count; i++)
				{
					item = stuff[i].ToString();
					bytes[i] = Byte.Parse(item,System.Globalization.NumberStyles.AllowHexSpecifier);
				}
				SetOffensivePlayToCall( playIndex, bytes);
			}
			else
			{
				MessageBox.Show(errStr);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="formationNumber"></param>
		/// <returns>Returns an array of ints</returns>
		public int[] GetFormationPoints(int formationNumber)
		{
			int[] ret      = new int[44];
			int[] tmp = null;
			int index = 0;
			for( int i =0; i < 11; i++)
			{
				tmp = GetPlayerFormationPoint(formationNumber, (OPlayer)i);
				ret[index++] = tmp[0];
				ret[index++] = tmp[1];
				ret[index++] = tmp[2];
				ret[index++] = tmp[3];
			}
			return ret;
		}

		public int[] GetPlayerFormationPoint(int formationNumber, OPlayer p)
		{
			int[] ret = new int[4];
			byte hi = 0;
			byte lo = 0;
			int pointerLoc = formationNumber*22 + 
				FORMATION_POINTER_STARTING_INDEX+((int)p*2);
			hi = MainForm.TheROM[pointerLoc+1];
			lo = MainForm.TheROM[pointerLoc];
			int playerYXloc =  lo + (hi * 0x100) - 0x2000 + 0x10;
//			sbyte tmp =0;
//			tmp = (sbyte)MainForm.TheROM[playerYXloc+1]; // x
//			ret[0] = tmp;
//			tmp = (sbyte)MainForm.TheROM[playerYXloc];   // y
//			ret[1] = -tmp;
			ret[0] = MainForm.TheROM[playerYXloc];
			ret[1] = MainForm.TheROM[playerYXloc+1];
			ret[2] = MainForm.TheROM[playerYXloc+2];
			ret[3] = MainForm.TheROM[playerYXloc+3];

			return ret;
		}

		/// <summary>
		/// returns an array of 11 ints.
		///  ret[0] = qb's lobyte pointer
		///  ret[1] = qb's hibyte pointer
		/// 'playNumber'.
		/// </summary>
		/// <param name="playNumber"></param>
		/// <param name="offense"></param>
		/// <returns></returns>
		public byte[] GetPlayPatternPointers(int playNumber, bool offense)
		{
			byte[] ret = null;
			if( MainForm.TheROM != null && playNumber > -1 && 
				( (offense && playNumber< 0x5c || !offense && playNumber< 0xFF)) 
				)
			{
				ret = new byte[22];
				int loc = 0;
				if( offense )
					loc = OFFENSIVE_PLAY_POINTERS + (playNumber*22);// + ((int)p * 2);
				else
					loc = DEFENSIVE_REACTION_POINTERS + (playNumber*22);// +((int)d*2);
				for(int i = 0;i < 22;  i++)
				{
					ret[i] = MainForm.TheROM[i+loc];
				}
			}
			return ret;
		}

		/// <summary>
		/// returns an arraylist of ints
		/// </summary>
		/// <param name="pointer"></param>
		/// <param name="offense"></param>
		/// <returns></returns>
		public ArrayList GetPlaysPointerAppearsIn( string pointer, bool offense )
		{
			ArrayList ret = null;

			if( pointer != null && pointer.Length == 4 )
			{
				ret = new ArrayList(10);
				byte[] tmp = null;
				string low  = pointer.Substring(0,2);
				string high = pointer.Substring(2,2);
				byte lo = Byte.Parse(low,System.Globalization.NumberStyles.AllowHexSpecifier);
				byte hi = Byte.Parse(high,System.Globalization.NumberStyles.AllowHexSpecifier);
				int j =0;
				int end   = 0xFF; // end for defense
				if( offense )
					end   = 0x5c;

				StringBuilder sb = new StringBuilder(100);

				for(int i = 0x00; i < end; i++ )
				{
					tmp = GetPlayPatternPointers(i, offense);
					for( j =0; j < tmp.Length; j+=2)
					{
						if( hi == tmp[j] && lo == tmp[j+1] )
						{
							//sb.Append(string.Format("{0:x2},",i));
							ret.Add(i);
							break;
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// returns a string like "10,20,21" which is a listing of plays
		/// currently using the pointer passed.
		///  offense goes 0x00 - 0x5B
		/// </summary>
		/// <param name="pointer"></param>
		/// <param name="offense"></param>
		/// <returns></returns>
		public string GetPlaysPointerAppearsIn_str(string pointer, bool offense)
		{
			string ret = null;
			StringBuilder sb = new StringBuilder(30);
			ArrayList list = GetPlaysPointerAppearsIn(pointer, offense);
			if( list != null && list.Count > 0 )
			{
				foreach(int i in list)
				{
					sb.Append(string.Format("{0:x2},",i));
				}
				ret = sb.ToString(0, sb.Length - 1); // trim last comma
			}
			return ret;
		}

//		int playerPointer = DEFENSIVE_REACTION_POINTERS + (playNumber*22) +((int)d*2);
		/// <summary>
		/// Returns an arraylist of stuff you want out of string 'text'.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="delims"></param>
		/// <returns></returns>
		public static ArrayList GetItems(string text, string delims)
		{
			string[] chunks = text.Split(delims.ToCharArray());
			ArrayList list = new ArrayList(chunks.Length);
			for(int i = 0; i< chunks.Length; i++)
			{
				if( chunks[i].Length > 0)
					list.Add(chunks[i]);
			}
			return list;
		}

	}
}
