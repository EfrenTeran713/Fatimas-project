using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetRodhe2.Models
{
    public class Switch
    {
        public int id_switch { get; set; }
        public int nombre_switch { get; set; }
        public string site { get; set; }
        public string modelo { get; set; }
        public string serie { get; set; }
        public string ip_switch { get; set; }
    }
}