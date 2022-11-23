using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
using FirebaseConnector.Models;
using FirebaseConnector.Controllers;
using System.Threading.Tasks;

class Admin : AdminInterface {
    public string name;
    public Admin(string name) {
        this.name = name.ToLower();
    }

    public override async Task orderSupplies(int id, string name, int amount) {
        ordersupplies info = new ordersupplies();
        info.ID = id;
        info.name = name;
        info.amount = amount;
        string docName = name + "_" + id.ToString();
        ordersuppliesController controller = new ordersuppliesController();
        Dictionary<string, object> temp = await controller.retrieveDocumentAsync(docName);
        if(temp != null) {
            Console.WriteLine("NOT NULL");
            info.amount += Convert.ToInt32(temp["amount"]);
            await controller.updateDocumentAsync(docName, info);
        } else {
            Console.WriteLine("NULL");
            await controller.addDocumentAsync(info, docName);
        }
    }

    public override async Task manageDoctors(string name) {
        doctorsController controller = new doctorsController();
        await controller.deleteDocumentAsync(name);
    }

    public override async Task billCustomer(int id, string patient, int amount) {
        patientbills info = new patientbills();
        info.ID = id;
        info.patient = patient;
        info.amount = amount;
        patientbillsController controller = new patientbillsController();
        Dictionary<string, object> temp = await controller.retrieveDocumentAsync(patient);
        if(temp != null) {
            Console.WriteLine("NOT NULL");
            info.amount += Convert.ToInt32(temp["amount"]);
            await controller.updateDocumentAsync(patient, info);
        } else {
            Console.WriteLine("NULL");
            await controller.addDocumentAsync(info, patient);
        }
    }

    public override async Task<Dictionary<string, object>> readComments(string name) {
        doctortoadmincommentsController controller = new doctortoadmincommentsController();
        Dictionary<string, object> temp = await controller.retrieveDocumentAsync(name);
        return temp;
    }

    public override async Task writeComments(int id, string from, string to, string message) {
        admintodoctorcomments info = new admintodoctorcomments();
        info.ID = id;
        info.admin = from;
        info.doctor = to;
        info.message = message;
        string docName = name + "_" + id.ToString();
        admintodoctorcommentsController controller = new admintodoctorcommentsController();
        await controller.addDocumentAsync(info, docName);
    }
}