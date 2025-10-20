using System.Text.RegularExpressions;
using System.Diagnostics;
using System;

namespace Core
{
    public class CmdTernary
    {  
        public bool Execute(string girdi)
        {
            Match match = Regex.Match(girdi, @"(.+?)\s*\|\s*find\s+""(.+?)""\s*\?\s*(.+?)\s*:\s*(.+)");
            if (!match.Success) return false;

            string anaKomut = match.Groups[1].Value.Trim();
            string aranan = match.Groups[2].Value.Trim();
            string dogruKomut = match.Groups[3].Value.Trim();
            string yanlisKomut = match.Groups[4].Value.Trim();

            string cikti = Lib.RunCmd(anaKomut);

            string secilenKomut = cikti.Contains(aranan) ? dogruKomut : yanlisKomut;
            secilenKomut = Lib.UseVariable(secilenKomut); // değişkenleri çöz

            Console.WriteLine("   ");
            Console.WriteLine("   " + Lib.RunCmd(secilenKomut));
            return true;
        }



    }
}
