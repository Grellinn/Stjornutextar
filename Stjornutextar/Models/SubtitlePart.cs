using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stjornutextar.Models
{
	public class SubtitlePart
	{
		public int ID { get; set; }
		public int PartNumber { get; set; }
		public string Time { get; set; }

		public List<SubtitlePartText> SubtitlePartTexts { get; set; }
	}
}