using System;
using System.Collections.Generic; 
using System.Data;
using FirebaseConnector.Controllers;
using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

public class Doctor : DoctorInterface{
    public string name { get; set; }
    public int id { get; set; }
    public int patientId { get; set; }
    public string patientName { get; set; }
    public string prescriptionName { get; set; }
    public object payload { get; set; }

    public Doctor(string name)
    {
        this.name = name.ToLower();
    }
    public Doctor()
    {
        this.name = "";
    }

    public override async Task givePrescriptions() {
        patientprescriptions prescribe = new patientprescriptions();
        patientprescriptionsController pc = new patientprescriptionsController();
        Console.WriteLine(this.patientName);
        Console.WriteLine(this.prescriptionName);
        prescribe.patient = this.patientName;
        prescribe.presciptions = this.prescriptionName;
        await pc.addDocumentAsync(prescribe);
    }
    public async Task givePrescriptions(UserModel user)
    {
        patientprescriptions prescribe = new patientprescriptions();
        patientprescriptionsController pc = new patientprescriptionsController();
        Console.WriteLine(user.d_user.patientName);
        Console.WriteLine(user.d_user.prescriptionName);
        prescribe.patient = patientName;
        prescribe.presciptions = prescriptionName;
        await pc.addDocumentAsync(prescribe);

    }

    public override async Task<List<Dictionary<string,object>>> viewAppointments() {
        Console.WriteLine("\nList of Appointments: ");
        List<Dictionary<string, object>> apointmentList = new List<Dictionary<string,object>>();
        doctorschedulesController docsched = new doctorschedulesController();
        Dictionary<string, object> qu = new Dictionary<string, object>();
        qu.Add("doctor_id", this.id);
        QuerySnapshot appointments= await docsched.Query("doctorschedules", qu);
        foreach (var appointment in appointments.Documents) {
            Dictionary<string, object> appt = appointment.ToDictionary();
            apointmentList.Add(appt);
            foreach (KeyValuePair<string, object> pair in appt)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
        }
        return apointmentList;
    }

    public override async Task<List<Dictionary<string,object>>> viewPatientInformation() {
        List<Dictionary<string,object>> PatientList = new List<Dictionary<string ,object>>();
        bool found = false;
        Dictionary<string, object> fields = new Dictionary<string, object>();
        fields.Add("patient", this.patientName);
        fields.Add("doctor", this.name);
        patientsController pc = new patientsController();

        QuerySnapshot qu = await pc.Query("doctorinchargeofpatient", fields);

        if (qu != null)
        {
            found = true;
            foreach (var pat in qu.Documents)
            {
                Dictionary<string, object> pa = pat.ToDictionary();
                PatientList.Add(pa);
                foreach (KeyValuePair<string, object> pair in pa)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }

            }
        }
        else {

            PatientList.Add(new Dictionary<string, object>() { { "error", "not found" } });
        }
        return PatientList;


    }

    

    //Look at schedule X
    //Write comments about patients X

    //read/look at comments from admin X

    //give prescriptions X
}