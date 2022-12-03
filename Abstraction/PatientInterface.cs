using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

public abstract class PatientInterface {
    public abstract Task viewPrescriptions();
    public abstract Task modifyAppointment();
    public abstract Task viewAppointment();
    public abstract Task payBill();
    public abstract Task updateInformation();

    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}