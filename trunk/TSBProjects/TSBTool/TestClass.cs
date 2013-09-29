using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace TSBTool
{
    /// <summary>
    /// 
    /// </summary>
    public class TestClass
    {
        static String diffTool = "C:\\Program Files\\ExamDiff Pro\\ExamDiff.exe";

        public static bool RunTests()
        {
            bool ret = false;
            //.\TSBToolSupreme "TSB 2007-32-111.nes" Test9.txt -out:output.nes
            RunTest("TSB 2007-32-111.nes", "Test9.txt", "-j -n -f -a -s -sch -pb -of -proBowl output.nes");

            return ret;
        }

        public static bool RunTest( string inputRom, string testFile,  string cmdLineArgs)
        {
            bool ret = false;
            String[] args = cmdLineArgs.Split(new char[] { ' ' });
            List<String> listArgs = new List<String>(args);
            listArgs.Insert(0, inputRom);
            listArgs.Insert(1, testFile);
            args = listArgs.ToArray();

            MainClass.RunMain(args);
            String testFileContents = File.ReadAllText(testFile);
            if (String.Compare(testFileContents, MainClass.TestString) != 0)
            {
                //fail
                File.WriteAllText("output.txt", testFileContents);
                String argLine = String.Concat(testFile, " output.txt");
                Process.Start(diffTool, argLine);
            }
            else
            {
                ret = true;
            }

            return ret;
        }
    }
}
