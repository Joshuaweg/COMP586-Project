using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class DoctorController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> ViewSchedule(Doctor doc)
        {
            string p_sched = "<table><tr><th>id</th><th>Doctor</th><th>Patient</th><th>Time</th></tr>\n";
         
                List<Dictionary<string,object>> sched = await doc.viewAppointments();
                foreach (var appt in sched) {
                    Timestamp dt = (Timestamp)appt["time"];
                    DateTime tm = dt.ToDateTime();
                    
                    p_sched += "<tr>";
                    
                    p_sched += "<td>" + appt["id"] + "</td>"+ "<td>" + appt["doctor"] + "</td>" + "<td>" + appt["patient"] + "</td>" + "<td>" + tm.ToString("f") + "</td>";
                
                    p_sched += "</tr>\n";
                }
            return p_sched;
        }
        [HttpPost]
        public async Task<IActionResult> orderPrescription(Doctor doc) {

            Console.WriteLine("Patient: "+doc.patientName);
            Console.WriteLine("Prescription: " + doc.prescriptionName);
            await doc.givePrescriptions();
            TempData["message"] = "Prescription created";
            return View("Index",doc);

        }
        public async Task<IActionResult> patientInfo (Doctor doc)
        {

            if (doc != null)
            {
                List<Dictionary<string,object>>pat = await doc.viewPatientInformation();
                doc.payload = pat;

            }
            return View("patients",doc);

        }
    }
}
