using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
<<<<<<< HEAD

abstract class DoctorInterface {
    public abstract void givePrescriptions();
    public abstract void viewAppointments();
    public abstract void viewPatientInformation();

=======
using System.Threading.Tasks;

abstract class DoctorInterface {
    public abstract Task givePrescriptions(int id, string patient, string presciptions);
    public abstract Task<Dictionary<string, object>> viewAppointments(string doctor);
    public abstract Task<Dictionary<string, object>> viewPatientInformation(string patient);
    public abstract Task<Dictionary<string, object>> readComments(string name);
    public abstract Task writeComments(int id, string from, string to, string message);
>>>>>>> 5df528a28e8c50372c71f9c8ad307d38fdd9f724
    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}