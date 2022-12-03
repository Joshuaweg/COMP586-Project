using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using WebApplication1.Models;
using WebApplication1.Services;
using FirebaseConnector.Controllers;
using Google.Cloud.Firestore;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProcessLogin(UserModel userModel)
        {
            string[] profile_path = new string[] { "Patient", "Admin", "Doctor" };
            string[] profile_ = new string[] { "Patient", "Admin", "Doctor" };
            SecurityService securityService = new SecurityService();
            await securityService.fill();
            if(await securityService.IsValid(userModel) )
            {
                
                if (userModel.profile == 0)
                {
                    return View("../" + profile_path[userModel.profile] + "/Index", userModel.p_user);
                }
                else if (userModel.profile == 1)
                {
                    return View("../" + profile_path[userModel.profile] + "/Index", userModel.a_user);
                }
                else return View("../" + profile_path[userModel.profile] + "/Index", userModel.d_user);
             
            }
            else
            {
                return View("Index", userModel);
            }
        }
    }
}
