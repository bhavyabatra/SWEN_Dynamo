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
            Table table = Table.LoadTable(client, "User");
            ScanFilter firstscanFilter = new ScanFilter();
            ScanFilter secondscanFilter = new ScanFilter();
            firstscanFilter.AddCondition("USID", ScanOperator.Equal, model.CheckWithUSIDandEmail);
            secondscanFilter.AddCondition("Email", ScanOperator.Equal, model.CheckWithUSIDandEmail);
            ScanOperationConfig firstconfig = new ScanOperationConfig()
            {
                Filter = firstscanFilter,
                Select = SelectValues.AllAttributes,
            };
            ScanOperationConfig secondconfig = new ScanOperationConfig()
            {
                Filter = secondscanFilter,
                Select = SelectValues.AllAttributes,
            };
            Search firstsearch = table.Scan(firstconfig);
            Search secondsearch = table.Scan(secondconfig);
            if(firstsearch.  !secondsearch.Equals(null))
            {
                var x = "First Search Failed";
                var y = "Second Search Succeed";
            }
            if (!firstsearch.Equals(null) && secondsearch.Equals(null))
            {
                var x = "Second Search Failed";
                var y = "First Search Succeed";
            }
            GetItemOperationConfig config = new GetItemOperationConfig()
            {
                AttributesToGet = new List<string>() { "Password", "USID", "RID", "Vcode", "Phone" },
            };
            //string             
            //Document document = table.GetItem(model.CheckWithUSIDandEmail, config); 

            //if (document != null)
            //{
            //    Document docs = table.GetItem(model.Email, config);
            //    string PasswordFromDynamoDB = docs["Password"];
            //    string UserRID = docs["RID"];
            //    string VcodeKey = docs["Vcode"];
            //     var UserPasswordEncoded = Helper.EncodePassword(model.Password, VcodeKey);
            //     if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID=="1")
            //    {
            //        return View("OutreachAdmin");
            //    }
            //     else if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID == "2")
            //    {
            //        return View("NationalLevelMember");
            //    }
            //    else if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID == "3")
            //    {
            //        return View("RegionalLevelMember");
            //    }
            //    else if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID == "4")
            //    {
            //        return View("ChapterLevelMember");
            //    }
            //    else if (PasswordFromDynamoDB == UserPasswordEncoded && UserRID == "5")
            //    {
            //        return View("SWEVolunteer");
            //    }
            //}

            return View();


        }

        public ActionResult SWEVolunteer()
        {
            return View("Lockout");
        }
    }
}