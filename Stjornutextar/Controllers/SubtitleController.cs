using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stjornutextar.Models;
using Stjornutextar.Repositories;
using System.IO;
using Stjornutextar.ViewModels;

namespace Stjornutextar.Controllers
{
    public class SubtitleController : Controller
    {
		// Búum til tilvik af repository-inum okkar til að vinna með í gegnum föllin.
		SubtitleRepository repo = new SubtitleRepository();

        // GET: /Subtitle/
        public ActionResult Index(string name)
        {
            string searchString = name;
			SubtitleListViewModel subtitleLVM = new SubtitleListViewModel();
			subtitleLVM.Categories = repo.PopulateCategories();
			subtitleLVM.Languages = repo.PopulateLanguages();

            if (!String.IsNullOrEmpty(searchString))
            {
				subtitleLVM.Subtitles = repo.GetSubtitleByName(searchString);
				return View(subtitleLVM);
            }
            else
            {
                subtitleLVM.Subtitles = repo.GetAllSubtitles();
				return View(subtitleLVM);
            }
           
        }

        // GET: /Subtitle/Create
        public ActionResult Create()
		{
			#region Búa til ViewModel og fylla inn í það flokka, tungumál og þýðingu
			SubtitleViewModel subtitleVM = new SubtitleViewModel();
			subtitleVM.Categories = repo.PopulateCategories();
			subtitleVM.Languages = repo.PopulateLanguages();
			#endregion

			return View(subtitleVM);
        }

        // POST: /Subtitle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
		public ActionResult Create(SubtitleViewModel subtitleVM)
        {
			if (ModelState.IsValid)
			{
				#region Sækir tilvik af Category og Language og setur í ViewModel-ið
				subtitleVM.Subtitle.Category = repo.GetCategory(subtitleVM.Subtitle.Category.ID);
				subtitleVM.Subtitle.Language = repo.GetLanguage(subtitleVM.Subtitle.Language.ID);
				#endregion

				#region Viðbótarupplýsingar fyrir Subtitle sem kerfið setur sjálft
				subtitleVM.Subtitle.PublishDate = DateTime.Now;
				subtitleVM.Subtitle.Status = "Óklárað";
				subtitleVM.Subtitle.Votes = 0;
				if(subtitleVM.UlSubtitleFile.ContentLength != 0)
				{
					//SubPart newSubPart = new SubPart();
					//newSubtitle.SubtitleFileText = new StreamReader(subtitleVM.SubFile.InputStream).Split("\r\n");
//					return Json(newSubtitle, JsonRequestBehavior.AllowGet);
				}
				#endregion

				repo.AddSubtitle(subtitleVM.Subtitle);
                return RedirectToAction("Index");
            }

           return View(subtitleVM);
        }

        // GET: /Subtitle/Edit/5
        public ActionResult Edit(int id)
		{
			#region Búa til ViewModel og fylla inn í það flokka, tungumál og þýðingu
			SubtitleViewModel subtitleVM = new SubtitleViewModel();
			subtitleVM.Categories = repo.PopulateCategories();
			subtitleVM.Languages = repo.PopulateLanguages();
			subtitleVM.Subtitle = repo.GetSubtitleById(id);
			#endregion

			if (subtitleVM.Subtitle == null)
            {
                return HttpNotFound();
            }
            return View(subtitleVM);
        }

        // POST: /Subtitle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
		public ActionResult Edit(SubtitleViewModel subtitleVM)
        {
			if (ModelState.IsValid)
            {
				#region Sækir tilvik af Category og Language og setur í ViewModel-ið
				subtitleVM.Subtitle.Category = repo.GetCategory(subtitleVM.Subtitle.Category.ID);
				subtitleVM.Subtitle.Language = repo.GetLanguage(subtitleVM.Subtitle.Language.ID);
				#endregion

				repo.UpdateSubtitle(subtitleVM.Subtitle);
                return RedirectToAction("Index");
            }
            return View(subtitleVM);
        }
    }
}
