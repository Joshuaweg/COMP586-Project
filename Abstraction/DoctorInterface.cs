using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
using System.Threading.Tasks;

abstract class DoctorInterface {
    public abstract Task givePrescriptions(int id, string patient, string presciptions);
    public abstract Task<Dictionary<string, object>> viewAppointments(string doctor);
    public abstract Task<Dictionary<string, object>> viewPatientInformation(string patient);
    public abstract Task<Dictionary<string, object>> readComments(string name);
    public abstract Task writeComments(int id, string from, string to, string message);
    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}