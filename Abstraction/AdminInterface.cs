using System;
using System.Collections.Generic; 
using Npgsql;
using System.Data;

public abstract class AdminInterface {
    public abstract Task orderSupplies();
    public abstract Task manageDoctors();
    public abstract Task billCustomer();

    
}