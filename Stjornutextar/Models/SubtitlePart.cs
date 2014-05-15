using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stjornutextar.Models
{
	public class SubtitlePart
	{
		public int ID { get; set; }
		public int partNumber { get; set; }
		public string time { get; set; }
		public string text1 { get; set; }
		public string text2 { get; set; }
		public string translatedText1 { get; set; }
		public string translatedText2 { get; set; }

		public int SubtitleID { get; set; }
		public Subtitle Subtitle { get; set; }
	}
}