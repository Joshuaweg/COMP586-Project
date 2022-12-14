using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

public abstract class PatientInterface {
    public abstract Task viewPrescriptions();
    public abstract Task viewAppointment();
    public abstract Task payBill();
    public abstract Task updateInformation();

    
}