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
            //Storestring(model.SurveyID);
            TempData["ID"] = model.SurveyID;
            TempData["Type"] = model.SurveyType;
            string str = Convert.ToString(model.SurveyType);
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
            if (str == "Student")
            {
                return RedirectToAction("StudentSurvey");
            }
            else if (str == "Parent")
            {
                return RedirectToAction("ParentSurvey");
            }
            else
            {
                return View();
            }
        }


        public ActionResult StudentSurvey(StudentSurveyModel mod)
        {
            
            mod.SurveyID = Convert.ToString(TempData["ID"]);
            mod.SurveyofType = Convert.ToString(TempData["Type"]); 
            return View(mod);
        }

        public ActionResult ParentSurvey(ParentSurveyModel mod)
        {

            mod.SurveyID = Convert.ToString(TempData["ID"]);
            mod.SurveyofType = Convert.ToString(TempData["Type"]);
            return View(mod);
        }
    }
}