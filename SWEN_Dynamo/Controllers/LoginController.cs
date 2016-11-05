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
       
        public ActionResult Index(LoginModel model)
        {
         
           
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "Logins");
            GetItemOperationConfig config = new GetItemOperationConfig()
            {
                AttributesToGet = new List<string>() { "Password","USID","RID","Vcode","Phone" },
            };

            
            Document document = table.GetItem(model.Email, config); 
         
            if (document != null)
            {
                Document docs = table.GetItem(model.Email, config);
                string PasswordFromDynamoDB = docs["Password"];
                string UserRID = docs["RID"];
                string VcodeKey = docs["Vcode"];
                 var UserPasswordEncoded = Helper.EncodePassword(model.Password, VcodeKey);
                 if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID=="1")
                {
                    return View("OutreachAdmin");
                }
                 else if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID == "2")
                {
                    return View("NationalLevelMember");
                }
                else if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID == "3")
                {
                    return View("RegionalLevelMember");
                }
                else if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID == "4")
                {
                    return View("ChapterLevelMember");
                }
                else if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID == "5")
                {
                    return View("SWEVolunteer");
                }
            }
          
                return View();


        }

        public ActionResult SWEVolunteer()
        {
            return View("Lockout");
        }
    }
}