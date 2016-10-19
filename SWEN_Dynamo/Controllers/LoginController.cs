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

namespace SWEN_Dynamo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            
           Table table = Table.LoadTable(client, "User");
            //table.GetItem(models.USID);

            //var getuser = (from s in User where s.USID == USID select s).FirstOrDefault();
                
            //    string tablename = "User";

            //var getUser = (from s in db.Logins where s.Email == WebMail select s).FirstOrDefault();
           return View("SWEVolunteer");
        }
    }
}