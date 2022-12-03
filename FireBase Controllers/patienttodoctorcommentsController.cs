﻿using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class patienttodoctorcommentsController:FireBaseController
    {
        public async Task addDocumentAsync(patienttodoctorcomments record, string doc)
        {
            FirestoreDb connect = createConnection();
            record.id = await createId("patienttodoctorcomments");
            DocumentReference docRef = connect.Collection("doctortopatientcomments").Document(record.id.ToString()+record.patient+record.doctor+"patdoccomm");
            object[] param = new object[] { record.patient, record.doctor, record.id, record.message };
            Dictionary<string, object> doctor = new Dictionary<string, object>();
            if (record.id != null) doctor.Add("id", record.id);
            if (record.doctor != null) doctor.Add("doctor", record.doctor);
            if (record.patient != null) doctor.Add("admin", record.patient);
            if (record.message != null) doctor.Add("message", record.message);
            await docRef.SetAsync(doctor, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("doctortopatientcomments").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctortopatientcomments").Document(documentid);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Console.WriteLine("Document data for {0} document:", snapshot.Id);
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
            }
            else
            {
                Console.WriteLine("Document {0} does not exist!", snapshot.Id);
            }
        }
        public async Task updateDocumentAsync(string documentid, doctortopatientcomments record)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctortopatientcomments").Document(documentid);
            Dictionary<string, object> doctor = new Dictionary<string, object>();
            if (record.doctor != null) doctor.Add("doctor", record.doctor);
            if (record.patient != null) doctor.Add("admin", record.patient);
            if (record.message != null) doctor.Add("message", record.message);
            await docRef.SetAsync(doctor, SetOptions.MergeAll);

            await docRef.UpdateAsync(doctor);

        }
    }
}
