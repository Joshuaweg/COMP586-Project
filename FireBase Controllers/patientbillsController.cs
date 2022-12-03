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
            record.id = await createId("patientbills");
            DocumentReference docRef = connect.Collection("patientbills").Document(record.id.ToString()+record.patient+record.amount.ToString()+"bills");
            Dictionary<string, object> patient = new Dictionary<string, object>();
            if (record.id != null) patient.Add("id", record.id);
            if (record.patient != null) patient.Add("patient", record.patient);
            if (record.amount != null) patient.Add("amount", record.amount);
            await docRef.SetAsync(patient, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(int id)
        {
            QuerySnapshot qs = await this.Query("patientbills", new Dictionary<string, object>() { { "id", id } });
            if (qs.Documents != null)
            {
                DocumentSnapshot doc = qs.Documents[0];
                DocumentReference docRef = doc.Reference;
                await docRef.DeleteAsync();
            }
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
        public async Task updateDocumentAsync(patientbills record)
        {
            QuerySnapshot qs = await this.Query("patientbills", new Dictionary<string, object>() { { "id", record.id } });
            DocumentSnapshot doc = qs.Documents[0];
            DocumentReference docRef = doc.Reference;
            Dictionary<string, object> patient = new Dictionary<string, object>();
            if (record.patient != null) patient.Add("patient", record.patient);
            if (record.amount != null) patient.Add("amouont", record.amount);

            await docRef.UpdateAsync(patient);

        }
    }
}
