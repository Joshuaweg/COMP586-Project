using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

public abstract class DoctorInterface {
    public abstract Task givePrescriptions();
    public abstract Task viewAppointments();
    public abstract Task viewPatientInformation();

    private protected NpgsqlConnection GetConnection() {
        return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres; Password=comp586;Database=postgres");
    }
}