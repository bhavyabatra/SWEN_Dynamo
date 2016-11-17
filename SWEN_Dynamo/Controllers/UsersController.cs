using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWEN_Dynamo.App_Start;
using System.Web.Mvc;
using SWEN_Dynamo.Models;
using Amazon.DynamoDBv2;
using Amazon.KeyManagementService;
using Amazon.DynamoDBv2.Model;
using Amazon.MachineLearning.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using System.Collections.Concurrent;
using Amazon.Runtime.Internal.Transform;
using Amazon.SecurityToken;

namespace SWEN_Dynamo.Controllers
{
    public class UsersController : Controller
    {
            

        public ActionResult Create(UserModel model)
        {
            // string keyId = "arn:aws:dynamodb:us-west-2:964019329379:table/User";
            // String pass = model.Password;
            // MemoryStream plainpass = new MemoryStream();
            //// plainpass
            // EncryptionRequest req = new EncryptionRequest();          

            var keyNew = Helper.GeneratePassword(10);
            var pass = Helper.EncodePassword(model.Password, keyNew);
            model.Password = pass;
            model.Vcode = keyNew;
            model.Datecreated = System.DateTime.Now;
            model.Datemodified = System.DateTime.Now; 
            
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
          
            string tablename = "User";
                   var request = new PutItemRequest
                {
                    TableName = tablename,
                    Item = new Dictionary<string, AttributeValue>()
      {
          { "USID", new AttributeValue { N = Convert.ToString(model.USID) } },
          { "Email", new AttributeValue { S = model.Email }},
          { "Datecreated", new AttributeValue { S = Convert.ToString(model.Datecreated) }},
          { "Datemodified", new AttributeValue { S = Convert.ToString(model.Datemodified) }},
          { "FirstName", new AttributeValue { S = model.Firstname }},
          { "LastName", new AttributeValue { S = model.Lastname }},
          { "Password", new AttributeValue { S = pass }},
          { "Vcode", new AttributeValue { S = keyNew }},
          { "RID", new AttributeValue { N = Convert.ToString(model.RID) }},
          { "RA", new AttributeValue { N = Convert.ToString(model.RA) }},
          { "FA", new AttributeValue { N = Convert.ToString(model.FA) }},
          { "SA", new AttributeValue { N = Convert.ToString(model.SA) }},
          { "Region", new AttributeValue { S = model.Region }},
          { "Phone", new AttributeValue { N = Convert.ToString(model.Phone) }},
                    }
                };
                client.PutItem(request);
                return View();

            }

        // GET: Users
        static List<UserModel> people = new List<UserModel>();
        public ActionResult UsersList()
        {
            UserModel usermodelobject = new UserModel();
           // usermodelobject.USID = 0;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            // Table table = new Table("Logins");
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.GreaterThan, 0);
            //Search search = table.Scan(scanFilter);
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> {  "RID" }
            };
            Search search = table.Scan(config);
            //static List< UserIDS > = new List<>();
            //GetItemOperationConfig config = new GetItemOperationConfig()
            //{
            //    AttributesToGet = new List<string>() { "RID" },
            //};
            //Document document = table.GetItem(usermodelobject.USID, config);
            //if (document != null)
            //{
            //    Document docs = table.GetItem(usermodelobject.USID, config);
            //    {
            //        people.Add(new UserModel() { USID = usermodelobject.USID, RID = Convert.ToInt32(docs["RID"])  });
            //    }
            //}
            List<Document> documentList = new List<Document>();
            do
            {
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    people.Add(new UserModel() { USID = usermodelobject.USID, RID = Convert.ToInt32(document["RID"]) });
                }
            } while (!search.IsDone);
            return View(people);
        }
    }
}
        
        
    




