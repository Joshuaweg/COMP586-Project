using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
using FirebaseConnector.Controllers;
using FirebaseConnector.Models;
using Google.Cloud.Firestore;

public class Admin : AdminInterface {
    public int id { get; set; }
    public string name { get; set; }
    public string supp { get; set; }
    public string doctor { get; set; }
    public string patient { get; set; }
    public string quantity { get; set; }
    public string bill { get; set; }
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

    public async Task readComments() {
        Console.WriteLine("\nList of Comments: ");
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From doctortoadmincomments";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                if(((string)reader[2]).ToLower() == name) {
                    Console.WriteLine(reader[1] + ": " + reader[3]);
                }
            }
            con.Close();
        }
    }

    public async Task writeComments() {
        Console.WriteLine("\nEnter the name of the doctor to message: ");
        string doctorName = Console.ReadLine();
        Console.WriteLine("\nEnter the message to send: ");
        string message = Console.ReadLine();
        using(NpgsqlConnection con = base.GetConnection()) {    
            string query = $"insert into admintodoctorcomments(admin,doctor,message)values('{name.ToLower()}','{doctorName.ToLower()}','{message}')";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if(n==1) {
                Console.WriteLine("\nMessage Sent");
            }
            con.Close();
        }
    }
    //Order supplies X
    //Manage doctors (fire them?) X

    //read/make comments to doctor X
    //read/make comments to admin X
}