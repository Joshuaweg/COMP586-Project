using System;
using System.Collections.Generic; 
using System.Data;
using FirebaseConnector.Controllers;
using FirebaseConnector.Models;
using Google.Cloud.Firestore;

public class Doctor : DoctorInterface, ChatHandler {
    public string name;
    public Doctor(string name) {
        this.name = name.ToLower();
    }

    public override async Task givePrescriptions() {
        Console.WriteLine("\nEnter the name of the patient: ");
        string patientName = Console.ReadLine();
        Console.WriteLine("\nEnter the prescription: ");
        string prescriptionName = Console.ReadLine();
        patientprescriptions prescribe = new patientprescriptions();
        patientprescriptionsController pc = new patientprescriptionsController();
        prescribe.patient = patientName;
        prescribe.presciptions = prescriptionName;
        await pc.addDocumentAsync(prescribe, patientName + prescriptionName);

    }
    
    public override async Task<List<Dictionary<string,object>>> viewAppointments() {
        Console.WriteLine("\nList of Appointments: ");
        List<Dictionary<string, object>> apointmentList = new List<Dictionary<string,object>>();
        doctorschedulesController docsched = new doctorschedulesController();
        Dictionary<string, object> qu = new Dictionary<string, object>();
        qu.Add("doctor", name);
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
        string patientName = Console.ReadLine();
        fields.Add("patient", patientName);
        fields.Add("doctor", name);
        patientsController pc = new patientsController();

        QuerySnapshot qu = await pc.Query("doctorinchargeofpatients", fields);

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

    public async Task<List<Dictionary<string,object>>> readComments() {
        List<Dictionary<string, object>> comments = new List<Dictionary<string, object>>();
        bool found = false;
        Dictionary<string, object> fields = new Dictionary<string, object>();
        fields.Add("doctor", name);
        patienttodoctorcommentsController pc = new patienttodoctorcommentsController();

        QuerySnapshot qu = await pc.Query("patienttodoctorcomments", fields);
        foreach (var comm in qu.Documents)
        {
            Dictionary<string, object> co = comm.ToDictionary();
            comments.Add(co);
            foreach (KeyValuePair<string, object> pair in co)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }

        }
        qu = await pc.Query("admintodoctorcomments", fields);
        foreach (var comm in qu.Documents)
        {
            Dictionary<string, object> co = comm.ToDictionary();
            comments.Add(co);
            foreach (KeyValuePair<string, object> pair in co)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }

        }


        return comments;
    }

    public async Task writeComments(string recepient, string message) {
        Dictionary<string, object> fields = new Dictionary<string, object>();
        fields.Add("doctor", name);
        fields.Add("patient", recepient);
        doctortoadmincommentsController ac = new doctortoadmincommentsController();
        doctortopatientcommentsController pc = new doctortopatientcommentsController();
        QuerySnapshot qu = await ac.Query("patients", fields);
        bool found = false;
        if (qu != null) {
            doctortopatientcomments dc = new doctortopatientcomments();
            dc.patient = recepient;
            dc.doctor = name;
            dc.message=message;
            await pc.addDocumentAsync(dc,name+"to"+recepient);
            found = true;
        }
        fields.Remove("patient");
        fields.Add("admin", recepient);
        qu = await ac.Query("admininchargeofdoctor", fields);
        if (qu != null) {
            doctortoadmincomments dca = new doctortoadmincomments();
            dca.admin = recepient;
            dca.doctor = name;
            dca.message = message;
            await ac.addDocumentAsync(dca, name + "to" + recepient);
            found = true;
        }
        if (!found) Console.WriteLine("Not found");

    }

    //Look at schedule X
    //Write comments about patients X

    //read/look at comments from admin X

    //give prescriptions X
}