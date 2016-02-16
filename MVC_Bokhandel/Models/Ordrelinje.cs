namespace MVC_Bokhandel.Models
{
    public class Ordrelinje
    {
        public int Id { get; set; }
        public int OrdreId { get; set; }
        public int BokId { get; set; }
        public int Antall { get; set; }
        public decimal PrisPrEnhet { get; set; }

        public virtual Bok Bok { get; set; }
        public virtual Ordre Ordre { get; set; }
    }
}
