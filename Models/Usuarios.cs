using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetRodhe2.Models
{
    public class Usuarios
    {
        public string Nombres {get; set; }
        public string Correo {get; set; }
        public string Contraseña {get; set; }
        public string Usuario {get; set; }

        public Rol IdRol { get; set; }



    }
}