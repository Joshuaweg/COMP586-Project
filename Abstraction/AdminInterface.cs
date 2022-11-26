using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

public abstract class AdminInterface {
    public abstract Task orderSupplies();
    public abstract Task manageDoctors();
    public abstract Task billCustomer();

    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}