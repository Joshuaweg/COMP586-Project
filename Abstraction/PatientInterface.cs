using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
<<<<<<< HEAD

abstract class PatientInterface {
    public abstract void viewPrescriptions();
    public abstract void modifyAppointment();
    public abstract void viewAppointment();
    public abstract void payBill();
    public abstract void updateInformation();

=======
using System.Threading.Tasks;
using FirebaseConnector.Models;
using FirebaseConnector.Controllers;

abstract class PatientInterface {
    public abstract Task<Dictionary<string, object>> viewPrescriptions(string patient);
    public abstract Task modifyAppointment(int id, string patient, DateTime time, string name);
    public abstract Task<Dictionary<string, object>> viewAppointment(string patient);
    public abstract Task payBill(string patient);
    public abstract Task updateInformation(patients patient);
    public abstract Task<Dictionary<string, object>> readComments(string name);
    public abstract Task writeComments(int id, string from, string to, string message);
>>>>>>> 5df528a28e8c50372c71f9c8ad307d38fdd9f724
    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}