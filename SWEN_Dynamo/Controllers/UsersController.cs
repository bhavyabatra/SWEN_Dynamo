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
            UserModel xx = new UserModel();
            xx.USID = 155;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            // Table table = new Table("Logins");
            Table table = Table.LoadTable(client, "User");
            GetItemOperationConfig config = new GetItemOperationConfig()
            {
                AttributesToGet = new List<string>() { "RID" },
            };
            Document document = table.GetItem(xx.USID, config);
            if (document != null)
            {
                Document docs = table.GetItem(xx.USID, config);
                {
                    people.Add(new UserModel() { USID = xx.USID, RID = Convert.ToInt32(docs["RID"]) });
                }
            }
            return View(people);
        }
    }
}
        
        
    




