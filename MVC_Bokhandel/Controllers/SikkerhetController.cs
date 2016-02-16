using MVC_Bokhandel.DAL;
using MVC_Bokhandel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Bokhandel.Controllers
{
    public class SikkerhetController : BaseController
    {

        public ActionResult Index()
        {
            //look
            // vis innlogging
            if (Session["LoggetInn"] == null)
            {
                SestSessionene(false, "");
            }
            else
            {
                MigrerHandleKurv(BrukereNavnet());
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Bruker innLogget)
        {
            // sjekk om innlogging OK
            if (bruker_i_db(innLogget))
            {
                // ja brukernavn og passord er OK!
                var n = innLogget.Navn;
                SestSessionene(true, n);
                return RedirectToAction("MinSide", "Kundes");
            }

            SestSessionene(false, "");
            return View();
        }
        public ActionResult Registrer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrer(Bruker innBruker)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var db = new BokhandelContext())
            {
                try
                {
                    var nyBruker = new DbBruker();
                    byte[] passordDB = lagHash(innBruker.Passord);
                    nyBruker.BrukerNavn = innBruker.Navn;
                    nyBruker.Passord = passordDB;
                    nyBruker.Kunde = new Kunde {BrukerNavn = innBruker.Navn};
                    db.Brukeres.Add(nyBruker);
                    db.SaveChanges();

                    var n = nyBruker.BrukerNavn;
                    SestSessionene(true, n);
                    MigrerHandleKurv(n);
                    //ErInnlogget(true);
                    return RedirectToAction("Details", "Kundes", new { id = n });
                }
                catch
                {
                    return View();
                }
            }
        }
        public static byte[] lagHash(string innPassord)
        {
            var algoritme = System.Security.Cryptography.SHA256.Create();
            var innData = System.Text.Encoding.ASCII.GetBytes(innPassord);
            var utData = algoritme.ComputeHash(innData);
            return utData;
        }

        private static bool bruker_i_db(Bruker innBruker)
        {
            using (var db = new BokhandelContext())
            {
                var passordDb = lagHash(innBruker.Passord);
                DbBruker funnetBruker = db.Brukeres.FirstOrDefault(
                    b => b.Passord == passordDb && b.BrukerNavn == innBruker.Navn);
                if (funnetBruker == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public ActionResult InnloggetSide()
        {
            if (ErLoggetInn())
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult LoggUt()
        {
            SestSessionene(false, "");
            return RedirectToAction("index");
        }

        public ActionResult IngenAdgang()
        {
            return View();
        }

        private void MigrerHandleKurv(string BrkerNavn)
        {
            //Assosiere handlekurv enheter med innlogget bruker
            var kurv = HandleKurv.GetKurv(this.HttpContext);

            kurv.MigrerKurv(BrukereNavnet());
            Session[HandleKurv.KurvSessionNøkkel] = BrukereNavnet();
        }
    }
}