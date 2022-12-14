using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

public abstract class DoctorInterface {
    public abstract Task givePrescriptions();
    public abstract Task viewAppointments();
    public abstract Task viewPatientInformation();

   
}