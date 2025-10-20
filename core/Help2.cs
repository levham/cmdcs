using System;

namespace Core{
    public class Help2{
        public void help2(){

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine(" \n=== CmdCs Help Menu ===\n");
Console.ForegroundColor = ConsoleColor.Cyan;

Console.WriteLine(" General Commands");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   help        ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//help content for cmd");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   help2       ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//help content for cmdcs");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   hdd         ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//show gb in free disk");

Console.WriteLine();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine(" Variables Commands");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   set         ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//define string and int variable");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   int         ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//define int variable");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   string      ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//define string variable");

Console.WriteLine();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine(" Array Commands");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   int[]       ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//define int array");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   string[]    ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//define string array");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   $dizi.length");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//array length");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   $dizi[0]    ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//array index");

Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   $dizi[]     ");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("//array");

Console.WriteLine();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine(" Ternary Commands");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   command | find \"\" ? true command : false command");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(" // ternary operation");

Console.WriteLine();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine(" Foreach");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("   foreach ($a in $d[]){echo $a}");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(" // iterate over array");

Console.WriteLine();

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine(" =====================");
Console.ResetColor();

        }
    }
}
