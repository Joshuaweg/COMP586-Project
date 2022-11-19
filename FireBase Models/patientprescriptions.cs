using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Models
{
    internal class patientprescriptions
    {
        public int ID { get; set; }
        public string patient { get; set; }
        public string presciptions { get; set; }
    }
}
