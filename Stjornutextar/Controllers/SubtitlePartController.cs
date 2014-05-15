using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stjornutextar.Models;

namespace Stjornutextar.Controllers
{
    public class SubtitlePartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /SubtitlePart/
        public ActionResult Index()
        {
            return View(db.SubtitleParts.ToList());
        }

        // GET: /SubtitlePart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubtitlePart subtitlepart = db.SubtitleParts.Find(id);
            if (subtitlepart == null)
            {
                return HttpNotFound();
            }
            return View(subtitlepart);
        }

        // GET: /SubtitlePart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SubtitlePart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,partNumber,time,text1,text2,translatedText1,translatedText2")] SubtitlePart subtitlepart)
        {
            if (ModelState.IsValid)
            {
                db.SubtitleParts.Add(subtitlepart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subtitlepart);
        }

        // GET: /SubtitlePart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubtitlePart subtitlepart = db.SubtitleParts.Find(id);
            if (subtitlepart == null)
            {
                return HttpNotFound();
            }
            return View(subtitlepart);
        }

        // POST: /SubtitlePart/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,partNumber,time,text1,text2,translatedText1,translatedText2")] SubtitlePart subtitlepart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subtitlepart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subtitlepart);
        }

        // GET: /SubtitlePart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubtitlePart subtitlepart = db.SubtitleParts.Find(id);
            if (subtitlepart == null)
            {
                return HttpNotFound();
            }
            return View(subtitlepart);
        }

        // POST: /SubtitlePart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubtitlePart subtitlepart = db.SubtitleParts.Find(id);
            db.SubtitleParts.Remove(subtitlepart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
