﻿using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using System.Diagnostics.CodeAnalysis;

namespace FirebaseConnector.Controllers
{
    internal class adminController : FireBaseController
    {
        public async Task addDocumentAsync(admins record)
        {
            FirestoreDb connect = createConnection();
            record.id = await createId("admins");
            DocumentReference docRef = connect.Collection("admins").Document(record.id.ToString()+record.name+"Admin");
            object[] param = new object[] { record.name, record.username, record.password };
            Dictionary<string, object> admin = new Dictionary<string, object>();
            admin.Add("id", record.id);
            if (record.name != null) admin.Add("name", record.name);
            if (record.password != null) admin.Add("password", SHA256Hasher.ComputeHash(record.password));
            if (record.username != null) admin.Add("username", record.username);
            await docRef.SetAsync(admin, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("admins").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("admins").Document(documentid);
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
        public async Task updateDocumentAsync(admins record)
        {
            QuerySnapshot qs = await this.Query("admins", new Dictionary<string, object>() { { "id", record.id } });
            DocumentSnapshot doc = qs.Documents[0];
            DocumentReference docRef = doc.Reference;
            Dictionary<string, object> admin = new Dictionary<string, object>();
            if (record.name != null) admin.Add("name", record.name);
            if (record.password != null) admin.Add("password", record.password);
            if (record.username != null) admin.Add("username", record.username);


            await docRef.UpdateAsync(admin);

        }
    }
}
