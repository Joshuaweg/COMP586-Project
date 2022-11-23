using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
<<<<<<< HEAD

class Admin : AdminInterface, ChatHandler {
=======
using FirebaseConnector.Models;
using FirebaseConnector.Controllers;
using System.Threading.Tasks;

class Admin : AdminInterface {
>>>>>>> 5df528a28e8c50372c71f9c8ad307d38fdd9f724
    public string name;
    public Admin(string name) {
        this.name = name.ToLower();
    }

<<<<<<< HEAD
    public override void orderSupplies() {
        Console.WriteLine("\nEnter the name of the product: ");
        string supplyName = Console.ReadLine().ToLower();
        Console.WriteLine("\nEnter the amount to order (lbs) (just the number): ");
        string supplyAmount = Console.ReadLine();

        int preExistingAmount = 0; 
        bool found = false;
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From orderedsupples";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                if(((string)reader[1]).ToLower() == supplyName.ToLower()) {
                    found = true;
                    preExistingAmount = Int32.Parse((string)reader[2]);
                    int temp = Int32.Parse(supplyAmount);
                    int tempAmount = temp + preExistingAmount;
                    supplyAmount = tempAmount.ToString();
                    break;
                }
            }
            con.Close();
        }
        if(!found) {
            using(NpgsqlConnection con = base.GetConnection()) {    
                string query = $"insert into orderedsupples(name,amount)values('{supplyName}','{supplyAmount}')";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if(n==1) {
                    Console.WriteLine("\nSupply Ordered");
                }
                con.Close();
            }
        } else {
            using(NpgsqlConnection con = base.GetConnection()) {    
                string query = $"UPDATE orderedsupples SET amount = {supplyAmount} where name = '{supplyName}'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if(n==1) {
                    Console.WriteLine("\nSupply Ordered");
                }
                con.Close();
            }
        }
    }

    public override void manageDoctors() {
        Console.WriteLine("\nEnter the name of the Doctor you want to fire: ");
        string firedDoctor = Console.ReadLine().ToLower();
        using(NpgsqlConnection con = base.GetConnection()) {    
            string query = $"DELETE FROM doctors WHERE name = '{firedDoctor}'";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if(n==1) {
                Console.WriteLine("\nDoctor Fired");
            }
            con.Close();
        }
    }

    public override void billCustomer() {
        Console.WriteLine("\nEnter the name of the patient to bill: ");
        string patientName = Console.ReadLine().ToLower();
        Console.WriteLine("\nEnter the amount to charge (just the number): ");
        string patientCharge = Console.ReadLine();

        int preExistingCharge = 0; 
        bool found = false;
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From patientbills";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                if(((string)reader[1]).ToLower() == patientName.ToLower()) {
                    found = true;
                    preExistingCharge = Int32.Parse((string)reader[2]);
                    int temp = Int32.Parse(patientCharge);
                    int tempAmount = temp + preExistingCharge;
                    patientCharge = tempAmount.ToString();
                    break;
                }
            }
            con.Close();
        }
        if(!found) {
            using(NpgsqlConnection con = base.GetConnection()) {    
                string query = $"insert into patientbills(patient,amount)values('{patientName}','{patientCharge}')";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if(n==1) {
                    Console.WriteLine("\nPatient Charged");
                }
                con.Close();
            }
        } else {
            using(NpgsqlConnection con = base.GetConnection()) {    
                string query = $"UPDATE patientbills SET amount = {patientCharge} where patient = '{patientName}'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if(n==1) {
                    Console.WriteLine("\nPatient Charged");
                }
                con.Close();
            }
        }
    }

    public void readComments() {
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

    public void writeComments() {
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
=======
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
>>>>>>> 5df528a28e8c50372c71f9c8ad307d38fdd9f724
}