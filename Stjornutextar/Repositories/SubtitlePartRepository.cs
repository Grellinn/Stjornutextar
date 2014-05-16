using Stjornutextar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Stjornutextar.Repositories
{
	public class SubtitlePartRepository
	{
		private static SubtitlePartRepository _instance;

		public static SubtitlePartRepository Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SubtitlePartRepository();
				return _instance;
			}
		}

		// Búum til tilvik af gagnagrunninum okkar til að vinna með.
		ApplicationDbContext db = new ApplicationDbContext();

		// Fall sem skilar öllum skjátextapörtum úr gagnagrunni
		public List<SubtitlePart> GetAllSubtitlePartsById(int id)
		{
			List<SubtitlePart> listOfSubtitleParts = db.SubtitleParts.Where(subPart => subPart.SubtitleID == id).ToList();

			return listOfSubtitleParts;
		}

		// Fall sem skilar SubtitlePart úr gagnagrunni til að breyta út frá id.
		public SubtitlePart GetSubtitlePartById(int id)
		{
			SubtitlePart subtitlePartById = db.SubtitleParts.Where(subPart => subPart.ID == id).SingleOrDefault();

			return subtitlePartById;
		}

		// Fall sem tekur við Subtitle, uppfærir tilvikið og vistar í gagnagrunni.
		public void UpdateSubtitlePart(SubtitlePart subtitlePart)
		{
			SubtitlePart tempSubPart = GetSubtitlePartById(subtitlePart.ID);

			if (tempSubPart != null)
			{
				// Uppfærum translatedText1 og translatedText2
				tempSubPart.translatedText1 = subtitlePart.translatedText1;
				tempSubPart.translatedText2 = subtitlePart.translatedText2;
				subtitlePart.SubtitleID = tempSubPart.SubtitleID;
				db.SaveChanges();
			}
		}
	}
}