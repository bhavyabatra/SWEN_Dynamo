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
        public ActionResult TestView(TestModel mod, string SaveSurvey)
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>() {

                 new SelectListItem() {Value="1", Text="Strong Agree" },
                new SelectListItem() {Value="2", Text="Agree" },
                new SelectListItem() {Value="3", Text="This" },
                new SelectListItem() {Value="4", Text="That" }

            };
            List<TestModel> tm = new List<TestModel>();
            string x;
            tm.Add(new TestModel() { AnswerOptions = listSelectListItems});
           // TempData["ans"] = mod.Answer;

            //.......................
            //AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            //string tablename = "Respondent";
            //Table table1 = Table.LoadTable(client, "Respondent");


            //var item = table1.GetItem("Test Four", "444@4.com");
            //  var result = client.GetItem(request);

            //List<TestModel> tm = new List<TestModel>();
            //List<TestModel> am = new List<TestModel>();
            // Issue request
            // foreach (var kvp in itme.)
            //     {//if (item["CQ1"] != null)
            //      {
            //    List<SelectListItem> list = new List<SelectListItem>() {
            //new SelectListItem(){ Value="1", Text="ActionScript"}, };
            //    mod = new TestModel();
            //    mod.Answer = new SelectList(list, "Value", "Text");
            //mod.tm.Add(new TestModel() { test = "Hello" });
            //  return View(mod.tm);

            //    }
            //am.Add(new TestModel { Answer = "Strongly Agree" });
            //am.Add(new TestModel { Answer = "Great" });
            //am.Add(new TestModel { Answer = "Well" });
            //  }
            // ViewData["DropDownSource"] = am;
            // return View(tm);
            //TestModel citiesViewModel = new TestModel()
            //{
            //if (!string.IsNullOrWhiteSpace(SaveSurvey))
            //{
            //    //};
            //    Console.WriteLine("Surcess");
            //}
            return View(tm);
        }

        [HttpPost, ActionName("TestView")]
        public ActionResult TestViewConfirm(TestModel mod, string SaveSurvey)
        {

            List<TestModel> tm = new List<TestModel>();
            if (!string.IsNullOrWhiteSpace(SaveSurvey))
            {
                //    List<SelectListItem> listSelectListItems = new List<SelectListItem>() {

                //    new SelectListItem() {Value="1", Text="Strong Agree" },
                //    new SelectListItem() {Value="2", Text="Agree" },
                //    new SelectListItem() {Value="3", Text="This" },
                //    new SelectListItem() {Value="4", Text="That" }
                //};
                //foreach (var item in listSelectListItems )
                //{
                //    if(item.Selected == true)
                //    {

                //    }

                //}
              
                   //TempData["we"] = m.Answers;
                   // x = Convert.ToString(TempData["we"]);
             
          //    TempData["we"] = mod.Answers;

                    //if (x == "Strong Agree")
                    //{
                    //    return View("TestCheck");
                    //}

                //  Console.WriteLine(mod.Answer);
            }
            return View(tm);
        }

        public ActionResult TestCheck()
        {
            return View();
        }

    }
}

       
 
    
