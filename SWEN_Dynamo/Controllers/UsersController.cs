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
        public ActionResult UsersList(UserModel usermodelobject)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.Clear();
            //}
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.GreaterThan, 0);
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "USID", "RID", "FirstName", "LastName", "Email", "RA", "SA", "FA", "Phone", "Datecreated", "Datemodified", "Region" }
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();

            do
            {
                people.Clear();
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {

                    people.Add(new UserModel()
                    {
                        USID = Convert.ToInt32(document["USID"]),
                        RID = Convert.ToInt32(document["RID"]),
                        Email = document["Email"],
                        Datecreated = Convert.ToDateTime(document["Datecreated"]),
                        Datemodified = Convert.ToDateTime(document["Datemodified"]),
                        Firstname = document["FirstName"],
                        Lastname = document["LastName"],
                        Phone = document["Phone"],
                        Region = document["Region"],
                        RA = Convert.ToInt32(document["RA"]),
                        FA = Convert.ToInt32(document["FA"]),
                        SA = Convert.ToInt32(document["SA"])
                    });

                }
            } while (!search.IsDone);

            return View(people);

        }

        public ActionResult Edit(int? id, UserModel mod)
        {
            if (ModelState.IsValid)
            {
                AmazonDynamoDBClient client = new AmazonDynamoDBClient();
                Table table = Table.LoadTable(client, "User");
                ScanFilter scanFilter = new ScanFilter();
                string tablename = "User";
                mod.Datemodified = System.DateTime.Now;
                var key = Helper.GeneratePassword(10);
                var passw = Helper.EncodePassword(mod.Password, key);
                mod.Password = passw;
                mod.Vcode = key;
                var request = new UpdateItemRequest
                {
                    TableName = tablename,
                    Key = new Dictionary<string, AttributeValue>() { { "USID", new AttributeValue { N = Convert.ToString(id) } }
                     },
                    ExpressionAttributeNames = new Dictionary<string, string>()
    {
        {"#FN", "FirstName"}, { "#LN","LastName"}, {"#EM", "Email"}, { "#RI","RID"}, {"#RA", "RA"}, { "#FA","FA"}, {"#SA", "SA"}, { "#RE","Region"},{"#PH", "Phone"}, {"#DM","Datemodified" }, {"#PA","Password" }, {"#VC", "Vcode" }

    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
    {
        {":FN",new AttributeValue { S = mod.Firstname}},
        { ":LN",new AttributeValue {S=mod.Lastname } },
        {":EM",new AttributeValue { S = mod.Email}},
        { ":PH",new AttributeValue {N = Convert.ToString(mod.Phone) } },
        {":RE",new AttributeValue { S = mod.Region}},
        { ":RI",new AttributeValue {N = Convert.ToString(mod.RID) } },
          {":FA",new AttributeValue { N = Convert.ToString(mod.FA)}},
        { ":SA",new AttributeValue {N = Convert.ToString(mod.SA) } },
        {":RA",new AttributeValue { N = Convert.ToString( mod.RA)}},
         {":DM",new AttributeValue { S = Convert.ToString(mod.Datemodified)}},
         {":PA", new AttributeValue { S = passw} },
         {":VC", new AttributeValue { S = key} },

         },
                    UpdateExpression = "SET #FN = :FN, #LN = :LN, #RA = :RA, #PH = :PH, #RE = :RE, #SA = :SA, #FA = :FA, #RI = :RI,#EM = :EM,#DM = :DM, #PA = :PA, #VC = :VC"
                };


                scanFilter.AddCondition("USID", ScanOperator.Equal, id);
                ScanOperationConfig config = new ScanOperationConfig()
                {
                    Filter = scanFilter,
                    Select = SelectValues.AllAttributes,
                    //AttributesToGet = new List<string> { "SuriveyID", "Email", "Objective1", "Question1" }
                };
                Search search = table.Scan(config);
                List<Document> documentList = new List<Document>();
                do
                {
                    //    // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives

                    documentList = search.GetNextSet();
                    if (documentList != null)
                    {
                        foreach (var document in documentList)
                        {

                            mod.USID = Convert.ToInt32(document["USID"]);
                            mod.Firstname = document["FirstName"];
                            mod.Lastname = document["LastName"];
                            mod.Email = document["Email"];
                            mod.Datemodified = Convert.ToDateTime(document["Datemodified"]);
                            mod.Datecreated = Convert.ToDateTime(document["Datecreated"]);
                            mod.Region = document["Region"];
                            mod.RID = Convert.ToInt32(document["RID"]);
                            mod.FA = Convert.ToInt32(document["FA"]);
                            mod.SA = Convert.ToInt32(document["SA"]);
                            mod.RA = Convert.ToInt32(document["RA"]);
                            mod.Phone = document["Phone"];
                            mod.Password = document["Password"];

                            var res = client.UpdateItem(request);
                            if (res != null)
                            {
                                client.UpdateItem(request);
                            }

                        }
                    }

                } while (!search.IsDone);

            }
            return View(mod);
        }


        public ActionResult Details(int? id)
        {
            UserModel det = new UserModel();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.Equal, id);
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();
            do
            {
                // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    det.USID = Convert.ToInt32(document["USID"]);
                    det.Firstname = document["FirstName"];
                    det.Lastname = document["LastName"];
                    det.Email = document["Email"];
                    det.Region = document["Region"];
                    det.RID = Convert.ToInt32(document["RID"]);
                    det.FA = Convert.ToInt32(document["FA"]);
                    det.SA = Convert.ToInt32(document["SA"]);
                    det.RA = Convert.ToInt32(document["RA"]);
                    det.Phone = document["Phone"];
                    det.Datemodified = Convert.ToDateTime(document["Datemodified"]);
                    det.Datecreated = Convert.ToDateTime(document["Datecreated"]);
                }
            } while (!search.IsDone);
            return View(det);
        }


        public ActionResult Delete(int id, UserModel det, string action)
        {
            //      UserModel del = new UserModel();
        //    UserModel det = new UserModel();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.Equal, id);
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();
            do
            {
                // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    det.USID = Convert.ToInt32(document["USID"]);
                    det.Firstname = document["FirstName"];
                    det.Lastname = document["LastName"];
                    det.Email = document["Email"];
                    det.Region = document["Region"];
                    det.RID = Convert.ToInt32(document["RID"]);
                    det.FA = Convert.ToInt32(document["FA"]);
                    det.SA = Convert.ToInt32(document["SA"]);
                    det.RA = Convert.ToInt32(document["RA"]);
                    det.Phone = document["Phone"];
                    det.Datemodified = Convert.ToDateTime(document["Datemodified"]);
                    det.Datecreated = Convert.ToDateTime(document["Datecreated"]);
                }
            } while (!search.IsDone);
            

            if (action == "Back to List")
            {
                RedirectToAction("UsersList");
            }

            return View(det);

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleteconfirmed(int id, UserModel del, string action)

        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tableName = "User";
            //if (ModelState.IsValid && HtmlHelper.Equals)

            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "USID", new AttributeValue { N = Convert.ToString(id) } } },
            };
            var response = client.DeleteItem(request);
            return View(del);
        }
    }
}










