using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEN_Dynamo.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using SWEN_Dynamo.App_Start;

namespace SWEN_Dynamo.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public string n { get; set; }
        public ActionResult TestView(TestModel x)
        {

            x.AnswerOptions = new List<SelectListItem>
                {
                    new SelectListItem() { Value = "Ok", Text = "Ok" },
                new SelectListItem() { Value = "Cool", Text = "Cool" },
                new SelectListItem() { Value = "This", Text = "This" },
                new SelectListItem() { Value = "That", Text = "That" }

            };
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tablename = "Respondent";
            Table table1 = Table.LoadTable(client, "Respondent");


            var item = table1.GetItem("Test Five", "4@44444.com");
            TempData["Item"] = item;

            foreach(var inserttoqlist in item)
            {
                if(inserttoqlist.Value != null && !inserttoqlist.Key.Equals("SurveyID") && !inserttoqlist.Key.Equals("ResponseToken") && !inserttoqlist.Key.EndsWith("A"))
                {
                    x.question.Add(inserttoqlist.Value);
                }
            }
            //if (item.Contains("O1_Q1"))
            //{
            //    x.question.Add(item["O1_Q1"]);
            //    //  x.ca.Add(new catchall() { qs = item["O1_Q1"] });

            //}
            //if (item.Contains("O1_Q2"))
            //{
            //    x.question.Add(item["O1_Q2"]);
            //    //  x.ca.Add(new catchall() { qs = item["O1_Q2"] });


            //}
            TempData["td"] = x.question;
            return View(x);
        }


        [HttpPost, ActionName("TestView")]
        public ActionResult TestViewConfirm(TestModel mod, string SaveSurvey)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Document it = (Document)TempData["Item"];

            string tablename = "Respondent";
            if (!string.IsNullOrWhiteSpace(SaveSurvey))

            {
                mod.AnswerOptions = new List<SelectListItem>
                {
                new SelectListItem() { Value = "Ok", Text = "Ok" },
                new SelectListItem() { Value = "Cool", Text = "Cool" },
                new SelectListItem() { Value = "This", Text = "This" },
                new SelectListItem() { Value = "That", Text = "That" }

            };


                mod.question = (List<string>)TempData["td"];

                //    foreach (var item in mod.question)
                for (int i = 0; i < mod.question.Count(); i++)
                {
                    mod.ca.Add(new catchall { qs = mod.question[i], ans = mod.answer[i] });

                }
                foreach (var itts in it)
                {
                    foreach (var itt in mod.ca)
                    {
                        if (itts.Value == itt.qs)
                        {
                            SWEN_DynamoUtilityClass.ParticipantUpdateRespondent("Test Five", "4@44444.com", itts.Key, itt.ans);
                            var x = itts.Key;
                        }
                    }
                }

               

          
            }

            return View(mod);

        }
        //public ActionResult TestCheck()
        //{
        //    return View();
        //}

    }
}
    


       
 
    
