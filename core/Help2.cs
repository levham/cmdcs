using System;

namespace Core{
    public class Help2{
        public void help(){

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" \n=== CmdCs Help Menu ===\n");
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(" General Commands");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("   help        ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("//help content for cmd");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("   help2        ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("//help content for cmd");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("   help3       ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("//example content for cmdcs");

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
            Console.WriteLine(" Loop Commands");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("   foreach ($a in $d[]){echo $a}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" // iterate over array");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("   for (int i=0;i<5;i++){ echo $i }");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" // for loop");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("   do { set i=0 } while ( $i < 5 ) { echo $i && i++ }");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" // do while loop");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("   while ( $i <= 3 ) { echo $i && i++ }");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" // while loop");

            Console.WriteLine();






            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" =====================");
            Console.ResetColor();

        }
    }
}
