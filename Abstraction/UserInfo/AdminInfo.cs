using System;
using System.Collections.Generic; 
using System.Collections;

class AdminInfo {
    private readonly string name; //X
    public ArrayList doctors; //X
    public Dictionary<string, string> adminComments; //X
    public Dictionary<string, string> doctorComments; //X

    public AdminInfo(string name) {
        this.name = name;
        doctors = new ArrayList();
        adminComments = new Dictionary<string,string>();
        doctorComments = new Dictionary<string,string>();
    }

    public string Name => name;

    //name
    //doctors
    //comments from admin
    //comments from doctor
}