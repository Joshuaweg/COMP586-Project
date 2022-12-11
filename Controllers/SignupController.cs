using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using FirebaseConnector.Controllers;
using FirebaseConnector.Models;
namespace HospitalManagementSystem.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> NewPatient(SignupModel newPat) {

            patients pat = new patients();
            pat.patient = newPat.patient.ToLower();
            pat.username = newPat.username.ToLower();
            pat.password = newPat.password;
            pat.currentdoctor=newPat.currentdoctor;
            pat.dateofbirth = newPat.dateofbirth;
            pat.address = newPat.address;
            pat.phonenumber = newPat.phonenumber;
            patientsController pc = new patientsController();
            await pc.addDocumentAsync(pat);

            return View("../Login/Index");
        }
    }
}
