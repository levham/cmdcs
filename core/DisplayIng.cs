using System;

namespace Core
{
    public class DisplayIng
    {
        public void display()
        {

            /// connect lib
            //	Lib lib=new Lib();

            /// colourful text
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Microsoft Windows [Version 10.0.19045.6456]");
            Console.WriteLine("(C) Microsoft Corporation. All rights reserved.");

            //colourful text
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();

            //text hdd and text date time
            //  Console.WriteLine("                    " + lib.Hdd() + " " + DateTime.Now.ToString());
            //  Console.WriteLine(); 
        }
    }
}