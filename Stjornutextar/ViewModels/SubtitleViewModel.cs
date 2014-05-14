using Stjornutextar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stjornutextar.ViewModels
{
	public class SubtitleViewModel
	{
		public Subtitle Subtitle { get; set; }
		public List<Category> Categories { get; set; }
		public List<Language> Languages { get; set; }
		public HttpPostedFileBase UlSubtitleFile { get; set; }
	}
}