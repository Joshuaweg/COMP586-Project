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
            if(securityService.IsValid(userModel) )
            {
                FireBaseController fb;
                if (userModel.profile == 0)
                {
                    fb = new patientsController();
                }
                else if (userModel.profile == 1)
                {
                    fb = new adminController();
                }
                else fb = new doctorsController();
                return View("../"+profile_path[userModel.profile]+"/Index", userModel);
            }
            else
            {
                return View("Index", userModel);
            }
        }
    }
}
