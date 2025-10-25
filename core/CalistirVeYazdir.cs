using System;
using System.Linq;

namespace Core
{
    /// CalistirCmd.RunCmd metodu tarafından çalıştırılan harici komutların
    /// çıktısını konsola belirli bir formatta (padding ile) yazar.
    public class CalistirVeYazdir
    {
        /// Verilen komutu çalıştırır ve çıktısını konsola yazar.
        public void Execute(string komut)
        {
            // Komut çalıştırma işi CalistirCmd'ye devredilmiştir.
            string result = CalistirCmd.RunCmd(komut); 

            if (!string.IsNullOrWhiteSpace(result))
            {
                // Kullanıcının isteği üzerine boşluk satırı ekle
                Console.WriteLine("    "); 

                // Çıktıyı satırlara ayır ve her satıra padding ekle
                // C# 5.0 uyumu için Environment.NewLine kullanıldı.
                string[] lines = result.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                
                foreach (string line in lines)
                {
                    // Her satırın başına "    " padding eklenir.
                    Console.WriteLine("    " + line);
                }
            }
        }
    }
}