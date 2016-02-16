//using MVC_Bokhandel.DAL;
//using MVC_Bokhandel.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace MVC_Bokhandel.Controllers
//{
//    public class SikkerhetController : Controller
//    {

//        public ActionResult Index()
//        {

//            // vis innlogging
//            if (Session["LoggetInn"] == null)
//            {
//                Session["LoggetInn"] = false;
//                ViewBag.Innlogget = false;
//            }
//            else
//            {
//                ViewBag.Innlogget = (bool)Session["LoggetInn"];
//            }
//            return View();
//        }

//        public bool ErInnlogget()
//        {
//            return (bool)Session["LoggetInn"];
//        }
//        [HttpPost]
//        public ActionResult Index(Bruker innLogget)
//        {
//            // sjekk om innlogging OK
//            if (bruker_i_db(innLogget))
//            {
//                // ja brukernavn og passord er OK!
//                Session["LoggetInn"] = true;
//                ViewBag.Innlogget = true;
//                Session["Brukernavn"] = innLogget.Navn;
//                //ErInnlogget(true);
//                //return View();
//                return RedirectToAction("Details", "Kundes", new { id = innLogget.Navn });
//            }
//            else
//            {
//                // brukernavn og passord er ikke OK!
//                Session["LoggetInn"] = false;
//                ViewBag.Innlogget = false;
//                return View();
//            }
//        }
//        public ActionResult Registrer()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Registrer(Bruker innBruker)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View();
//            }
//            using (var db = new BokhandelContext())
//            {
//                try
//                {
//                    var nyBruker = new DbBruker();
//                    byte[] passordDB = lagHash(innBruker.Passord);
//                    nyBruker.BrukerNavn = innBruker.Navn;
//                    nyBruker.Passord = passordDB;
//                    nyBruker.Kunde = new Kunde { BrukerNavn = innBruker.Navn };
//                    db.Brukeres.Add(nyBruker);
//                    db.SaveChanges();
//                    Session["LoggetInn"] = true;
//                    ViewBag.Innlogget = true;
//                    Session["Brukernavn"] = nyBruker.BrukerNavn;
//                    //ErInnlogget(true);
//                    return RedirectToAction("Details", "Kundes", new { id = nyBruker.BrukerNavn });
//                }
//                catch
//                {
//                    return View();
//                }
//            }
//        }
//        public static byte[] lagHash(string innPassord)
//        {
//            byte[] innData, utData;
//            var algoritme = System.Security.Cryptography.SHA256.Create();
//            innData = System.Text.Encoding.ASCII.GetBytes(innPassord);
//            utData = algoritme.ComputeHash(innData);
//            return utData;
//        }

//        private static bool bruker_i_db(Bruker innBruker)
//        {
//            using (var db = new BokhandelContext())
//            {
//                byte[] passordDB = lagHash(innBruker.Passord);
//                DbBruker funnetBruker = db.Brukeres.FirstOrDefault(
//                    b => b.Passord == passordDB && b.BrukerNavn == innBruker.Navn);
//                if (funnetBruker == null)
//                {
//                    return false;
//                }
//                else
//                {
//                    return true;
//                }
//            }
//        }
//        public ActionResult InnloggetSide()
//        {
//            if (Session["LoggetInn"] != null)
//            {
//                bool loggetInn = (bool)Session["LoggetInn"];
//                if (loggetInn)
//                {
//                    return View();
//                }
//            }
//            return RedirectToAction("Index");
//        }
//        public ActionResult LoggUt()
//        {
//            Session["LoggetInn"] = false;
//            return RedirectToAction("index");
//        }
//    }
//}