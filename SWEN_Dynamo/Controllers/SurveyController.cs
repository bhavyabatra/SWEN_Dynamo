using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using SWEN_Dynamo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEN_Dynamo.App_Start;

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
            TempData["EventName"] = model.EventName;
            string str = Convert.ToString(model.SurveyType);
            var request = new PutItemRequest
            {

                TableName = tablename,
                Item = new Dictionary<string, AttributeValue>()
      {
          { "SurveyID", new AttributeValue { S = model.SurveyID } },
          { "EventName", new AttributeValue { S = model.EventName } },
          { "USID", new AttributeValue { N = Convert.ToString(model.CreatedBy) }},
          { "SurveyType", new AttributeValue { S = Convert.ToString(model.SurveyType) }},
                    {"O1", new AttributeValue { S = "false" } },
                    {"O2", new AttributeValue { S = "false" } },
                    {"O3", new AttributeValue { S = "false" } }
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
          
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
             Table table1= Table.LoadTable(client, "SurveyCatalog");
            //Table table2 = Table.LoadTable(client, "Objectives");
            // Table table3 = Table.LoadTable(client, "Questions");

            mod.SurveyID = Convert.ToString(TempData["ID"]);
            mod.SurveyofType = Convert.ToString(TempData["Type"]);
            mod.EventName = Convert.ToString(TempData["EventName"]);
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("SurveyID", ScanOperator.Equal, mod.SurveyID);
            //scanFilter.AddCondition("SurveyType", ScanOperator.Equal,mod.SurveyofType);
            //scanFilter.AddCondition("EventName", ScanOperator.Equal, mod.EventName);
            //  string tablename = "SurveyCatalog";
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,
            };
            Search search = table1.Scan(config);
            List<Document> documentList = new List<Document>();
            //do
            {
                // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {

                     mod.Objective1 = Convert.ToBoolean(document["O1"]);
                     mod.Objective2 = Convert.ToBoolean(document["O2"]);
                     mod.Objective3 = Convert.ToBoolean(document["O3"]);

                }
            } while (!search.IsDone) ;
            TempData["NewID"] = mod.SurveyID;

            // var request = new UpdateItemRequest
            //{
            //    TableName = tablename,
            //    Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = mod.SurveyID } }, { "SurveyID", new AttributeValue { N = "0" } }
            //                     },
            //    ExpressionAttributeNames = new Dictionary<string, string>()
            //    {
            //        {"#O1", "O1"},

            //    },
            //    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
            //    {
            //        {":O1",new AttributeValue { BOOL = mod.Objective1 }},


            //         },
            //    UpdateExpression = "SET #O1 = :O1"
            //};


            //var request = new UpdateItemRequest
            //{
            //    TableName = tablename,
            //    Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = mod.SurveyID } }, { "SurveyID", new AttributeValue { N = "0" } }
            //                 },
            //    ExpressionAttributeNames = new Dictionary<string, string>()
            //{
            //    {"#O1", "O1"},

            //},
            //    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
            //{
            //    {":O1",new AttributeValue { BOOL = mod.Objective1 }},


            //     },
            //    UpdateExpression = "SET #O1 = :O1"
            //};
            //var res = client.UpdateItem(request);
            //if (res != null)
            //{
            //    client.UpdateItem(request);
            //}




            return View(mod);
        }


       // start from here...............
        [HttpPost, ActionName("StudentSurvey")]
        public ActionResult StudentSurveyConfirmed(StudentSurveyModel mod)
        {
               mod.SurveyID = Convert.ToString(TempData["NewID"]);
            //    mod.SurveyofType = "Student"; //Convert.ToString(TempData["Type"]);
            //                                  //  mod.EventName = Convert.ToString(TempData["EventName"]);
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table1 = Table.LoadTable(client, "SurveyCatalog");
            //    //Table table2 = Table.LoadTable(client, "Objectives");
            //    // Table table3 = Table.LoadTable(client, "Questions");
            //    //mod.Objective1 = true;
            //    //mod.Objective2 = true;
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("SurveyID", ScanOperator.Equal, mod.SurveyID);
           // scanFilter.AddCondition("SurveyType", ScanOperator.Equal, mod.SurveyofType);
            string tablename = "SurveyCatalog";
            if (mod.Objective1 == true)

            {
                SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective1), "O1");

                //var request = new UpdateItemRequest
                //{

                //    TableName = tablename,
                //    Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = mod.SurveyID } }
                //                 },
                //    ExpressionAttributeNames = new Dictionary<string, string>()
                //{
                //    {"#O1", "O1"},

                //},
                //    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                //{
                //    {":O1",new AttributeValue { S = Convert.ToString(mod.Objective1) }},
                //     },
                //    UpdateExpression = "SET #O1 = :O1"
                //};
                //var res = client.UpdateItem(request);
                //if (res != null)
                //{
                //    client.UpdateItem(request);
                //}
            }
            if (mod.Objective2 == true)
            {
                SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective2), "O2");


            }
            //ScanOperationConfig config = new ScanOperationConfig()
            //{
            //    Filter = scanFilter,
            //    Select = SelectValues.AllAttributes,
            //};
            //Search search = table1.Scan(config);
            //List<Document> documentList = new List<Document>();
            //do
            //{
            //    // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives

            //    documentList = search.GetNextSet();
            //    if (documentList != null)
            //    {
            //        foreach (var document in documentList)
            //        {

            //        }
            //    }
            //} while (!search.IsDone);
            //    return View(mod);
            //}
            //        mod.SurveyID = Convert.ToString(TempData["ID"]);
            //        mod.SurveyofType = Convert.ToString(TempData["Type"]);
            //        mod.EventName = Convert.ToString(TempData["EventName"]);
            //        AmazonDynamoDBClient client = new AmazonDynamoDBClient();

            //        Table table1 = Table.LoadTable(client, "SurveyCatalog");
            //        //Table table2 = Table.LoadTable(client, "Objectives");
            //        // Table table3 = Table.LoadTable(client, "Questions");
            //        //mod.Objective1 = true;
            //        //mod.Objective2 = true;

            //        ScanFilter scanFilter = new ScanFilter();
            //        scanFilter.AddCondition("SurveyID", ScanOperator.Equal, mod.SurveyID);
            //        scanFilter.AddCondition("SurveyType", ScanOperator.Equal, mod.SurveyofType);
            //        ScanOperationConfig config = new ScanOperationConfig()
            //        {
            //            Filter = scanFilter,
            //            Select = SelectValues.AllAttributes,
            //        };
            //        Search search = table1.Scan(config);
            //        List<Document> documentList = new List<Document>();

            //        //    ScanFilter sf = new ScanFilter();
            //        string tablename = "SurveyCatalog";

            //        var request = new UpdateItemRequest
            //        {
            //            TableName = tablename,
            //            Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = mod.SurveyID } }, {"USID", new AttributeValue { S = "0" } }
            //                 },
            //            ExpressionAttributeNames = new Dictionary<string, string>()
            //{
            //    {"#O1", "O1"}, 

            //},
            //            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
            //{
            //    {":O1",new AttributeValue { BOOL = mod.Objective1}},


            //     },
            //            UpdateExpression = "SET #O1 = :O1"
            //        };
            //        //do
            //        //{
            //            //    // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives

            //            //documentList = search.GetNextSet();
            //            //if (documentList != null)
            //            //{
            //            //    foreach (var document in documentList)
            //            //    {

            //                    var res = client.UpdateItem(request);
            //                    if (res != null)
            //                    {
            //                        client.UpdateItem(request);
            //                    }
            //        //        }
            //        //    }
            //        //} while (!search.IsDone);
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