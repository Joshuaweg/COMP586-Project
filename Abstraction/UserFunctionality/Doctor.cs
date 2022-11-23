using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

class Doctor : DoctorInterface, ChatHandler {
    public string name;
    public Doctor(string name) {
        this.name = name.ToLower();
    }

    public override void givePrescriptions() {
        Console.WriteLine("\nEnter the name of the patient: ");
        string patientName = Console.ReadLine();
        Console.WriteLine("\nEnter the prescription: ");
        string prescriptionName = Console.ReadLine();
        using(NpgsqlConnection con = base.GetConnection()) {    
            string query = $"insert into patientprescriptions(patient,prescriptions)values('{patientName.ToLower()}','{prescriptionName.ToLower()}')";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if(n==1) {
                Console.WriteLine("\nPrescription Made");
            }
            con.Close();
        }
    }
    
    public override void viewAppointments() {
        Console.WriteLine("\nList of Appointments: ");
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From doctorschedules";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                if((string)reader[1] == name) {
                    Console.WriteLine(reader[2] + ": " + reader[3] + " " + reader[4]);
                }
            }
            con.Close();
        }
    }

    public override void viewPatientInformation() {
        Console.WriteLine("\nEnter the Full Name of the Patient:");
        string patientName = Console.ReadLine();
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From patients  ";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            bool found = false;
            while (reader.Read()) {
                if(((string)reader[0]).ToLower() == patientName.ToLower()) {
                    Console.WriteLine("Patient Information: ");
                    Console.WriteLine("Date Of Birth: " + reader[3]);
                    Console.WriteLine("Current Doctor: " + reader[4]);
                    Console.WriteLine("Address: " + reader[5]);
                    Console.WriteLine("Phone Number: " + reader[6]);
                    Console.WriteLine("Insurance Provider: " + reader[7]);
                    found = true;
                    break;
                }
            }
            if(!found) {
                Console.WriteLine("\nPatient Not Found, Check Spelling/Punctuation");
            }
            con.Close();
        }
    }

    public void readComments() {
        Console.WriteLine("\nList of Patient Comments: ");
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From patienttodoctorcomments";
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
        Console.WriteLine("\nList of Admin Comments: ");
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From admintodoctorcomments";
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
        Console.WriteLine("\nWho would you like to message (Admin or Patient): ");
        string messageType = Console.ReadLine().ToLower();
        if(messageType == "admin") {
            Console.WriteLine("\nEnter the name of the admin to message: ");
            string adminName = Console.ReadLine();
            Console.WriteLine("\nEnter the message to send: ");
            string message = Console.ReadLine();
            using(NpgsqlConnection con = base.GetConnection()) {    
                string query = $"insert into doctortoadmincomments(doctor,admin,message)values('{name.ToLower()}','{adminName.ToLower()}','{message}')";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if(n==1) {
                    Console.WriteLine("\nMessage Sent");
                }
                con.Close();
            }
        } else if(messageType == "patient") {
            Console.WriteLine("\nEnter the name of the patient to message: ");
            string patientName = Console.ReadLine();
            Console.WriteLine("\nEnter the message to send: ");
            string message = Console.ReadLine();
            using(NpgsqlConnection con = base.GetConnection()) {    
                string query = $"insert into doctortopatientcomments(doctor,patient,message)values('{name.ToLower()}','{patientName.ToLower()}','{message}')";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if(n==1) {
                    Console.WriteLine("\nMessage Sent");
                }
                con.Close();
            }
        }
        
    }

    //Look at schedule X
    //Write comments about patients X

    //read/look at comments from admin X

    //give prescriptions X
}