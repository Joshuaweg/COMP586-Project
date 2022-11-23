using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;
using System.Threading.Tasks;

abstract class AdminInterface {
    public abstract Task orderSupplies(int id, string name, int amount);
    public abstract Task manageDoctors(string name);
    public abstract Task billCustomer(int id, string patient, int amount);
    public abstract Task<Dictionary<string, object>> readComments(string name);
    public abstract Task writeComments(int id, string from, string to, string message);
    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}