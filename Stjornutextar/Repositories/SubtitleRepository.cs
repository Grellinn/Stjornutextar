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

		// Fall sem skilar fyrstu 10 skjátextunum úr gagnagrunni
		public List<Subtitle> GetFirst10Subtitles()
		{
			List<Subtitle> allSubtitles = db.Subtitles.OrderByDescending(s => s.PublishDate).Take(10).ToList();
			
			return allSubtitles;
		}

        // Fall sem sækir alla skjátexta í gagnagrunn eftir flokkum
        public List<Subtitle> GetSubtitlesByCategory(int? id)
        {
            List<Subtitle> subtitlesByCategory = db.Subtitles.Where(sub => sub.Category.ID == id).ToList();

            return subtitlesByCategory;
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
			List<Category> CatToReturn = db.Categories.ToList();

			return CatToReturn;
		}

		// Fall sem tekur öll tungumál úr gagnagrunni og vistar í Languages lista
		public List<Language> PopulateLanguages()
		{
			List<Language> LangToReturn = db.Languages.ToList();

			return LangToReturn;
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
			Subtitle getSubtitleById = db.Subtitles.Where(sub => sub.ID == id).SingleOrDefault();
			
			return getSubtitleById;
		}

		// Fall sem uppfærir Subtitle í gagnagrunni.
		public void UpdateSubtitle(Subtitle s)
		{
			Subtitle tempS = GetSubtitleById(s.ID);
			s.SubtitleParts = null;

			if (tempS != null)
			{
				// Uppfærum Category, Language og MediaURL
				tempS.Category = s.Category;
				tempS.Language = s.Language;
				tempS.MediaURL = s.MediaURL;
				SaveSubtitle();
			}
		}

		// Fall sem breytir stöðu á þýðingu
		public void ChangeSubtitleStatus(Subtitle subtitle)
		{

		}

		// Fall sem skilar tilviki af Category úr gagnagrunni út frá categoryID sem er sent inn
		internal Category GetCategory(int categoryId)
		{
			return db.Categories.Where(cat => cat.ID == categoryId).FirstOrDefault();
		}

		// Fall sem skilar tilviki af Language úr gagnagrunni út frá languageID sem er sent inn
		internal Language GetLanguage(int languageID)
		{
			return db.Languages.Where(lang => lang.ID == languageID).FirstOrDefault();
		}
	}	
}