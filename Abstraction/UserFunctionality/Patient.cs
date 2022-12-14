using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
using FirebaseConnector.Models;
using FirebaseConnector.Controllers;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc;

public class Patient : PatientInterface{
    //user Info
    public int id { get; set; }
    public string name { get; set; }
    //will be used to pass information from view to view
    public object payload { get; set; }
    //viewappointments
    public enum Options
    {
        Add,
        Delete
    }
    public Options o_sel { get; set; }
    //--adding appointments
    public DateTime appointment { get; set; }
    public int doctor_id { get; set; }
    //--deleting appointments
    public int sched_id { get; set; }
    //update information
    public string address { get; set; }
    public string phone { get; set; }
    //Pay Bill
    public int bill_id { get; set; }
    //end of form fields
    public Patient(string name) {
        this.name = name.ToLower();
    }
    public Patient() {
        this.name = "";
    }

    public override async Task<List<Dictionary<string, object>>> viewPrescriptions() {
        List<Dictionary<string,object>> result = new List<Dictionary<string,object>>();
        patientprescriptionsController pc = new patientprescriptionsController();
        QuerySnapshot qs = await pc.Query("patientprescriptions", new Dictionary<string, object>() { { "patient", this.name } });
        Console.WriteLine(this.name);
        Console.WriteLine(qs.Documents.Count);
        foreach (DocumentSnapshot doc in qs.Documents) {
            result.Add(doc.ToDictionary());
            Console.WriteLine(doc.ToDictionary()["prescriptions"]);
        }
        return result;

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

   

    //Create/Cancel Appointment X

    //read/write at comments from Doctor X

    //Change doctors X
    //look at medication X
}