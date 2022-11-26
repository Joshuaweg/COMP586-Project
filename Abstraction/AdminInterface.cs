using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

abstract class AdminInterface {
    public abstract void orderSupplies();
    public abstract void manageDoctors();
    public abstract void billCustomer();

    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}