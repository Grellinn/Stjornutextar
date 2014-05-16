using Stjornutextar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stjornutextar.ViewModels
{
	public class FileType : ValidationAttribute
	{
		private string _fileExtention;

		public FileType(string fileExtention)
		{
			_fileExtention = fileExtention;
		}

		public override bool IsValid(object value)
		{
			// don't duplicate the required validator - return true
			if (value == null)
			{
				return true;
			}

			// is the file extention correct?
			var postedFileName = ((HttpPostedFileBase)value).FileName.ToLower();

			if (postedFileName.Substring(postedFileName.LastIndexOf('.') + 1) != _fileExtention)
			{
				return false;
			}

			return true;
		}
	}
	
	public class SubtitleViewModel
	{
		public Subtitle Subtitle { get; set; }
		public List<Category> Categories { get; set; }
		public List<Language> Languages { get; set; }
		[Required]
		[FileType("srt", ErrorMessage = "Vinsamlegast settu inn .srt skrá.")] // Verður að hlaða inn .srt skrá
		public HttpPostedFileBase SubtitleFile { get; set; }
	}
}