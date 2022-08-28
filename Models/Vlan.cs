using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetRodhe2.Models
{
    public class Vlan
    {
        public int id_vlan { get; set; }
        public int no_vlan { get; set; }
        public string nombre_vlan { get; set; }
        public string ip_subred { get; set; }
        public string gw_vlan{ get; set; }
        public string sub_net_mask{ get; set; }
        public string ip_minima{ get; set; }
        public string ip_maxima{ get; set; }

        //los demas datos que tenga la vlan
    }
}