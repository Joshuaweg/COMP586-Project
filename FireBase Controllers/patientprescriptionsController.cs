using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class patientprescriptionsController:FireBaseController
    {
        public async Task addDocumentAsync(patientprescriptions record)
        {
            FirestoreDb connect = createConnection();
            record.id = await createId("patientprescriptions");
            DocumentReference docRef = connect.Collection("patientprescriptions").Document(record.id.ToString()+record.patient+record.presciptions+"prescribe");
            Dictionary<string, object> patient = new Dictionary<string, object>();
            if (record.id != null) patient.Add("id", record.id);
            if (record.patient != null) patient.Add("patient", record.patient);
            if (record.presciptions != null) patient.Add("prescriptions", record.presciptions);
            
            await docRef.SetAsync(patient, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("patientprescriptions").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("patientprescriptions").Document(documentid);
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
        public async Task updateDocumentAsync(patientprescriptions record)
        {
            QuerySnapshot qs = await this.Query("patientprescriptions", new Dictionary<string, object>() { { "id", record.id } });
            DocumentSnapshot doc = qs.Documents[0];
            DocumentReference docRef = doc.Reference;
            Dictionary<string, object> patient = new Dictionary<string, object>();
            if (record.patient != null) patient.Add("patient", record.patient);
            if (record.presciptions != null) patient.Add("prescriptions", record.presciptions);

            await docRef.UpdateAsync(patient);

        }
    }
}
