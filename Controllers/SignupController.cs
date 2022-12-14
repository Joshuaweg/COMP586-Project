using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Google.Cloud.Firestore;
using FirebaseConnector.Controllers;
using FirebaseConnector.Models;
using System.Linq.Expressions;

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
            int d_id=new int();
            pat.patient = newPat.patient.ToLower();
            pat.username = newPat.username.ToLower();
            pat.password = newPat.password;
            pat.currentdoctor=newPat.currentdoctor;
            pat.dateofbirth = newPat.dateofbirth;
            pat.address = newPat.address;
            pat.phonenumber = newPat.phonenumber;
            patientsController pc = new patientsController();
            int patient_id = await pc.addDocumentAsync(pat);
           QuerySnapshot doc_id=await pc.Query("doctors", new Dictionary<string, object>() { { "name", pat.currentdoctor } });
            foreach (var doc in doc_id.Documents) {
                d_id = Convert.ToInt32(doc.ToDictionary()["id"]);
            }
            doctorinchargeofpatients dip = new doctorinchargeofpatients();
            doctorinchargeofpatientsController dpc = new doctorinchargeofpatientsController();
            dip.doctor = pat.currentdoctor;
            dip.patient = pat.patient;
            dip.doctor_id = d_id;
            dip.patient_id=patient_id;
            await dpc.addDocumentAsync(dip);

            return View("../Login/Index");
        }
    }
}
