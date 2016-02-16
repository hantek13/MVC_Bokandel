using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace MVC_Bokhandel.Models
{
    public enum Format
    {
        Innbundet, Pocket
    }

    public class Bok
    {
        public int Id { get; set; }
        public long Isbn { get; set; }
        public string Tittel { get; set; }
        [ForeignKey("Sjanger")]
        public int SjangerId { get; set; }
        public int AntallSider { get; set; }

        public Format? Format { get; set; }
        public int AntallBokerILager { get; set; }
        public string BildeUrl { get; set; }
        public string BokUrl { get; set; }
        public decimal Pris { get; set; }

        public virtual List<BokForfatter> BokForfattere { get; set; }
        public virtual Sjanger Sjanger { get; set; }
        public virtual List<Ordrelinje> Ordrelinjer { get; set; }  
        
    }
}