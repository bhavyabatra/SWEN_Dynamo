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
    public class ResponseController : Controller
    {
        // GET: Survey


       // static List<ResponseModel> s = new List<ResponseModel>();
     
        public ActionResult ResponseView(ResponseModel sc)
        {

           // ResponseModel sc = new ResponseModel();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "SurveyCatalog");
            //Table tab = Table.LoadTable(client, "SurveyCatalog");
            ScanFilter scanFilter = new ScanFilter();
            //Copy action will copy survey from Survey catalog to Response Table and key response token which will be unique. and surveyID and Email ID will be maintained corresponding to that
            
            scanFilter.AddCondition("SuriveyID", ScanOperator.Equal, "99");//Here we can write code to fetch the survey id

            //Create a scan query to check number of objectives and questions 
            // Create a function in utility class for returning objectives and questions strings and return strings for . 
            // Maintain a list of response token in json or somewhere to block re launching of surveys. SO Befor launching survey is not listed then no query. 
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,
                //AttributesToGet = new List<string> { "SuriveyID", "Email", "Objective1", "Question1" }
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();
            do
            {
                // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    sc.SurveyID = Convert.ToInt32(document["SuriveyID"]);
                    sc.Email = document["Email"];
                    sc.O1 = document["O1"];
                    sc.O2 = document["O2"];
                   // sc.O3 = document["O3"];
                    
                    //s.Add(new ResponseModel() { SurveyID = Convert.ToInt32(document["SuriveyID"]), O1 = document["Objective1"], Email = document["Email"], Q1 = document["Question1"], O1_Q1_A = document["AIDVal"] });
                }
            } while (!search.IsDone);
            string tablename = "SurveyCatalog";
            var ans = sc.O1_Q1_A;
            var request = new UpdateItemRequest
            {
                TableName = tablename,
                Key = new Dictionary<string, AttributeValue>() { { "SuriveyID", new AttributeValue { S = "99" } },
                    { "Email", new AttributeValue { S = "bhavyabatra88@gmail.com" } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
    {
        {"#A", "AIDVal1"},

    },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
    {
        {":AIDV",new AttributeValue { S = ans}},
         },
                UpdateExpression = "SET #A = :AIDV"
            };
         var res = client.UpdateItem(request);
            if (res != null)
            {
                client.UpdateItem(request);
            }
            return View(sc);
        }
    }
} 