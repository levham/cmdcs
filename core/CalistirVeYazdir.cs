using System;

namespace Core
{
    public class CalistirVeYazdir
    {
        public void Execute(string komut)
        {
            Console.WriteLine("   ");
            Console.WriteLine("   " + Lib.RunCmd(komut));
        }
    }
}
