using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
using FirebaseConnector.Models;
using FirebaseConnector.Controllers;
using System.Threading.Tasks;

class Doctor : DoctorInterface {
    public string name;
    public Doctor(string name) {
        this.name = name.ToLower();
    }

    public override async Task givePrescriptions(int id, string patient, string prescriptions) {
        patientprescriptionsController controller = new patientprescriptionsController();
        patientprescriptions info = new patientprescriptions();
        info.ID = id;
        info.patient = patient;
        info.presciptions = prescriptions;
        string docName = patient + "_" + id.ToString();
        await controller.addDocumentAsync(info, docName);
    }
    
    public override async Task<Dictionary<string, object>> viewAppointments(string doctor) {
        doctorschedulesController controller = new doctorschedulesController();
        return await controller.retrieveDocumentAsync(doctor);
    }

    public override async Task<Dictionary<string, object>> viewPatientInformation(string patient) {
        patientsController controller = new patientsController();
        return await controller.retrieveDocumentAsync(patient);
    }

    public override async Task<Dictionary<string, object>> readComments(string name) {
        admintodoctorcommentsController controller = new admintodoctorcommentsController();
        Dictionary<string, object> temp = await controller.retrieveDocumentAsync(name);
        return temp;
    }

    public async Task<Dictionary<string, object>> readPatientComments(string name) {
        patienttodoctorcommentsController controller = new patientstodoctorcommentsController();
        Dictionary<string, object> temp = await controller.retrieveDocumentAsync(name);
        return temp;
    }

    //need patienttodoctorcomments controller

    public override async Task writeComments(int id, string from, string to, string message) {
        doctortoadmincomments info = new doctortoadmincomments();
        info.ID = id;
        info.doctor = from;
        info.admin = to;
        info.message = message;
        string docName = name + "_" + id.ToString();
        doctortoadmincommentsController controller = new doctortoadmincommentsController();
        await controller.addDocumentAsync(info, docName);
    }


    public async Task writeCommentsToPatient(int id, string from, string to, string message) {
        doctortopatientcomments info = new doctortopatientcomments();
        info.ID = id;
        info.doctor = from;
        info.patient = to;
        info.message = message;
        string docName = name + "_" + id.ToString();
        doctortopatientcommentsController controller = new doctortopatientcommentsController();
        await controller.addDocumentAsync(info, docName);
    }
}