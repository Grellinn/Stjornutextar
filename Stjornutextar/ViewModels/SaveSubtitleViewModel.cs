using Stjornutextar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stjornutextar.ViewModels
{
	public class SaveSubtitleViewModel
	{
		//public string Title { get; set; }
		//public string Category { get; set; }
		//public string Language { get; set; }
		//public string MediaUrl { get; set; }
		//public DateTime PublishDate { get; set; }
		//[NotMapped]
		//public HttpPostedFileBase SubFile { get; set; }
		public Subtitle Subtitle { get; set; }
		//[NotMapped]
		//public List<SelectListItem> Categories { get; set; }
		//[NotMapped]
		//public List<SelectListItem> Languages { get; set; }
		//public List<SelectListItem> Titles { get; set; }

		public List<Category> Categories { get; set; }
		public List<Language> Languages { get; set; }
		
		//public IEnumerable<Category> Categories = new List<Category>();
		//public IEnumerable<Language> Languages = new List<Language>();
		//public List<Title> Titles { get; set; }
	}
}