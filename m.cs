using Core;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data;
using System.IO;

class m
{
    static void Main()
    {
        //connect lib
        Lib lib = new Lib();

        //colourful text
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Microsoft Windows [Version 10.0.19045.6456]");
        Console.WriteLine("(c) Microsoft Corporation. Tüm hakları saklıdır.");

        //colourful text
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();

        //text hdd and text date time
        Console.WriteLine("                    " + lib.Hdd() + " " + DateTime.Now.ToString());
        Console.WriteLine();

        //step number is "colourful text number" for cmdcs command line .
        int stepnumber = 0;

        while (true)
        {

            //for colourful cmdcs command line -> yellow--> white
            stepnumber++;
            Console.ForegroundColor = stepnumber == 1 ? ConsoleColor.Yellow : ConsoleColor.White;
            if (stepnumber == 2) stepnumber = 0;

            Console.Write(">> ");
 

            /// cmdcs command control area
                string girdi = Console.ReadLine();
                girdi=girdi.Trim();

bool isHandled = false;

if (string.IsNullOrWhiteSpace(girdi)) continue;

if (girdi == "help2") {
    lib.help2();
    isHandled = true;
}
else if (girdi == "hdd") {
    Console.WriteLine(lib.Hdd() + "gb free ");
    isHandled = true;
}
else if (lib.setfind(girdi)) isHandled = true;
else if (lib.find_intarray(girdi)) isHandled = true;
else if (lib.find_stringarray(girdi)) isHandled = true;
else if (lib.find_int(girdi)) isHandled = true;
else if (lib.find_string(girdi)) isHandled = true;
else if (lib.cmdTernary(girdi)) isHandled = true;

// Foreach en sona alındı
if (!isHandled)
{
    var foreachChecker = new ForeachCommand();
    
    if (lib.foreachCmd(girdi)) isHandled = true;
    else if (girdi.StartsWith("foreach"))
    {
        var checker = new ForeachCommand();
        if (!checker.CanExecute(girdi))
            Console.WriteLine("Foreach komutu geçersiz veya dizi tanımlı değil.");
        isHandled = true;
    }

}

 

if (!isHandled)
{
    string ifade = Lib.UseVariable(girdi);
    lib.CalistirVeYazdir(ifade);
}




    /*
                if (string.IsNullOrWhiteSpace(girdi)) continue;
               
            /// cmdcs help content
                if (girdi == "help2") {
                    lib.help2();
                    continue;
                }

            /// hdd free disk usage
                if (girdi == "hdd") {
                    Console.WriteLine(lib.Hdd()+"gb free ");
                    continue;
                }

            /// set a = 
                if (lib.setfind(girdi)) continue;

            //int[]
                if (lib.find_intarray(girdi)) continue;
            //string[]
                if (lib.find_stringarray(girdi)) continue;

            //int a=
                if (lib.find_int(girdi)) continue;
            //string a=
                if (lib.find_string(girdi)) continue;

            /// foreach ($a in $d[])
            if (lib.foreachCmd(girdi)) continue;
 
            /// dir | find "m" ? echo evet : echo hayır
                if (lib.cmdTernary(girdi)) continue;

           ///run cmd code 
                string ifade = Lib.UseVariable(girdi);
                lib.CalistirVeYazdir(ifade);
 
*/

        }
    }
}
