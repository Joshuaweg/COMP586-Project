using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
<<<<<<< HEAD

abstract class AdminInterface {
    public abstract void orderSupplies();
    public abstract void manageDoctors();
    public abstract void billCustomer();

=======
using System.Threading.Tasks;

abstract class AdminInterface {
    public abstract Task orderSupplies(int id, string name, int amount);
    public abstract Task manageDoctors(string name);
    public abstract Task billCustomer(int id, string patient, int amount);
    public abstract Task<Dictionary<string, object>> readComments(string name);
    public abstract Task writeComments(int id, string from, string to, string message);
>>>>>>> 5df528a28e8c50372c71f9c8ad307d38fdd9f724
    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}