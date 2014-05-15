using Stjornutextar.Models;
using System;
using System.Collections.Generic;
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
		public List<SubtitlePart> GetAllSubtitleParts(int id)
		{
			List<SubtitlePart> listOfSubtitleParts = db.SubtitleParts.Where(subPart => subPart.SubtitleID == id).ToList();

			return listOfSubtitleParts;
		}
	}
}