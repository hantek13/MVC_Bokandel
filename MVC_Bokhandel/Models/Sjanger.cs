using System.Collections.Generic;
using System.Data.Entity;
namespace MVC_Bokhandel.Models
{
    public partial class Sjanger
    {
        public int Id { get; set; }
        public string Navn { get; set; }

        public List<Bok> Boker { get; set; }
    }
}