using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class doctorinchargeofpatientsController : FireBaseController
    {
        public async Task addDocumentAsync(doctorinchargeofpatients record)
        {
            FirestoreDb connect = createConnection();
            record.id = await createId("doctorinchargeofpatient");
            DocumentReference docRef = connect.Collection("doctorinchargeofpatient").Document(record.id.ToString()+record.doctor+record.patient+"docpat");
            object[] param = new object[] { record.patient, record.doctor, record.id };
            Dictionary<string, object> admin = new Dictionary<string, object>();
            if (record.id != null) admin.Add("id", record.id);
            admin.Add("patient_id", record.patient_id);
            admin.Add("doctor_id", record.doctor_id);
            if (record.doctor != null) admin.Add("doctor", record.doctor);
            if (record.patient != null) admin.Add("patient", record.patient);
            await docRef.SetAsync(admin, SetOptions.MergeAll);
            
        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("doctorinchargeofpatients").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("doctorinchargeofpatients").Document(documentid);
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
        public async Task updateDocumentAsync(doctorinchargeofpatients record)
        {
            QuerySnapshot qs = await this.Query("patients", new Dictionary<string, object>() { { "id", record.id } });
            DocumentSnapshot doc = qs.Documents[0];
            DocumentReference docRef = doc.Reference;
            Dictionary<string, object> admin = new Dictionary<string, object>();
            if (record.doctor != null) admin.Add("doctor", record.doctor);
            if (record.patient != null) admin.Add("patient", record.patient);

            await docRef.UpdateAsync(admin);

        }
    }
}
