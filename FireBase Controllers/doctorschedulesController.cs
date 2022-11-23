﻿using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class doctorschedulesController:FireBaseController
    {
        public async Task addDocumentAsync(doctorschedules record, string doc)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctorSchedules").Document(doc);
            object[] param = new object[] { record.patient, record.doctor, record.ID, record.time };
            Dictionary<string, object> doctor = new Dictionary<string, object>();
            if (record.ID != null) doctor.Add("ID", record.ID);
            if (record.doctor != null) doctor.Add("doctor", record.doctor);
            if (record.patient != null) doctor.Add("patient", record.patient);
            if (record.time != null) doctor.Add("time", record.time);
            await docRef.SetAsync(doctor, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("doctorSchedules").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task<Dictionary<string, object>> retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctorSchedules").Document(documentid);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Console.WriteLine("Document data for {0} document:", snapshot.Id);
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
                return city;
            }
            else
            {
                Console.WriteLine("Document {0} does not exist!", snapshot.Id);
                return null;
            }
        }
        public async Task updateDocumentAsync(string documentid, doctorschedules record)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctorSchedules").Document(documentid);
            object[] param = new object[] { record.patient, record.doctor, record.ID, record.time };
            Dictionary<string, object> doctor = new Dictionary<string, object>();
            if (record.ID != null) doctor.Add("ID", record.ID);
            if (record.doctor != null) doctor.Add("doctor", record.doctor);
            if (record.patient != null) doctor.Add("patient", record.patient);
            if (record.time != null) doctor.Add("time", record.time);

            await docRef.UpdateAsync(doctor);

        }
    }
}
