﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stjornutextar.Models
{
	public class Subtitle
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string Status { get; set; }
		public string MediaURL { get; set; }
		public string SubFile { get; set; }
		public DateTime PublishDate { get; set; }

		#region Foreign Keys
		public int CategoryID { get; set; }
		public int LanguageID { get; set; }
		public int CommentID { get; set; }
		public int TitleID { get; set; }
		#endregion
	}
}