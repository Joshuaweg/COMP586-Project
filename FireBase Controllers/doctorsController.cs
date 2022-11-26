using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class doctorsController:FireBaseController
    {
        public async Task addDocumentAsync(doctors record, string doc)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctors").Document(doc);
            object[] param = new object[] { record.name, record.username, record.password };
            Dictionary<string, object> doctor = new Dictionary<string, object>();
            if (record.name != null) doctor.Add("name", record.name);
            if (record.username != null) doctor.Add("username", record.username);
            if (record.password != null) doctor.Add("password", SHA256Hasher.ComputeHash(record.password));
            await docRef.SetAsync(doctor, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("doctors").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctors").Document(documentid);
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
        public async Task updateDocumentAsync(string documentid, doctors record)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctors").Document(documentid);
            object[] param = new object[] { record.name, record.username, record.password };
            Dictionary<string, object> doctor = new Dictionary<string, object>();
            if (record.name != null) doctor.Add("name", record.name);
            if (record.username != null) doctor.Add("username", record.username);
            if (record.password != null) doctor.Add("patient", record.password);

            await docRef.UpdateAsync(doctor);

        }
    }
}
