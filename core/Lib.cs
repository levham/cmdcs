using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace Core
{
    public class Lib
    {

        public static Config Config { get; private set; }
        public static void InitConfig(string path) { Config = new Config(); Config.Load(path); }


        // STATIC SÖZLÜKLER
        public static Dictionary<string, string> degiskenler = new Dictionary<string, string>();
        public static Dictionary<string, int[]> intArrays = new Dictionary<string, int[]>();
        public static Dictionary<string, string[]> stringArrays = new Dictionary<string, string[]>();

        // DİĞER KOMUTLAR (DEĞİŞİKLİK YOK)
        public void help2() { new Help2().help(); }
        public void help3() { new Help3().help(); }
        public void cls() { Console.Clear(); display(); }
        public void display()
        {
            // Ayar dosyasındaki 'defaultpage' değerine göre ilgili display metodunu çağır.
            if (Config.DefaultPage == "Display1")
            {
                new Display().display();
            }
            else
            {
                new Display().display2();
            }
        }
        public string Hdd() { return new Hdd().hdd_bilgi(); }



        // FileWriter örneği (Yakalama mantığını yönetir)
        // Constructor'da başlatılacak
        private FileWriter _fileWriter;

        public Lib()
        {
            // FileWriter constructor'ı güncellendiği için Lib'in kendisini iletiyoruz.
            _fileWriter = new FileWriter(this);
        }

        // DOSYA YAKALAMA İŞLEMLERİ

        public bool TryStartFileCapture(string girdi)
        {
            return _fileWriter.TryStartCapture(girdi);
        }

        public bool IsFileCapturing()
        {
            return _fileWriter.IsCapturing;
        }

        public bool HandleFileCaptureInput(string girdi)
        {
            return _fileWriter.HandleInputAndCheckEnd(girdi);
        }

        // KOMUT BULUCULAR (DEĞİŞİKLİK YOK)
        public bool setfind(string girdi) { return new SetFind().FindSet(girdi); }
        public bool find_int(string girdi) { return new IntFind().FindInt(girdi); }
        public bool find_string(string girdi) { return new StringFind().FindString(girdi); }

        public bool find_intarray(string girdi) { return new IntArray().FindIntArray(girdi); }
        public bool find_stringarray(string girdi) { return new StringArray().FindStringArray(girdi); }

        public bool forCmd(string girdi) { return new ForCommand().Execute(girdi); }
        public bool foreachCmd(string girdi) { return new ForeachCommand().Execute(girdi); }
        public bool doWhileCmd(string girdi) { return new DoWhileCommand().Execute(girdi); }

        public bool whileCmd(string girdi) { return new WhileCommand().Execute(girdi); }
        public static string UseVariable(string girdi, Dictionary<string, string> geciciDegiskenler = null)
        {
            return new UseVariable().ReplaceVariables(girdi, geciciDegiskenler);
        }

        public bool cmdTernary(string girdi) { return new CmdTernary().Execute(girdi); }

        /// Komutu CalistirVeYazdir sınıfına yönlendirir.
        public void CalistirVeYazdir(string ifade)
        {
            new CalistirVeYazdir().Execute(ifade);
        }
    }
}
