using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
using FirebaseConnector.Controllers;
using FirebaseConnector.Models;
using Google.Cloud.Firestore;

public class Admin : AdminInterface {
    //User Info
    public int id { get; set; }
    public string name { get; set; }

    //for sending any info from one view to the next
    public object payload { get; set; }
    //form fields
        //orderSupplies
    public string supp { get; set; }
    public string quantity { get; set; }
        //manageDoctors
    public string doctor { get; set; }
       //billCustomer
    public string patient { get; set; }
    public string bill { get; set; }
    //end of form fields
    public Admin(string name) {
        this.name = name.ToLower();
    }
    public Admin() {
        this.name = "";
    }

    public override async Task orderSupplies() {
        ordersupplies os = new ordersupplies();
        ordersuppliesController osc = new ordersuppliesController();
        os.name = supp;
        os.amount = Convert.ToInt32(quantity);
        await osc.addDocumentAsync(os, supp + quantity);
        supp = null;
        quantity = "";
        
    }
    public async Task<List<Dictionary<string,object>>> getSupplies()
    {
        ordersupplies os = new ordersupplies();
        ordersuppliesController osc = new ordersuppliesController();
        List<Dictionary<string,object>> orders = await osc.getQuery("ordersupplies", new List<string>() { "name","amount" });
        return orders;
    }

    public override async Task manageDoctors() {
        doctorsController dc = new doctorsController();
        string doc_id = "";
        QuerySnapshot qs = await dc.Query("doctors", new Dictionary<string, object>() { { "name", this.doctor } });
        foreach (DocumentSnapshot doc in qs.Documents) {
            doc_id = doc.Id;
            Dictionary<string, object> fields = doc.ToDictionary();
            if (fields["name"].ToString().Equals(doctor)) {
                await dc.deleteDocumentAsync(doc_id);
            }
        }
        doctor = null;
    }

    public override async Task billCustomer() {
        patientbillsController pbc = new patientbillsController();
        patientbills pb = new patientbills();
        pb.patient = patient;
        pb.amount = Convert.ToInt32(bill);
        
        await pbc.addDocumentAsync(pb, patient + bill);
    }

    

   

}