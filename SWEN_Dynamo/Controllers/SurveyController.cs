using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using SWEN_Dynamo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWEN_Dynamo.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey
        public string tada;
        public void Storestring(string storethisstring)
        {
            string x = storethisstring;
            tada = x;
       }
        public ActionResult SurveyStart(SurveyModel model)
        {
           // AmazonDynamoDBClient client = new AmazonDynamoDBClient();
          
         //   string tablename = "SurveyCatalog";
            string y = Convert.ToString(model.SurveyType);
           // string z = "Null";
     //       var request = new PutItemRequest
     //       {
     //           TableName = tablename,
     //           Item = new Dictionary<string, AttributeValue>()
     // {
     //     { "SurveyID", new AttributeValue { S = model.SurveyID } },
     //     { "USID", new AttributeValue { N = Convert.ToString(model.CreatedBy) }},
     //     { "SurveyType", new AttributeValue { S = Convert.ToString(model.SurveyType) }},
     //}
               
     //       };
     //       //switch(y)
     //       //{
     //       //    case "Survey": client.PutItem(request);
     //       //        return View("StudentSurvey");
     //       //    }
     //       client.PutItem(request);
            return View(model);
      
        }
        [HttpPost, ActionName("SurveyStart")]
        public ActionResult SurveyStartConfirmed(SurveyModel model)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tablename = "SurveyCatalog";
            //string x = "Null";
            Storestring(model.SurveyID);
            TempData["ID"] = tada;
            string y = Convert.ToString(model.SurveyType);
            var request = new PutItemRequest
            {

                TableName = tablename,
                Item = new Dictionary<string, AttributeValue>()
      {
          { "SurveyID", new AttributeValue { S = model.SurveyID } },
          { "USID", new AttributeValue { N = Convert.ToString(model.CreatedBy) }},
          { "SurveyType", new AttributeValue { S = Convert.ToString(model.SurveyType) }},
     }
                     
        };
       
            client.PutItem(request);
            //if(y== "StudentSurvey")
            //}
            //{
            return RedirectToAction("StudentSurvey");
           // return View();
        }


        public ActionResult StudentSurvey(StudentSurveyModel mod)
        {
            
           
            mod.id = Convert.ToString(TempData["ID"]);
            return View(mod);
        }
    }
}