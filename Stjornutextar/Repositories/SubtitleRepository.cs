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
		public List<Subtitle> GetAllSubtitles()
		{
			List<Subtitle> allSubtitles = db.Subtitles.OrderByDescending(s => s.PublishDate).ToList();
			
			return allSubtitles;
		}

		//fall sem sækir alla skjátexta í gagnagrunn eftir leitarstreng
		public List<Subtitle> GetSubtitleByName(string name)
		{
			List<Subtitle> subtitleByName = db.Subtitles.Where(s => s.Title.ToLower().Contains(name.ToLower())).ToList();

			return subtitleByName;
		}

		// Fall sem tekur alla flokka úr gagnagrunni og vistar í Categories lista
		public List<Category> PopulateCategories()
		{
			List<Category> CatToReturn = new List<Category>();
			
			foreach (var category in db.Categories)
			{
					CatToReturn.Add(category);
			}

			return CatToReturn;
		}

		// Fall sem tekur öll tungumál úr gagnagrunni og vistar í Languages lista
		public List<Language> PopulateLanguages()
		{
			List<Language> LangToReturn = new List<Language>();
			foreach (var language in db.Languages)
			{
				//if (LangToReturn.Where(a => a.ID == language.ID) == null)
					LangToReturn.Add(language);
			}

			return LangToReturn;
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

		// Fall sem bætir Subtitle í gagnagrunn.
		public void AddSubtitle(Subtitle s)
		{
			db.Subtitles.Add(s);
			SaveSubtitle();
		}

		// Fall sem vistar Subtitles í gagnagrunni.
		public void SaveSubtitle()
		{
			db.SaveChanges();
		}

		// Fall sem sækir Subtitle eftir Id-i hans eða null ef hann er ekki til.
		public Subtitle GetSubtitleById(int id)
		{
			Subtitle getSubtitleById = (from s in db.Subtitles
										 where s.ID == id
										select s).SingleOrDefault();

			return getSubtitleById;
		}

		// Fall sem uppfærir Subtitle í gagnagrunni.
		public void UpdateSubtitle(Subtitle s)
		{
			Subtitle tempS = GetSubtitleById(s.ID);

			if (tempS != null)
			{
				// Uppfærum Category, Language og MediaURL
				tempS.Category = s.Category;
				tempS.Language = s.Language;
				tempS.MediaURL = s.MediaURL;
				SaveSubtitle();
			}
		}

		internal Category GetCategory(int categoryId)
		{
			return db.Categories.Where(cat => cat.ID == categoryId).FirstOrDefault();
		}

		internal Language GetLanguage(int languageID)
		{
			return db.Languages.Where(lang => lang.ID == languageID).FirstOrDefault();
		}
	}	
}