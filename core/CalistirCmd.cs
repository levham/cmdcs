using System;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Core
{
    /// <summary>
    /// CMD veya harici uygulamaları çalıştırma ve dahili komutları (CD) işleme sorumluluğunu taşır.
    /// Çıktı formatlaması ile ilgilenmez.
    /// </summary>
    public static class CalistirCmd
    {
        /// <summary>
        /// Komutu bir CMD süreci üzerinden çalıştırır veya dahili olarak işler (CD gibi).
        /// </summary>
        public static string RunCmd(string komut)
        {
            string trimmedKomut = komut.Trim();
            
            // --- CD KOMUTU İŞLEME (Dahili Dizin Değişikliği) ---
            if (trimmedKomut.StartsWith("cd ", StringComparison.OrdinalIgnoreCase) || 
                trimmedKomut.Equals("cd", StringComparison.OrdinalIgnoreCase) ||
                trimmedKomut.Equals("cd..", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    // "cd" komutundan sonraki kısmı al (dizin yolu)
                    string path = trimmedKomut.Length > 2 && trimmedKomut.StartsWith("cd", StringComparison.OrdinalIgnoreCase) ? trimmedKomut.Substring(2).Trim() : "";

                    // Eğer yol boşsa (sadece 'cd' yazılmışsa), mevcut dizini göster.
                    if (string.IsNullOrEmpty(path))
                    {
                        return Directory.GetCurrentDirectory();
                    }
                    
                    // Dizini değiştir.
                    Directory.SetCurrentDirectory(path);
                    return ""; // Başarılı, çıktı yok
                }
                catch (Exception ex)
                {
                    return "Hata: Dizin değiştirilemedi. Lütfen yolu kontrol edin. (" + ex.Message + ")";
                }
            }
            // --- CD KOMUTU İŞLEME SONU ---
            
            // Diğer tüm komutlar için CMD sürecini kullanmaya devam et
            var psi = new ProcessStartInfo("cmd.exe", "/c " + komut)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    return !string.IsNullOrWhiteSpace(error) ? error : output;
                }
            }
            catch (Exception ex)
            {
                return "Hata: Komut çalıştırılırken bir sorun oluştu: " + ex.Message;
            }
        }
    }
}
