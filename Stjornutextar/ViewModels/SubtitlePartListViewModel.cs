using Stjornutextar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stjornutextar.ViewModels
{
	public class SubtitlePartListViewModel
	{
		public Subtitle Subtitle { get; set; }
		public List<Category> Categories { get; set; }
		public List<Language> Languages { get; set; }
		public List<SubtitlePart> SubtitleParts { get; set; }
	}
}