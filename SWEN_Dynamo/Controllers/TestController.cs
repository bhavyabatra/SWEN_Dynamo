using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEN_Dynamo.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace SWEN_Dynamo.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult TestView()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tablename = "Respondent";
            Table table1 = Table.LoadTable(client, "Respondent");
            TestModel mod = new TestModel();
            var item = table1.GetItem("Test Four", "444@4.com");
            //  var result = client.GetItem(request);
            
            List<TestModel> tm = new List<TestModel>();
              // Issue request
             // foreach (var kvp in itme.)
            {
               
                tm.Add(new TestModel { test = "Hello" });
                tm.Add(new TestModel { test = "Hi" });
                tm.Add(new TestModel { test = "Great" });
                tm.Add(new TestModel { test = "Well" });
            }
            return View(tm);
        }
    }
}