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

namespace Stjornutextar.Controllers
{
    public class SubtitlePartController : Controller
    {
		// Búum til tilvik af repository-inum okkar til að vinna með í gegnum föllin.
		SubtitlePartRepository repo = new SubtitlePartRepository();

        // GET: /SubtitlePart/
        public ActionResult Index(int id)
        {
            return View(repo.GetAllSubtitleParts(id));
        }

		// GET: /SubtitlePart/Edit/5
		public ActionResult Edit(int id)
		{
			SubtitlePart subtitlePart = repo.GetSubtitlePartById(id);
			if (subtitlePart == null)
			{
				return HttpNotFound();
			}
			return View(subtitlePart);
		}

		// POST: /SubtitlePart/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(SubtitlePart subtitlePart)
		{
			if (ModelState.IsValid)
			{
				repo.UpdateSubtitlePart(subtitlePart);
				return RedirectToAction("Index/" + subtitlePart.SubtitleID);
			}
			return View(subtitlePart);
		}
    }
}
