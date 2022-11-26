using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class patientbillsController:FireBaseController
    {
        public async Task addDocumentAsync(patientbills record, string doc)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("patientbills").Document(doc);
            Dictionary<string, object> patient = new Dictionary<string, object>();
            if (record.ID != null) patient.Add("ID", record.ID);
            if (record.patient != null) patient.Add("patient", record.patient);
            if (record.amount != null) patient.Add("amouont", record.amount);
            await docRef.SetAsync(patient, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("patientbills").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("patientbills").Document(documentid);
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
        public async Task updateDocumentAsync(string documentid, patientbills record)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("patientbills").Document(documentid);
            Dictionary<string, object> patient = new Dictionary<string, object>();
            if (record.ID != null) patient.Add("ID", record.ID);
            if (record.patient != null) patient.Add("patient", record.patient);
            if (record.amount != null) patient.Add("amouont", record.amount);

            await docRef.UpdateAsync(patient);

        }
    }
}
