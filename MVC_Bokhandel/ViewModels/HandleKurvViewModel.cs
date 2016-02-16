using System.Collections.Generic;
using MVC_Bokhandel.Models;

namespace MVC_Bokhandel.ViewModels
{
    public class HandleKurvViewModel
    {
        public int Id { get; set; }
        public List<Kurv> KurvEnheter { get; set; }
        public decimal KurvTotal;
    }
}