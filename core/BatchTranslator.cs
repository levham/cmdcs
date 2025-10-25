using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// C# benzeri döngü ve koşul komutlarını Batch Script (.bat) diline çevirir.
    /// </summary>
    public class BatchTranslator
    {
        private Lib _libInstance;

        public BatchTranslator(Lib libInstance)
        {
            _libInstance = libInstance;
        }

        /// <summary>
        /// Giriş ifadesini alır, eğer çevrilebilir bir yapıysa Batch koduna çevirir.
        /// Aksi takdirde orijinal ifadeyi döndürür.
        /// </summary>
        public string TranslateCommand(string input)
        {
            // C# 5.0 uyumluluğu için out değişkenlerini burada tanımlıyoruz
            string ternaryResult;
            string loopResult;

            // 1. Ternary Operatörü (?:) Çevirisi
            if (TryTranslateTernary(input, out ternaryResult))
            {
                return ternaryResult;
            }

            // 2. Loop Yapıları (for, while, do-while, foreach) Çevirisi
            if (TryTranslateLoop(input, out loopResult))
            {
                return loopResult;
            }

            // 3. Basit Değişken ve İşlem Çevirileri (i++, i--, set)
            // Örn: i++ veya set i=10
            string processedInput = BatchHelper.TranslateMathOperation(input);
            processedInput = BatchHelper.TranslateVariables(processedInput);

            // Geleneksel echo komutu
            if (processedInput.Trim().StartsWith("echo", StringComparison.OrdinalIgnoreCase))
            {
                // echo komutu içindeki değişkenleri Batch formatına çevir
                return processedInput;
            }

            return input; // Hiçbir çeviri kuralı eşleşmezse orijinali döndür
        }

        // --- TERNARY ÇEVİRİ ---

        private bool TryTranslateTernary(string input, out string batchCode)
        {
            // Örn: dir | find "m" ? echo evet : echo hayır
            Match match = Regex.Match(input, @"^(?<Condition>.+)\s*\?\s*(?<TrueCommand>.+)\s*:\s*(?<FalseCommand>.+)$", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                // 1. Koşul komutunu yakala
                string condition = match.Groups["Condition"].Value.Trim();
                string trueCmd = match.Groups["TrueCommand"].Value.Trim();
                string falseCmd = match.Groups["FalseCommand"].Value.Trim();

                // BatchHelper kullanarak etiketleri tanımla
                string falseLabel = BatchHelper.GetUniqueLabel("TER_FALSE");
                string endLabel = BatchHelper.GetUniqueLabel("TER_END");

                batchCode = new StringBuilder()
                    .AppendLine("rem Ternary Komutu Baslangic")
                    .AppendLine(condition) // Komutu çalıştır (FIND/DIR gibi)
                                           // Hata varsa (yani bulamazsa ERRORLEVEL 1), False'a git
                    .AppendLine("IF ERRORLEVEL 1 GOTO " + falseLabel)
                    .AppendLine(BatchHelper.TranslateVariables(trueCmd)) // True komutunu çalıştır
                    .AppendLine("GOTO " + endLabel)
                    .AppendLine(":" + falseLabel) // False Label
                    .AppendLine(BatchHelper.TranslateVariables(falseCmd)) // False komutunu çalıştır
                    .AppendLine(":" + endLabel) // End Label
                    .ToString();

                return true;
            }

            batchCode = null;
            return false;
        }

        // --- LOOP ÇEVİRİSİ ---

        private bool TryTranslateLoop(string input, out string batchCode)
        {
            // Batch'te foreach ve int[] tanımlaması doğrudan desteklenmediği için 
            // şimdilik sadece 'for' ve 'while' yapılarını hedef alıyoruz.

            // 1. WHILE Çevirisi
            // Örn: while ( $i <= 3 ) { echo $i && i++ }
            Match whileMatch = Regex.Match(input, @"^\s*while\s*\((?<Condition>.+)\)\s*{(?<Body>.+)}$", RegexOptions.IgnoreCase);
            if (whileMatch.Success)
            {

                // Condition ifadesindeki değişkenleri Batch formatına çevir ($i -> %i%)
                string rawCondition = whileMatch.Groups["Condition"].Value.Trim();
                string condition = BatchHelper.TranslateCondition(BatchHelper.TranslateVariables(rawCondition));

                string body = whileMatch.Groups["Body"].Value.Trim();

                string startLabel = BatchHelper.GetUniqueLabel("WHILE_START");
                string endLabel = BatchHelper.GetUniqueLabel("WHILE_END");

                // Body içindeki komutları & ile ayır ve çevir (i++ -> set /a i=%i%+1)
                string[] commands = body.Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder bodyCode = new StringBuilder();
                foreach (var cmd in commands)
                {
                    bodyCode.AppendLine(BatchHelper.TranslateMathOperation(BatchHelper.TranslateVariables(cmd.Trim())));
                }

                // TranslateCondition, koşul YANLIŞ ise GOTO yapılması için ters koşulu döndürür.
                batchCode = new StringBuilder()
                    .AppendLine("rem While Loop Baslangic")
                    .AppendLine(":" + startLabel)

                    // Koşul kontrolü: Koşul DOĞRU DEĞİLSE döngüyü bitir.
                    // Örn: while(i<5) -> IF NOT %i% LSS 5 GOTO WHILE_END
                    .AppendLine("IF NOT " + condition + " GOTO " + endLabel)

                    .AppendLine(bodyCode.ToString())
                    .AppendLine("GOTO " + startLabel)
                    .AppendLine(":" + endLabel)
                    .ToString();

                return true;
            }

            // 2. FOR Çevirisi 
            // Örn: for (int i=0; i<5; i++){ echo $i }
            Match forMatch = Regex.Match(input, @"^\s*for\s*\(\s*(?<Init>.*?)\s*;\s*(?<Condition>.*?)\s*;\s*(?<Step>.*?)\s*\)\s*{(?<Body>.+)}$", RegexOptions.IgnoreCase);
            if (forMatch.Success)
            {
                // Init: int i=0 -> set /a i=0
                string init = forMatch.Groups["Init"].Value.Trim().Replace("int ", "set /a ").Replace("=", "=").Replace(" ", "");

                // Condition: i<5 -> NOT %i% GEQ 5 (Koşulun tersi)
                string rawCondition = forMatch.Groups["Condition"].Value.Trim();
                string condition = BatchHelper.TranslateCondition(BatchHelper.TranslateVariables(rawCondition));

                // Step: i++ -> set /a i=%i%+1
                string step = BatchHelper.TranslateMathOperation(forMatch.Groups["Step"].Value.Trim());

                // Body: echo $i
                string body = forMatch.Groups["Body"].Value.Trim();

                string startLabel = BatchHelper.GetUniqueLabel("FOR_START");
                string endLabel = BatchHelper.GetUniqueLabel("FOR_END");

                batchCode = new StringBuilder()
                    .AppendLine("rem For Loop Baslangic")
                    .AppendLine(init) // Başlangıç Değeri
                    .AppendLine(":" + startLabel)

                    // Koşul kontrolü: Koşul DOĞRU DEĞİLSE döngüyü bitir.
                    // Örn: for(...; i<5; ...) -> IF NOT %i% LSS 5 GOTO FOR_END
                    .AppendLine("IF NOT " + condition + " GOTO " + endLabel)

                    // Döngü Gövdesi
                    .AppendLine(BatchHelper.TranslateVariables(body))

                    // Adım (Step)
                    .AppendLine(step)
                    .AppendLine("GOTO " + startLabel)
                    .AppendLine(":" + endLabel)
                    .ToString();

                return true;
            }

            batchCode = null;
            return false;
        }

        // --- DİĞER KOMUTLAR (foreach, int[] gibi C# özel komutları şimdilik desteklenmiyor) ---
    }
}
