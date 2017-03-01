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

        public string Storestring(string storethisstring)
        {
            string x;
            string y;
            x = storethisstring;
            y = x;
            return y;
        }
        public ActionResult SurveyStart(SurveyModel model)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string x = Storestring(model.SurveyID);
            string tablename = "SurveyCatalog";
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

            if(model.SurveyType.Equals("Student"))
            {
                return View("StudentSurvey" );
            }
            return View();
      
        }

        public ActionResult StudentSurvey(StudentSurveyModel mod) 
        {

            //string id = Storestring(); 
            return View();
        }
    }
}