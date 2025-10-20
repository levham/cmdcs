using System;
using System.Text.RegularExpressions;

namespace Core
{
    public class ForeachCommand
    {
 public bool Execute(string girdi)
{
    if (!girdi.StartsWith("foreach (") || !girdi.Contains(") {") || !girdi.EndsWith("}"))
        return false;

    int parantezBasla = girdi.IndexOf("(");
    int parantezBitis = girdi.IndexOf(")");
    int govdeBasla = girdi.IndexOf("{");
    int govdeBitis = girdi.LastIndexOf("}");

    if (parantezBasla == -1 || parantezBitis == -1 || govdeBasla == -1 || govdeBitis == -1)
        return false;

    string tanim = girdi.Substring(parantezBasla + 1, parantezBitis - parantezBasla - 1).Trim();
    string govde = girdi.Substring(govdeBasla + 1, govdeBitis - govdeBasla - 1).Trim();

    int inIndex = tanim.IndexOf(" in ");
    if (inIndex == -1) return false;

    string itemName = tanim.Substring(0, inIndex).Trim().TrimStart('$');
    string arrayRaw = tanim.Substring(inIndex + 4).Trim();

    if (!arrayRaw.StartsWith("$") || !arrayRaw.EndsWith("[]")) return false;

    string arrayName = arrayRaw.Substring(1, arrayRaw.Length - 3);

    string[] dizi = null;
    if (Lib.stringArrays.ContainsKey(arrayName))
        dizi = Lib.stringArrays[arrayName];
    else if (Lib.intArrays.ContainsKey(arrayName))
        dizi = Array.ConvertAll(Lib.intArrays[arrayName], x => x.ToString());
    else
        return false;

    foreach (var eleman in dizi)
    {
        string temp = govde.Replace("$" + itemName, eleman);
        string sonuc = Lib.UseVariable(temp);
        string cikti = Lib.RunCmd(sonuc);
        Console.WriteLine("  " + cikti);
    }

    return true;
}
  
public bool CanExecute(string girdi)
{
    // ) ile { arasındaki boşluğu kaldır
    girdi = girdi.Replace(") {", "){");

    if (!girdi.StartsWith("foreach") || !girdi.EndsWith("}"))
        return false;

    int parantezBasla = girdi.IndexOf("(");
    int parantezBitis = girdi.IndexOf(")");
    int govdeBasla = girdi.IndexOf("{");
    int govdeBitis = girdi.LastIndexOf("}");

    if (parantezBasla == -1 || parantezBitis == -1 || govdeBasla == -1 || govdeBitis == -1)
        return false;

    string tanim = girdi.Substring(parantezBasla + 1, parantezBitis - parantezBasla - 1).Trim();
    int inIndex = tanim.IndexOf(" in ");
    if (inIndex == -1) return false;

    string arrayRaw = tanim.Substring(inIndex + 4).Trim();
    if (!arrayRaw.StartsWith("$") || !arrayRaw.EndsWith("[]")) return false;

    string arrayName = arrayRaw.Substring(1, arrayRaw.Length - 3);

    return Lib.stringArrays.ContainsKey(arrayName) || Lib.intArrays.ContainsKey(arrayName);
}


    }
}
