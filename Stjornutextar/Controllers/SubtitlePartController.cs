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
using Stjornutextar.ViewModels;

namespace Stjornutextar.Controllers
{
    [Authorize]
	public class SubtitlePartController : Controller
    {
		// Búum til tilvik af repository-inum okkar til að vinna með í gegnum föllin.
		SubtitlePartRepository subPartRepo = new SubtitlePartRepository();
		SubtitleRepository subRepo = new SubtitleRepository();

        // GET: /SubtitlePart/
        public ActionResult Index(int id)
        {
			SubtitlePartListViewModel subPartLVM = new SubtitlePartListViewModel();
			subPartLVM.Subtitle = subRepo.GetSubtitleById(id);
			subPartLVM.SubtitleParts = subPartRepo.GetAllSubtitlePartsById(id);
			
			return View(subPartLVM);
        }

		// GET: /SubtitlePart/Edit/5
		public ActionResult Edit(int id)
		{
			SubtitlePart subtitlePart = subPartRepo.GetSubtitlePartById(id);
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
				subPartRepo.UpdateSubtitlePart(subtitlePart);
				Subtitle subtitle = subRepo.GetSubtitleById(subtitlePart.SubtitleID);
				subtitle.SubtitleParts = subPartRepo.GetAllSubtitlePartsById(subtitlePart.SubtitleID);
				subtitle.countTranslations++;

				if (subtitle.countTranslations == subtitle.SubtitleParts.Count)
					subtitle.Status = "Klárað";
				subRepo.UpdateSubtitle(subtitle);

				return RedirectToAction("Index/" + subtitlePart.SubtitleID);
			}
			return View(subtitlePart);
		}
    }
}
