using System;
using System.Collections.Generic;

namespace Core {
    public class Lib {
        public void help2()      {  new Help2().help(); }
        public void help3()      {  new Help3().help(); }
        public void cls()        {  Console.Clear();   display(); }
        public void display()    {  new Display().display(); } 
        public string Hdd()      {  return new Hdd().hdd_bilgi(); }

        public bool setfind(string girdi)    {  return new SetFind().FindSet(girdi); }
        public bool find_int(string girdi)   {  return new IntFind().FindInt(girdi); }
        public bool find_string(string girdi){  return new StringFind().FindString(girdi); }

        public static Dictionary<string, string> degiskenler = new Dictionary<string, string>();
        public static Dictionary<string, int[]> intArrays = new Dictionary<string, int[]>();
        public static Dictionary<string, string[]> stringArrays = new Dictionary<string, string[]>();
        
        public bool find_intarray(string girdi)   {  return new IntArray().FindIntArray(girdi); }
        public bool find_stringarray(string girdi){  return new StringArray().FindStringArray(girdi); }
 
        public bool forCmd(string girdi)              {  return new ForCommand().Execute(girdi); } 
        public bool foreachCmd(string girdi)          {  return new ForeachCommand().Execute(girdi); }  
        public bool doWhileCmd(string girdi)          {  return new DoWhileCommand().Execute(girdi); }

        public bool whileCmd(string girdi)            {  return new WhileCommand().Execute(girdi); }
        public static string UseVariable(string girdi, Dictionary<string, string> geciciDegiskenler = null)
        {
            return new UseVariable().ReplaceVariables(girdi, geciciDegiskenler);
        }

        public bool cmdTernary(string girdi)          {  return new CmdTernary().Execute(girdi); }

        public void CalistirVeYazdir(string girdi)    {  new CalistirVeYazdir().Execute(girdi); }

        public static string RunCmd(string komut)
        {
            var psi = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/c " + komut)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = System.Diagnostics.Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                return !string.IsNullOrWhiteSpace(error) ? error : output;
            }
        }
 

} }
