using System;

namespace Core{
	public class Display{
	    public void display(){

	    	/// connect lib
	    		Lib lib=new Lib();

	        /// colourful text
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Microsoft Windows [Version 10.0.19045.6456]");
                Console.WriteLine("(c) Microsoft Corporation. Tüm hakları saklıdır.");

            //colourful text
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();

            //text hdd and text date time
                Console.WriteLine("                    " + lib.Hdd() + " " + DateTime.Now.ToString());
                Console.WriteLine(); 
		}
	}
}