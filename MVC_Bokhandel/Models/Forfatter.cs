using System.Data.Entity;
using System.Collections.Generic;


namespace MVC_Bokhandel.Models
{
    public class Forfatter
    {
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Epost { get; set; }
        public string Url { get; set; }

        public virtual List<BokForfatter> BokForfattere { get; set; }

    }
}
