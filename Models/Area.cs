using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetRodhe2.Models
{
    public class Area
    {
        public int id_area { get; set; }
        public string abrev_area { get; set; }
        public string descripcion { get; set; }
        public Edificios IEdificios{ get; set; }
    }
}