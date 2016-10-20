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

namespace SWEN_Dynamo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        //public ActionResult FirstView()
        //{
        //    return View("Index");
        //}
        public ActionResult Index(LoginModel model)
        {



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

          
            
                return View("OutreachAdmin");
            
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