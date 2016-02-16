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
    public class BoksController : BaseController
    {
        private BokhandelContext db = new BokhandelContext();


        // GET: Boks
        public ActionResult Index()
        {
            ViewBag.Allesj = db.Sjangers.ToList();
            return View(db.Boks.ToList());
        }

        // GET: Boks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bok bok = db.Boks.Find(id);
            if (bok == null)
            {
                return HttpNotFound();
            }
            return View(bok);
        }

        // GET: Boks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Isbn,Tittel,AntallSider,Format,AntallBokerILager,BildeUrl,BokUrl")] Bok bok)
        {
            if (ModelState.IsValid)
            {
                db.Boks.Add(bok);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bok);
        }

        // GET: Boks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bok bok = db.Boks.Find(id);
            if (bok == null)
            {
                return HttpNotFound();
            }
            return View(bok);
        }

        // POST: Boks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Isbn,Tittel,AntallSider,Format,AntallBokerILager,BildeUrl,BokUrl")] Bok bok)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bok).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bok);
        }

        // GET: Boks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bok bok = db.Boks.Find(id);
            if (bok == null)
            {
                return HttpNotFound();
            }
            return View(bok);
        }

        // POST: Boks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bok bok = db.Boks.Find(id);
            db.Boks.Remove(bok);
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
