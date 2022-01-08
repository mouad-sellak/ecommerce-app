using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationVoiture.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                // pour détecter la culture a partir du navigateur de l'utilisateur
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "") { lang = GestionLanguages.GetDefaultLanguage(); }
            }
            new GestionLanguages().SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }
    }
}