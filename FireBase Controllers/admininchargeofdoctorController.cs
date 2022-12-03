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
    internal class admininchargeofdoctorController:FireBaseController
    {
        public async Task addDocumentAsync(admininchargeofdoctor record)
        {
            FirestoreDb connect = createConnection();
            record.id = await createId("admins");
            DocumentReference docRef = connect.Collection("admininchargeofdoctor").Document(record.id.ToString()+record.admin+record.ToString()+"addoc");
            object[] param = new object[] { record.admin, record.doctor, record.id };
            Dictionary<string, object> admin = new Dictionary<string, object>();
            if (record.admin != null) admin.Add("admin", record.admin);
            if (record.doctor != null) admin.Add("doctor", record.doctor);
            admin.Add("id", record.id);
            await docRef.SetAsync(admin, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("admininchargerofdoctor").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("admininchargerofdoctor").Document(documentid);
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
        public async Task updateDocumentAsync(admininchargeofdoctor record)
        {
            QuerySnapshot qs = await this.Query("admininchargeofdoctors", new Dictionary<string, object>() { { "id", record.id } });
            DocumentSnapshot doc = qs.Documents[0];
            DocumentReference docRef = doc.Reference;
            Dictionary<string, object> admin = new Dictionary<string, object>();
            if(record.admin != null)admin.Add("admin",record.admin);
            if(record.doctor != null) admin.Add("doctor", record.doctor);
           
            

            await docRef.UpdateAsync(admin);

        }
    }
}
