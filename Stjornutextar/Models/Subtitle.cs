using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stjornutextar.Models
{
	public class UrlType : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			// don't duplicate the required validator - return true
			if (value == null)
			{
				return true;
			}

			// is the file extention correct?
			//var postedFileName = ((HttpPostedFileBase)value).FileName.ToLower();
			string temp = value.ToString();
			
			if (temp.StartsWith("http://") != true && temp.StartsWith("https://") != true)
			{
				return false;
			}

			return true;
		}
	}
	
	public class Subtitle
	{
		public int ID { get; set; }
		[Required(ErrorMessage = "Title is required!")] // Verður að taka gildi, gefur annars villumeldingu
		public string Title { get; set; }
		public string Status { get; set; }
		[UrlType(ErrorMessage = "Vinsamlegast hafðu hlekkinn á eftirfarandi formi: http:/www.example.com/")] // Verður að hlaða inn .srt skrá
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