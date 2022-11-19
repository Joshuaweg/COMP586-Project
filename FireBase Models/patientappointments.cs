using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Models
{
    internal class patientappointments
    {
        public int ID { get; set; }
       public  string patient { get; set; }
        public DateTime time { get; set; }
       public  string name { get; set; }
    }
}
