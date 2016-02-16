using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MVC_Bokhandel.Models
{
    public class Poststed
    {
        [Key]
        [StringLength(4, MinimumLength = 4)]
        public string PostNr { get; set; }
        public string Poststedet { get; set; }

        public virtual List<Kunde> Kunder { get; set; }
    }
}
