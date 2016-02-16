using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MVC_Bokhandel.Models;

namespace MVC_Bokhandel.Controllers
{
    /*
     * Brukes for å definnere en session variabel som skal brukes av alle andre kontrollerne, siden sessions definnert i en 
     * kontroller ikke er synnelige i andre kontrollere
     */
    public class BaseController : Controller
    {

    private HttpSessionStateBase _session;
    protected HttpSessionStateBase CrossControllerSession
    {
        get
        {
            if (_session == null) _session = Session;
            return _session;
        }
        set {
                _session = Session;
            }
        }

       public static MvcHtmlString DeleteLink(AjaxHelper ajaxHelper, string linkText, string actionName, object routeValues)
     {
         return ajaxHelper.ActionLink(linkText, actionName, routeValues, new AjaxOptions
         {
             Confirm = "Are you sure you want to delete this item?",
             HttpMethod = "DELETE",
             OnSuccess = "function() { window.location.reload(); }"
         });
     }
    
        public bool ErLoggetInn()
        {
            //var sessionnen = Session["LoggetInn"];
            if (CrossControllerSession["LoggetInn"] != null) return (bool)CrossControllerSession["LoggetInn"];
            ViewBag.Innlogget = false;
            return false;
        }

        public string BrukereNavnet()
        {
           // var sessionnen = Session["BrukerNavn"];
            if (CrossControllerSession["BrukerNavn"] == null)
                return "";
            return (string)CrossControllerSession["BrukerNavn"];
        }

        public void SestSessionene(bool innLogget, string bNavn)
        {
            CrossControllerSession["LoggetInn"] = innLogget;
            CrossControllerSession["BrukerNavn"] = bNavn;
        }
    }
}