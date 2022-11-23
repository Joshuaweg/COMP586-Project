using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
using FirebaseConnector.Models;
using FirebaseConnector.Controllers;
using System.Threading.Tasks;

class Patient : PatientInterface {
    public string name;
    public Patient(string name) {
        this.name = name.ToLower();
    }

    public override async Task<Dictionary<string, object>> viewPrescriptions(string patient) {
        patientprescriptionsController controller = new patientprescriptionsController();
        return await controller.retrieveDocumentAsync(patient);
    }

    public override async Task modifyAppointment(int id, string patient, DateTime time, string name) {
        patientappointments info = new patientappointments();
        info.ID = id;
        info.patient = patient;
        info.time = time.ToUniversalTime();
        info.name = name;
        string docName = name + "_" + id.ToString();
        patientappointmentsController controller = new patientappointmentsController();
        await controller.addDocumentAsync(info, docName);
    }

    public override async Task<Dictionary<string, object>> viewAppointment(string patient) {
        patientappointmentsController controller = new patientappointmentsController();
        return await controller.retrieveDocumentAsync(patient);
    }

    public override async Task payBill(string patient) {
        patientbillsController controller = new patientbillsController();
        await controller.deleteDocumentAsync(patient);
    }

    public override async Task updateInformation(patients patient) {
        patientsController controller = new patientsController();
        await controller.deleteDocumentAsync(patient.patient);
        await controller.addDocumentAsync(patient, patient.patient);
    }

    public override async Task<Dictionary<string, object>> readComments(string name) {
        doctortopatientcommentsController controller = new doctortopatientcommentsController();
        Dictionary<string, object> temp = await controller.retrieveDocumentAsync(name);
        return temp;
    }

    public override async Task writeComments(int id, string from, string to, string message) {
        patienttodoctorcomments info = new patienttodoctorcomments();
        info.ID = id;
        info.patient = from;
        info.doctor = to;
        info.message = message;
        string docName = name + "_" + id.ToString();
        patienttodoctorcommentsController controller = new patienttodoctorcommentsController();
        await controller.addDocumentAsync(info, docName);
    }
}