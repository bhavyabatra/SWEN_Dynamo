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
        public string n { get; set; }
        public ActionResult TestView(TestModel m)
        {
            m.q.Add(new ques() { quest = "wewewee"});

            //m.q.Add(new ques() { quest = "23424" });

            //m.q.Add(new ques() { quest = "qqeqq" });

            //m.q.Add(new ques() { quest = "sdfwre" });

           // List<TestModel> ml = new List<TestModel>();
           //ml.Add(new TestModel { questions = "hi"} );
           // ml.Add(new TestModel { questions = "hello" });
           // ml.Add(new TestModel { questions = "up" });
           // ml.Add(new TestModel { questions = "good" });
           // ml.Add(new TestModel { AnswerOptions = m.AnswerOptions  });
           // ml.Add(new TestModel { answer = m.answer });
            //ml.Add (new TestModel { AnswerOptions = "Yes" });
            // ml.Add(new TestModel { answeroptions = "No" });
            // ml.Add(new TestModel { answeroptions = "mayBe" });
            // ml.Add(new TestModel { answeroptions = "Whatever" });
            //m.tm = new List<TestModel>();
            //m.tm.Add(new TestModel {ans = "Hi" });
            //m.tm.Add(new TestModel { ans = "Hi" });
            //m.tm.Add(new TestModel { ans = "Hi" });
            //m.tm.Add(new TestModel { ans = "Hi" });
            //m.tm.Add(new TestModel { ans = "Hi" });



            //.......................................................................
            //m = new TestModel();
            //m.tq.Add(new Test { testquestions = "Hi"});
            //m.tq.Add(new Test { testquestions = "Hello" });
            //m.tq.Add(new Test { testquestions = "Wassup" });
            //m.tq.Add(new Test { testquestions = "Ok" });

            //m.test = new List<Test>();
            //List Ft = new List<Ft>();
            //AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            //string tablename = "Respondent";
            //Table table1 = Table.LoadTable(client, "Respondent");


            //var item = table1.GetItem("Test Four", "444@4.com");
            //  var result = client.GetItem(request);
            //var model = new List<TestModel>();

            m.AnswerOptions = new List<SelectListItem>
            {
                new SelectListItem() {Value="Strong Agree", Text="Strong Agree" },
                new SelectListItem() {Value="Agree", Text="Agree" },
                new SelectListItem() {Value="This", Text="This" },
                new SelectListItem() {Value="That", Text="That" }

            };
            //model.Add(new TestModel(m.AnswerOptions=model.AnswerOptions));
            //   model.Add(new TestModel(model.AnswerOptions));

            // model.AnswerOptions = AnswerOptions;

            //    List<TestModel> tm = new List<TestModel>();

            //tm.Add(new TestModel() { AnswerOptions = model.AnswerOptions});

            //   TempData["XYZ"] = model.Answers;

            // TempData["ans"] = mod.Answer;

            //.......................


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
            // return (model);
            //......................................................................
            return View(m);
        }

        [HttpPost, ActionName("TestView")]
        public ActionResult TestViewConfirm(TestModel mod, string SaveSurvey)
        {
            //List<TestModel> ml = new List<TestModel>();
            //ml.Add(new TestModel { questions = "hi" });
            //ml.Add(new TestModel { questions = "hello" });
            //ml.Add(new TestModel { questions = "up" });
            //ml.Add(new TestModel { questions = "good" });
            //ml.Add(new TestModel { AnswerOptions = m.AnswerOptions });
            //ml.Add(new TestModel { answer = m.answer });

            //List<TestModel> tm = new List<TestModel>();
            if (!string.IsNullOrWhiteSpace(SaveSurvey))
            {
                //    var model = new TestModel();
                //    model.AnswerOptions = new List<SelectListItem>
                //{
                //    new SelectListItem() {Value="Strong Agree", Text="Strong Agree"  },
                //    new SelectListItem() {Value="Agree", Text="Agree" },
                //    new SelectListItem() {Value="This", Text="This" },
                //    new SelectListItem() {Value="That", Text="That" }

                //};

                var ma = mod.answer;
            // List<TestModel> tmod = new List<TestModel>();

            //tm.Add(new TestModel() { Answers = model.Answers });
            string n = mod.answer;
            //List<SelectListItem> listSelectListItems = new List<SelectListItem>() {

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

            // TempData["we"] = m.Answers;
            // x = Convert.ToString(TempData["we"]);

            //    TempData["we"] = mod.Answers;

            //if (x == "Strong Agree")
            //{
            //    return View("TestCheck");
            }

            //  Console.WriteLine(mod.Answer);
            return View(mod);
        }
       
        

        //public ActionResult TestCheck()
        //{
        //    return View();
        //}

    }
}

       
 
    
