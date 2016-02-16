using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Bokhandel.DAL;
using MVC_Bokhandel.Models;

namespace MVC_Bokhandel.Controllers
{
    public class KundesController : BaseController
    {
        private BokhandelContext db = new BokhandelContext();

        // GET: Kundes
        public ActionResult Index()
        {
            var kundes = db.Kundes.Include(k => k.DbBruker).Include(k => k.Poststed);
            return View(kundes.ToList());
        }

        // GET: Kundes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kunde kunde = db.Kundes.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }

            if (ErLoggetInn())
            {
                if(BrukereNavnet().Equals(id))
                return View(kunde);

                return RedirectToAction("IngenAdgang", "Sikkerhet");
            }

            return RedirectToAction("Index", "Sikkerhet");

        }



        // GET: Kundes/Create
        public ActionResult Create()
        {
//            ViewBag.BrukerNavn = new SelectList(db.Brukeres, "BrukerNavn", "BrukerNavn");
//            ViewBag.PostNr = new SelectList(db.Poststeds, "PostNr", "Poststedet");
            return View();
        }

        // POST: Kundes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrukerNavn,Etternavn,Fornavn,Adresse,PostNr,Fodselsdato,TelefonNr,Epost")] Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                db.Kundes.Add(kunde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

//            ViewBag.BrukerNavn = new SelectList(db.Brukeres, "BrukerNavn", "BrukerNavn", kunde.BrukerNavn);
//            ViewBag.PostNr = new SelectList(db.Poststeds, "PostNr", "Poststedet", kunde.PostNr);
            return View(kunde);
        }

        // GET: Kundes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kunde kunde = db.Kundes.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrukerNavnet = id;
//            ViewBag.BrukerNavn = new SelectList(db.Brukeres, "BrukerNavn", "BrukerNavn", kunde.BrukerNavn);
//            ViewBag.PostNr = new SelectList(db.Poststeds, "PostNr", "Poststedet", kunde.PostNr);
            if (!ErLoggetInn()) return RedirectToAction("Index", "Sikkerhet");
            if (!BrukereNavnet().Equals(id))
                return RedirectToAction("IngenAdgang", "Sikkerhet");
            
            return View(kunde);

            
        }

        // POST: Kundes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrukerNavn,Etternavn,Fornavn,Adresse,PostNr,Fodselsdato,TelefonNr,Epost")] Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kunde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Kundes", new { id = kunde.BrukerNavn });
//                return RedirectToAction("Index");
            }
//            ViewBag.BrukerNavn = new SelectList(db.Brukeres, "BrukerNavn", "BrukerNavn", kunde.BrukerNavn);
//            ViewBag.PostNr = new SelectList(db.Poststeds, "PostNr", "Poststedet", kunde.PostNr);
            return View(kunde);
        }

        // GET: Kundes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kunde kunde = db.Kundes.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // POST: Kundes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Kunde kunde = db.Kundes.Find(id);
            db.Kundes.Remove(kunde);
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

        public ActionResult MinSide()
        {
            if (!ErLoggetInn())
            {
                return RedirectToAction("IngenAdgang", "Sikkerhet");
            }
            ViewBag.Brukernavnet = BrukereNavnet();
            return View();
        }

        public ActionResult MinOrdreHistorikk(string id)
        {
            var ordreModel = db.Ordres.Include("DbBruker")
                .Where(o => o.BrukerNavn == id)
            .ToList();

            if (ordreModel.Count == 0)
            {
                return RedirectToAction("IngenOrdre");
            }
            ViewBag.Brukernavnet = BrukereNavnet();
            ViewBag.MineOrdrer = ordreModel;
            return View();
        }

        public ActionResult IngenOrdre()
        {
            return View();
        }

        public ActionResult OrdreDetalje(int id)
        {
            var ordreDetaljeModel = db.Ordrelinjes.Include("Ordre")
                .Where(ol => ol.OrdreId == id)
            .ToList();

            if (ordreDetaljeModel.Count == 0)
            {
                return RedirectToAction("IngenOrdre");
            }
            ViewBag.Brukernavnet = BrukereNavnet();
            ViewBag.Ordredetaljer = ordreDetaljeModel; return View();
        }

    }
}
