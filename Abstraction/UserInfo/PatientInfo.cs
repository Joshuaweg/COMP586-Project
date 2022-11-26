using System;
using System.Collections.Generic; 
using System.Collections;

class PatientInfo {
    private readonly string name; //X
    public ArrayList prescriptions; //X
    private readonly string dateOfBirth; //X
    public ArrayList doctorComments; //X
    public ArrayList appointments; //X
    private string currDoctor; //X
    private string address; //X
    private string phoneNumber; //X
    private string insuranceProvider; //X
    public Dictionary<string, int> bills; //X

    public PatientInfo(string name, string dateOfBirth, string currDoctor, string address, 
    string phoneNumber, string insuranceProvider) {
        this.name = name;
        prescriptions = new ArrayList();
        this.dateOfBirth = dateOfBirth;
        doctorComments = new ArrayList();
        appointments = new ArrayList();
        this.currDoctor = currDoctor;
        this.address = address;
        this.phoneNumber = phoneNumber;
        this.insuranceProvider = insuranceProvider;
        bills = new Dictionary<string,int>();
    }

    public string Birthday => dateOfBirth;
    public string Name => name;

    public string Doctor {
        get => currDoctor;
        set => currDoctor = value;
    }

    public string Address {
        get => address;
        set => address = value;
    }

    public string PhoneNumber {
        get => phoneNumber;
        set => phoneNumber = value;
    }

    public string Insurance {
        get => insuranceProvider;
        set => insuranceProvider = value;
    }

    //name
    //prescriptions
    //appointments
    //date of birth
    //comments from doctor
    //current doctor
}