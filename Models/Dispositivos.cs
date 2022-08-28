using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetRodhe2.Models
{
    public class Dispositivos
    {
        public int id_dispositivos { get; set; }
        public string nombre_dispositivos { get; set; }
        public string mac_add { get; set; }
        public Area IArea { get; set; }
        public Vlan IVlan { get; set; }
        public string ip_add { get; set; }
        public Personal IPersonal { get; set; }
        public string admin_wireless_router { get; set; }
        public string passwordrouter { get; set; }
        public string nombre_en_telefono { get; set; }

    }
}