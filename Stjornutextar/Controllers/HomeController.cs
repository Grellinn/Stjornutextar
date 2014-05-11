using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stjornutextar.Controllers
{
	public class HomeController : Controller
	{
		// TODO:
		/* 0 Install-Package EntityFramework
		 * 1 Model class
		 * 2 Context class
		 * 3 Initializer class
		 * 4 reference initializer in web.config.
		 * 5 add connection string in web.config.
		 * 6 create scaffolding from model and data context.
		 */
		
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}