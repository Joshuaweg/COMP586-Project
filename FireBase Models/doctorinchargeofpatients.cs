using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Models
{
    internal class doctorinchargeofpatients
    {
       public int id { get; set; }
       public int doctor_id { get; set; }
        public int patient_id {get; set; }
       public string doctor { get; set; }
       public string patient { get; set; }
    }
}
