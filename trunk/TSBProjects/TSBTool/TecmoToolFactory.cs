using System;

namespace TSBTool
{
	public enum TeamIndex_32
	{
		bills=0,     dolphins, patriots, jets,
		bengals,    browns,  ravens,   steelers,
		colts,      texans,  jaguars,  titans,
		broncos,    chiefs,  raiders,  chargers,  
		redskins,   giants,  eagles,   cowboys,
		bears,      lions,   packers,  vikings,   
		buccaneers, saints,  falcons,  panthers,
		AFC,     NFC,
		fortyNiners,   rams, seahawks,   cardinals
	}
	/// <summary>
	/// Summary description for TecmoToolFactory.
	/// </summary>
	public class TecmoToolFactory
	{

		public static ITecmoTool GetToolForRom(string fileName)
		{
			ITecmoTool tool = null;
			ROM_TYPE type = ROM_TYPE.NONE;
			try
			{
				type = CheckRomType(fileName);
			}
			catch( UnauthorizedAccessException )
			{
				type = ROM_TYPE.READ_ONLY_ERROR;
				MainClass.ShowError("ERROR opening ROM, Please check ROM to make sure it's not 'read-only'.");
				return null;
			}
			catch(Exception e)
			{
				MainClass.ShowError(string.Format("ERROR determining ROM type. Exception=\n{0}\n{1}",
					e.Message,e.StackTrace));
				return null;
			}

			if( type == ROM_TYPE.CXROM )
			{
				tool = new CXRomTSBTool();
				tool.Init(fileName);
				TecmoTool.Teams = new string[] 
					{
						"bills",     "dolphins", "patriots", "jets",
						"bengals",    "browns",  "ravens",   "steelers",
						"colts",      "texans",  "jaguars",  "titans",
						"broncos",    "chiefs",  "raiders",  "chargers",  
						"redskins",   "giants",  "eagles",   "cowboys",
						"bears",      "lions",   "packers",  "vikings",   
						"buccaneers", "saints",  "falcons",  "panthers",
						 
						"AFC",     "NFC",
						"49ers",   "rams", "seahawks",   "cardinals"
					};
				
			}
			else if( type == ROM_TYPE.SNES )
			{
				TecmoTool.Teams = new string[] {
				"bills",   "colts",  "dolphins", "patriots",  "jets",
				"bengals", "browns", "oilers",   "steelers",
				"broncos", "chiefs", "raiders",  "chargers",  "seahawks",
				"cowboys", "giants", "eagles",   "cardinals", "redskins",
				"bears",   "lions",  "packers",  "vikings",   "buccaneers",
				"falcons", "rams",   "saints",   "49ers"
				  };
				if( fileName != null )
					tool = new SNES_TecmoTool(fileName);
				else
					tool = new SNES_TecmoTool();
			}
			else
			{
				if( fileName != null )
					tool = new TecmoTool(fileName);
				else
					tool = new TecmoTool();
				TecmoTool.Teams = new string[] 
					{
						"bills",   "colts",  "dolphins", "patriots",  "jets",
						"bengals", "browns", "oilers",   "steelers",
						"broncos", "chiefs", "raiders",  "chargers",  "seahawks",
						"redskins","giants", "eagles",   "cardinals", "cowboys",
						"bears",   "lions",  "packers",  "vikings",   "buccaneers",
						"49ers",   "rams",   "saints",   "falcons"
					};
			}

			return tool;
		}

		/// <summary>
		/// returns 0 if regular NES TSB rom
		///         1 if it's cxrom TSBROM type.
		/// Throws exceptions (UnauthorizedAccessException and others)
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static ROM_TYPE CheckRomType(string fileName )
		{
			ROM_TYPE ret = ROM_TYPE.NES;
			System.IO.FileStream s1=null;
			try
			{
				if( System.IO.File.Exists(fileName) )
				{
					byte[] fileBytes = null;
					System.IO.FileInfo f1 = new System.IO.FileInfo(fileName);
					long len = f1.Length;
					s1 = new System.IO.FileStream(fileName, System.IO.FileMode.Open );
					fileBytes = new byte[(int)len];
					s1.Read(fileBytes,0,(int)len);
					

					if( fileBytes != null && 
						fileBytes.Length > 0x99 &&
						fileBytes[0x48] == 0xff )
						//					if( fileName.ToLower().EndsWith(".nes") && len > 0x70000 ) //cxrom size=0x80010
					{
						ret = ROM_TYPE.CXROM;
#if(DEBUG)
						Console.Error.WriteLine("This is a 32 team ROM");
#endif
					}
					else if( fileName.ToLower().EndsWith(".smc"))
					{
						ret = ROM_TYPE.SNES;
#if(DEBUG)
						Console.Error.WriteLine("This is a SNES ROM");
#endif
					}
					else
					{
#if(DEBUG)
						Console.Error.WriteLine("This seems to be the Regular TSB nes ROM.");
#endif
					}
				}
			}
//			catch(UnauthorizedAccessException)
//			{
//				
//			}
//			catch(Exception e )
//			{
//				Console.Error.WriteLine("ERROR! Function MainClass.CheckRomType {0}\n{1}",
//					e.Message, e.StackTrace);
//			}
			finally
			{
				if( s1 != null )
					s1.Close();
			}

			return ret;
		}
	}
}
