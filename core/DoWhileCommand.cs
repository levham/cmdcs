using System;
using System.Text.RegularExpressions;

namespace Core
{
    /// <summary>
    /// do-while döngü yapısını işler ve döngü komutlarını çalıştırır.
    /// Format: do {komut_1 && komut_2} while (koşul) {komut_3 && komut_4}
    /// </summary>
    public class DoWhileCommand
    {
        public bool Execute(string girdi)
        {
            // Komutun temel yapısını kontrol et
            if (!girdi.Trim().StartsWith("do") || !girdi.Contains("while") || !girdi.Contains("{") || !girdi.Contains("}"))
                return false;

            // do, while ve body bloklarını ayıkla
            // NOT: Regex, do bloğu ve loop bloğu için farklı süslü parantez grupları bekliyor.
            // Bu format, mevcut komut yapınızla tutarlı kabul edilmiştir.
            Match match = Regex.Match(girdi, @"do\s*{(?<body>.*)}\s*while\s*\((?<condition>.*)\)\s*{(?<loop>.*)}", RegexOptions.Singleline);

            if (!match.Success) return false;

            string doBlock = match.Groups["body"].Value.Trim();
            string condition = match.Groups["condition"].Value.Trim();
            string loopBody = match.Groups["loop"].Value.Trim();

            // EvaluateMath ve Lib nesnesi (gerektiğinde)
            // Varsayım: EvaluateMath sınıfınız mevcuttur.
            var evaluator = new EvaluateMath(); 
            Lib lib = new Lib();

            // 1. DO BLOĞUNU BİR KERE ÇALIŞTIR
            ExecuteCommands(doBlock, lib);

            // 2. WHILE DÖNGÜSÜ
            int loopLimiter = 0; 
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
                    // Koşul çözümlenir.
                    if (!Convert.ToBoolean(evaluator.Evaluate(resolvedCondition)))
                        break;
                }
                catch
                {
                    Console.WriteLine("Hata: Koşul çözümlenemedi veya hatalı matematiksel ifade içeriyor: " + resolvedCondition);
                    break;
                }

                // Döngü içeriğini çalıştır
                ExecuteCommands(loopBody, lib);
            }

            return true;
        }

        /// <summary>
        /// Tek bir komut bloğu içindeki komutları (&& ile ayrılmış) sırayla yürüten yardımcı metot.
        /// </summary>
        private void ExecuteCommands(string commands, Lib lib)
        {
            string[] commandList = commands.Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var cmd in commandList)
            {
                string trimmedCmd = cmd.Trim();
                if (string.IsNullOrWhiteSpace(trimmedCmd)) continue;

                // 1. SET KOMUTU KONTROLÜ
                // Lib'deki yönlendirici metodu kullanıyoruz.
                if (lib.setfind(trimmedCmd)) continue; // SetFind'i direkt çağırmaktan kaçınılır.

                // 2. ARTIRMA/AZALTMA İŞLEMLERİ (i++ / i--)
                Match incrementMatch = Regex.Match(trimmedCmd, @"^(\w+)(\+\+|--)$");
                if (incrementMatch.Success)
                {
                    string varName = incrementMatch.Groups[1].Value;
                    string operation = incrementMatch.Groups[2].Value;
                    
                    // C# 5.0 uyumluluğu için out değişkenleri dışarıda tanımlandı.
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
                        Console.WriteLine("Hata: '" + varName + "' değişkeni bulunamadı veya sayısal değil.");
                    }
                    continue; // Bu komut işlendi, sıradakine geç
                }
                
                // 3. DİĞER KOMUTLAR (echo, dir, vb.)
                
                // Komutu çalıştırmadan hemen önce değişken çözümleme yapılır
                string ifade = Lib.UseVariable(trimmedCmd);
                
                // Çalıştırılabilir bir ifade varsa, Lib'in formatlayıcı metodunu kullan
                if (!string.IsNullOrWhiteSpace(ifade))
                {
                    // Komutu CalistirCmd.RunCmd ile çalıştırıp çıktısını formatlı (paddingli) şekilde yazdırır.
                    lib.CalistirVeYazdir(ifade);
                }
            }
        }
    }
}
