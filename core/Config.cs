using System;
using System.IO;

namespace Core
{

	public class Config
	{

		public string DefaultPage { get; private set; }
		public string[] LineColor { get; private set; }
		public bool Note { get; private set; }

		public Config()
		{
			DefaultPage = "Display1";
			LineColor = new string[] { "Yellow", "White" };
			Note = false;
		}
		public void Load(string path)
		{
			try
			{
				if (!File.Exists(path))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("[HATA] Ayar dosyasi bulunamadi: " + path);
					Console.WriteLine("[BİLGİ] Varsayılan ayarlar kullanılıyor (DefaultPage=Display1).");
					Console.ResetColor();
					return;
				}

				var lines = File.ReadAllLines(path);
				foreach (var line in lines)
				{
					var parts = line.Split('=');
					if (parts.Length == 2)
					{
						var key = parts[0].Trim().ToLower();
						var value = parts[1].Trim();

						if (key == "defaultpage") DefaultPage = value;
						else if (key == "note")
						{
							bool noteValue;
							if (bool.TryParse(value, out noteValue))
							{
								Note = noteValue;
							}
						}
						else if (key == "linecolor") LineColor = value.Split(',');
					}
				}
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("[HATA] Ayar dosyasi okunurken bir sorun olustu: " + ex.Message);
				Console.WriteLine("[BİLGİ] Varsayılan ayarlar kullanılıyor.");
				Console.ResetColor();
			}
		}

	}
}