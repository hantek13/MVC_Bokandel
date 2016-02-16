using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Bokhandel.Controllers;
using MVC_Bokhandel.DAL;

namespace MVC_Bokhandel.Models
{
    public partial class HandleKurv
    {
        BokhandelContext bokhandelContext = new BokhandelContext();
        private string HandleKurvId { get; set; }
        public const string KurvSessionNøkkel = "CartId";

        public static HandleKurv GetKurv(HttpContextBase context)
        {
            var kurv = new HandleKurv();
            kurv.HandleKurvId = kurv.GetKurvId(context);
            return kurv;
        }

        public static HandleKurv GetKurv(Controller controller)
        {
            return GetKurv(controller.HttpContext);
        }

        public void LeggTilKurv(Bok bok)
        {
            // Få de tilsvarende kurv og bok forekomster
            Kurv kurvEnhet = bokhandelContext.Kurvs.SingleOrDefault((Kurv k) => k.KurvId == HandleKurvId
                                                                    && k.BokId == bok.Id);
            if (kurvEnhet == null)
            {
                kurvEnhet = new Kurv
                {
                    BokId = bok.Id,
                    KurvId = HandleKurvId,
                    Tell = 1,
                    Opprettelsesdato = DateTime.Now
                };
                bokhandelContext.Kurvs.Add(kurvEnhet);
            }
            else
            {
                // Hvis enheten eksisterer i kurven, da legg en enhet til
                kurvEnhet.Tell++;
            }

            bokhandelContext.SaveChanges();
        }

        public int FjernFraKurv(int id)
        {
            //Hent kurven
            var kurvEnhet = bokhandelContext.Kurvs.Single(
                kurv => kurv.KurvId == HandleKurvId
                        && kurv.RekordId == id);

            int enhetTell = 0;

            if (kurvEnhet != null)
            {
                if (kurvEnhet.Tell > 1)
                {
                    kurvEnhet.Tell--;
                    enhetTell = kurvEnhet.Tell;
                }
                else
                {
                    bokhandelContext.Kurvs.Remove(kurvEnhet);
                }

                bokhandelContext.SaveChanges();
            }
            return enhetTell;
        }

        public void TømKurv()
        {
            var kurvEnheter = bokhandelContext.Kurvs.Where(kurv => kurv.KurvId == HandleKurvId);
            foreach (var kurvEnhet in kurvEnheter)
            {
                bokhandelContext.Kurvs.Remove(kurvEnhet);
            }
            bokhandelContext.SaveChanges();
        }

        public List<Kurv> GetKurvEnheter()
        {
            return bokhandelContext.Kurvs.Where(kurv => kurv.KurvId == HandleKurvId).ToList();
        }

        public int GetTell()
        {
            int? tell = (from kurvEnheter in bokhandelContext.Kurvs
                where kurvEnheter.KurvId == HandleKurvId
                select (int?) kurvEnheter.Tell).Sum();

            //Return 0 hvis alle entriene er null
            return tell ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from kurvEnheter in bokhandelContext.Kurvs
                where kurvEnheter.KurvId == HandleKurvId
                select (int?) kurvEnheter.Tell*kurvEnheter.Bok.Pris).Sum();
            return total ?? decimal.Zero;
        }

        public int OpprettOrdre(Ordre ordre)
        {
            decimal ordreTotal = 0;

            var kurvEnheter = GetKurvEnheter();

            //Gå gjennom enhetene i kurven mens du legger ordre detaljene for hver enhet
            foreach (var enhet in kurvEnheter)
            {
                var ordreLinje = new Ordrelinje
                {
                    BokId = enhet.BokId,
                    OrdreId = ordre.Id,
                    PrisPrEnhet = enhet.Bok.Pris,
                    Antall = enhet.Tell
                };

                // Angi ordrets total sum av handlekurven
                ordreTotal += (enhet.Tell*enhet.Bok.Pris);
                bokhandelContext.Ordrelinjes.Add(ordreLinje);
            }

            // Angi ordrets total sum av ordreTotal tell
            ordre.Total = ordreTotal;
            
            bokhandelContext.SaveChanges();
            TømKurv();
            return ordre.Id;
        }

        public string GetKurvId(HttpContextBase context)
        {
            if (context.Session[KurvSessionNøkkel] == null)
            {
                if (!string.IsNullOrWhiteSpace((string)context.Session["BrukereNavnet"]))
                {
                    context.Session[KurvSessionNøkkel] = (string)context.Session["BrukereNavnet"];
                }
                else
                {
                    //Generer en ny tilfeldig GUID ved hjelp av System.Guid klassen
                    Guid tempKurvId = Guid.NewGuid();

                    context.Session[KurvSessionNøkkel] = tempKurvId.ToString();
                }
            }
            return context.Session[KurvSessionNøkkel].ToString();
        }


        public void MigrerKurv(string brukerNavn)
        {
            var handleKurv = bokhandelContext.Kurvs.Where(k => k.KurvId == HandleKurvId);
            foreach (Kurv enhet in handleKurv)
            {
                enhet.KurvId = brukerNavn;
            }
            bokhandelContext.SaveChanges();
        }
    }
}