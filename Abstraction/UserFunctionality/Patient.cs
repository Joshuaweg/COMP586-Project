using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
using FirebaseConnector.Models;
using FirebaseConnector.Controllers;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

public class Patient : PatientInterface{
    public int id { get; set; }
    public int doctor_id { get; set; }
    public string name { get; set; }
    public Options o_sel { get; set; }
    public string address { get; set; }
    public string phone { get; set; }
    public enum Options { 
          Add,
          Delete
    }
    public DateTime appointment { get; set; }
    public int sched_id { get; set; }
    public int bill_id { get; set; }
    public Patient(string name) {
        this.name = name.ToLower();
    }
    public Patient() {
        this.name = "";
    }

    public override async Task<List<Dictionary<string,object>>> viewPrescriptions() {
        List<Dictionary<string,object>> result = new List<Dictionary<string,object>>();
        patientprescriptionsController pc = new patientprescriptionsController();
        QuerySnapshot qs = await pc.Query("patientprescriptions", new Dictionary<string, object>() { { "name", this.name } });
        foreach (DocumentSnapshot doc in qs.Documents) {
            result.Append(doc.ToDictionary());
        }

        return result;

    }

    public override async Task modifyAppointment() {
        
        return;
    }


    public override async Task<List<Dictionary<string, object>>> viewAppointment()
    {
        FireBaseController fb = new FireBaseController();
        List<Dictionary<string, object>> results = await fb.getQuery("patientappointments", new List<string>() { "id", "patient", "time" });
        return results;
    }
    public async Task<List<Dictionary<string, object>>> viewBills()
    {
        FireBaseController fb = new FireBaseController();
        List<Dictionary<string, object>> results = await fb.getQuery("patientbills", new List<string>() { "id", "patient", "amount" });
        return results;
    }

    public override async Task payBill() {
        patientbillsController pbc = new patientbillsController();
        await pbc.deleteDocumentAsync(this.bill_id);
    }

    public override async Task updateInformation() {
        patientsController pc = new patientsController();
        patients pa = new patients();
        Console.WriteLine(this.name);
        QuerySnapshot qs = await pc.Query("patients", new Dictionary<string, object>() { { "patient", this.name } });
        if (qs.Documents.Count()>0) {
            DocumentSnapshot doc = qs.Documents[0];
            Dictionary<string, object> result = doc.ToDictionary();
            pa.id = Convert.ToInt32(result["id"]);
            pa.phonenumber = this.phone;
            pa.address = this.address;
            await pc.updateDocumentAsync(pa);
        }
        
    }

    public async Task readComments() {
        return;
    }

    public async Task writeComments() {
        return;
    }

    //Create/Cancel Appointment X

    //read/write at comments from Doctor X

    //Change doctors X
    //look at medication X
}