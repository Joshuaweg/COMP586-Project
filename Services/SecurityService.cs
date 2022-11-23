using WebApplication1.Models;
using FirebaseConnector.Controllers;
using FirebaseConnector.Models;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class SecurityService
    {

        Dictionary<string,string> knownPatients;
        Dictionary<string, string> knownAdmins;
        Dictionary<string, string> knownDoctors;


       

        public async Task fill() {

            FireBaseController fb = new FireBaseController();
            knownPatients = await fb.getCredentials("patients");
            knownAdmins = await fb.getCredentials("admins");
            knownDoctors = await fb.getCredentials("doctors");
        }
        public bool IsValid(UserModel user)
        {
            bool valid = false;
            List<Dictionary<string, string>> ProfileGroups = new List<Dictionary<string, string>>();
            ProfileGroups.Add(knownPatients);
            ProfileGroups.Add(knownAdmins);
            ProfileGroups.Add(knownDoctors);
            //return true if found in the list
            Console.WriteLine(user.profile.ToString());
            try
            {
                string key = ProfileGroups[user.profile][user.UserName];
                if (key != null && SHA256Hasher.ComputeHash(user.Password).Equals(key)) valid = true;
            }
            catch{
                valid = false;
            }
            return valid;
        }
    }
}
