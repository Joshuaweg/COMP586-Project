using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class patientappointmentsController:FireBaseController
    {
        public async Task addDocumentAsync(patientappointments record)
        {
            FirestoreDb connect = createConnection();
            record.id = await createId("patientappointments");
            DocumentReference docRef = connect.Collection("patientappointments").Document(record.id.ToString()+record.patient+"schedule");
            Dictionary<string, object> patient = new Dictionary<string, object>();
            patient.Add("id", record.id);
            patient.Add("patient_id", record.patient_id);
            patient.Add("doctor_id", record.doctor_id);
            if (record.patient!= null) patient.Add("patient", record.patient);
            if (record.name != null) patient.Add("name", record.name);
            if (record.time != null) patient.Add("time", record.time);
            await docRef.SetAsync(patient, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(int id)
        {
            QuerySnapshot qs = await this.Query("patientappointments", new Dictionary<string, object>() { { "id", id } });
            if (qs != null)
            {
                DocumentSnapshot doc = qs.Documents[0];
                DocumentReference docRef = doc.Reference;
                await docRef.DeleteAsync();
            }
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("patientappointments").Document(documentid);
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
        public async Task updateDocumentAsync(string documentid, patientappointments record)
        {
            QuerySnapshot qs = await this.Query("patientappointments", new Dictionary<string, object>() { { "id", record.id } });
            DocumentSnapshot doc = qs.Documents[0];
            DocumentReference docRef = doc.Reference;
            Dictionary<string, object> patient = new Dictionary<string, object>();
            if (record.patient != null) patient.Add("patient", record.patient);
            if (record.name != null) patient.Add("name", record.name);
            if (record.time != null) patient.Add("time", record.time);

            await docRef.UpdateAsync(patient);

        }
    }
}
