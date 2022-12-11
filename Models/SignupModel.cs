using FirebaseConnector.Controllers;
using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication1.Models
{
    public class SignupModel
    {

        public string patient { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
        public string currentdoctor { get; set; }
        public DateTime dateofbirth { get; set; }
        public SignupModel() { 
        
        }
        
        

        
        
    }
}
