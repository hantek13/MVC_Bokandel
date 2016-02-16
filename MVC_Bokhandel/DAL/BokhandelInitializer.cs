using System;
using System.Collections.Generic;
using MVC_Bokhandel.Controllers;
using MVC_Bokhandel.Models;

namespace MVC_Bokhandel.DAL
{
    public class BokhandelInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BokhandelContext>
    {
        protected override void Seed(BokhandelContext context) 
        {

            var sjangere = new List<Sjanger>
            {
                new Sjanger{Navn = "Politikk og samfunn"},
                new Sjanger{Navn = "Barn"}
            };
            sjangere.ForEach(s => context.Sjangers.Add(s));
            context.SaveChanges();


            var boker = new List<Bok>
            {

                new Bok{AntallBokerILager = 4, Tittel = "Min skyld", Isbn = 9788282056762, Format=Format.Innbundet, AntallSider = 200, Pris = 500, SjangerId = 1, BildeUrl = "/Content/Bilder/min_skyld.jpe"},
                new Bok{AntallBokerILager = 10, Tittel = "Ole Brum", Isbn = 9788282056770, Format=Format.Pocket, AntallSider = 30, Pris = (decimal)100.50, SjangerId = 2, BildeUrl = "/Content/Bilder/OleBrumm.jpg"}
            };
            boker.ForEach(b => context.Boks.Add(b));
            context.SaveChanges();


            var poststeder = new List<Poststed>
            {
                new Poststed{PostNr = "0182", Poststedet = "Oslo"}
            };
            poststeder.ForEach(p => context.Poststeds.Add(p));
            context.SaveChanges();

            var forfattere = new List<Forfatter>
            {
                new Forfatter{Fornavn = "Jens", Etternavn = "Brevik", Epost = "jens.brevik@noe.no", Url = "www.noe.no"},
                new Forfatter{Fornavn = "Ole", Etternavn = "Martin", Epost = "ole.brevik@noe.no", Url = "www.olenoe.no"},
                new Forfatter{Fornavn = "Ela", Etternavn = "Johannsonn", Epost = "Ela.brevik@noe.no", Url = "www.elanoe.no"}

            };
            forfattere.ForEach(f => context.Forfatters.Add(f));
            context.SaveChanges();

            var bokForfattere = new List<BokForfatter>
            {
                new BokForfatter{BokId = 1, ForfatterId = 1},
                new BokForfatter{BokId = 1, ForfatterId = 2 },
                new BokForfatter{BokId = 2, ForfatterId = 3}
            };
            bokForfattere.ForEach(bf => context.BokForfatters.Add(bf));
            context.SaveChanges();


            var brukere = new List<DbBruker>
            {
                new DbBruker{BrukerNavn = "haimif", Passord = SikkerhetController.lagHash("123456")},
                new DbBruker{BrukerNavn = "hanif", Passord = SikkerhetController.lagHash("123456")}

            };
            brukere.ForEach((b => context.Brukeres.Add(b)));
            context.SaveChanges();

            var kunder = new List<Kunde>
            {
                new Kunde{BrukerNavn = "haimif", Fornavn = "Haimanot", Etternavn = "Tekie", Epost = "haimif@yahoo.com", Fodselsdato = 270189, Adresse = "Storgata 79, hus 9 Leil. 105", PostNr = "0182", TelefonNr = 46377592, Kjonn = 'K'},
                new Kunde{BrukerNavn = "hanif", Fornavn = "Hanna", Etternavn = "Tekie", Epost = "hanna@yahoo.com", Fodselsdato = 130890, Adresse = "Storgata 61, hus 4 Leil. 323", PostNr = "0182", TelefonNr = 46377592, Kjonn = 'K'}

            };
            kunder.ForEach(k => context.Kundes.Add(k));
            context.SaveChanges();

            var leveringsadresser = new List<Leveringsadresse>
            {
                new Leveringsadresse{Adresse = "Storgata 65, hus 6 Leili. 206", PostId = 1}
            };
            leveringsadresser.ForEach(l => context.Leveringsadresses.Add(l));
            context.SaveChanges();

            //var ordrer = new List<Ordre>
            //{
            //    new Ordre{KundeId = 1, LeveringsadresseId = 1, OrdreDato = DateTime.Parse("2014-09-26"), SendtDato = DateTime.Parse("2014-08-27"), BetaltDato = DateTime.Parse("2014-10-26")}
            //};
            //ordrer.ForEach(o => context.Ordres.Add(o));
            //context.SaveChanges();

            //var ordrelinjer = new List<Ordrelinje>
            //{
            //    new Ordrelinje{OrdreId = 1, BokId = 1, Antall = 2, PrisPrEnhet = 105.50},
            //    new Ordrelinje{OrdreId = 1, BokId = 2, Antall = 3, PrisPrEnhet = 60.50}
            //};
            //ordrelinjer.ForEach(ol => context.Ordrelinjes.Add(ol));
            //context.SaveChanges();
            

        }
    }
}