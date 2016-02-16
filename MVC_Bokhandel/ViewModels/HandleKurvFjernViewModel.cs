namespace MVC_Bokhandel.ViewModels
{
    public class HandleKurvFjernViewModel
    {
        public int Id { get; set; }
        public string Melding { get; set; }
        public decimal KurvTotal { get; set; }
        public int KurvTell { get; set; }
        public int EnhetTell { get; set; }
        public int SletId { get; set; }
    }
}