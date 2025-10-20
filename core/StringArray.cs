using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Core
{
    public class StringArray
    {
        
public bool FindStringArray(string girdi)
{
    // Başta "string[]" var mı kontrol et
    if (!girdi.StartsWith("string[]"))
        return false;

    // Değişken adını al
    int nameStart = girdi.IndexOf("string[]") + "string[]".Length;
    int nameEnd = girdi.IndexOf("=", nameStart);
    if (nameEnd == -1)
        return false;

    string arrayName = girdi.Substring(nameStart, nameEnd - nameStart).Trim();

    // Süslü parantez içindeki stringleri al
    int braceStart = girdi.IndexOf("{", nameEnd);
    int braceEnd = girdi.IndexOf("}", braceStart);
    if (braceStart == -1 || braceEnd == -1)
        return false;

    string rawValuesText = girdi.Substring(braceStart + 1, braceEnd - braceStart - 1);
    string[] rawValues = rawValuesText.Split(',');

    List<string> values = new List<string>();
    foreach (string val in rawValues)
    {
        string cleaned = val.Trim();
        if (cleaned.StartsWith("\"") && cleaned.EndsWith("\""))
        {
            cleaned = cleaned.Substring(1, cleaned.Length - 2); // Çift tırnakları çıkar
        }
        values.Add(cleaned);
    }

    // Lib'e ekle
    if (Lib.stringArrays.ContainsKey(arrayName))
        Lib.stringArrays[arrayName] = values.ToArray();
    else
        Lib.stringArrays.Add(arrayName, values.ToArray());

    return true;
}


    }
}
