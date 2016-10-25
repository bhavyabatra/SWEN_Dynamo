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
    public class QuestionsController : Controller
    {
        // GET: Questions
        public ActionResult ManageObjectivesAndQuestions(QuestionsModel mod)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();

            string tablename = "QuestionsDB";
            var request = new PutItemRequest
            {
                TableName = tablename,
                Item = new Dictionary<string, AttributeValue>()
      {
          { "OID", new AttributeValue { N = Convert.ToString(mod.OID) } },
          { "Objective", new AttributeValue { S = mod.Objective }},
          { "QID", new AttributeValue { N = Convert.ToString(mod.QID) }},
          { "Question", new AttributeValue { S = mod.Question }},
          { "SurveyType", new AttributeValue { S = mod.SurveyType }},
     }
            };
            client.PutItem(request);
            return View();
        }
    }
}