using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

namespace Core
{
    /// <summary>
    /// Batch Script (.bat) diline özgü yardımcı fonksiyonları ve çeviri mantığını içerir.
    /// Özellikle değişken formatlama, matematik işlemleri ve IF koşullarının çevrilmesinden sorumludur.
    /// </summary>
    public static class BatchHelper
    {
        private static int _labelCounter = 0;

        // C# benzeri operatörlerin Batch Script karşılıkları
        private static readonly Dictionary<string, string> ConditionOperators = new Dictionary<string, string>
        {
            {"<=", "LEQ"},  // Less than or Equal to
            {">=", "GEQ"},  // Greater than or Equal to
            {"==", "EQU"},  // Equal to
            {"!=", "NEQ"},  // Not Equal to
            {"<", "LSS"},   // Less than
            {">", "GTR"}    // Greater than
        };

        /// <summary>
        /// Benzersiz bir GOTO etiketi oluşturur.
        /// </summary>
        /// <param name="prefix">Etiket öneki (örn: WHILE_START).</param>
        /// <returns>Benzersiz etiket adı.</returns>
        public static string GetUniqueLabel(string prefix)
        {
            _labelCounter++;
            return prefix + "_" + _labelCounter;
        }

        /// <summary>
        /// C# değişkenlerini ($i) Batch değişken formatına (%i%) çevirir.
        /// </summary>
        /// <param name="input">İfade.</param>
        /// <returns>Batch değişkenleri ile güncellenmiş ifade.</returns>
        public static string TranslateVariables(string input)
        {
            // $var ifadesini %var% olarak değiştirir.
            return Regex.Replace(input, @"\$([a-zA-Z0-9_]+)", "%$1%");
        }

        /// <summary>
        /// C# benzeri matematiksel işlemleri (i++, i--) Batch'in SET /A formatına çevirir.
        /// </summary>
        /// <param name="input">Giriş ifadesi (örn: i++).</param>
        /// <returns>Batch SET /A komutu.</returns>
        public static string TranslateMathOperation(string input)
        {
            string trimmed = input.Trim();

            // 1. Artırma (i++) ve Azaltma (i--)
            Match match = Regex.Match(trimmed, @"^([a-zA-Z0-9_]+)(\+\+|--)$");
            if (match.Success)
            {
                string varName = match.Groups[1].Value;
                string op = match.Groups[2].Value == "++" ? "+" : "-";
                // i++ -> set /a i=%i%+1
                return string.Format("set /a {0}=%{0}%{1}1", varName, op);
            }

            // 2. Basit SET komutları (set i=0, int i=5)
            // Sadece 'int' anahtar kelimesini kaldırıp 'set /a' ekler.
            if (trimmed.StartsWith("set ", StringComparison.OrdinalIgnoreCase) || trimmed.Contains("="))
            {
                string command = trimmed;
                if (command.StartsWith("int ", StringComparison.OrdinalIgnoreCase))
                {
                    command = command.Substring(4).Trim(); // "int " kısmını kaldır
                }
                if (!command.StartsWith("set /a", StringComparison.OrdinalIgnoreCase))
                {
                    // Değişken ataması set /a gerektiriyorsa (örn: i=0)
                    if (Regex.IsMatch(command, @"^[a-zA-Z0-9_]+\s*=\s*[0-9]+"))
                    {
                        return "set /a " + command;
                    }
                }
            }

            return input; // Eşleşme yoksa orijinali döndür
        }

        /// <summary>
        /// C# koşul ifadelerini Batch koşul formatına çevirir ve döngüden çıkış mantığı için
        /// koşulun Batch karşılığını oluşturur.
        /// Örn: "%i% <= 3" -> "%i% LEQ 3"
        /// </summary>
        /// <param name="condition">C# benzeri koşul ifadesi.</param>
        /// <returns>Batch Script IF komutu için çevrilmiş koşul.</returns>
        public static string TranslateCondition(string condition)
        {
            // Değişkenleri zaten TranslateVariables() ile çevrilmiş kabul ediyoruz (%i% < 5)
            string batchCondition = condition.Trim();

            foreach (var op in ConditionOperators)
            {
                // Regex'i kullanarak sol ve sağ operandları operatörle ayır.
                Match match = Regex.Match(batchCondition,
                    string.Format(@"^\s*(?<Left>.+?)\s*{0}\s*(?<Right>.+?)\s*$", Regex.Escape(op.Key)));

                if (match.Success)
                {
                    string left = match.Groups["Left"].Value.Trim();
                    string right = match.Groups["Right"].Value.Trim();
                    // Koşulu doğrudan Batch operatörüyle değiştir. Örn: "%i% < 5" -> "%i% LSS 5"
                    return string.Format("{0} {1} {2}", left, op.Value, right);
                }
            }

            // Hiçbir operatör eşleşmezse, orijinal koşulu döndür
            // (Bu durumda Batch IF'in koşulu olduğu gibi kabul edilir, bu riskli olabilir.)
            return batchCondition;
        }
    }
}
