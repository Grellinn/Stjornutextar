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
using System.Text;

namespace Stjornutextar.Controllers
{
    [Authorize]
	public class SubtitleController : Controller
    {
		// Búum til tilvik af repository-inum okkar til að vinna með í gegnum föllin.
		SubtitleRepository subRepo = new SubtitleRepository();
		SubtitlePartRepository subPartRepo = new SubtitlePartRepository();

        // GET: /Subtitle/
		[AllowAnonymous]
        public ActionResult Index(string name, int? id)
        {
            SubtitleListViewModel subtitleLVM = new SubtitleListViewModel();
            subtitleLVM.Categories = subRepo.PopulateCategories();
            subtitleLVM.Languages = subRepo.PopulateLanguages();


            if (!String.IsNullOrEmpty(name))
            {
                subtitleLVM.Subtitles = subRepo.GetSubtitleByName(name);
                return View(subtitleLVM);
            }
            else if (id != null)
            {
                subtitleLVM.Subtitles = subRepo.GetSubtitlesByCategory(id);
                return View(subtitleLVM);
            }
            else 
            {
                subtitleLVM.Subtitles = subRepo.GetFirst10Subtitles();
                return View(subtitleLVM);
            }
           
        }

        // GET: /Subtitle/Create
        public ActionResult Create()
		{
			#region Búa til ViewModel og fylla inn í það flokka, tungumál og þýðingu
			SubtitleViewModel subtitleVM = new SubtitleViewModel();
			subtitleVM.Categories = subRepo.PopulateCategories();
			subtitleVM.Languages = subRepo.PopulateLanguages();
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
				subtitleVM.Subtitle.Category = subRepo.GetCategory(subtitleVM.Subtitle.Category.ID);
				subtitleVM.Subtitle.Language = subRepo.GetLanguage(subtitleVM.Subtitle.Language.ID);
				#endregion

				#region Viðbótarupplýsingar fyrir Subtitle sem kerfið setur sjálft
				subtitleVM.Subtitle.PublishDate = DateTime.Now;
				subtitleVM.Subtitle.Status = "Óklárað";
				subtitleVM.Subtitle.Votes = 0;
				
				if(subtitleVM.UlSubtitleFile.ContentLength != 0)
				{
					// Innihald skráar sett inn í SubtitleFileText breytu í Subtitle
					subtitleVM.Subtitle.SubtitleFileText = new StreamReader(subtitleVM.UlSubtitleFile.InputStream, Encoding.UTF8, false).ReadToEnd();
					// Búinn til listi af strengjum og SubtitleFileText splittað upp í strengja brot
					List<string> TempSubtitleParts = subtitleVM.Subtitle.SubtitleFileText.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None).ToList();
					// Listi sem inniheldur hvert þýðingarbrot splittað upp fyrir ID, tíma og texta
					List<List<string>> SubtitlePartsDivided = new List<List<string>>();

					foreach (var subtitlePart in TempSubtitleParts)
					{
						List<string> temp = new List<string>();

						foreach (var str in subtitlePart.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList())
						{
							temp.Add(str);
						}
						SubtitlePartsDivided.Add(temp);
					}
					SubtitlePartsDivided.RemoveAt(SubtitlePartsDivided.Count()-1);

					subtitleVM.Subtitle.SubtitleParts = new List<SubtitlePart>();
					// Færum innihald skjátextaskráar sem var hlaðið inn úr listum í model og vistum í gagnagrunni
					foreach (var subtitlePartDivided in SubtitlePartsDivided)
					{
						SubtitlePart subtitlePart = new SubtitlePart();

						for (int i = 0; i < subtitlePartDivided.Count(); i++)
						{
							if (i == 0)
							{
								if (subtitlePartDivided[i] != "")
									subtitlePart.partNumber = Convert.ToInt32(subtitlePartDivided[i]);
								else
									break;
							}
							else if (i == 1)
								subtitlePart.time = subtitlePartDivided[i];
							else if (i == 2)
								subtitlePart.text1 = subtitlePartDivided[i];
							else
								subtitlePart.text2 = subtitlePartDivided[i];
						}
						subtitlePart.SubtitleID = subtitleVM.Subtitle.ID;
						subtitleVM.Subtitle.SubtitleParts.Add(subtitlePart);
					}
				}
				#endregion

				subRepo.AddSubtitle(subtitleVM.Subtitle);
                return RedirectToAction("Index");
            }

           return View(subtitleVM);
        }

        // GET: /Subtitle/Edit/5
        public ActionResult Edit(int id)
		{
			#region Búa til ViewModel og fylla inn í það flokka, tungumál og þýðingu
			SubtitleViewModel subtitleVM = new SubtitleViewModel();
			subtitleVM.Categories = subRepo.PopulateCategories();
			subtitleVM.Languages = subRepo.PopulateLanguages();
			subtitleVM.Subtitle = subRepo.GetSubtitleById(id);
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
				subtitleVM.Subtitle.Category = subRepo.GetCategory(subtitleVM.Subtitle.Category.ID);
				subtitleVM.Subtitle.Language = subRepo.GetLanguage(subtitleVM.Subtitle.Language.ID);
				#endregion

				subRepo.UpdateSubtitle(subtitleVM.Subtitle);
                return RedirectToAction("Index");
            }
            return View(subtitleVM);
        }

		// GET: /Subtitle/Download/5
		[AllowAnonymous]
		public FileStreamResult CreateFile(int id)
		{
			Subtitle subtitle = subRepo.GetSubtitleById(id);
			List<SubtitlePart> subtitleParts = subPartRepo.GetAllSubtitlePartsById(id);
			
			string data = "";
			string tempData;

			for (int i = 0; i < subtitleParts.Count; i++) // Rúllar í gegnum alla SubtitleParta í listanum og skrifar bætir við í einn streng
			{
				if (subtitleParts[i].translatedText2 != null )
				{
					tempData = (subtitleParts[i].partNumber + "\r\n" + subtitleParts[i].time + "\r\n" + subtitleParts[i].translatedText1 + "\r\n" + subtitleParts[i].translatedText2 + "\r\n" + "\r\n");
					data = data + tempData;
				}
				else
				{
					tempData = (subtitleParts[i].partNumber + "\r\n" + subtitleParts[i].time + "\r\n" + subtitleParts[i].translatedText1 + "\r\n" + "\r\n");
					data = data + tempData;
				}
			}
			//todo: add some data from your database into that string:
			var string_with_your_data = data;

			var byteArray = Encoding.UTF8.GetBytes(string_with_your_data);
			var stream = new MemoryStream(byteArray);

			return File(stream, "text/plain", subtitle.Title + ".srt");
		}
    }
}
