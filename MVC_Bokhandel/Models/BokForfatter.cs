using System.Data.Entity;
namespace MVC_Bokhandel.Models
{
    public class BokForfatter
    {
        public int Id { get; set; }
        public int BokId{get; set;}
        public int ForfatterId { get; set; }

        public virtual Bok Bok { get; set; }
        public virtual Forfatter Forfattter { get; set; } 
    }
}
