using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Bokhandel.Models
{
    public class Kurv
    {
        [Key]
        public int RekordId { get; set; }
        //Man kan legge bøker til kurven annonymt
        // men er nødt til å logge på for å kunne kjøpe
        public string KurvId { get; set; }
        public int BokId { get; set; }
        public int Tell { get; set; }
        public DateTime Opprettelsesdato { get; set; }
        public virtual Bok Bok { get; set; }
    }
}