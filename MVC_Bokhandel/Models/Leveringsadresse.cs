using System.Data.Entity;
namespace MVC_Bokhandel.Models
{
    public class Leveringsadresse
    {
        public int Id { get; set; }
        public string Adresse { get; set; }
        public int PostId { get; set; }

        public virtual Poststed Poststed { get; set; }

    }
}