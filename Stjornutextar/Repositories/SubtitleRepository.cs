using Stjornutextar.DAL;
using Stjornutextar.Models;
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
		AppContext db = new AppContext();

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
				subtitleByID.CategoryID = s.CategoryID;
				subtitleByID.LanguageID = s.LanguageID;
				subtitleByID.MediaURL = s.MediaURL;
				subtitleByID.PublishDate = s.PublishDate;
				subtitleByID.Status = s.Status;
				subtitleByID.SubFile = s.SubFile;
				subtitleByID.Title = s.Title;
				subtitleByID.Votes = s.Votes;
				subtitleByID.CommentID = s.CommentID;
				subtitleByID.TitleID = s.TitleID; 
			}
		}

		// Fall sem eyðir út Subtitle í gagnagrunni.
		public void RemoveSubtitle(Subtitle s)
		{
			db.Subtitles.Remove(s);
		}

		// Fall sem skilar lista af flokkum úr gagnagrunni.
		public void FeedCategoryList()
		{
			List<SelectListItem> categories = new List<SelectListItem>();

			categories.Add(new SelectListItem { Text = "", Value = "" });
			categories.Add(new SelectListItem { Text = "", Value = "" });
		}
	}
}