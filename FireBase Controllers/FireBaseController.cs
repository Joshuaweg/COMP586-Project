using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace FirebaseConnector.Controllers
{
    public class FireBaseController
    {
        public static FirestoreDb createConnection()
        {
            string credential_path = @"..\WebApplication1\bin\Debug\net6.0\client_secret.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);

            FirestoreDb db = FirestoreDb.Create("hosman-53b85");
            return db;
        }
        public async Task<Dictionary <string,string>> getCredentials(string collect) {
            FirestoreDb db = createConnection();
            Query collection = db.Collection(collect);
            Dictionary<string, string> creds = new Dictionary<string, string>();
            QuerySnapshot collectionSnapshot = await collection.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
            {
                Dictionary<string, object> document = documentSnapshot.ToDictionary();
                creds.Add(document["username"].ToString(), document["password"].ToString());
            }
            return creds;
        }
        public async Task <List<Dictionary<string, object>>> getQuery(string collect, List<string> cols)
        {
            FirestoreDb db = createConnection();
            Query collection = db.Collection(collect);
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
            QuerySnapshot collectionSnapshot = await collection.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in collectionSnapshot.Documents)
            {
                Dictionary<string, object> record = new Dictionary<string, object>();
                Dictionary<string, object> document = documentSnapshot.ToDictionary();
                foreach (string col in cols)
                {
                    record.Add(col, document[col]);
                }
                results.Add(record);
            }
            return results;
        }
        public async Task<QuerySnapshot> Query(string collect,Dictionary<string,object> fields)
        {
            FirestoreDb db = createConnection();
            CollectionReference collectRef = db.Collection(collect);
            Query query = collectRef;
            foreach (string key in fields.Keys) {
                Console.WriteLine(key);
                Console.WriteLine(fields[key]);
                query = query.WhereEqualTo(key,fields[key]);
            } 
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
           
            return querySnapshot;
        }
        public async Task<QuerySnapshot> Query(string collect, Dictionary<string, object> fields,string sort)
        {
            FirestoreDb db = createConnection();
            CollectionReference collectRef = db.Collection(collect);
            Query query = collectRef;
            foreach (string key in fields.Keys)
            {
                Console.WriteLine(key);
                Console.WriteLine(fields[key]);
                query = query.WhereEqualTo(key, fields[key]);
            }
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            return querySnapshot;
        }
        public static async Task<int> createId(string collect) {
            int id = 0;
            FirestoreDb db = createConnection();
            CollectionReference collectRef = db.Collection(collect);
            Query query = collectRef.OrderByDescending("id");
            QuerySnapshot qs = await query.GetSnapshotAsync();
            IReadOnlyList<DocumentSnapshot>  docs = qs.Documents;
            if (docs.Count > 0)
            {
                Dictionary<string, object> doc = docs[0].ToDictionary();
                id = Convert.ToInt32(doc["id"]) + 1;
            }
            else {
                id = 1;
            }
            return id;
        
        
        
        }

    }
}
