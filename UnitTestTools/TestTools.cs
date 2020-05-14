using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace UnitTestTools
{
    class TestElement
    {
        string _name;
        bool _success;

        public TestElement(string name)
        {
            _name = name;
            _success = false;
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public bool Success
        {
            get
            {
                return _success;
            }
        }
        public string toString()
        {
            if (_success)
            {
                return _name + " succeeded!";
            }
            return _name + " failed!";
        }
        public void Finish()
        {
            _success = true;
        }
    }

    public class TestTools
    {
        private static string _time_stamp = null;
        private static string _log_dir = null;
        private static List<TestElement> _tests;

        private static List<TestElement> Tests
        {
            get
            {
                if (_tests == null)
                {
                    _tests = new List<TestElement>();
                }
                return _tests;
            }
        }

        private static string TimeStamp
        {
            get
            {
                if (_time_stamp == null)
                {
                    _time_stamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                }
                return _time_stamp;
            }
        }

        private static string LogDir
        {
            get
            {
                if (_log_dir == null)
                {
                    string cwd = Directory.GetCurrentDirectory();
                    _log_dir = Path.GetFullPath(Path.Combine(cwd, "../../../.testruns"));
                }
                if (!Directory.Exists(_log_dir))
                {
                    Directory.CreateDirectory(_log_dir);
                }
                return _log_dir;
            }
        }

        private static string LogFile
        {
            get
            {
                string log_file = Path.Combine(LogDir, TimeStamp + ".testlog");
                // log_file = Path.GetFullPath(log_file);
                if (!File.Exists(log_file))
                {
                    StreamWriter sw = new StreamWriter(log_file);
                    sw.WriteLine("Logfile generated " + DateTime.Now.ToString());
                    sw.Close();
                }
                return log_file;

            }
        }
        private static void LogLine(string line)
        {
            line.Replace("\n", " ");
            StreamWriter sw = new StreamWriter(LogFile, true);
            sw.WriteLine(line);
            sw.Close();
        }
        private static void RunCommand(string cmd, string args, bool print=false)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = cmd;
                p.StartInfo.Arguments = args;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.Start();
                string stdout = p.StandardOutput.ReadToEnd();
                string stderr = p.StandardError.ReadToEnd();
                if (print)
                {
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    Console.WriteLine(cmd + " " + args + " STDOUT:");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    Console.WriteLine(stdout);
                    Console.WriteLine(cmd + " " + args + " STDERR:");
                    Console.WriteLine(stderr);
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    Console.WriteLine();
                }
            }
            catch(Win32Exception e) {
                Console.WriteLine("error running command {0}",e);
            }
        }
        private static void AddLogFile()
        {
            RunCommand("git", "add -f " + LogFile);
        }
        private static void Status()
        {
            RunCommand("git", "status");
        }
        private static void Commit()
        {
            RunCommand("git", "commit -m " + TimeStamp);
        }
        private static bool CommitLogFile()
        {
            bool success = true;
            foreach (var item in Tests)
            {
                LogLine(item.toString());
                if (!item.Success)
                {
                    success = false;
                }
            }
            StreamWriter sw = new StreamWriter(LogFile, true);
            sw.WriteLine("Logfile closed " + DateTime.Now.ToString());
            sw.Close();
            AddLogFile();
            Status();
            Commit();
            return success;
        }

        public static void StartTest(string test_name)
        {
            Console.WriteLine("Started Test: {0}", test_name);
            Tests.Add(new TestElement(test_name));
        }
        public static void EndTest(string test_name)
        {
            bool found = false;
            foreach (var item in Tests)
            {
                if (item.Name == test_name)
                {
                    item.Finish();
                    found = true;
                }
            }
            if (!found)
            {
                LogLine("Test " + test_name + " not found!");
            }
        }
        public static bool AllTests()
        {
            return CommitLogFile();
        }
    }
}
