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
        public ActionResult Index()
        {
            return View(repo.GetFirst10Subtitles());
        }

        // GET: /Subtitle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var getSubtitleById = repo.GetSubtitleById(id);
            if (getSubtitleById == null)
            {
                return HttpNotFound();
            }
            return View(getSubtitleById);
        }

        // GET: /Subtitle/Create
        public ActionResult Create()
        {
			ViewBag.Categories = repo.FeedCategoryList();
			ViewBag.Languages = repo.FeedLanguageList();
			ViewBag.Titles = repo.FeedTitleList();
			
			return View();
        }

        // POST: /Subtitle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Title,Category,Language,MediaURL")] SaveSubtitleViewModel subtitleVM)
        {
			subtitleVM.Categories = repo.FeedCategoryList();
			subtitleVM.Languages = repo.FeedLanguageList();
			subtitleVM.Titles = repo.FeedTitleList();

			if (ModelState.IsValid)
            {
				repo.CreateSubtitle(subtitleVM);
                return RedirectToAction("Index");
            }

           return View(subtitleVM);
        }
		
        // GET: /Subtitle/Edit/5
        public ActionResult Edit(int? id)
        {
			ViewBag.Categories = repo.FeedCategoryList();
			ViewBag.Languages = repo.FeedLanguageList();
			ViewBag.Titles = repo.FeedTitleList();
			
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var subtitleByID = repo.GetSubtitleById(id);
            if (subtitleByID == null)
            {
                return HttpNotFound();
            }
            return View(subtitleByID);
        }

        // POST: /Subtitle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Title,Category,Language,MediaURL")] Subtitle subtitle)
        {
			ViewBag.Categories = repo.FeedCategoryList();
			ViewBag.Languages = repo.FeedLanguageList();
			ViewBag.Titles = repo.FeedTitleList();
			
			if (ModelState.IsValid)
            {
				repo.UpdateSubtitle(subtitle);
                return RedirectToAction("Index");
            }
            return View(subtitle);
        }
		
        // GET: /Subtitle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var subtitleByID = repo.GetSubtitleById(id);
            if (subtitleByID == null)
            {
                return HttpNotFound();
            }
            return View(subtitleByID);
        }

        // POST: /Subtitle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			var subtitleByID = repo.GetSubtitleById(id);
			repo.RemoveSubtitle(subtitleByID);
			repo.SaveSubtitle();
            return RedirectToAction("Index");
        }

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing)
		//	{
		//		db.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}
    }
}
