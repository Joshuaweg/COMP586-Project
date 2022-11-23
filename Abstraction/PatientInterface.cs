using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
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
    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}