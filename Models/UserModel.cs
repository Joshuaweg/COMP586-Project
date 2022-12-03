using FirebaseConnector.Controllers;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication1.Models
{
    public class UserModel
    {
        [BindProperty]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string name { get; set; }
        [BindProperty]
        public int profile { get; set; } = 0;
        public Dictionary<int, string> profiles { get; } = new Dictionary<int, string>() { { 0, "Patient" }, { 1, "Admin" }, { 2, "Doctor" } };
        public Doctor d_user { get; set; }
        public Admin a_user { get; set; }
        public Patient p_user { get; set; }
        public async Task buildUser()
        {
            FireBaseController fb = new FireBaseController();
            QuerySnapshot qu;
            string nm = "";
            switch (profile)
            {

                case 0:
                    qu = await fb.Query("patients", new Dictionary<string, object>(){ { "username",UserName} });
                    foreach (var res in qu) {
                        Dictionary<string, object> data = res.ToDictionary();
                        nm = data["patient"].ToString();
                        break;
                    }
                    p_user = new Patient(nm);
                    break;
                case 1:
                     qu = await fb.Query("admins", new Dictionary<string, object>() { { "username", UserName } });
                    foreach (var res in qu)
                    {
                        Dictionary<string, object> data = res.ToDictionary();
                        nm = data["name"].ToString();
                        break;
                    }
                    a_user = new Admin(nm);
                    break;
                case 2:
                    qu = await fb.Query("doctors", new Dictionary<string, object>() { { "username", UserName } });
                    foreach (var res in qu)
                    {
                        Dictionary<string, object> data = res.ToDictionary();
                        nm = data["name"].ToString();
                        break;
                    }
                    d_user = new Doctor(nm);
                    Console.WriteLine(  d_user.name);
                    break;
            }
            return ;
        }
        public void logout() {

            p_user = null;
            d_user = null;
            a_user = null;
             
            }

        
        
    }
}
