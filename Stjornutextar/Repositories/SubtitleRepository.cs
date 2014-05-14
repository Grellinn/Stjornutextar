﻿using Stjornutextar.Models;
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
		public IEnumerable<Subtitle> GetAllSubtitles()
		{
			var allSubtitles = (from s in db.Subtitles
								orderby s.PublishDate descending
								select s);

			return allSubtitles;
		}

		//fall sem sækir alla skjátexta í gagnagrunn eftir leitarstreng
		public IEnumerable<Subtitle> GetSubtitleByName(string name)
		{
			var subByName = db.Subtitles.Where(t => t.Title.Contains(name));

			return subByName;
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
		public Subtitle GetSubtitleById(int? id)
		{
			var getSubtitleById = (from s in db.Subtitles
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
	}	
}