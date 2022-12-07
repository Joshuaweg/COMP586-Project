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
            string sched = "<table><caption>Upcoming Appoinments</caption><tr><th>ID</th><th>name</th><th>patient</th><th>Time</th></tr>\n";
            patientappointmentsController pa = new patientappointmentsController();
            QuerySnapshot qs = await pa.Query("patientappointments", new Dictionary<string, object>() { { "patient_id", pat.id } });
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
            doctorschedulesController dsc = new doctorschedulesController();
            switch (pat.o_sel) {

                case Patient.Options.Add:
                    patientappointments pa = new patientappointments();
                    doctorschedules ds = new doctorschedules();
                    ds.doctor_id = pat.doctor_id;
                    ds.time = pat.appointment.ToUniversalTime();
                    ds.patient = pat.name;
                    ds.patient_id = pat.id;
                    pa.patient = pat.name;
                    pa.patient_id = pat.id;
                    pa.doctor_id = pat.doctor_id;
                    pa.name = pat.name;
                    pa.time = pat.appointment.ToUniversalTime();
                    await pac.addDocumentAsync(pa);
                    await dsc.addDocumentAsync(ds);
                    Console.WriteLine("Both tables updated");

                    break;
                case Patient.Options.Delete:
                    Timestamp ts = new Timestamp();

                    Console.WriteLine(pat.sched_id.ToString());
                    QuerySnapshot pqs = await pac.Query("patientappointments", new Dictionary<string, object>() { { "id", pat.sched_id } });
                    foreach (var doc in pqs.Documents) {
                        Dictionary<string, object> dict = doc.ToDictionary();
                        ts = (Timestamp)dict["time"];
                    }
                    Console.WriteLine("Timestamp from schedule: "+ts.ToString());
                    await pac.deleteDocumentAsync(pat.sched_id);
                    QuerySnapshot qs = await dsc.Query("doctorschedules", new Dictionary<string, object>() { { "doctor_id", pat.doctor_id }, { "patient_id", pat.id }, { "time", ts } });
                    foreach (var doc in qs.Documents) {
                        Console.WriteLine(doc.Id);
                        await dsc.deleteDocumentAsync(doc.Id);
                    }
                    Console.WriteLine("both tables had records removed");
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
