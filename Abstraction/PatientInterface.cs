using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

abstract class PatientInterface {
    public abstract void viewPrescriptions();
    public abstract void modifyAppointment();
    public abstract void viewAppointment();
    public abstract void payBill();
    public abstract void updateInformation();

    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}