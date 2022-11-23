using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
<<<<<<< HEAD

class Doctor : DoctorInterface, ChatHandler {
=======
using FirebaseConnector.Models;
using FirebaseConnector.Controllers;
using System.Threading.Tasks;

class Doctor : DoctorInterface {
>>>>>>> 5df528a28e8c50372c71f9c8ad307d38fdd9f724
    public string name;
    public Doctor(string name) {
        this.name = name.ToLower();
    }

<<<<<<< HEAD
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
=======
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
>>>>>>> 5df528a28e8c50372c71f9c8ad307d38fdd9f724
}