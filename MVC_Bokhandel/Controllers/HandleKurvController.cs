using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Bokhandel.DAL;
using MVC_Bokhandel.Models;
using MVC_Bokhandel.ViewModels;

namespace MVC_Bokhandel.Controllers
{
    public class HandleKurvController : BaseController
    {
        BokhandelContext bokhandelContext = new BokhandelContext();

        // GET: HandleKurv
        public ActionResult Index()
        {
            var kurv = HandleKurv.GetKurv(this.HttpContext);

            //setter opp vår ViewModel
            var viewModel = new HandleKurvViewModel
            {
                KurvEnheter = kurv.GetKurvEnheter(),
                KurvTotal = kurv.GetTotal()
            };
            return View(viewModel);
        }

        public ActionResult LeggTilKurv(int id)
        {
            //hent boka fra databasen
            var tilLagtBok = bokhandelContext.Boks
                .Single(bok => bok.Id == id);

            //legg boka til handle kurven
            var kurv = HandleKurv.GetKurv(this.HttpContext);
            kurv.LeggTilKurv(tilLagtBok);

            //Gå tilbake til hoved siden i bokhandlen
            //look
            return RedirectToAction("Index", "Boks");
        }

        //AJAX: /HandleKurv/FjernFraKurv/5
        [HttpPost]
        public ActionResult FjernFraKurv(int id)
        {
            //Fjern enhet fra kurven
            var kurv = HandleKurv.GetKurv(this.HttpContext);

            //hent navnet til boka for vise frem bekreftelse
            string bokTittel = bokhandelContext.Kurvs
                .Single(enhet => enhet.RekordId == id).Bok.Tittel;

            //Fjern fra kurven
            int enhetTell = kurv.FjernFraKurv(id);

            //Vis frem bekreftelses melding
            var resultater = new HandleKurvFjernViewModel
            {
                Melding = Server.HtmlDecode(bokTittel) + 
                " er blitt fjernet fra din handlekurv.",
                KurvTotal = kurv.GetTotal(),
                KurvTell = kurv.GetTell(),
                EnhetTell = enhetTell,
                SletId = id
            };
            return Json(resultater);
        }

        //Get: /HandleKurv/KurvSammendrag
        [ChildActionOnly]
        public ActionResult KurvSammendrag()
        {
            var kurv = HandleKurv.GetKurv(this.HttpContext);

            ViewData["KurvTell"] = kurv.GetTell();
            return PartialView("KurvSammendrag");
        }
    }
}