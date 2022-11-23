using System;
using System.Collections.Generic; 
using System.Collections;

class DoctorInfo {
    private readonly string name; //X
    public ArrayList patients; //X
    public Dictionary<string, string> adminComments; //X
    public Dictionary<string, string> schedule; //X

    public DoctorInfo(string name) {
        this.name = name;
        patients = new ArrayList();
        adminComments = new Dictionary<string,string>();
        schedule = new Dictionary<string,string>();
    }

    public string Name => name;

    //name
    //patients
    //comments
    //schedule
}