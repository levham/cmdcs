using System.Text.RegularExpressions;
using System.Data;

namespace Core
{
   public class SetFind
{
    public bool FindSet(string girdi)
    {
        Match match = Regex.Match(girdi, @"set\s*(\w+)\s*=\s*(.+)");
        if (!match.Success) return false;

        string key = match.Groups[1].Value;
        string rawValue = match.Groups[2].Value;

        string expr = Lib.UseVariable(rawValue); // $x gibi ifadeleri çöz

        string result;
        try
        {
            double value = new EvaluateMath().Evaluate(expr);
            result = value.ToString(); // örn: "3"
        }
        catch
        {
            result = expr; // düz metin olarak sakla
        }

        if (Lib.degiskenler.ContainsKey(key))
            Lib.degiskenler[key] = result;
        else
            Lib.degiskenler.Add(key, result);

        return true;
    }
}

}
