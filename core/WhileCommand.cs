using System;
using System.Text.RegularExpressions; 
using System.Linq;
namespace Core
{
    public class WhileCommand
    {
        public bool Execute(string girdi)
        {
            // Komutun temel yapısını kontrol et
            // while ( $a < 5 ) { echo $a && i++ } formatını yakalamalı
            if (!girdi.Trim().StartsWith("while") || !girdi.Contains("{") || !girdi.Contains("}"))
                return false;

            // while ve body bloklarını ayıkla
            Match match = Regex.Match(girdi, @"while\s*\((?<condition>.*)\)\s*{(?<body>.*)}", RegexOptions.Singleline);

            if (!match.Success) return false;

            string condition = match.Groups["condition"].Value.Trim();
            string loopBody = match.Groups["body"].Value.Trim();

            var evaluator = new EvaluateMath(); // Var olduğunu varsayıyoruz.
            var lib = new Lib(); // Komut çalıştırmak için

            int loopLimiter = 0; // Sonsuz döngüleri engellemek için güvenlik önlemi
            while (true)
            {
                loopLimiter++;
                if (loopLimiter > 1000)
                {
                    Console.WriteLine("Hata: Döngü limiti aşıldı (1000 iterasyon).");
                    break;
                }

                // Koşul içindeki değişkenleri değerleriyle değiştiriyoruz
                string resolvedCondition = Lib.UseVariable(condition);

                try
                {
                    // Koşulu değerlendir. False ise döngüden çık.
                    if (!Convert.ToBoolean(evaluator.Evaluate(resolvedCondition)))
                        break;
                }
                catch
                {
                    // Değişken tanımlanmamışsa veya koşul çözümlenemezse
                    Console.WriteLine("Hata: Koşul çözümlenemedi: " + resolvedCondition);
                    break;
                }

                // Döngü içeriğini çalıştır (DoWhileCommand'dan kopyalanan mantık)
                ExecuteCommands(loopBody, lib);
            }

            return true;
        }



        // Komutları yürüten yardımcı metot. DoWhileCommand'daki ile aynı olmalı.
        private void ExecuteCommands(string commands, Lib lib)
        {
            string[] commandList = commands.Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cmd in commandList)
            {
                string trimmedCmd = cmd.Trim();
                if (string.IsNullOrWhiteSpace(trimmedCmd)) continue;

                // 'set' komutunu kontrol et
                if (new SetFind().FindSet(trimmedCmd)) continue;

                // i++ ve i-- gibi artırma/azaltma işlemlerini yakala
                Match incrementMatch = Regex.Match(trimmedCmd, @"^(\w+)(\+\+|--)$");
                if (incrementMatch.Success)
                {
                    string varName = incrementMatch.Groups[1].Value;
                    string operation = incrementMatch.Groups[2].Value;
                    
                    string currentValueStr;
                    int currentValue;

                    if (Lib.degiskenler.TryGetValue(varName, out currentValueStr) && int.TryParse(currentValueStr, out currentValue))
                    {
                        if (operation == "++") { currentValue++; }
                        else { currentValue--; }
                        Lib.degiskenler[varName] = currentValue.ToString();
                    }
                    else
                    {
                        Console.WriteLine("Hata: '" + varName + "' değişkeni bulunamadı veya sayısal değil.");
                    }
                    continue; 
                }

                // Diğer tüm komutları çalıştırırken DEĞİŞKEN ÇÖZÜMLEME yap
                string ifade = Lib.UseVariable(trimmedCmd); 
                
               if (!string.IsNullOrWhiteSpace(ifade))
		        {
		             string result = CalistirCmd.RunCmd(ifade);

		             if (!string.IsNullOrWhiteSpace(result))
		             {
		                 // C# 5 UYUMLULUK DÜZELTMESİ: All() metodu yerine manuel kontrol
		                 bool resultIsNumeric = true;
		                 string trimmedResult = result.Trim();
		                 
		                 // Sadece rakam içerip içermediğini kontrol et
		                 if (trimmedResult.Length > 0)
		                 {
		                     foreach (char c in trimmedResult)
		                     {
		                         if (!char.IsDigit(c) && !char.IsWhiteSpace(c)) // Boşluk da olabilir
		                         {
		                             resultIsNumeric = false;
		                             break;
		                         }
		                     }
		                 }
		                 else
		                 {
		                     resultIsNumeric = false;
		                 }


		                 // Echo çıktısı sadece sayılardan oluşuyorsa (i++ sonrası)
		                 if (trimmedCmd.StartsWith("echo", StringComparison.OrdinalIgnoreCase) && resultIsNumeric)
		                 {
		                      // C# 5 UYUMLULUK DÜZELTMESİ: String birleştirme hatasını önlemek için Format kullanın
		                      // veya basit string birleştirme kullanın.
		                      // Console.WriteLine(string.Format(" {0}", trimmedResult)); // Daha güvenli C# 5
		                      Console.WriteLine(" " + trimmedResult); // Basit birleştirme
		                 }
		                 // Diğer komut çıktıları
		                 else if (!trimmedCmd.StartsWith("echo", StringComparison.OrdinalIgnoreCase))
		                 {
		                     // C# 5 UYUMLULUK DÜZELTMESİ:
		                     // Console.WriteLine(string.Format(" {0}", trimmedResult)); // Daha güvenli C# 5
		                     Console.WriteLine(" " + trimmedResult); // Basit birleştirme
		                 }
		             }
		        }
            }
        }




    }
}