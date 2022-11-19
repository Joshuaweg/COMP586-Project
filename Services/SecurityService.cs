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
            //return true if found in the list
            string key = knownDoctors[user.UserName];
            if (key != null && SHA256Hasher.ComputeHash(user.Password).Equals(key)) valid = true;
            return valid;
        }
    }
}
