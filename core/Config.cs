using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Core
{

	public class Config
	{

		public string DefaultPage { get; private set; }
		public string[] LineColor { get; private set; }
		public bool Note { get; private set; }

		/// <summary>
		/// C# 5.0 uyumluluğu için varsayılan değerleri yapıcı metot içinde atar.
		/// </summary>
		public Config()
		{
			DefaultPage = "Display.cs";
			LineColor = new string[] { "Yellow", "White" };
			Note = false;
		}
		public void Load(string path)
		{
			var lines = File.ReadAllLines(path);
			foreach (var line in lines)
			{
				if (line.StartsWith("defaultpage="))
					DefaultPage = line.Substring(line.IndexOf('=') + 1).Trim();
				else if (line.StartsWith("note="))
				{
					// C# 7.0 öncesi uyumluluk için 'out' değişkeni önceden tanımlanmalıdır.
					bool noteValue;
					bool.TryParse(line.Substring(line.IndexOf('=') + 1).Trim(), out noteValue);
					Note = noteValue;
				}
				else if (line.StartsWith("linecolor="))
					LineColor = line.Substring(line.IndexOf('=') + 1).Trim().Split(',');
			}
		}
	}
}