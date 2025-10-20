using System.Text.RegularExpressions;
using System.Data;

namespace Core
{
    public class IntFind
    {
 

        public bool FindInt(string girdi)
        {
            Match match = Regex.Match(girdi, @"int\s+(\w+)\s*=\s*(.+)");
            if (!match.Success) return false;

            string key = match.Groups[1].Value;
            string expr = Lib.UseVariable(match.Groups[2].Value); // ← düzeltildi

            try
            {
                double result = new EvaluateMath().Evaluate(expr);
                Lib.degiskenler[key] = result.ToString();
            }
            catch
            {
                Lib.degiskenler[key] = expr;
            }

            return true;
        }



    }
}
