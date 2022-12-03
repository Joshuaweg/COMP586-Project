using FirebaseConnector.Controllers;
using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class PatientController : Controller
    {
        // GET: PatientController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PatientController/Details/5
        public async Task<string> viewSchedule(Patient pat) {
            string sched = "<table><tr><th>ID</th><th>name</th><th>patient</th><th>Time</th></tr>\n";
            patientappointmentsController pa = new patientappointmentsController();
            QuerySnapshot qs = await pa.Query("patientappointments", new Dictionary<string, object>() { { "patient", pat.name } });
            foreach (DocumentSnapshot doc in qs.Documents) {
                Dictionary<string, object> data = doc.ToDictionary();
                Timestamp ts = (Timestamp)data["time"];
                DateTime dt = ts.ToDateTime();
                sched += "<tr><td>" + data["id"].ToString() + "</td><td>" + data["name"].ToString() + "</td><td>" + data["patient"].ToString() + "</td><td>" + dt.ToString("f") + "</td><tr>\n";
            }

            return sched;
        }
        public async Task<List<Dictionary<int, string>>> getSchedule(Patient pat) {
            List<Dictionary<string,object>> sched =await pat.viewAppointment();
            List<Dictionary<int, string>> schs = new List<Dictionary<int, string>>();
            foreach (Dictionary<string, object> appt in sched) {
                Dictionary<int, string> sch = new Dictionary<int, string>();
                Timestamp ts = (Timestamp)appt["time"];
                string time = ts.ToDateTime().ToString("f");
                sch.Add((int)Convert.ToInt32(appt["id"]), time);
                schs.Add(sch);
            
            }

            return schs;
                }
        public async Task<List<Dictionary<int, int>>> getBills(Patient pat)
        {
            List<Dictionary<string, object>> bill = await pat.viewBills();
            List<Dictionary<int, int>> bills = new List<Dictionary<int, int>>();
            foreach (Dictionary<string, object> bil in bill)
            {
                Dictionary<int, int> bl = new Dictionary<int, int>();
                bl.Add((int)Convert.ToInt32(bil["id"]), Convert.ToInt32(bil["amount"]));
                bills.Add(bl);

            }

            return bills;
        }

        public async Task<IActionResult> updateSchedule(Patient pat) {
            patientappointmentsController pac = new patientappointmentsController();
            switch (pat.o_sel) {

                case Patient.Options.Add:
                    patientappointments pa = new patientappointments();
                    pa.patient = pat.name;
                    pa.name = pat.name;
                    pa.time = pat.appointment.ToUniversalTime();
                    await pac.addDocumentAsync(pa);
                    break;
                case Patient.Options.Delete:
                    Console.WriteLine(pat.sched_id.ToString());
                    await pac.deleteDocumentAsync(pat.sched_id);
                    break;
            
            
            
            }

            return View("Index", pat);
        }
        public async Task<IActionResult> pay(Patient pat) {
            await pat.payBill();
            return View("Index", pat);
            



        }
        // GET: PatientController/Create
        public async Task<IActionResult> updateContact(Patient pat) {
            await pat.updateInformation();
            return View("Index", pat); 
        }
    }
}
