﻿using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class doctortoadmincommentsController:FireBaseController
    {
        public async Task addDocumentAsync(doctortoadmincomments record, string doc)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctortoadmincomments").Document(doc);
            object[] param = new object[] { record.admin, record.doctor, record.ID, record.message };
            Dictionary<string, object> doctor = new Dictionary<string, object>();
            if (record.ID != null) doctor.Add("ID", record.ID);
            if (record.doctor != null) doctor.Add("doctor", record.doctor);
            if (record.admin != null) doctor.Add("admin", record.admin);
            if (record.message != null) doctor.Add("message", record.message);
            await docRef.SetAsync(doctor, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("doctortoadmincomments").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task<Dictionary<string, object>> retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctortoadmincomments").Document(documentid);
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
        public async Task updateDocumentAsync(string documentid, doctortoadmincomments record)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctortoadmincomments").Document(documentid);
            object[] param = new object[] { record.admin, record.doctor, record.ID, record.message };
            Dictionary<string, object> doctor = new Dictionary<string, object>();
            if (record.ID != null) doctor.Add("ID", record.ID);
            if (record.doctor != null) doctor.Add("doctor", record.doctor);
            if (record.admin != null) doctor.Add("admin", record.admin);
            if (record.message != null) doctor.Add("message", record.message);
            await docRef.SetAsync(doctor, SetOptions.MergeAll);

            await docRef.UpdateAsync(doctor);

        }
    }
}
