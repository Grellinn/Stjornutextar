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
			SubtitleViewModel subtitleVM = new SubtitleViewModel();

			List<Category> repoCategories = repo.PopulateCategories();
			List<Category> Categories = new List<Category>();
			foreach (var category in repoCategories)
			{
				if (Categories.Where(c => c.CategoryName == category.CategoryName).FirstOrDefault() == null)
					Categories.Add(category);
			}
			subtitleVM.Categories =  Categories;

			List<Language> repoLanguages = repo.PopulateLanguages();
			List<Language> Languages = new List<Language>();
			foreach (var language in repoLanguages)
			{
				if (Languages.Where(l => l.LanguageName == language.LanguageName).FirstOrDefault() == null)
					Languages.Add(language);
			}
			subtitleVM.Languages = Languages;

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
				#region Sækir nafn á Category og Language í gagnagrunn og setur nafn í tilvikin í Subtitle
				subtitleVM.Subtitle.Category.CategoryName = repo.GetCategoryName(subtitleVM.Subtitle.Category.ID);
				subtitleVM.Subtitle.Language.LanguageName = repo.GetLanguageName(subtitleVM.Subtitle.Language.ID);
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
			SubtitleViewModel subtitleVM = new SubtitleViewModel();
			subtitleVM.Categories = repo.PopulateCategories();

//5			List<Category> CategoriesFromDB = repo.PopulateCategories();
			
			//foreach (var category in CategoriesFromDB)
			//{
			//	if (subtitleVM.Categories.Where(a => a.CategoryName == category.CategoryName).FirstOrDefault() == null)
			//		subtitleVM.Categories.Add(category);
			//	else if (subtitleVM.Categories.Where(c => c.ID == category.ID) == null)
			//		subtitleVM.Categories.Add(category);
			//}

			subtitleVM.Languages = repo.PopulateLanguages();
			subtitleVM.Subtitle = repo.GetSubtitleById(id);

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
				#region Sækir nafn á Category og Language í gagnagrunn og setur nafn í tilvikin í Subtitle
				subtitleVM.Subtitle.Category.CategoryName = repo.GetCategoryName(subtitleVM.Subtitle.Category.ID);
				subtitleVM.Subtitle.Language.LanguageName = repo.GetLanguageName(subtitleVM.Subtitle.Language.ID);
				#endregion

				repo.UpdateSubtitle(subtitleVM.Subtitle);
                return RedirectToAction("Index");
            }
            return View(subtitleVM);
        }
		//[Bind(Include = "Title,Category,Language,MediaURL")]
    }
}
