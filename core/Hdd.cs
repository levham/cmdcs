using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Core{
	public class Hdd {
		/*
			public string test2() 
			{
				return "iyi";
			}
		*/
		public string hdd_bilgi(){
			DriveInfo[] suruculer = DriveInfo.GetDrives();
			string hdd_durum="";
			foreach (DriveInfo surucu in suruculer)
			{
			    if (surucu.IsReady)
			    {
			        if (surucu.Name == "C:\\"){ 
			           // hdd_durum="Hdd:"+FormatByte(surucu.TotalFreeSpace)+"/"+FormatByte(surucu.TotalSize);
			            hdd_durum=FormatByte(surucu.TotalFreeSpace);
			        }
			    }
			}
			return hdd_durum;
		}

		public string FormatByte(long byteDegeri){
			double gb = byteDegeri / (1024.0 * 1024 * 1024);
			//return gb.ToString("0.00") + " GB";
			return gb.ToString("0.00") ;
		}

	}
}