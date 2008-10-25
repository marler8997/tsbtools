using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;

namespace TSBSeasonGen
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class MainClass
	{

		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

		public  static bool   AutoCorrectDefenseSimData = true;
		public  static bool   GenerateForSNES           = false;
		public  static string LeagueFolder              = "NFL_DATA";
		public  static bool   GenerateSchedule          = true;
		public  static bool   GUIMode                   = false;
		private static ArrayList Errors;
		public static string VERSION = "2.0";

		public static string TEAM_HOW_TO=
@"Ways to Define a team:
	   Method:         | Example:        | Common Errors:
	1. By year         | [1990]          | Team does not exist in this year.
	2. By substitution | [1988 Chargers] | Team does not exist in this year.
	3. By filename     | [C:\myTeam.txt] | Incorrect File format, file does not exist.
	                                     | Use the [...] button to select a file. Make 
	                                     | sure that you choose one that is in the correct 
	                                     | format. (The format of the files in 
	                                     | 'NFL_DATA/year/team' are correct format).


When you use this form to fill in the teams, you are basically specifying
a TSBSeasonGen config file. This config file can be saved and re-imported later.
";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			ResetErrors();
			int year = GetYear(args);
			string configFile = GetConfigFileName(args);
			SetOptions(args);

			if( args.Length == 0 )
			{
				//Application.Run(new MainForm());
				GUIMode = true;
				ShowWindow(GetConsoleWindow(), WindowShowStyle.Hide);
				Application.Run(new TeamSelectForm() );
				ShowWindow(GetConsoleWindow(), WindowShowStyle.Show);
			}
			else if(configFile != null )
			{
				if( File.Exists(configFile) )
					GenSeason(configFile);
				else
				{
					string str = Directory.GetCurrentDirectory()+ Path.DirectorySeparatorChar + configFile;
					MainClass.AddError(string.Format(
                      "File '{0}' does not exist!\n",str));
				}
			}
			else if(year > 1950)
			{
				GenSeason(year);
			}
			else
			{
				ShowHelp();
			}
		}

		public static void AddError(string error)
		{
			if( Errors == null)
			{
				ResetErrors();
			}
			Errors.Add(error);
		}

		public static ArrayList GetErrors()
		{
			return Errors;
		}

		public static bool ShowErrors()
		{
			bool ret = false;
			string errors = "";
			if( Errors != null )
			{
				StringBuilder sb = new StringBuilder();
				foreach(string err in Errors)
				{
					sb.Append(string.Format("{0}\r\n",err));
				}
				errors = sb.ToString();
				ret = true;
			}
			if( errors.Length > 0 )
			{
				if(GUIMode)
				{
					MessageBox.Show(errors);
				}
				else
				{
					Console.Error.WriteLine(errors);
				}
			}

			ResetErrors();
			return ret;
		}

		private static void ResetErrors()
		{
			Errors = new ArrayList();

		}

		/// <summary>
		/// Sets up options for the program.
		/// </summary>
		/// <param name="args"></param>
		static void SetOptions(string[] args)
		{
			string arg;
			for(int i = 0; i < args.Length; i++ )
			{
				arg = args[i].ToLower();
				switch (arg)
				{
					case "-snes": case "/snes":
						GenerateForSNES = true;
						break;
					case "-noautocorrect": case "/noautocorrect":
						AutoCorrectDefenseSimData = false;
						break;
					case "-noschedule": case "/noschedule":
						GenerateSchedule = false;
						break;
				}
			}
		}

		static void GenSeason(string configFile)
		{
			//try{
			Season s = new Season(configFile);
			string str = s.GenerateSeason();
			
			if( System.IO.Path.DirectorySeparatorChar == '\\' )
			{
				str = str.Replace("\r\n","\n");
				str = str.Replace("\n", "\r\n");
			}
			Console.WriteLine(str);
			//}catch(Exception e){
			//	MainClass.AddError(e.StackTrace+"\n"+e.Message);
			//}
		}

		static void GenSeason(int year)
		{
			//try{
			//Console.Error.WriteLine("Year = {0}",year);
			Season s = new Season(year);
			string str = s.GenerateSeason();

			if( System.IO.Path.DirectorySeparatorChar == '\\' )
			{
				str = str.Replace("\r\n","\n");
				str = str.Replace("\n", "\r\n");
			}
			Console.WriteLine(str);
			//}
			/*catch(Exception e)
			{
				Console.Error.WriteLine(e.StackTrace+"\n"+e.Message);
			}*/
		}

		public static void WriteFile(string filename, string contents)
		{
			try
			{
				StreamWriter sw = new StreamWriter(filename);
				sw.Write(contents);
				sw.Close();
			}
			catch(Exception e)
			{
				MessageBox.Show(string.Format("Error writing file {0}\n\n{1}",filename,e.Message));
			}
		}

		static string GetConfigFileName(string[] args)
		{
			string fileName = null;
			int index = -1;
			string s = "-config";
			for(int i = 0; i < args.Length; i++)
			{
				index = args[i].IndexOf(s);
					if( index > -1)
					{
						try
						{
							fileName = args[i].Substring(index + s.Length+1);
						}
						catch{
							MainClass.AddError(
                             "Config file param is like '-config:ConfigFile'");
						}
					}
			}
			return fileName;
		}

		static int GetYear(string[] args)
		{
			int ret = 0;
			Regex r = new Regex("([0-9]+)");
			Match m;
			for(int i = 0; i < args.Length; i++)
			{
				m = r.Match(args[i]);
				if( m != Match.Empty)
				{
					ret = Int32.Parse(m.Groups[1].ToString());
				}
			}
			return ret;
		}

		static void ShowHelp()
		{
			Console.Error.WriteLine(
@"{0}

USAGE: TSBSeasonGen <year>
===OR===
TSBSeasonGen -config:<config file name>

Other Options:
-noAutoCorrect		Do not auto correct(calculate) defensive player sim values.
-snes			Generate output for SNES_TSBTool.
-noSchedule		Do not Generate a schedule.
 ", VERSION
				);
		}
	}
	/// <summary>Enumeration of the different ways of showing a window using
	/// ShowWindow</summary>
	public enum WindowShowStyle : uint
	{
		/// <summary>Hides the window and activates another window.</summary>
		/// <remarks>See SW_HIDE</remarks>
		Hide = 0,
		/// <summary>Activates and displays a window. If the window is minimized
		/// or maximized, the system restores it to its original size and
		/// position. An application should specify this flag when displaying
		/// the window for the first time.</summary>
		/// <remarks>See SW_SHOWNORMAL</remarks>
		ShowNormal = 1,
		/// <summary>Activates the window and displays it as a minimized window.</summary>
		/// <remarks>See SW_SHOWMINIMIZED</remarks>
		ShowMinimized = 2,
		/// <summary>Activates the window and displays it as a maximized window.</summary>
		/// <remarks>See SW_SHOWMAXIMIZED</remarks>
		ShowMaximized = 3,
		/// <summary>Maximizes the specified window.</summary>
		/// <remarks>See SW_MAXIMIZE</remarks>
		Maximize = 3,
		/// <summary>Displays a window in its most recent size and position.
		/// This value is similar to "ShowNormal", except the window is not
		/// actived.</summary>
		/// <remarks>See SW_SHOWNOACTIVATE</remarks>
		ShowNormalNoActivate = 4,
		/// <summary>Activates the window and displays it in its current size
		/// and position.</summary>
		/// <remarks>See SW_SHOW</remarks>
		Show = 5,
		/// <summary>Minimizes the specified window and activates the next
		/// top-level window in the Z order.</summary>
		/// <remarks>See SW_MINIMIZE</remarks>
		Minimize = 6,
		/// <summary>Displays the window as a minimized window. This value is
		/// similar to "ShowMinimized", except the window is not activated.</summary>
		/// <remarks>See SW_SHOWMINNOACTIVE</remarks>
		ShowMinNoActivate = 7,
		/// <summary>Displays the window in its current size and position. This
		/// value is similar to "Show", except the window is not activated.</summary>
		/// <remarks>See SW_SHOWNA</remarks>
		ShowNoActivate = 8,
		/// <summary>Activates and displays the window. If the window is
		/// minimized or maximized, the system restores it to its original size
		/// and position. An application should specify this flag when restoring
		/// a minimized window.</summary>
		/// <remarks>See SW_RESTORE</remarks>
		Restore = 9,
		/// <summary>Sets the show state based on the SW_ value specified in the
		/// STARTUPINFO structure passed to the CreateProcess function by the
		/// program that started the application.</summary>
		/// <remarks>See SW_SHOWDEFAULT</remarks>
		ShowDefault = 10,
		/// <summary>Windows 2000/XP: Minimizes a window, even if the thread
		/// that owns the window is hung. This flag should only be used when
		/// minimizing windows from a different thread.</summary>
		/// <remarks>See SW_FORCEMINIMIZE</remarks>
		ForceMinimized = 11
	}
}
