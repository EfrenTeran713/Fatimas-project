using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NetRodhe2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Dispositivo() 
        {
            return View(@"~\Dispositivos\Dispositivo.cshtml");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();

            Session["Usuario"] = null;

            return RedirectToAction("Index", "Acceso");

        }
        public ActionResult SinPermiso()
        {
            ViewBag.Message = "No cuenta con permisos para ver esta página";

            return View();

        }
    }
}