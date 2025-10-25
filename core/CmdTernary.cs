using System.Text.RegularExpressions;
using System.Diagnostics;
using System;

namespace Core
{
    /// <summary>
    /// CMD çıktısı tabanlı koşullu komut çalıştırma (Ternary) sınıfı.
    /// Format: [anaKomut] | find "aranan" ? [doğruKomut] : [yanlışKomut]
    /// </summary>
    public class CmdTernary
    {  
        public bool Execute(string girdi)
        {
            // Regex: Grup 1: anaKomut, Grup 2: aranan, Grup 3: doğruKomut, Grup 4: yanlışKomut
            Match match = Regex.Match(girdi, @"(.+?)\s*\|\s*find\s+""(.+?)""\s*\?\s*(.+?)\s*:\s*(.+)");
            if (!match.Success) return false;

            string anaKomut = match.Groups[1].Value.Trim();
            string aranan = match.Groups[2].Value.Trim();
            string dogruKomut = match.Groups[3].Value.Trim();
            string yanlisKomut = match.Groups[4].Value.Trim();

            // 1. Hata Düzeltildi: anaKomutu çalıştırmak için CalistirCmd.RunCmd kullanılır.
            string cikti = CalistirCmd.RunCmd(anaKomut);

            // 2. Koşula göre çalıştırılacak komut seçilir.
            string secilenKomut = cikti.Contains(aranan) ? dogruKomut : yanlisKomut;
            
            // Seçilen komut içindeki değişkenleri çöz
            secilenKomut = Lib.UseVariable(secilenKomut); 

            // 3. Hata Düzeltildi: Seçilen komutu çalıştırmak ve çıktısını formatlamak için 
            // Lib.CalistirVeYazdir metodu kullanılır. Bu, CalistirVeYazdir.Execute'yi çağırır.
            Lib lib = new Lib(); 
            lib.CalistirVeYazdir(secilenKomut);
            
            return true;
        }
    }
}
