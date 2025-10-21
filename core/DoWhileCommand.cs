using System;
using System.Text.RegularExpressions;

namespace Core
{
    public class DoWhileCommand
    {
        public bool Execute(string girdi)
        {
            // Komutun temel yapısını kontrol et
            if (!girdi.Trim().StartsWith("do") || !girdi.Contains("while") || !girdi.Contains("{") || !girdi.Contains("}"))
                return false;

            // do, while ve body bloklarını ayıkla
            Match match = Regex.Match(girdi, @"do\s*{(?<body>.*)}\s*while\s*\((?<condition>.*)\)\s*{(?<loop>.*)}", RegexOptions.Singleline);

            if (!match.Success) return false;

            string doBlock = match.Groups["body"].Value.Trim();
            string condition = match.Groups["condition"].Value.Trim();
            string loopBody = match.Groups["loop"].Value.Trim();

            var evaluator = new EvaluateMath(); // Bu sınıfın sizde olduğunu varsayıyorum.

            // 1. DO BLOĞUNU BİR KERE ÇALIŞTIR
            ExecuteCommands(doBlock);

            // 2. WHILE DÖNGÜSÜ
            int loopLimiter = 0; // Sonsuz döngüleri engellemek için bir güvenlik önlemi
            while (true)
            {
                loopLimiter++;
                if (loopLimiter > 1000)
                {
                    Console.WriteLine("Hata: Döngü limiti aşıldı (1000 iterasyon).");
                    break;
                }

                // DEĞİŞTİ: Koşul içindeki değişkenleri değerleriyle değiştiriyoruz ($i -> 0 gibi)
                string resolvedCondition = Lib.UseVariable(condition);

                try
                {
                    // Değerlendirme sonucu boolean değilse döngüden çık
                    if (!Convert.ToBoolean(evaluator.Evaluate(resolvedCondition)))
                        break;
                }
                catch
                {
                    Console.WriteLine("Hata: Koşul çözümlenemedi: " + resolvedCondition);
                    break;
                }

                // Döngü içeriğini çalıştır
                ExecuteCommands(loopBody);
            }

            return true;
        }

        // YENİ: Komutları yürüten yardımcı bir metot ekledik
      private void ExecuteCommands(string commands)
{
    // BURADA DEĞİŞKENLER ÇÖZÜMLENİYOR
   // string expandedCommands = Lib.UseVariable(commands);
         
            string[] commandList = commands.Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cmd in commandList)
            {
                string trimmedCmd = cmd.Trim();
                if (string.IsNullOrWhiteSpace(trimmedCmd)) continue;
        if (new SetFind().FindSet(trimmedCmd)) continue; 

                // YENİ: i++ ve i-- gibi artırma/azaltma işlemlerini burada yakalıyoruz
        Match incrementMatch = Regex.Match(trimmedCmd, @"^(\w+)(\+\+|--)$");
                if (incrementMatch.Success)
                {
                    string varName = incrementMatch.Groups[1].Value;
                    string operation = incrementMatch.Groups[2].Value;
                    
                    // DÜZELTME: 'out' değişkenleri C# 5 uyumluluğu için dışarıda tanımlandı.
                    string currentValueStr;
                    int currentValue;

                    if (Lib.degiskenler.TryGetValue(varName, out currentValueStr) && int.TryParse(currentValueStr, out currentValue))
                    {
                        if (operation == "++")
                        {
                            currentValue++;
                        }
                        else // --
                        {
                            currentValue--;
                        }
                        Lib.degiskenler[varName] = currentValue.ToString();
                    }
                    else
                    {
                        // DÜZELTME: C# eski versiyonları için $ kullanımı kaldırıldı.
                        Console.WriteLine("Hata: '" + varName + "' değişkeni bulunamadı veya sayısal değil.");
                    }
                    continue; // Bu komut işlendi, sıradakine geç
                }


        // 3. Diğer tüm komutları çalıştırırken DEĞİŞKEN ÇÖZÜMLEME yap
        string ifade = Lib.UseVariable(trimmedCmd); // Komutu çalıştırmadan hemen önce çözümlüyoruz.
        
        // Sadece değişken çözümleme boş döndüyse ve komut boş değilse çalıştır.
        if (!string.IsNullOrWhiteSpace(ifade))
        {
             // Diğer tüm komutları cmd.exe'de çalıştır
             string result = Lib.RunCmd(ifade);
             if (!string.IsNullOrWhiteSpace(result))
             {
                 Console.WriteLine("  " + result.Trim());
             }
        }
            }
        }
    }
}

