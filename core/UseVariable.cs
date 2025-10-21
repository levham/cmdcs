using System;

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core
{ 
    public class UseVariable
    { 

public string ReplaceVariables(string girdi, Dictionary<string, string> geciciDegiskenler = null)
{
    string sonuc = girdi;

    // 0. Geçici değişkenler varsa önce onları uygula
    if (geciciDegiskenler != null)
    {
        foreach (var kvp in geciciDegiskenler)
        {
            string target = "$" + kvp.Key;
            sonuc = sonuc.Replace(target, kvp.Value);
        }
    }


    // 1. Dizi elemanları: $dizi[2]
    int start = sonuc.IndexOf("$");
    while (start != -1)
    {
        int bracketOpen = sonuc.IndexOf("[", start);
        int bracketClose = bracketOpen != -1 ? sonuc.IndexOf("]", bracketOpen) : -1;

        if (bracketOpen != -1 && bracketClose != -1)
        {
            string arrayName = sonuc.Substring(start + 1, bracketOpen - start - 1);
            string indexText = sonuc.Substring(bracketOpen + 1, bracketClose - bracketOpen - 1);


            //if (int.TryParse(indexText, out int index) && index >= 0)
            int index;
            if (int.TryParse(indexText, out index))
            {
                string replacement = "";
                if (Lib.intArrays.ContainsKey(arrayName) && index < Lib.intArrays[arrayName].Length)
                    replacement = Lib.intArrays[arrayName][index].ToString();
                else if (Lib.stringArrays.ContainsKey(arrayName) && index < Lib.stringArrays[arrayName].Length)
                    replacement = Lib.stringArrays[arrayName][index];

                string target = "$" + arrayName + "[" + indexText + "]";
                sonuc = sonuc.Replace(target, replacement);
                start = sonuc.IndexOf("$", start + replacement.Length);
            }
            else
            {
                start = sonuc.IndexOf("$", start + 1);
            }
        }
        else
        {
            start = sonuc.IndexOf("$", start + 1);
        }
    }

    // 2. Dizi uzunluğu: $dizi.length
    Match lengthMatch = Regex.Match(sonuc, @"\$(\w+)\.length");
    while (lengthMatch.Success)
    {
        string arrayName = lengthMatch.Groups[1].Value;
        string replacement = "";

        if (Lib.intArrays.ContainsKey(arrayName))
            replacement = Lib.intArrays[arrayName].Length.ToString();
        else if (Lib.stringArrays.ContainsKey(arrayName))
            replacement = Lib.stringArrays[arrayName].Length.ToString();

        sonuc = sonuc.Replace(lengthMatch.Value, replacement);
        lengthMatch = Regex.Match(sonuc, @"\$(\w+)\.length");
    }

    // 3. Tüm dizi: $dizi[]
    start = sonuc.IndexOf("$");
    while (start != -1)
    {
        int end = sonuc.IndexOf("[]", start);
        if (end != -1)
        {
            string arrayName = sonuc.Substring(start + 1, end - start - 1);
            string replacement = "";

            if (Lib.intArrays.ContainsKey(arrayName))
                replacement = string.Join(" ", Lib.intArrays[arrayName]);
            else if (Lib.stringArrays.ContainsKey(arrayName))
                replacement = string.Join(" ", Lib.stringArrays[arrayName]);

            string target = "$" + arrayName + "[]";
            sonuc = sonuc.Replace(target, replacement);
            start = sonuc.IndexOf("$", start + replacement.Length);
        }
        else
        {
            break;
        }
    }

    // 4. Normal değişkenler: $isim
    foreach (var kvp in Lib.degiskenler)
    {
        string target = "$" + kvp.Key;
        if (sonuc.Contains(target))
            sonuc = sonuc.Replace(target, kvp.Value);
    }
 

// 5. Dizi adıyla tüm içeriği: $dizi
foreach (var kvp in Lib.intArrays)
{
    string target = "$" + kvp.Key;
    if (sonuc.Contains(target))
        sonuc = sonuc.Replace(target, string.Join(" ", kvp.Value));
}
foreach (var kvp in Lib.stringArrays)
{
    string target = "$" + kvp.Key;
    if (sonuc.Contains(target))
        sonuc = sonuc.Replace(target, string.Join(" ", kvp.Value));
}


/*

MatchCollection kalanlar = Regex.Matches(sonuc, @"\$(\w+)");
foreach (Match m in kalanlar)
{
    string key = m.Groups[1].Value;

    bool tanimli =
        (geciciDegiskenler != null && geciciDegiskenler.ContainsKey(key)) ||
        Lib.degiskenler.ContainsKey(key) ||
        Lib.intArrays.ContainsKey(key) ||
        Lib.stringArrays.ContainsKey(key);

    if (!tanimli)
    {
        // Eğer geçici değişkenler içinde varsa uyarı verme
        Console.WriteLine("Hata: Tanımsız değişken: $" + key);
    }
}

*/




    return sonuc;
}




    }
}
