using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

class Patient : PatientInterface{
    public string name;
    public Patient(string name) {
        this.name = name.ToLower();
    }

    public override void viewPrescriptions() {
        Console.WriteLine("\nList of Prescriptions: ");
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From patientprescriptions";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                if(((string)reader[1]).ToLower() == name) {
                    Console.WriteLine(reader[2]);
                }
            }
            con.Close();
        }
    }

    public override void modifyAppointment() {
        Console.WriteLine("Input how you would like to modify the appointment (delete or add): ");
        string option = Console.ReadLine().ToLower();
        if(option == "delete") {
            Console.WriteLine("Enter the appointment title you would like to delete: ");
            string deleteAppointment = Console.ReadLine().ToLower();
            using(NpgsqlConnection con = base.GetConnection()) {    
                string query = $"DELETE FROM patientappointments WHERE title = '{deleteAppointment}' AND patient = '{name}'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if(n==1) {
                    Console.WriteLine("\nAppointment Deleted");
                }
                con.Close();
            }
        } else if(option == "add") {
            Console.WriteLine("\nEnter the title of the appointment: ");
            string title = Console.ReadLine();
            Console.WriteLine("\nEnter the time for the appointment: ");
            string time = Console.ReadLine();
            Console.WriteLine("\nEnter the date for the appointment: ");
            string date = Console.ReadLine();
            using(NpgsqlConnection con = base.GetConnection()) {    
                string query = $"insert into patientappointments(patient,time,date,title)values('{name.ToLower()}','{time.ToLower()}','{date.ToLower()}','{title.ToLower()}')";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                int n = cmd.ExecuteNonQuery();
                if(n==1) {
                    Console.WriteLine("\nAppointment Made");
                }
                con.Close();
            }
        }
    }

    public override void viewAppointment() {
        Console.WriteLine("\nList of Appointments: ");
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From patientappointments";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                if(((string)reader[1]).ToLower() == name) {
                    Console.WriteLine(reader[4] + ": " + reader[2] + " " + reader[3]);
                }
            }
            con.Close();
        }
    }

    public override void payBill() {
        using(NpgsqlConnection con = base.GetConnection()) {    
            string query = $"DELETE FROM patientbills WHERE patient = '{name}'";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if(n==1) {
                Console.WriteLine("\nBill Paid");
            }
            con.Close();
        }
    }

    public override void updateInformation() {
        Console.WriteLine("\nPlease Provide The Name Of Your Primary Doctor");
        string currentDoctor = Console.ReadLine().ToLower();
        Console.WriteLine("\nPlease Provide Your Address");
        string address = Console.ReadLine().ToLower();
        Console.WriteLine("\nPlease Provide Your Phone Number");
        string phoneNumber = Console.ReadLine();
        Console.WriteLine("\nPlease Provide Your Insurance Provider");
        string insurance = Console.ReadLine().ToLower();
        using(NpgsqlConnection con = base.GetConnection()) {    
            string query = $"UPDATE patients SET currentdoctor = '{currentDoctor}', address = '{address}', phonenumber = '{phoneNumber}', insuranceprovider = '{insurance}' WHERE name = '{name}'";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if(n==1) {
                Console.WriteLine("\nPatient Updated");
            }
            con.Close();
        }
    }

    public void readComments() {
        Console.WriteLine("\nList of Comments: ");
        using(NpgsqlConnection con = base.GetConnection()) {
            string query = $"Select * From doctortopatientcomments";
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
            string query = $"insert into patienttodoctorcomments(patient,doctor,message)values('{name.ToLower()}','{doctorName.ToLower()}','{message}')";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if(n==1) {
                Console.WriteLine("\nMessage Sent");
            }
            con.Close();
        }
    }

    //Create/Cancel Appointment X

    //read/write at comments from Doctor X

    //Change doctors X
    //look at medication X
}