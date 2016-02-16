using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Bokhandel.Models
{
    public class Kunde
    {
        [Key]
        [ForeignKey("DbBruker")]
        public string BrukerNavn { get; set; }
        public string Etternavn { get; set; }
        public string Fornavn { get; set; }
        public string Adresse { get; set; }
       [ForeignKey("Poststed")]
        public string PostNr { get; set; }
        public int Fodselsdato { get; set; }
        public char Kjonn { get; set; }
        public int TelefonNr { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Eposten er ugyldig.")]
        public string Epost { get; set; }

        public virtual Poststed Poststed { get; set; }
        public virtual DbBruker DbBruker { get; set; }
    }
}