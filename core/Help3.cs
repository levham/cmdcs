using System;

namespace Core{
    public class Help3{
        public void help()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" \n=== CmdCs Example Guide ===\n");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" 1. General Commands ");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    help        ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Shows the Cmd help menu.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    help2       ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Lists CmdCs commands.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    help3       ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Example CmdCs commands.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    hdd         ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Displays free disk space in GB.");

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" 2. String Variables (set)");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    set a=3             ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Defines a variable (string/int).");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    echo $a             ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Prints the value of the variable.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    set b=25+33-20*4/2  ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Calculates the expression and assigns it as a string.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    set d=\"evet\"        ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Assigns a string in quotes.");

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" 3. Int Variables (int)");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    int a= 3            ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Defines an int variable.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    int b=25+33-20*4/2  ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Calculates the expression and assigns it as an int.");

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" 4. String Variables (string)");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    string d=\"evet\"       ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Defines a string variable.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    string e=\"evet\" +\"hayır\"");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Performs string concatenation.");

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" 5. Array Definitions");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    int[] t ={1}        ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Defines an int array.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    int[] y ={1,34}     ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Int array with multiple elements.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    string[] m ={\"abcd\"}");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Defines a string array.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    string[] z ={\"abcd\",\"bcd\"}");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// String array with multiple elements.");

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" 6. Ternary Commands");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    dir | find \"m\" ? echo evet : echo hayır");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Executes conditional command based on output.");

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(" 7. Loop Commands");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    int[] m= {1,2,3,4}            ");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Defines an array for foreach.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    foreach ($a in $m[]) { echo $a }");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Iterates over array using foreach.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    for (int i=0;i<5;i++){ echo $i }");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Standard for loop (0 to 4).");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    for (int i=5;i>1;i--){ echo $i }");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Countdown for loop.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    do { set i=0 } while ( $i < 5 ) { echo $i && i++ }");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Basic do-while loop.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    do { set i=0 && help } while ( $i < 5 ) { ... }");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Initial execution and multiple commands in do block.");
        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("    while ( $i <= 3 ) { echo $i && i++ }");
        Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("// Loop until $i exceeds 3, printing and incrementing each time.");


        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" ===========================");
        Console.ResetColor();
    }
}
}