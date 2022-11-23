using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

abstract class DoctorInterface {
    public abstract void givePrescriptions();
    public abstract void viewAppointments();
    public abstract void viewPatientInformation();

    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}