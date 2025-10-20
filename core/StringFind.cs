using System.Text.RegularExpressions;

namespace Core
{
    public class StringFind
    {
        public bool FindString(string girdi)
        {
            // string e="evet" + "hayır"
            Match match = Regex.Match(girdi, @"^string\s+(\w+)\s*=\s*(.+)$");
            if (!match.Success) return false;

            string key = match.Groups[1].Value;
            string expr = match.Groups[2].Value;

            // Tüm çift tırnak içindeki stringleri bul
            MatchCollection strings = Regex.Matches(expr, "\"(.*?)\"");
            string result = "";

            foreach (Match str in strings)
            {
                result += str.Groups[1].Value;
            }

            if (Lib.degiskenler.ContainsKey(key))
                Lib.degiskenler[key] = result;
            else
                Lib.degiskenler.Add(key, result);

            return true;
        }

    }
}
