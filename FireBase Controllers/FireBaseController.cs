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

    }
}
