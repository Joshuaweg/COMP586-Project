using FirebaseConnector.Models;
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
    internal class patientsController:FireBaseController
    {
        public async Task<int> addDocumentAsync(patients record)
        {
            FirestoreDb connect = createConnection();
            record.id = await createId("patients");
            DocumentReference docRef = connect.Collection("patients").Document(record.id.ToString()+record.patient+"pat");
            Dictionary<string, object> patient = new Dictionary<string, object>();
            patient.Add("id", record.id);
            if (record.patient != null) patient.Add("patient", record.patient);
            if (record.address != null) patient.Add("address", record.address);
            if (record.currentdoctor != null) patient.Add("currentdoctor", record.currentdoctor);
            if (record.phonenumber != null) patient.Add("phonenumber", record.phonenumber);
            if (record.dateofbirth != DateTime.MinValue) patient.Add("dateofbirth", record.dateofbirth.ToUniversalTime());
            if (record.username != null) patient.Add("username", record.username);
            if (record.password != null) patient.Add("password", SHA256Hasher.ComputeHash(record.password));
            await docRef.SetAsync(patient, SetOptions.MergeAll);
            return record.id;
        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("patients").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("patients").Document(documentid);
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
        public async Task updateDocumentAsync(patients record)
        {
            QuerySnapshot qs = await this.Query("patients", new Dictionary<string, object>() { { "id", record.id } });
            DocumentSnapshot doc = qs.Documents[0];
            DocumentReference docRef = doc.Reference;
            Dictionary<string, object> patient = new Dictionary<string, object>();
            if(record.patient != null)patient.Add("patient",record.patient);
            if(record.address != null) patient.Add("address", record.address);
            if (record.currentdoctor != null) patient.Add("currentdoctor", record.currentdoctor);
            if (record.phonenumber != null) patient.Add("phonenumber", record.phonenumber);
            if (record.dateofbirth != DateTime.MinValue) patient.Add("dateofbirth", record.dateofbirth);
            if (record.username!=null) patient.Add("username", record.username);
            if (record.password != null) patient.Add("password", record.password);

            await docRef.UpdateAsync(patient);

        }
    }
}
