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
		public string Category { get; set; }
		public string Language { get; set; }
		public string Status { get; set; }
		public string MediaURL { get; set; }
		public DateTime PublishDate { get; set; }
		public int Votes { get; set; }
		[NotMapped]
		public HttpPostedFileBase SubFile { get; set; }

		//#region Foreign Keys
		//public int CategoryID { get; set; }
		//public int LanguageID { get; set; }
		//public int CommentID { get; set; }
		//public int TitleID { get; set; }
		//#endregion
	}
}