using Core;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;
using System.IO;



class Program
{
    static void Main()
    {



        /// connect lib
        Lib lib = new Lib();

        // Ayar dosyasının tam yolunu, programın çalıştığı dizine göre belirle.
        // Bu, FileNotFoundException hatasını önler.
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string settingsPath = Path.Combine(baseDirectory, "Setting.txt");
        Lib.InitConfig(settingsPath);

        // Ayar dosyasından okunan değeri ve hangi ekranın yüklendiğini teyit etmek için bir mesaj ekleyelim.
        // Renk ayarını da dosyadan alalım.
        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Lib.Config.LineColor[0], true);

        Console.WriteLine();
        Console.ResetColor();



        /// diplay for cmdcs
        lib.display();



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
            girdi = girdi.Trim();

            bool isHandled = false;
            if (string.IsNullOrWhiteSpace(girdi)) continue;




            /// Eğer yakalama modundaysak, girdiyi FileWriter'a ilet.
            if (lib.IsFileCapturing())
            {
                // HandleFileCaptureInput, } komutunu görürse dosyayı kaydeder ve true döner.
                if (lib.HandleFileCaptureInput(girdi))
                {
                    continue;
                }
                // Yakalamaya devam et
                continue;
            }


            // Dosya Yakalamayı Başlatma (Zincirin ilk sırasında olmalı)
            if (lib.TryStartFileCapture(girdi))
            {
                isHandled = true;
            }




            /// cls for cmdcs
            if (girdi == "cls") { lib.cls(); continue; }


            /// help for cmdcs
            if (girdi == "help2") { lib.help2(); isHandled = true; }
            if (girdi == "help3") { lib.help3(); isHandled = true; }

            /// hdd free disk usage
            else if (girdi == "hdd") { Console.WriteLine(lib.Hdd() + "gb free "); isHandled = true; }

            /// set a = 
            else if (lib.setfind(girdi)) isHandled = true;

            //int[]
            else if (lib.find_intarray(girdi)) isHandled = true;
            //string[]
            else if (lib.find_stringarray(girdi)) isHandled = true;
            //int a=
            else if (lib.find_int(girdi)) isHandled = true;
            //string a=
            else if (lib.find_string(girdi)) isHandled = true;

            /// dir | find "m" ? echo evet : echo hayır
            else if (lib.cmdTernary(girdi)) isHandled = true;

            /// foreach ($a in $d[])
            if (lib.foreachCmd(girdi))
            {
                isHandled = true;
            }

            /// for ( int i=0; i<5 ;i++){ echo $i  }   
            if (lib.forCmd(girdi))
            {
                isHandled = true;
            }
            /// dowhile
            if (lib.doWhileCmd(girdi))
            {
                isHandled = true;
            }

            /// while
            if (lib.whileCmd(girdi)) // YENİ EKLENECEK KONTROL
            {
                isHandled = true;
            }


            /// run cmd code 
            if (!isHandled)
            {
                string ifade = Lib.UseVariable(girdi);
                lib.CalistirVeYazdir(ifade);
            }

        }





    }
}
