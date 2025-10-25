using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Core
{
    // Dosya yakalama (capturing) durumunu ve işlemlerini yöneten sınıf.
    public class FileWriter
    {
        private bool _isFileCaptureMode = false;
        private string _currentFileName = "";
        private StringBuilder _fileContentBuffer = new StringBuilder();
        private string _currentExtension = ""; 
        private Lib _libInstance; // Lib referansını saklamak için eklendi
        private BatchTranslator _batchTranslator; // Çevirici örneği

        // Kısaltma Sözlüğü (Sadece BAT için örnek)
        private static readonly Dictionary<string, string> BatAbbreviations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"eo", "@echo off"},
            {"e", "echo"},
            {"cls", "cls"},
            {"pause", "pause"},
            {"exit", "exit"},
        };
        
        // Constructor, Lib instance'ını alacak şekilde güncellendi
        public FileWriter(Lib libInstance)
        {
            _libInstance = libInstance;
            // BatchTranslator'ı başlatırken Lib örneğini geçiriyoruz
            _batchTranslator = new BatchTranslator(_libInstance);
        }

        // Dosya yakalama modunun açık olup olmadığını döndürür.
        public bool IsCapturing
        {
            get { return _isFileCaptureMode; }
        }

        // ----------------------------------------------------
        // BAŞLANGIÇ İŞLEMİ
        // ----------------------------------------------------

        // Girdiyi kontrol eder ve yakalama modunu başlatırsa true döndürür.
        public bool TryStartCapture(string girdi)
        {
            string trimmedInput = girdi.Trim();
            
            Match match = Regex.Match(trimmedInput, @"^public\s+class\s+(?<FileName>\w+)\s+:\s+(?<Extension>\w+)\s*{$", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string fileName = match.Groups["FileName"].Value;
                string extension = match.Groups["Extension"].Value.ToLower();
                
                if (!string.IsNullOrWhiteSpace(fileName) && !string.IsNullOrWhiteSpace(extension))
                {
                    string newFileName = fileName + "." + extension;
                    
                    _currentFileName = newFileName;
                    _currentExtension = extension;
                    _isFileCaptureMode = true;
                    
                    Console.WriteLine("Dosya yakalama modu açıldı. " + _currentFileName + " dosyasına yazılıyor. Çıkmak için '}' yazın.");
                    
                    return true;
                }
            }
            return false;
        }

        // ----------------------------------------------------
        // YAKALAMA VE BİTİRME İŞLEMLERİ
        // ----------------------------------------------------

        private bool CheckEndCommand(string girdi)
        {
            return girdi.Trim() == "}";
        }

        /// <summary>
        /// Sadece belirli uzantılar için kısaltmaları açar.
        /// </summary>
        private string ProcessAbbreviations(string girdi)
        {
            // Eğer uzantı BAT değilse veya boşluk içermiyorsa kısaltma arama
            if (_currentExtension != "bat") return girdi;

            string trimmed = girdi.Trim();
            
            // İlk kelimeyi (komutu) bul
            int firstSpace = trimmed.IndexOf(' ');
            string command = firstSpace == -1 ? trimmed : trimmed.Substring(0, firstSpace);
            string args = firstSpace == -1 ? "" : trimmed.Substring(firstSpace).TrimStart();

            // Kısaltma sözlüğünde komut varsa değiştir
            string fullCommand; // C# 5.0 uyumluluğu için değişkeni dışarıda tanımla
            if (BatAbbreviations.TryGetValue(command, out fullCommand))
            {
                return fullCommand + (string.IsNullOrEmpty(args) ? "" : " " + args);
            }
            
            return girdi; 
        }

        // Yakalama modundayken girdiyi işler. Bittiğinde true döndürür.
        public bool HandleInputAndCheckEnd(string girdi)
        {
            if (CheckEndCommand(girdi))
            {
                WriteContentToFile(_currentFileName, _fileContentBuffer.ToString());
                
                _isFileCaptureMode = false;
                _currentFileName = "";
                _fileContentBuffer.Clear();
                _currentExtension = "";
                return true; 
            }
            
            string processedGirdi = girdi;

            // 1. Kısaltmaları İşle (eo -> @echo off)
            processedGirdi = ProcessAbbreviations(processedGirdi);

            // 2. Eğer BAT dosyası ise C# benzeri yapıları çevir
            if (_currentExtension == "bat")
            {
                // BatchTranslator'ı kullanarak C# benzeri yapıyı Batch koduna çevirir.
                processedGirdi = _batchTranslator.TranslateCommand(processedGirdi);
            }

            // Normal İçerik Ekle (Çevrilmiş veya orijinal haliyle)
            _fileContentBuffer.AppendLine(processedGirdi);
            return false; 
        }

        // ----------------------------------------------------
        // DOSYA YAZMA METODU
        // ----------------------------------------------------

        private void WriteContentToFile(string fileName, string content)
        {
            try
            {
                string finalContent = content;
                string extension = Path.GetExtension(fileName).ToLower();

                // Uzantı .bat ise, içeriğin başına @echo off ve kod etiketini ekle
                if (extension == ".bat")
                {
                    // Eğer zaten @echo off yoksa ekle
                    if (!content.TrimStart().StartsWith("@echo off", StringComparison.OrdinalIgnoreCase))
                    {
                        finalContent = "@echo off" + Environment.NewLine + finalContent;
                    }
                    // Batch dosyasının sonunda GOTO :EOF ekleyerek fonksiyonel koddan sonraki etiketlere atlamayı sağlar
                    finalContent += Environment.NewLine + "GOTO :EOF" + Environment.NewLine;
                }

                File.WriteAllText(fileName, finalContent);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Başarılı: '" + fileName + "' dosyasına " + finalContent.Length + " byte içerik yazıldı.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hata: Dosya yazılırken bir sorun oluştu: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}
