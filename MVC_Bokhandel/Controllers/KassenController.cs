using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Bokhandel.DAL;
using MVC_Bokhandel.Models;

namespace MVC_Bokhandel.Controllers
{
    public class KassenController : BaseController
    {
        BokhandelContext bokhandelContext = new BokhandelContext();
        private const string PromoKode = "FREE"; // for å forenkle betaling
        // GET: Kassen
        public ActionResult AdresseOgBetaling()
        {
            if (!ErLoggetInn()) return RedirectToAction("Index", "Sikkerhet");
            return View();
        }

        [HttpPost]

        public ActionResult AdresseOgBetaling(FormCollection verdier)
        {
            var ordre = new Ordre();
            TryUpdateModel(ordre);

            try
            {
                if (string.Equals(verdier["PromoKode"], PromoKode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(ordre);
                }
                else
                {
                    ordre.BrukerNavn = BrukereNavnet();
                    ordre.OrdreDato = DateTime.Now;

                    //Lagre Ordre
                    bokhandelContext.Ordres.Add(ordre);
                    bokhandelContext.SaveChanges();

                    //Behnadl ordret
                    var kurv = HandleKurv.GetKurv(this.HttpContext);
                    kurv.OpprettOrdre(ordre);

                    return RedirectToAction("Fulfort", new {id = ordre.Id});
                }
            }
            catch (Exception)
            {
                //Ugyldig
                return View(ordre);
            }
        }

        public ActionResult Fulfort(int id)
        {
            if (!ErLoggetInn()) return RedirectToAction("Index", "Sikkerhet");

            //bool erGyldig = bokhandelContext.Ordres.Any(
            //    o => o.Id == id &&
            //         o.BrukerNavn == BrukereNavnet());
                        Ordre o = bokhandelContext.Ordres.Find(id);
            //if (kunde == null)

            bool erGyldig = o != null;
            if (erGyldig)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}