using System;
using System.Text.RegularExpressions;

namespace Core
{
    public class ForeachCommand
    {
  
public bool Execute(string girdi)
{
    // Temel sözdizimi kontrolü
    if (!girdi.StartsWith("foreach") || !girdi.Contains("(") || !girdi.Contains(")") || !girdi.Contains("{") || !girdi.EndsWith("}"))
        return false;

    int parantezBasla = girdi.IndexOf("(");
    int parantezBitis = girdi.IndexOf(")");
    int govdeBasla = girdi.IndexOf("{");
    int govdeBitis = girdi.LastIndexOf("}");

    if (parantezBasla == -1 || parantezBitis == -1 || govdeBasla == -1 || govdeBitis == -1)
        return false;

    // foreach ($a in $m[]) kısmını ayır
    string tanim = girdi.Substring(parantezBasla + 1, parantezBitis - parantezBasla - 1).Trim();
    int inIndex = tanim.IndexOf(" in ");
    if (inIndex == -1) return false;

    string itemName = tanim.Substring(0, inIndex).Trim().TrimStart('$');
    string arrayRaw = tanim.Substring(inIndex + 4).Trim();

    if (!arrayRaw.StartsWith("$") || !arrayRaw.EndsWith("[]")) return false;

    string arrayName = arrayRaw.Substring(1, arrayRaw.Length - 3);

    // Gövdeyi al
    string govde = girdi.Substring(govdeBasla + 1, govdeBitis - govdeBasla - 1).Trim();

    // Diziye eriş
    string[] dizi = null;
    if (Lib.stringArrays.ContainsKey(arrayName))
        dizi = Lib.stringArrays[arrayName];
    else if (Lib.intArrays.ContainsKey(arrayName))
        dizi = Array.ConvertAll(Lib.intArrays[arrayName], x => x.ToString());
    else
        return false;

    // Döngüyü çalıştır
    foreach (var eleman in dizi)
    {
        string temp = govde.Replace("$" + itemName, eleman);
        string ifade = Lib.UseVariable(temp);
        string cikti = CalistirCmd.RunCmd(ifade);
        Console.WriteLine("  " + cikti);
    }

    return true;
}




    }
}
