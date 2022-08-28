using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using NetRodhe2.Models;
using NetRodhe2.Logica;
using System.Web.Security;

namespace NetRodhe2.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string usuario, string contraseña)
        {
            Usuarios objeto = new LO_Usuario().EncontrarUsuario(usuario, contraseña);
            if(objeto.Nombres != null)
            {
                FormsAuthentication.SetAuthCookie(objeto.Usuario, false);

                Session["Usuario"] = objeto;

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}