using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using SWEN_Dynamo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using SWEN_Dynamo.App_Start;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;

namespace SWEN_Dynamo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //public ActionResult FirstView()
        //{
        //    return View("Index");
        //}
        //[HttpPost]
        //[AllowAnonymous]
        public ActionResult Index(LoginModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
          // UserModel mod = new UserModel();
           AmazonDynamoDBClient client = new AmazonDynamoDBClient();
           // Table table = new Table("Logins");
            Table table = Table.LoadTable(client, "Logins");
           

            GetItemOperationConfig config = new GetItemOperationConfig()
            {
                AttributesToGet = new List<string>() { "Password" },
            };

            //var value = config[Attribute];
            Document document = table.GetItem(model.Email, config); //Document document = table.GetItem(model.USID, model.Email, config);
            string zz = model.Password;                                                        //string yy = document["Email"];
                                                                    // string xyz = document["Password"];

            if (document != null)
            {
                Document docs = table.GetItem(model.Email, config);
                string yy = docs["Password"];
                if (yy == zz )
                {
                    return View("OutreachAdmin");
                }
            }
            //string Checkpassword = model.Password;
            //foreach (var attribute in document.GetAttributeNames())
            //{
            //    string stringValue = null;
            //    var value = document[attribute];
            //    if (value == "Password")
            //        stringValue = value.AsPrimitive().Value.ToString();
            //            if(stringValue == model.Password)
            //    {
            //        return View("OutreachAdmin");
            //    }
            //}
            //    if (document != null)

            //{
            //    //Table tab = Table.LoadTable(client, "User");
            //    //ScanFilter sc = new ScanFilter();
            //    //sc.AddCondition("RID", ScanOperator.Equal, "2");
            //    //Search se = tab.Scan(sc);
            //    //List<Document> docl = new List<Document>();
            //    //docl = se.GetNextSet();
            //    //if (docl != null)
            //    //GetItemOperationConfig config = new GetItemOperationConfig()
            //    //{
            //    //    AttributesToGet = new List<string>() { model.Password },
            //    //};
            //    {
            //        //  table.GetItem(model.USID, model.Email, config);
            //        return View("OutreachAdmin");
            //    }
            //}
                //DynamoDBContext context = new DynamoDBContext(client);


                //string Id = Convert.ToString(model.USID); //Partition key 
                //string mail = Co
                //DateTime twoWeeksAgoDate = DateTime.UtcNow.Subtract(new TimeSpan(14, 0, 0, 0)); // Date to compare.
                //IEnumerable<"Login"> latestReplies = context.Query<Login>(Id, QueryOperator.GreaterThan, twoWeeksAgoDate);
                //Table table = Table.LoadTable(client, "Login");
                // string xyz = table.GetItem(model.USID);
                // string yzx = table.GetItem(model.Email);
                // if (xyz != null && yzx != null)

                // {
                //     return View("OutreachAdmin");
                // }
                //UserModel models = new UserModel();
                //AmazonDynamoDBClient client = new AmazonDynamoDBClient();
                //string tableName = "User";
                //model.USID = models.USID;
                //var request = new GetItemRequest
                //{
                //    TableName = tableName,
                //    Key = new Dictionary<string, AttributeValue>() { { "USID", new AttributeValue { N = Convert.ToString(models.USID) } } },
                //};
                //Table table = Table.LoadTable(client, "User");
                //var res = client.GetItem(request);

                //// Check the response.
                //var result = Convert.ToBoolean(res.GetItemResult);

                //switch(result)
                //{
                //case (result != null) : 
                return View();



            //}


            //table.GetItem(models.USID);

            //if (result != null)
            //{
            //    //var getuser = (from s in User where s.USID == USID select s).FirstOrDefault();

            //    //    string tablename = "User";

            //    //var getUser = (from s in db.Logins where s.Email == WebMail select s).FirstOrDefault();
            //    //      string tablename = "User";
            //    //      var request = new PutItemRequest
            //    //      {
            //    //          TableName = tablename,
            //    //          Item = new Dictionary<string, AttributeValue>()
            //    //{
            //    //    { "USID", new AttributeValue { N = Convert.ToString(models.USID) } },
            //    //    { "Email", new AttributeValue { S = models.Email }},
            //    //       }
            //    //      };
            //    //      client.PutItem(request);
            //    return View("SWEVolunteer");





            //}
            //else 
            //{
            //        return View("Lockout");
            //    }



        }

        public ActionResult SWEVolunteer()
        {
            return View("Lockout");
        }
    }
}