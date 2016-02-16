using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Bokhandel.Models
{
    public enum Rolle
    {
        Admin, Kunde
    }

    public class Bruker
    {
        [Required(ErrorMessage = "Navn må oppgis")]
        public string Navn { get; set; }
        [Required(ErrorMessage = "Passord må oppgis")]
        public string Passord { get; set; }

        [Required(ErrorMessage = "Passordet må bekreftes")]
        [Display(Name = "Bekreft passordet")]
        [Compare("Passord", ErrorMessage = "Passordene samsvarer ikke.")]
        public string BekreftPassord { get; set; }
    }

    public class DbBruker
    {
        [Key]
        public string BrukerNavn { get; set; }
        public byte[] Passord { get; set; }

        public virtual Kunde Kunde { get; set; }

        public virtual List<Ordre> Ordrer{ get;set; }
    }
}