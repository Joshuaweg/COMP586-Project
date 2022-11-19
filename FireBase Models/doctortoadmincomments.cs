using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Models
{
    internal class doctortoadmincomments
    {
        public int ID { get; set; }
        public string doctor { get; set; }
        public string admin { get; set; }  
        public string message { get; set; }
    }
}
