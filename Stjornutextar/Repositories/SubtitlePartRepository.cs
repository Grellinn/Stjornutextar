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


	}
}