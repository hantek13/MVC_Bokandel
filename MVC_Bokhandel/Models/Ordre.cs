using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System;
using System.Collections.Generic;


namespace MVC_Bokhandel.Models
{
    public class Ordre
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        [ForeignKey("DbBruker")]
        public string BrukerNavn { get; set; }
        [Required(ErrorMessage = "Fornavnet må angis")]
        [StringLength(160)]
        public string Fornavn { get; set; }
        [Required(ErrorMessage = "Etternavnet må angis")]
        [StringLength(160)]
        public string Etternavn { get; set; }
        [Required(ErrorMessage = "Adresse må angis")]
        [StringLength(70)]
        public string Adresse { get; set; }
        [Required(ErrorMessage = "Post nummeret må angis")]
        [DisplayName("Post nummer")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Ugyldig post nummer")]
        public string PostNr { get; set; }
        [Required(ErrorMessage = "Telfon nummer må angis")]
        public string TelefonNr { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Ugyldig epost")]
        [DataType(DataType.EmailAddress)]
        public string Epost { get; set; }
        [ScaffoldColumn(false)]
        public DateTime OrdreDato { get; set; }
        
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        public List<Ordrelinje> Ordrelinjer { get; set; }
        public virtual DbBruker DbBruker { get; set; }
    }
}