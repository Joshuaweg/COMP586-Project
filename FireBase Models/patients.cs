using FirebaseConnector.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseConnector.Controllers;
using Google.Cloud.Firestore;

namespace FirebaseConnector.Models
{
     public class patients : FireBaseController
    {
        public int ID { get; set; }
        public string patient { get; set; }
        public string currentdoctor { get; set; }
        public DateTime dateofbirth { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
        public string username { get; set; }
        public string password { get; set; }

       

    }
}
