using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Models
{
    internal class doctortopatientcomments
    {
        public int ID { get; set; }
        public string doctor { get; set; }
        public string patient { get; set; }
        public string message { get; set; }
    }
}
