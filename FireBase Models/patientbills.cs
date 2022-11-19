using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Models
{
    internal class patientbills
    {
        public int ID { get; set; }
        public string patient { get; set; }
        public int amount { get; set; }
    }
}
