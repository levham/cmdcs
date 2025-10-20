using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Core
{
    public class IntArray
    {
 public bool FindIntArray(string girdi)
{
    girdi = girdi.Trim();

    // Başta "int[]" var mı kontrol et
    if (!girdi.StartsWith("int[]"))
        return false;

    // "=" işaretinin konumunu bul
    int eqIndex = girdi.IndexOf('=');
    if (eqIndex == -1)
        return false;

    // Süslü parantezlerin konumunu bul
    int braceStart = girdi.IndexOf('{', eqIndex);
    int braceEnd = girdi.IndexOf('}', braceStart);
    if (braceStart == -1 || braceEnd == -1)
        return false;

    // Dizi adını al
    string arrayName = girdi.Substring(5, eqIndex - 5).Trim(); // "int[]" 5 karakter

    // Dizi içeriğini al
    string rawValuesText = girdi.Substring(braceStart + 1, braceEnd - braceStart - 1);
    string[] rawValues = rawValuesText.Split(',');

    List<int> values = new List<int>();
    foreach (string val in rawValues)
    {
        int number;
        if (int.TryParse(val.Trim(), out number))
        {
            values.Add(number);
        }
    }

    // Lib'e ekle
    if (Lib.intArrays.ContainsKey(arrayName))
        Lib.intArrays[arrayName] = values.ToArray();
    else
        Lib.intArrays.Add(arrayName, values.ToArray());

    return true;
}



    }
}
