using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

class Admin : AdminInterface {
    public string name;
    public Admin(string name) {
        this.name = name.ToLower();
    }

    public override async Task orderSupplies() {
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

    public override async Task manageDoctors() {
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

    public override async Task billCustomer() {
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