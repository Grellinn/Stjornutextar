using Stjornutextar.Models;
using Stjornutextar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stjornutextar.Repositories
{
	public class SubtitleRepository
	{
		private static SubtitleRepository _instance;

		public static SubtitleRepository Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SubtitleRepository();
				return _instance;
			}
		}

		// Búum til tilvik af gagnagrunninum okkar til að vinna með.
		ApplicationDbContext db = new ApplicationDbContext();

		// Fall sem sækir alla skjátexta í gagnagrunn og skilar 10 nýjustu
		public IEnumerable<Subtitle> GetFirst10Subtitles()
		{
			var first10Subtitles = (from s in db.Subtitles
									orderby s.PublishDate ascending
									select s).Take(10);
			
			return first10Subtitles;
		}

		// Fall sem sækir Subtitles eftir Id-i hans eða null ef hann er ekki til.
		public Subtitle GetSubtitleById(int? id)
		{
			var getSubtitleById = (from s in db.Subtitles
								   where s.ID == id
								   select s).SingleOrDefault();

			return getSubtitleById;
		}

		// Fall sem vistar Subtitles í gagnagrunni.
		public void SaveSubtitle()
		{
			db.SaveChanges();
		}

		// Fall sem býr til Subtitle í gagnagrunni.
		public void AddSubtitle(Subtitle s)
		{
			db.Subtitles.Add(s);
		}

		// Fall sem uppfærir Subtitle eftir Id-inu hans í gagnagrunninum og skilar
		// þér í Index eða NotFound View ef engu er skilað til baka
		public void UpdateSubtitle(Subtitle s)
		{
			Subtitle subtitleByID = GetSubtitleById(s.ID);

			if (subtitleByID != null)
			{
				subtitleByID.MediaURL = s.MediaURL;
				subtitleByID.PublishDate = s.PublishDate;
				subtitleByID.Status = s.Status;
				subtitleByID.SubFile = s.SubFile;
				subtitleByID.Title = s.Title;
				subtitleByID.Votes = s.Votes;
				subtitleByID.Language = s.Language;
				subtitleByID.Category = s.Category;
				subtitleByID.CommentID = s.CommentID;
				subtitleByID.TitleID = s.TitleID;
				subtitleByID.CategoryID = s.CategoryID;
				subtitleByID.LanguageID = s.LanguageID;

				db.SaveChanges();
			}
		}

		public void CreateSubtitle(SaveSubtitleViewModel sVM)
		{
			Subtitle newSubtitle = new Subtitle();

			#region tekið úr ViewModel yfir í Subtitle
			newSubtitle.Title = sVM.Title;
			newSubtitle.Language = sVM.Language;
			newSubtitle.Category = sVM.Category;
			newSubtitle.MediaURL = sVM.MediaUrl;
			#endregion

			#region Viðbótarupplýsingar fyrir Subtitle
			newSubtitle.PublishDate = DateTime.Now;
			newSubtitle.Status = "Óklárað";
			newSubtitle.Votes = 0;
			#endregion

			AddSubtitle(newSubtitle); 
			SaveSubtitle();
		}

		// Fall sem eyðir út Subtitle í gagnagrunni.
		public void RemoveSubtitle(Subtitle s)
		{
			db.Subtitles.Remove(s);
		}

		// Fall sem skilar lista af flokkum úr gagnagrunni.
		public List<SelectListItem> FeedCategoryList()
		{
			List<SelectListItem> categories = new List<SelectListItem>();
			
			foreach (var c in db.Categories)
			{
				string tempValue = Convert.ToString(c.ID);
				categories.Add(new SelectListItem { Text = c.CategoryName, Value = tempValue });
			}

			return categories;
		}

		// Fall sem skilar lista af flokkum úr gagnagrunni.
		public List<SelectListItem> FeedLanguageList()
		{
			List<SelectListItem> languages = new List<SelectListItem>();

			foreach (var l in db.Languages)
			{
				string tempValue = Convert.ToString(l.ID);
				languages.Add(new SelectListItem { Text = l.LanguageName, Value = tempValue });
			}

			return languages;
		}

		// Fall sem skilar lista af titlum úr gagnagrunni.
		public List<SelectListItem> FeedTitleList()
		{
			List<SelectListItem> titles = new List<SelectListItem>();

			foreach (var t in db.Titles)
			{
				string tempValue = Convert.ToString(t.ID);
				titles.Add(new SelectListItem { Text = t.TitleName, Value = tempValue });
			}

			return titles;
		}
	}
}