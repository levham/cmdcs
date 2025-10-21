using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace Core
{
    public class ForCommand
    {
        public bool Execute(string girdi)
        {
            // Temel sözdizimi kontrolü
            if (!girdi.StartsWith("for") || !girdi.Contains("(") || !girdi.Contains(")") || !girdi.Contains("{") || !girdi.EndsWith("}"))
                return false;

            int parantezBasla = girdi.IndexOf("(");
            int parantezBitis = girdi.IndexOf(")");
            int govdeBasla = girdi.IndexOf("{");
            int govdeBitis = girdi.LastIndexOf("}");

            if (parantezBasla == -1 || parantezBitis == -1 || govdeBasla == -1 || govdeBitis == -1)
                return false;

            string tanim = girdi.Substring(parantezBasla + 1, parantezBitis - parantezBasla - 1).Trim();
            string govde = girdi.Substring(govdeBasla + 1, govdeBitis - govdeBasla - 1).Trim();

            // for (int i=0; i<5; i++)
            string[] parts = tanim.Split(';');
            if (parts.Length != 3) return false;

            string init = parts[0].Trim();      // int i=0
            string condition = parts[1].Trim(); // i<5
            string increment = parts[2].Trim(); // i++

            // Değişken adı ve başlangıç değeri
            Match initMatch = Regex.Match(init, @"int\s+(\w+)\s*=\s*(\d+)");
            if (!initMatch.Success) return false;

            string varName = initMatch.Groups[1].Value;
            int startValue = int.Parse(initMatch.Groups[2].Value);

            // Döngü koşulu
            string conditionExpr = condition.Replace(varName, startValue.ToString());
            var evaluator = new EvaluateMath();

            // Döngü
            int i = startValue;
            while (true)
            {
                string expr = condition.Replace(varName, i.ToString());
                try
                {
                    if (!Convert.ToBoolean(evaluator.Evaluate(expr)))
                        break;
                }
                catch
                {
                    break;
                }

                // Gövdeyi çalıştır
                var gecici = new Dictionary<string, string> { { varName, i.ToString() } };
                string ifade = Lib.UseVariable(govde, gecici);

                // && ile birden fazla komut varsa ayır
                string[] commands = ifade.Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var cmd in commands)
                {
                    string trimmed = cmd.Trim();

                    // SET komutu ise yorumlayıcıya tanımla
                    if (new SetFind().FindSet(trimmed)) continue;

                    // CMD'ye gönder
                    string cikti = Lib.RunCmd(trimmed);
                    Console.WriteLine("  " + cikti);
                }








                // Artış
                if (increment == varName + "++") i++;
                else if (increment == varName + "--") i--;
                else
                {
                    Match incMatch = Regex.Match(increment, varName + @"\s*=\s*(.+)");
                    if (incMatch.Success)
                    {
                        string incExpr = incMatch.Groups[1].Value.Replace(varName, i.ToString());
                        i = (int)evaluator.Evaluate(incExpr);
                    }
                    else break;
                }
            }
          

            return true;
        }
    }
}
