using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Bokhandel.DAL;
using MVC_Bokhandel.Models;

namespace MVC_Bokhandel.Controllers
{
    public class ButikkController : BaseController
    {
        BokhandelContext bokhandelContext = new BokhandelContext();
 
        // GET: Butikk
        public ActionResult Index()
        {
            var sjangere = bokhandelContext.Sjangers.ToList();
            return View(sjangere);
        }

        //GET: /Butikk/Browse?sjanger=Krim
        public ActionResult Browse(string sjanger)
        {
            // Hent sjanger og tilhørende Boks fra database
           // Sjanger sjangerModel = bokhandelContext.Sjangers.FirstOrDefault(s => s.Navn == "Barn");
       
             var sjangerModel = bokhandelContext.Sjangers.Include("Boker")
            .Where(s => s.Navn == sjanger)
            .SingleOrDefault();

            
            return View(sjangerModel);
        }

        // GET: /Butikk/Details/5
        public ActionResult Details(int id)
        {
            var bok = bokhandelContext.Boks.Find(id);

            return RedirectToAction("Details","Boks", new{ id});
        }


        // GET: /Store/GenreMenu

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var sjangere = bokhandelContext.Sjangers.ToList();

            return PartialView(sjangere);
        }
    }
}