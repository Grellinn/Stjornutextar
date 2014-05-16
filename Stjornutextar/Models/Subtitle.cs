using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stjornutextar.Models
{
	public class Subtitle
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string Status { get; set; }
		public string MediaURL { get; set; }
		public DateTime PublishDate { get; set; }
		public int Votes { get; set; }
		public int countTranslations { get; set; }
		
		public string SubtitleFileText { get; set; }

		public Category Category { get; set; }
		public Language Language { get; set; }

		public List<SubtitlePart> SubtitleParts { get; set; }
	}
}