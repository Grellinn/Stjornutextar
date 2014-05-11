using Stjornutextar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
	}
}