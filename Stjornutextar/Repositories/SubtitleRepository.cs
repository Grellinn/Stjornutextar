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

		// Fall sem tekur alla flokka úr gagnagrunni og vistar í Categories lista
		public List<Category> PopulateCategories()
		{
			List<Category> Categories = new List<Category>();

			foreach (var c in db.Categories)
			{
				Categories.Add(new Category { CategoryName = c.CategoryName, ID = c.ID });
			}
			
			return Categories;
		}
		
		// Fall sem tekur öll tungumál úr gagnagrunni og vistar í Languages lista
		public List<Language> PopulateLanguages()
		{
			List<Language> Languages = new List<Language>();
			
			foreach (var l in db.Languages)
			{
				Languages.Add(new Language { LanguageName = l.LanguageName, ID = l.ID });
			}
			
			return Languages;
		}

		// Fall sem sækir nafn á tungumáli út frá Id
		public string GetLanguageName(int id)
		{
			string languageName = (from l in db.Languages
								  where id == l.ID
								  select l.LanguageName).SingleOrDefault();

			return languageName;
		}

		// Fall sem sækir nafn á flokk út frá Id
		public string GetCategoryName(int id)
		{
			string categoryName = (from c in db.Categories
								   where id == c.ID
								   select c.CategoryName).SingleOrDefault();

			return categoryName;
		}
		
		
		// Fall sem eyðir út Subtitle í gagnagrunni.
		public void RemoveSubtitle(Subtitle s)
		{
			db.Subtitles.Remove(s);
		}

		// Fall sem vistar Subtitles í gagnagrunni.
		public void SaveSubtitle()
		{
			db.SaveChanges();
		}

		// Fall sem bætir Subtitle í gagnagrunn.
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
				subtitleByID.Language = (from l in db.Languages
										 where s.Language == Convert.ToString(l.ID)
										 select l.LanguageName).SingleOrDefault();
				subtitleByID.Category = (from c in db.Categories
										 where s.Category == Convert.ToString(c.ID)
										 select c.CategoryName).SingleOrDefault();
				//subtitleByID.CommentID = s.CommentID;
				//subtitleByID.TitleID = s.TitleID;
				//subtitleByID.CategoryID = s.CategoryID;
				//subtitleByID.LanguageID = s.LanguageID;

				db.SaveChanges();
			}
		}

		// Fall sem sækir Subtitle eftir Id-i hans eða null ef hann er ekki til.
		public Subtitle GetSubtitleById(int? id)
		{
			var getSubtitleById = (from s in db.Subtitles
								   where s.ID == id
								   select s).SingleOrDefault();

			return getSubtitleById;
		}

	}
}