using Newtonsoft.Json;
using NetRodhe2.Logica;
using NetRodhe2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web;
using System.Data;
using System.Configuration;

namespace NetRodhe2.Controllers
{
    public class DispositivosController : Controller
    {
        // GET: Dispositivos
        public ActionResult Dispositivos()
        {
            return View("Dispositivo");
        }

        public ActionResult Area()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarDispositivo()
        {
            List<Dispositivos> oLista = new List<Dispositivos>();

            oLista = DispositivosLogica.Instancia.Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarDispositivos(string objeto)
        {

            Response oresponse = new Response() { resultado = true, mensaje = "" };

            try
            {
                Dispositivos nDispositivo = new Dispositivos();
                nDispositivo = JsonConvert.DeserializeObject<Dispositivos>(objeto);

                

                if (nDispositivo.id_dispositivos == 0)
                {
                    int id = DispositivosLogica.Instancia.Registrar(nDispositivo);
                    nDispositivo.id_dispositivos = id;
                    oresponse.resultado = nDispositivo.id_dispositivos == 0 ? false : true;

                }
                else
                {
                    oresponse.resultado = DispositivosLogica.Instancia.Modificar(nDispositivo);
                }


            }
            catch (Exception e)
            {
                oresponse.resultado = false;
                oresponse.mensaje = e.Message;
            }

            return Json(oresponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarDispositivos(int id)
        {
            bool respuesta = false;
            respuesta = DispositivosLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarArea()
        {
            List<Area> objetoLista = new List<Area>();
            objetoLista = AreaLogica.Instancia.Listar();
            return Json(new { data = objetoLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarArea(Area objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.id_area == 0) ? AreaLogica.Instancia.Registrar(objeto) : AreaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarArea(int id)
        {
            bool respuesta = false;
            respuesta = AreaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarVlan()
        {
            List<Vlan> oLista = new List<Vlan>();
            oLista = VlanLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarVlan(Vlan objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.id_vlan == 0) ? VlanLogica.Instancia.Registrar(objeto) : VlanLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarVlan(int id)
        {
            bool respuesta = false;
            respuesta = VlanLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPersonal()
        {
            List<Personal> oLista = new List<Personal>();
            oLista = PersonalLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarPersonal(Personal objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.id_personal == 0) ? PersonalLogica.Instancia.Registrar(objeto) : PersonalLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarPersonal(int id)
        {
            bool respuesta = false;
            respuesta = PersonalLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        public class Response
        {

            public bool resultado { get; set; }
            public string mensaje { get; set; }
        }
    }
}