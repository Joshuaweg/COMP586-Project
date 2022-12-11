using FirebaseConnector.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseConnector.Controllers
{
    internal class ordersuppliesController:FireBaseController
    {
        public async Task addDocumentAsync(ordersupplies record, string doc)
        {
            FirestoreDb connect = createConnection();
            record.id = await createId("ordersupplies");
            DocumentReference docRef = connect.Collection("ordersupplies").Document(record.id.ToString()+record.name+record.amount.ToString()+"supplies");
            object[] param = new object[] { record.name, record.amount, record.id };
            Dictionary<string, object> order = new Dictionary<string, object>();
            if (record.id != null) order.Add("id", record.id);
            if (record.name != null) order.Add("name", record.name);
            if (record.amount != null) order.Add("amount", record.amount);
            await docRef.SetAsync(order, SetOptions.MergeAll);

        }
        public async Task deleteDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference cityRef = connect.Collection("ordersupplies").Document(documentid);
            await cityRef.DeleteAsync();
        }
        public async Task retrieveDocumentAsync(string documentid)
        {
            FirestoreDb connect = createConnection();
            DocumentReference docRef = connect.Collection("ordersupplies").Document(documentid);
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
        public async Task updateDocumentAsync(ordersupplies record)
        {
            QuerySnapshot qs = await this.Query("ordersupplies", new Dictionary<string, object>() { { "id", record.id } });
            DocumentSnapshot doc = qs.Documents[0];
            DocumentReference docRef = doc.Reference;
            Dictionary<string, object> order = new Dictionary<string, object>();
            if (record.name != null) order.Add("name", record.name);
            if (record.amount != 0) order.Add("amount", record.amount);
            

            await docRef.UpdateAsync(order);

        }
    }
}
