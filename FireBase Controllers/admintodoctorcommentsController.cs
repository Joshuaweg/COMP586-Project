using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class admintodoctorcommentsController:FireBaseController
    {
        public async Task addDocumentAsync(admintodoctorcomments record)
        {
            FirestoreDb connect = createConnection();
            record.id = await createId("admintodoctorcomments");
            DocumentReference docRef = connect.Collection("admintodoctorcomments").Document(record.id.ToString()+record.admin+record.doctor+"addoccomm");
            object[] param = new object[] { record.id, record.admin, record.doctor, record.message };
            Dictionary<string, object> admin = new Dictionary<string, object>();
            admin.Add("id", record.id);
            if (record.admin != null) admin.Add("admin", record.admin);
            if (record.doctor != null) admin.Add("doctor", record.doctor);
            if (record.message != null) admin.Add("message", record.message);
            await docRef.SetAsync(admin, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("admintodoctorcomments").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("admintodoctorcomments").Document(documentid);
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
        public async Task updateDocumentAsync( admintodoctorcomments record)
        {
            QuerySnapshot qs = await this.Query("admintodoctorcomments", new Dictionary<string, object>() { { "id", record.id } });
            DocumentSnapshot doc = qs.Documents[0];
            DocumentReference docRef = doc.Reference;
            Dictionary<string, object> admin = new Dictionary<string, object>();
            if (record.admin != null) admin.Add("admin", record.admin);
            if (record.doctor != null) admin.Add("doctor", record.doctor);
            if (record.message != null) admin.Add("message", record.message);


            await docRef.UpdateAsync(admin);

        }
    }
}
