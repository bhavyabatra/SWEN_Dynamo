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

        public ActionResult TestCheck (StudentSurveyModel m)
        {
            return View(m);

        }
        //        //public ActionResult TestView(TestModel x)
        //        //{

        //        //    x.AnswerOptions = new List<SelectListItem>
        //        //        {
        //        //            new SelectListItem() { Value = "Ok", Text = "Ok" },
        //        //        new SelectListItem() { Value = "Cool", Text = "Cool" },
        //        //        new SelectListItem() { Value = "This", Text = "This" },
        //        //        new SelectListItem() { Value = "That", Text = "That" }

        //        //    };
        //        //    AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        //        //    string tablename = "Respondent";
        //        //    Table table1 = Table.LoadTable(client, "Respondent");


        //        //    var item = table1.GetItem("Test Four", "444@44.com");
        //        //    TempData["Item"] = item;

        //        //    foreach (var inserttoqlist in item)
        //        //    {
        //        //        if (inserttoqlist.Value != null && !inserttoqlist.Key.Equals("SurveyID") && !inserttoqlist.Key.Equals("ResponseToken") && !inserttoqlist.Key.EndsWith("A") && !inserttoqlist.Key.StartsWith("C"))
        //        //        {
        //        //            x.question.Add(inserttoqlist.Value);
        //        //        }
        //        //        if (inserttoqlist.Value != null && !inserttoqlist.Key.Equals("SurveyID") && !inserttoqlist.Key.Equals("ResponseToken") && !inserttoqlist.Key.EndsWith("A") && inserttoqlist.Key.StartsWith("C"))
        //        //        {
        //        //            x.customquestion.Add(inserttoqlist.Value);
        //        //        }

        //        //    }

        //        //    TempData["GrabSimpleQuestionList"] = x.question;
        //        //    TempData["GrabCustomQuestionList"] = x.customquestion;
        //        //    return View(x);
        //        //}


        //     //   [HttpPost, ActionName("TestView")]
        //        //public ActionResult TestViewConfirm(TestModel mod, string SaveSurvey)
        //        //{
        //        //    AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        //        //    Document it = (Document)TempData["Item"];

        //        //    string tablename = "Respondent";
        //        //    if (!string.IsNullOrWhiteSpace(SaveSurvey))

        //        //    {
        //        //        mod.AnswerOptions = new List<SelectListItem>
        //        //        {
        //        //        new SelectListItem() { Value = "Ok", Text = "Ok" },
        //        //        new SelectListItem() { Value = "Cool", Text = "Cool" },
        //        //        new SelectListItem() { Value = "This", Text = "This" },
        //        //        new SelectListItem() { Value = "That", Text = "That" }

        //        //    };


        //        //        mod.question = (List<string>)TempData["GrabSimpleQuestionList"];
        //        //        mod.customquestion = (List<string>)TempData["GrabCustomQuestionList"];

        //        //        //    foreach (var item in mod.question)
        //        //        for (int i = 0; i < mod.question.Count(); i++)
        //        //        {
        //        //            mod.NormalQuestionsObject.Add(new NormalQuestions { qs = mod.question[i], ans = mod.answer[i] });

        //        //        }
        //        //        foreach (var itts in it)
        //        //        {
        //        //            foreach (var itt in mod.NormalQuestionsObject)
        //        //            {
        //        //                if (itts.Value == itt.qs)
        //        //                {
        //        //                    SWEN_DynamoUtilityClass.ParticipantUpdateRespondent("Test Four", "444@44.com", itts.Key, itt.ans);
        //        //                    var x = itts.Key;
        //        //                }
        //        //            }
        //        //        }
        //        //        for (int i = 0; i < mod.customquestion.Count(); i++)
        //        //        {
        //        //            mod.CustomQuestionsClassObject.Add(new CustomQuestions { cques = mod.customquestion[i], canswer = mod.customanswer[i] });

        //        //        }
        //        //        foreach (var itts in it)
        //        //        {
        //        //            foreach (var CustomItem in mod.CustomQuestionsClassObject)
        //        //            {
        //        //                if (itts.Value == CustomItem.cques)
        //        //                {
        //        //                    SWEN_DynamoUtilityClass.ParticipantUpdateRespondent("Test Four", "444@44.com", itts.Key, CustomItem.canswer);
        //        //                    var x = itts.Key;
        //        //                }
        //        //            }
        //        //        }



        //        //    }

        //        //    return View(mod);

        //        //}

    }
}






