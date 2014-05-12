using Stjornutextar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stjornutextar.ViewModels
{
	public class SaveSubtitleViewModel
	{
		public string Title { get; set; }
		public string Category { get; set; }
		public string Language { get; set; }
		public string MediaUrl { get; set; }
		public DateTime PublishDate { get; set; }
		public HttpPostedFileBase SubFile { get; set; }

		public List<SelectListItem> Categories { get; set; }
		public List<SelectListItem> Languages { get; set; }
		public List<SelectListItem> Titles { get; set; }

		//public List<Category> Categories { get; set; }
		//public List<Language> Languages { get; set; }
		//public List<Title> Titles { get; set; }
	}
}