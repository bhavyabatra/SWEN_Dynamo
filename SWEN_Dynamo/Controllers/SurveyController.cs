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
            model.ParentID = Convert.ToInt64(TempData["IDCreate"]);
            TempData["ParentID"] = model.ParentID;
            string y = Convert.ToString(model.SurveyType);
            return View(model);

        

        }
        [HttpPost, ActionName("SurveyStart")]
        public ActionResult SurveyStartConfirmed(SurveyModel model)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tablename = "SurveyCatalog";
            //string x = "Null";
            //Storestring(model.SurveyID);
            model.ParentID = Convert.ToInt64(TempData["ParentID"]);
            TempData["ID"] = model.SurveyID;
            TempData["Type"] = model.SurveyType;
            TempData["EventName"] = model.EventName;
          //  TempData["USID"] = model.ParentID;
            string str = Convert.ToString(model.SurveyType);
            var request = new PutItemRequest
            {

                TableName = tablename,
                Item = new Dictionary<string, AttributeValue>()
      {
                    { "SurveyID", new AttributeValue { S = model.SurveyID } },
                    { "EventName", new AttributeValue { S = model.EventName } },
                    { "USID", new AttributeValue { N = Convert.ToString(model.ParentID) }},
                    { "SurveyType", new AttributeValue { S = Convert.ToString(model.SurveyType) }},
                    {"O1", new AttributeValue { S = "false" } },
                    {"O1_Q1", new AttributeValue { S = "false" } },
                    {"O1_Q2", new AttributeValue { S = "false" } },
                    {"O1_Q3", new AttributeValue { S = "false" } },
                    {"O1_Q4", new AttributeValue { S = "false" } },
                    {"O1_Q5", new AttributeValue { S = "false" } },
                    {"O1_Q6", new AttributeValue { S = "false" } },
                    {"O2", new AttributeValue { S = "false" } },
                    {"O2_Q1", new AttributeValue { S = "false" } },
                    {"O2_Q2", new AttributeValue { S = "false" } },
                    {"O2_Q3", new AttributeValue { S = "false" } },
                    {"O2_Q4", new AttributeValue { S = "false" } },
                    {"O2_Q5", new AttributeValue { S = "false" } },
                    {"O2_Q6", new AttributeValue { S = "false" } },
                    {"O3", new AttributeValue { S = "false" } },
                    {"O3_Q1", new AttributeValue { S = "false" } },
                    {"O4", new AttributeValue { S = "false" } },
                    {"O4_Q1", new AttributeValue { S = "false" } },
                    {"O4_Q2", new AttributeValue { S = "false" } },
                    {"O5", new AttributeValue { S = "false" } },
                    {"O5_Q1", new AttributeValue { S = "false" } },
                    {"O5_Q2", new AttributeValue { S = "false" } },
                    {"O5_Q3", new AttributeValue { S = "false" } },
                    {"O6", new AttributeValue { S = "false" } },
                    {"O6_Q1", new AttributeValue { S = "false" } },
                    {"O7", new AttributeValue { S = "false" } },
                    {"O7_Q1", new AttributeValue { S = "false" } },
                    {"O7_Q2", new AttributeValue { S = "false" } },
                    {"O7_Q3", new AttributeValue { S = "false" } },
                    {"O7_Q4", new AttributeValue { S = "false" } },
                    {"O7_Q5", new AttributeValue { S = "false" } },
                    {"O8", new AttributeValue { S = "false" } },
                    {"O8_Q1", new AttributeValue { S = "false" } },
                    {"O8_Q2", new AttributeValue { S = "false" } },
                    {"O8_Q3", new AttributeValue { S = "false" } },
                    {"O9", new AttributeValue { S = "false" } },
                    {"O9_Q1", new AttributeValue { S = "false" } },
                    {"O9_Q2", new AttributeValue { S = "false" } },
                    {"O9_Q3", new AttributeValue { S = "false" } },
                    {"O9_Q4", new AttributeValue { S = "false" } },
                    {"O10", new AttributeValue { S = "false" } },
                    {"O10_Q1", new AttributeValue { S = "false" } },
                    {"O10_Q2", new AttributeValue { S = "false" } },
                    {"O10_Q3", new AttributeValue { S = "false" } },
                    {"O10_Q4", new AttributeValue { S = "false" } },
                     {"O11", new AttributeValue { S = "false" } },
                     {"CQ1", new AttributeValue { S = model.CustomQuestion1} },
                    {"CQ2", new AttributeValue { S = model.CustomQuestion2} },
                     {"CQ3", new AttributeValue { S = model.CustomQuestion1} },
                    {"CQ4", new AttributeValue { S = model.CustomQuestion2} },
                    {"CQ5", new AttributeValue { S = model.CustomQuestion1} },
                    
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

        public ActionResult Navigator(StudentSurveyModel mod, string id)
        {
            string IDofSurvey = id;
            TempData["ID"] = id;
            string TypeOfSurvey = SWEN_DynamoUtilityClass.SurveyEditorNavigation(IDofSurvey);
            TempData["Type"] = TypeOfSurvey;
            TempData["EventName"] = SWEN_DynamoUtilityClass.FetchEventNameFromSID(IDofSurvey);
            if (TypeOfSurvey == "Student")
            {
                return RedirectToAction("StudentSurvey");
            }
            else
            {
                return RedirectToAction("ParentSurvey");
            }
        }
        public ActionResult StudentSurvey(StudentSurveyModel mod)
        {
          

            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table1 = Table.LoadTable(client, "SurveyCatalog");
            //Table table2 = Table.LoadTable(client, "Objectives");
            //Table table3 = Table.LoadTable(client, "Questions");
            mod.SurveyID = Convert.ToString(TempData["ID"]);
            mod.EventName = Convert.ToString(TempData["EventName"]);
            mod.SurveyofType = Convert.ToString(TempData["Type"]);
            mod.USID = SWEN_DynamoUtilityClass.FetchUSIDFromSurveyID(mod.SurveyID);
            TempData["USID"] = mod.USID;

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
            do
            {
                // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {

                    mod.Objective1 = Convert.ToBoolean(document["O1"]);
                    mod.O1_Q1 = Convert.ToBoolean(document["O1_Q1"]);
                    mod.O1_Q2 = Convert.ToBoolean(document["O1_Q2"]);
                    mod.O1_Q3 = Convert.ToBoolean(document["O1_Q3"]);
                    mod.O1_Q4 = Convert.ToBoolean(document["O1_Q4"]);
                    mod.O1_Q5 = Convert.ToBoolean(document["O1_Q5"]);
                    mod.O1_Q6 = Convert.ToBoolean(document["O1_Q6"]);
                    mod.Objective2 = Convert.ToBoolean(document["O2"]);
                    mod.O2_Q1 = Convert.ToBoolean(document["O2_Q1"]);
                    mod.O2_Q2 = Convert.ToBoolean(document["O2_Q2"]);
                    mod.O2_Q3 = Convert.ToBoolean(document["O2_Q3"]);
                    mod.O2_Q4 = Convert.ToBoolean(document["O2_Q4"]);
                    mod.O2_Q5 = Convert.ToBoolean(document["O2_Q5"]);
                    mod.O2_Q6 = Convert.ToBoolean(document["O2_Q6"]);
                    mod.Objective3 = Convert.ToBoolean(document["O3"]);
                    mod.O3_Q1 = Convert.ToBoolean(document["O3_Q1"]);
                    mod.Objective4 = Convert.ToBoolean(document["O4"]);
                    mod.O4_Q1 = Convert.ToBoolean(document["O4_Q1"]);
                    mod.O4_Q2 = Convert.ToBoolean(document["O4_Q2"]);
                    mod.Objective5 = Convert.ToBoolean(document["O5"]);
                    mod.O5_Q1 = Convert.ToBoolean(document["O5_Q1"]);
                    mod.O5_Q2 = Convert.ToBoolean(document["O5_Q2"]);
                    mod.O5_Q3 = Convert.ToBoolean(document["O5_Q3"]);
                    mod.Objective6 = Convert.ToBoolean(document["O6"]);
                    mod.O6_Q1 = Convert.ToBoolean(document["O6_Q1"]);
                    mod.Objective7 = Convert.ToBoolean(document["O7"]);
                    mod.O7_Q1 = Convert.ToBoolean(document["O7_Q1"]);
                    mod.Objective8 = Convert.ToBoolean(document["O8"]);
                    mod.O8_Q1 = Convert.ToBoolean(document["O8_Q1"]);
                    mod.Objective9 = Convert.ToBoolean(document["O9"]);
                    mod.O9_Q1 = Convert.ToBoolean(document["O9_Q1"]);
                    mod.O9_Q2 = Convert.ToBoolean(document["O9_Q2"]);
                    mod.O9_Q3 = Convert.ToBoolean(document["O9_Q3"]);
                    mod.O9_Q4 = Convert.ToBoolean(document["O9_Q4"]);
                    mod.Objective10 = Convert.ToBoolean(document["O10"]);
                    mod.O10_Q1 = Convert.ToBoolean(document["O10_Q1"]);
                    mod.O10_Q2 = Convert.ToBoolean(document["O10_Q2"]);
                    mod.O10_Q3 = Convert.ToBoolean(document["O10_Q3"]);
                    mod.O10_Q4 = Convert.ToBoolean(document["O10_Q4"]);
                    mod.Objective11 = Convert.ToBoolean(document["O11"]);
                    mod.CustomQuestion1 = document["CQ1"];
                    mod.CustomQuestion2 = document["CQ2"];
                    mod.CustomQuestion3 = document["CQ3"];
                    mod.CustomQuestion4 = document["CQ4"];
                    mod.CustomQuestion5 = document["CQ5"];
                    

                }
            } while (!search.IsDone) ;
            TempData["NewID"] = mod.SurveyID;
            TempData["NewSurveyType"] = mod.SurveyofType;
            TempData["NewEventName"] = mod.EventName;


            return View(mod);
        }


        [HttpPost, ActionName("StudentSurvey")]
        public ActionResult StudentSurveyConfirmed(StudentSurveyModel mod, string SaveSurvey, string DeploySurvey, string DeleteSurvey)
        {
            if (!string.IsNullOrWhiteSpace(SaveSurvey))
            {
                if (TempData["NewID"] != null)
                {
                    mod.SurveyID = Convert.ToString(TempData["NewID"]);
                    mod.SurveyofType = Convert.ToString(TempData["NewSurveyType"]);
                    mod.EventName = Convert.ToString(TempData["NewEventName"]);
                }
                else
                {
                    mod.SurveyID = Convert.ToString(TempData["RedirectID"]);
                    mod.SurveyofType = Convert.ToString(TempData["RedirectType"]);
                    mod.EventName = Convert.ToString(TempData["RedirectEventName"]);
                }

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
                if (mod.Objective1 == true || mod.Objective1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective1), "O1");
                }
                if (mod.O1_Q1 == true || mod.O1_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q1), "O1_Q1");
                }
                if (mod.O1_Q2 == true || mod.O1_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q2), "O1_Q2");
                }
                if (mod.O1_Q3 == true || mod.O1_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q3), "O1_Q3");
                }
                if (mod.O1_Q4 == true || mod.O1_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q4), "O1_Q4");
                }
                if (mod.O1_Q5 == true || mod.O1_Q5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q5), "O1_Q5");
                }
                if (mod.O1_Q6 == true || mod.O1_Q6 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q6), "O1_Q6");
                }
                if (mod.Objective2 == true || mod.Objective2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective2), "O2");
                }
                if (mod.O2_Q1 == true || mod.O2_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q1), "O2_Q1");
                }
                if (mod.O2_Q2 == true || mod.O2_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q2), "O2_Q2");
                }
                if (mod.O2_Q3 == true || mod.O2_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q3), "O2_Q3");
                }
                if (mod.O2_Q4 == true || mod.O2_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q4), "O2_Q4");
                }
                if (mod.O2_Q5 == true || mod.O2_Q5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q5), "O2_Q5");
                }
                if (mod.O2_Q6 == true || mod.O2_Q6 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q6), "O2_Q6");
                }
                if (mod.Objective3 == true || mod.Objective3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective3), "O3");
                }
                if (mod.O3_Q1 == true || mod.O3_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O3_Q1), "O3_Q1");
                }
                if (mod.Objective4 == true || mod.Objective4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective4), "O4");
                }
                if (mod.O4_Q1 == true || mod.O4_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O4_Q1), "O4_Q1");
                }
                if (mod.O4_Q2 == true || mod.O4_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O4_Q2), "O4_Q2");
                }
                if (mod.Objective5 == true || mod.Objective5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective5), "O5");
                }
                if (mod.O5_Q1 == true || mod.O5_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q1), "O5_Q1");
                }
                if (mod.O5_Q2 == true || mod.O5_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q2), "O5_Q2");
                }
                if (mod.O5_Q3 == true || mod.O5_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q3), "O5_Q3");
                }
                if (mod.Objective6 == true || mod.Objective6 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective6), "O6");
                }
                if (mod.O6_Q1 == true || mod.O6_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O6_Q1), "O6_Q1");
                }
                if (mod.Objective7 == true || mod.Objective7 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective7), "O7");
                }
                if (mod.O7_Q1 == true || mod.O7_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q1), "O7_Q1");
                }
                if (mod.Objective8 == true || mod.Objective8 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective8), "O8");
                }
                if (mod.O8_Q1 == true || mod.O8_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O8_Q1), "O8_Q1");
                }
                if (mod.Objective9 == true || mod.Objective9 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective9), "O9");
                }
                if (mod.O9_Q1 == true || mod.O9_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O9_Q1), "O9_Q1");
                }
                if (mod.O9_Q2 == true || mod.O9_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O9_Q2), "O9_Q2");
                }
                if (mod.O9_Q3 == true || mod.O9_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O9_Q3), "O9_Q3");
                }
                if (mod.O9_Q4 == true || mod.O9_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O9_Q4), "O9_Q4");
                }
                if (mod.Objective10 == true || mod.Objective10 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective10), "O10");
                }
                if (mod.O10_Q1 == true || mod.O10_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O10_Q1), "O10_Q1");
                }
                if (mod.O10_Q2 == true || mod.O10_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O10_Q2), "O10_Q2");
                }
                if (mod.O10_Q3 == true || mod.O10_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O10_Q3), "O10_Q3");
                }
                if (mod.O10_Q4 == true || mod.O10_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O10_Q4), "O10_Q4");
                }
                if (mod.Objective11 == true || mod.Objective11 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective11), "O11");
                }
                if (mod.CustomQuestion1 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion1, "CQ1");
                 }
                if (mod.CustomQuestion1 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ1");
                 }
                if (mod.CustomQuestion2 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion2, "CQ2");
                }
                if (mod.CustomQuestion2 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ2");
                }
                if (mod.CustomQuestion3 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion3, "CQ3");
                }
                if (mod.CustomQuestion3 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ3");
                }
                if (mod.CustomQuestion4 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion4, "CQ4");
                }
                if (mod.CustomQuestion4 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ4");
                }
                if (mod.CustomQuestion5 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion5, "CQ5");
                }
                if (mod.CustomQuestion5 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ5");
                }
                TempData["RedirectID"] = mod.SurveyID;
                TempData["RedirectType"] = mod.SurveyofType;
                TempData["RedirectEventName"] = mod.EventName;

            }
            if (!string.IsNullOrWhiteSpace(DeploySurvey))
            {
                if (TempData["NewID"] != null)
                {
                    mod.SurveyID = Convert.ToString(TempData["NewID"]);
                    mod.SurveyofType = Convert.ToString(TempData["NewSurveyType"]);
                    mod.EventName = Convert.ToString(TempData["NewEventName"]);
                }
                else
                {
                    mod.SurveyID = Convert.ToString(TempData["RedirectID"]);
                    mod.SurveyofType = Convert.ToString(TempData["RedirectType"]);
                    mod.EventName = Convert.ToString(TempData["RedirectEventName"]);
                }

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
                if (mod.Objective1 == true || mod.Objective1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective1), "O1");
                }
                if (mod.O1_Q1 == true || mod.O1_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q1), "O1_Q1");
                }
                if (mod.O1_Q2 == true || mod.O1_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q2), "O1_Q2");
                }
                if (mod.O1_Q3 == true || mod.O1_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q3), "O1_Q3");
                }
                if (mod.O1_Q4 == true || mod.O1_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q4), "O1_Q4");
                }
                if (mod.O1_Q5 == true || mod.O1_Q5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q5), "O1_Q5");
                }
                if (mod.O1_Q6 == true || mod.O1_Q6 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q6), "O1_Q6");
                }
                if (mod.Objective2 == true || mod.Objective2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective2), "O2");
                }
                if (mod.O2_Q1 == true || mod.O2_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q1), "O2_Q1");
                }
                if (mod.O2_Q2 == true || mod.O2_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q2), "O2_Q2");
                }
                if (mod.O2_Q3 == true || mod.O2_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q3), "O2_Q3");
                }
                if (mod.O2_Q4 == true || mod.O2_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q4), "O2_Q4");
                }
                if (mod.O2_Q5 == true || mod.O2_Q5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q5), "O2_Q5");
                }
                if (mod.O2_Q6 == true || mod.O2_Q6 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q6), "O2_Q6");
                }
                if (mod.Objective3 == true || mod.Objective3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective3), "O3");
                }
                if (mod.O3_Q1 == true || mod.O3_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O3_Q1), "O3_Q1");
                }
                if (mod.Objective4 == true || mod.Objective4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective4), "O4");
                }
                if (mod.O4_Q1 == true || mod.O4_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O4_Q1), "O4_Q1");
                }
                if (mod.O4_Q2 == true || mod.O4_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O4_Q2), "O4_Q2");
                }
                if (mod.Objective5 == true || mod.Objective5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective5), "O5");
                }
                if (mod.O5_Q1 == true || mod.O5_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q1), "O5_Q1");
                }
                if (mod.O5_Q2 == true || mod.O5_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q2), "O5_Q2");
                }
                if (mod.O5_Q3 == true || mod.O5_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q3), "O5_Q3");
                }
                if (mod.Objective6 == true || mod.Objective6 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective6), "O6");
                }
                if (mod.O6_Q1 == true || mod.O6_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O6_Q1), "O6_Q1");
                }
                if (mod.Objective7 == true || mod.Objective7 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective7), "O7");
                }
                if (mod.O7_Q1 == true || mod.O7_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q1), "O7_Q1");
                }
                if (mod.Objective8 == true || mod.Objective8 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective8), "O8");
                }
                if (mod.O8_Q1 == true || mod.O8_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O8_Q1), "O8_Q1");
                }
                if (mod.Objective9 == true || mod.Objective9 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective9), "O9");
                }
                if (mod.O9_Q1 == true || mod.O9_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O9_Q1), "O9_Q1");
                }
                if (mod.O9_Q2 == true || mod.O9_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O9_Q2), "O9_Q2");
                }
                if (mod.O9_Q3 == true || mod.O9_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O9_Q3), "O9_Q3");
                }
                if (mod.O9_Q4 == true || mod.O9_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O9_Q4), "O9_Q4");
                }
                if (mod.Objective10 == true || mod.Objective10 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective10), "O10");
                }
                if (mod.O10_Q1 == true || mod.O10_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O10_Q1), "O10_Q1");
                }
                if (mod.O10_Q2 == true || mod.O10_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O10_Q2), "O10_Q2");
                }
                if (mod.O10_Q3 == true || mod.O10_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O10_Q3), "O10_Q3");
                }
                if (mod.O10_Q4 == true || mod.O10_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O10_Q4), "O10_Q4");
                }
                if (mod.Objective11 == true || mod.Objective11 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective11), "O11");
                }
                if (mod.CustomQuestion1 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion1, "CQ1");
                }
                if (mod.CustomQuestion1 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ1");
                }
                if (mod.CustomQuestion2 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion2, "CQ2");
                }
                if (mod.CustomQuestion2 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ2");
                }
                if (mod.CustomQuestion3 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion3, "CQ3");
                }
                if (mod.CustomQuestion3 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ3");
                }
                if (mod.CustomQuestion4 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion4, "CQ4");
                }
                if (mod.CustomQuestion4 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ4");
                }
                if (mod.CustomQuestion5 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion5, "CQ5");
                }
                if (mod.CustomQuestion5 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ5");
                }

                TempData["RedirectID"] = mod.SurveyID;
                TempData["RedirectType"] = mod.SurveyofType;
                TempData["RedirectEventName"] = mod.EventName;
                return RedirectToAction("DeploySurveyStart");
            }
            if (!string.IsNullOrWhiteSpace(DeleteSurvey))
            {
                if (TempData["NewID"] != null)
                {
                    mod.SurveyID = Convert.ToString(TempData["NewID"]);
                    mod.SurveyofType = Convert.ToString(TempData["NewSurveyType"]);
                    mod.EventName = Convert.ToString(TempData["NewEventName"]);
                }
                else
                {
                    mod.SurveyID = Convert.ToString(TempData["RedirectID"]);
                    mod.SurveyofType = Convert.ToString(TempData["RedirectType"]);
                    mod.EventName = Convert.ToString(TempData["RedirectEventName"]);
                }


                //    mod.SurveyofType = "Student"; //Convert.ToString(TempData["Type"]);
                //                                  //  mod.EventName = Convert.ToString(TempData["EventName"]);
                AmazonDynamoDBClient client = new AmazonDynamoDBClient();
                Table table1 = Table.LoadTable(client, "SurveyCatalog");
                string tableName = "SurveyCatalog";


                var request = new DeleteItemRequest
                {
                    TableName = tableName,
                    Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = Convert.ToString(mod.SurveyID) } } },
                };
                var response = client.DeleteItem(request);
                return RedirectToAction("StudentSurveyDeletedAck");
            }
                return View(mod);
        }

        public ActionResult ParentSurvey(ParentSurveyModel mod)
        {

            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table1 = Table.LoadTable(client, "SurveyCatalog");
            //Table table2 = Table.LoadTable(client, "Objectives");
            //Table table3 = Table.LoadTable(client, "Questions");
            mod.SurveyID = Convert.ToString(TempData["ID"]);
            mod.EventName = Convert.ToString(TempData["EventName"]);
            mod.SurveyofType = Convert.ToString(TempData["Type"]);
            mod.USID = SWEN_DynamoUtilityClass.FetchUSIDFromSurveyID(mod.SurveyID);
            TempData["USID"] = mod.USID;

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
            do
            {
                // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {

                    mod.Objective1 = Convert.ToBoolean(document["O1"]);
                    mod.O1_Q1 = Convert.ToBoolean(document["O1_Q1"]);
                    mod.Objective2 = Convert.ToBoolean(document["O2"]);
                    mod.Objective3 = Convert.ToBoolean(document["O3"]);
                    mod.O3_Q1 = Convert.ToBoolean(document["O3_Q1"]);
                    mod.Objective4 = Convert.ToBoolean(document["O4"]);
                    mod.O4_Q1 = Convert.ToBoolean(document["O4_Q1"]);
                    mod.Objective5 = Convert.ToBoolean(document["O5"]);
                    mod.O5_Q1 = Convert.ToBoolean(document["O5_Q1"]);
                    mod.O5_Q2 = Convert.ToBoolean(document["O5_Q2"]);
                    mod.O5_Q3 = Convert.ToBoolean(document["O5_Q3"]);
                    mod.Objective6 = Convert.ToBoolean(document["O6"]);
                    mod.O6_Q1 = Convert.ToBoolean(document["O6_Q1"]);
                    mod.Objective7 = Convert.ToBoolean(document["O7"]);
                    mod.O7_Q1 = Convert.ToBoolean(document["O7_Q1"]);
                    mod.O7_Q2 = Convert.ToBoolean(document["O7_Q2"]);
                    mod.O7_Q3 = Convert.ToBoolean(document["O7_Q3"]);
                    mod.O7_Q4 = Convert.ToBoolean(document["O7_Q4"]);
                    mod.O7_Q5 = Convert.ToBoolean(document["O7_Q5"]);
                    mod.Objective8 = Convert.ToBoolean(document["O8"]);
                    mod.O8_Q1 = Convert.ToBoolean(document["O8_Q1"]);
                    mod.O8_Q2 = Convert.ToBoolean(document["O8_Q2"]);
                    mod.O8_Q3 = Convert.ToBoolean(document["O8_Q3"]);
                    mod.Objective9 = Convert.ToBoolean(document["O9"]);
                    mod.CustomQuestion1 = document["CQ1"];
                    mod.CustomQuestion2 = document["CQ2"];
                    mod.CustomQuestion3 = document["CQ3"];
                    mod.CustomQuestion4 = document["CQ4"];
                    mod.CustomQuestion5 = document["CQ5"];


                }
            } while (!search.IsDone);
            TempData["NewID"] = mod.SurveyID;
            TempData["NewSurveyType"] = mod.SurveyofType;
            TempData["NewEventName"] = mod.EventName;


            return View(mod);
        }

        [HttpPost, ActionName("ParentSurvey")]
        public ActionResult ParentSurveyConfirmed(ParentSurveyModel mod, string SaveSurvey, string DeploySurvey, string DeleteSurvey)
        {
            if (!string.IsNullOrWhiteSpace(SaveSurvey))
            {
                if (TempData["NewID"] != null)
                {
                    mod.SurveyID = Convert.ToString(TempData["NewID"]);
                    mod.SurveyofType = Convert.ToString(TempData["NewSurveyType"]);
                    mod.EventName = Convert.ToString(TempData["NewEventName"]);
                }
                else
                {
                    mod.SurveyID = Convert.ToString(TempData["RedirectID"]);
                    mod.SurveyofType = Convert.ToString(TempData["RedirectType"]);
                    mod.EventName = Convert.ToString(TempData["RedirectEventName"]);
                }

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
                if (mod.Objective1 == true || mod.Objective1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective1), "O1");
                }
                if (mod.O1_Q1 == true || mod.O1_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q1), "O1_Q1");
                }
                if (mod.Objective2 == true || mod.Objective2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective2), "O2");
                }
                if (mod.O2_Q1 == true || mod.O2_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q1), "O2_Q1");
                }
                if (mod.Objective3 == true || mod.Objective3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective3), "O3");
                }
                if (mod.O3_Q1 == true || mod.O3_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O3_Q1), "O3_Q1");
                }
                if (mod.Objective4 == true || mod.Objective4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective4), "O4");
                }
                if (mod.O4_Q1 == true || mod.O4_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O4_Q1), "O4_Q1");
                }
                if (mod.Objective5 == true || mod.Objective5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective5), "O5");
                }
                if (mod.O5_Q1 == true || mod.O5_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q1), "O5_Q1");
                }
                if (mod.O5_Q2 == true || mod.O5_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q2), "O5_Q2");
                }
                if (mod.O5_Q3 == true || mod.O5_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q3), "O5_Q3");
                }
                if (mod.Objective6 == true || mod.Objective6 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective6), "O6");
                }
                if (mod.O6_Q1 == true || mod.O6_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O6_Q1), "O6_Q1");
                }
                if (mod.Objective7 == true || mod.Objective7 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective7), "O7");
                }
                if (mod.O7_Q1 == true || mod.O7_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q1), "O7_Q1");
                }
                if (mod.O7_Q2 == true || mod.O7_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q2), "O7_Q2");
                }
                if (mod.O7_Q3 == true || mod.O7_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q3), "O7_Q3");
                }
                if (mod.O7_Q4 == true || mod.O7_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q4), "O7_Q4");
                }
                if (mod.O7_Q5 == true || mod.O7_Q5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q5), "O7_Q5");
                }
                if (mod.Objective8 == true || mod.Objective8 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective8), "O8");
                }
                if (mod.O8_Q1 == true || mod.O8_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O8_Q1), "O8_Q1");
                }
                if (mod.O8_Q2 == true || mod.O8_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O8_Q2), "O8_Q2");
                }
                if (mod.O8_Q3 == true || mod.O8_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O8_Q3), "O8_Q3");
                }
                if (mod.Objective9 == true || mod.Objective9 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective9), "O9");
                }
                
                if (mod.CustomQuestion1 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion1, "CQ1");
                }
                if (mod.CustomQuestion1 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ1");
                }
                if (mod.CustomQuestion2 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion2, "CQ2");
                }
                if (mod.CustomQuestion2 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ2");
                }
                if (mod.CustomQuestion3 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion3, "CQ3");
                }
                if (mod.CustomQuestion3 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ3");
                }
                if (mod.CustomQuestion4 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion4, "CQ4");
                }
                if (mod.CustomQuestion4 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ4");
                }
                if (mod.CustomQuestion5 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion5, "CQ5");
                }
                if (mod.CustomQuestion5 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ5");
                }
                TempData["RedirectID"] = mod.SurveyID;
                TempData["RedirectType"] = mod.SurveyofType;
                TempData["RedirectEventName"] = mod.EventName;

            }
            if (!string.IsNullOrWhiteSpace(DeploySurvey))
            {
                if (TempData["NewID"] != null)
                {
                    mod.SurveyID = Convert.ToString(TempData["NewID"]);
                    mod.SurveyofType = Convert.ToString(TempData["NewSurveyType"]);
                    mod.EventName = Convert.ToString(TempData["NewEventName"]);
                }
                else
                {
                    mod.SurveyID = Convert.ToString(TempData["RedirectID"]);
                    mod.SurveyofType = Convert.ToString(TempData["RedirectType"]);
                    mod.EventName = Convert.ToString(TempData["RedirectEventName"]);
                }

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
                if (mod.Objective1 == true || mod.Objective1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective1), "O1");
                }
                if (mod.O1_Q1 == true || mod.O1_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q1), "O1_Q1");
                }
                 if (mod.Objective2 == true || mod.Objective2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective2), "O2");
                }
                if (mod.O2_Q1 == true || mod.O2_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O2_Q1), "O2_Q1");
                }
                if (mod.Objective3 == true || mod.Objective3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective3), "O3");
                }
                if (mod.O3_Q1 == true || mod.O3_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O3_Q1), "O3_Q1");
                }
                if (mod.Objective4 == true || mod.Objective4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective4), "O4");
                }
                if (mod.O4_Q1 == true || mod.O4_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O4_Q1), "O4_Q1");
                }
                if (mod.Objective5 == true || mod.Objective5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective5), "O5");
                }
                if (mod.O5_Q1 == true || mod.O5_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q1), "O5_Q1");
                }
                if (mod.O5_Q2 == true || mod.O5_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q2), "O5_Q2");
                }
                if (mod.O5_Q3 == true || mod.O5_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O5_Q3), "O5_Q3");
                }
                if (mod.Objective6 == true || mod.Objective6 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective6), "O6");
                }
                if (mod.O6_Q1 == true || mod.O6_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O6_Q1), "O6_Q1");
                }
                if (mod.Objective7 == true || mod.Objective7 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective7), "O7");
                }
                if (mod.O7_Q1 == true || mod.O7_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q1), "O7_Q1");
                }
                if (mod.O7_Q2 == true || mod.O7_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q2), "O7_Q2");
                }
                if (mod.O7_Q3 == true || mod.O7_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q3), "O7_Q3");
                }
                if (mod.O7_Q4 == true || mod.O7_Q4 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q4), "O7_Q4");
                }
                if (mod.O7_Q5 == true || mod.O7_Q5 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O7_Q5), "O7_Q5");
                }
                if (mod.Objective8 == true || mod.Objective8 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective8), "O8");
                }
                if (mod.O8_Q1 == true || mod.O8_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O8_Q1), "O8_Q1");
                }
                if (mod.O8_Q2 == true || mod.O8_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O8_Q2), "O8_Q2");
                }
                if (mod.O8_Q3 == true || mod.O8_Q3 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O8_Q3), "O8_Q3");
                }
                if (mod.Objective9 == true || mod.Objective9 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective9), "O9");
                }
                if (mod.CustomQuestion1 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion1, "CQ1");
                }
                if (mod.CustomQuestion1 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ1");
                }
                if (mod.CustomQuestion2 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion2, "CQ2");
                }
                if (mod.CustomQuestion2 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ2");
                }
                if (mod.CustomQuestion3 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion3, "CQ3");
                }
                if (mod.CustomQuestion3 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ3");
                }
                if (mod.CustomQuestion4 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion4, "CQ4");
                }
                if (mod.CustomQuestion4 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ4");
                }
                if (mod.CustomQuestion5 != null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, mod.CustomQuestion5, "CQ5");
                }
                if (mod.CustomQuestion5 == null)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, "Null", "CQ5");
                }

                TempData["RedirectID"] = mod.SurveyID;
                TempData["RedirectType"] = mod.SurveyofType;
                TempData["RedirectEventName"] = mod.EventName;
                return RedirectToAction("DeploySurveyStart");
            }

            if (!string.IsNullOrWhiteSpace(DeleteSurvey))
            {
                if (TempData["NewID"] != null)
                {
                    mod.SurveyID = Convert.ToString(TempData["NewID"]);
                    mod.SurveyofType = Convert.ToString(TempData["NewSurveyType"]);
                    mod.EventName = Convert.ToString(TempData["NewEventName"]);
                }
                else
                {
                    mod.SurveyID = Convert.ToString(TempData["RedirectID"]);
                    mod.SurveyofType = Convert.ToString(TempData["RedirectType"]);
                    mod.EventName = Convert.ToString(TempData["RedirectEventName"]);
                }


                //    mod.SurveyofType = "Student"; //Convert.ToString(TempData["Type"]);
                //                                  //  mod.EventName = Convert.ToString(TempData["EventName"]);
                AmazonDynamoDBClient client = new AmazonDynamoDBClient();
                Table table1 = Table.LoadTable(client, "SurveyCatalog");
                string tableName = "SurveyCatalog";


                var request = new DeleteItemRequest
                {
                    TableName = tableName,
                    Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = Convert.ToString(mod.SurveyID) } } },
                };
                var response = client.DeleteItem(request);
                return RedirectToAction("ParentSurveyDeletedAck");
            }

            return View(mod);
        }

        public ActionResult SurveyList(long? id, SurveyModel mod)
        {
            long ID = Convert.ToInt64(id);
            TempData["IDCreate"] = ID;
            int RIDofUser = SWEN_DynamoUtilityClass.CheckRIDwithUSID(ID);
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "SurveyCatalog");
            ScanFilter scanFilter = new ScanFilter();
            if (RIDofUser == 1)
            {
                scanFilter.AddCondition("USID", ScanOperator.GreaterThan, 0);
            }
            else
            {
                scanFilter.AddCondition("USID", ScanOperator.Equal, ID);
            }
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();

            do
            {
                mod.SurveyModelList.Clear();
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    mod.SurveyModelList.Add(new SurveyModel()
                    {
                        SurveyID = document["SurveyID"],
                        ST = document["SurveyType"],
                        EventName = document ["EventName"],
                        USID = Convert.ToInt64(document ["USID"])

                    });

                }
            } while (!search.IsDone);
            return View(mod.SurveyModelList);

        }

        public ActionResult StudentSurveyDeletedAck(StudentSurveyModel mod)
     
        {
            mod.USID = Convert.ToInt64(TempData["USID"]);
            return View(mod);
        }

        public ActionResult ParentSurveyDeletedAck(ParentSurveyModel mod)

        {
            mod.USID = Convert.ToInt64(TempData["USID"]);
            return View(mod);
        }

        public ActionResult DeploySurveyStart(DeploySurveyStart mod)
        {
            mod.SurveyID = Convert.ToString(TempData["RedirectID"]);
            string tablename = "Respondent";
            TempData["SurveyDeployID"] = mod.SurveyID;

            //var em = new List<Emails>();
            //if (mod.emailids != null)
            //{
            //    foreach (var e in em)
            //    {
            //        em.Add(new Emails { email = Convert.ToString(mod.emailids) });
            //    }
            //}
            return View(mod);
        }
        [HttpPost, ActionName("DeploySurveyStart")]
        public ActionResult DeploySurveyStartConfirmed(DeploySurveyStart mod)
        {
            mod.SurveyID = Convert.ToString(TempData["SurveyDeployID"]);
            string RT =  mod.ResponseToken0 ;
            string TrimRT = RT.Trim();
            string NewRT = TrimRT.Replace(" ", "");
            List<string> mail = new List<string>();
            string[] values = NewRT.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
                 mail.Add(values[i]);
            }
            foreach (var m in mail)
            {
                SWEN_DynamoUtilityClass.DeploymentStepFinal(mod.SurveyID, m);
                SWEN_DynamoUtilityClass.SendEmail(m);
            }
           return View("DeploySurveyAck");

        }

        public ActionResult TakeSurvey(TakeSurvey mod)
        {
            TempData["ResponseToken"] = mod.ResponseToken;

            return View(mod);
        }
        [HttpPost, ActionName("TakeSurvey")]
        public ActionResult TakeSurveyStart(TakeSurvey mod)
        {
            TempData["ResponseToken"] = mod.ResponseToken;

            return RedirectToAction("TakeSurveyStepTwo");
        }
        public ActionResult TakeSurveyStepTwo(TakeSurveyStepTwo mod, string action)
        {
            mod.ResponseToken = Convert.ToString(TempData["ResponseToken"]);
            TempData["FinalResponseToken"] = mod.ResponseToken;
            List<TakeSurveyStepTwo> TakeSurveylist = new List<TakeSurveyStepTwo>();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            {
                Dictionary<string, AttributeValue> lastKeyEvaluated = null;
                do
                {
                    var request = new ScanRequest
                    {
                        TableName = "Respondent",
                        ExclusiveStartKey = lastKeyEvaluated,
                        ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {":Responsevalue", new AttributeValue { S = mod.ResponseToken    }}
                },
                        FilterExpression = "ResponseToken = :Responsevalue",
                        ProjectionExpression = "SurveyID, SurveyComplete"
                    };
                    var response = client.Scan(request);
                    foreach (Dictionary<string, AttributeValue> item in response.Items)
                    {
                       
                        
                        if (item.ContainsKey("SurveyComplete") && (item["SurveyComplete"].S == "True" || item["SurveyComplete"].S == "true"))
                       { 
                       
                                TakeSurveylist.Add(new TakeSurveyStepTwo() { TakeSurveyID = (item["SurveyID"].S), SurveyStatus = "Submitted", EventName = SWEN_DynamoUtilityClass.FetchEventNameFromSID(item["SurveyID"].S) });
                          
                       }
                       else 
                        {
                            TakeSurveylist.Add(new TakeSurveyStepTwo() { TakeSurveyID = (item["SurveyID"].S), SurveyStatus = "Pending", EventName = SWEN_DynamoUtilityClass.FetchEventNameFromSID(item["SurveyID"].S) });
                        }

                    }
                    lastKeyEvaluated = response.LastEvaluatedKey;
                } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
              
                return View(TakeSurveylist);
            }


        }
        //[HttpPost, ActionName("TakeSurveyStepTwo")]
        //public ActionResult TakeSurveyStepTwoPost(TakeSurveyStepTwo mod, string action)
        //{
        //    if (action == "Open Survey")
        //    {
        //        return RedirectToAction("SurveyStart");
        //    }
        //    return View(mod);
        //}
        public ActionResult TakeSurveyFinal(string id, TakeSurveyFinalModel FinalTakeSurveyObject)
        {
            string FinalResponseToken = Convert.ToString(TempData["FinalResponseToken"]);
            TempData["FinalPostID"] = id;
            TempData["FinalPostResponseToken"] = FinalResponseToken;
            FinalTakeSurveyObject.AnswerOptions = new List<SelectListItem>
                {
                    new SelectListItem() { Value = "Strongly Agree", Text = "Strongly Agree" },
                new SelectListItem() { Value = "Agree", Text = "Agree" },
                new SelectListItem() { Value = "Disagree", Text = "Disagree" },
                new SelectListItem() { Value = "Strongly Disagree", Text = "Strongly Disagree" }

            };
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tablename = "Respondent";
            Table table1 = Table.LoadTable(client, "Respondent");


            var item = table1.GetItem(id, FinalResponseToken);
            TempData["Item"] = item;
            DynamoDBEntry SurveyCompleteFlag = null;
            if (item.Contains("SurveyComplete"))
            {
                item.TryGetValue("SurveyComplete", out SurveyCompleteFlag);
                if (SurveyCompleteFlag == "True")
                {
                    return Redirect("SurveySubmissionAck");
                }
            }
            else
            {
                foreach (var inserttoqlist in item)
                {
                    if (inserttoqlist.Value != null && !inserttoqlist.Key.Equals("SurveyID") && !inserttoqlist.Key.Equals("ResponseToken") && !inserttoqlist.Key.EndsWith("A") && !inserttoqlist.Key.StartsWith("C"))
                    {
                        FinalTakeSurveyObject.question.Add(inserttoqlist.Value);
                    }
                    if (inserttoqlist.Value != null && !inserttoqlist.Key.Equals("SurveyID") && !inserttoqlist.Key.Equals("ResponseToken") && !inserttoqlist.Key.EndsWith("A") && inserttoqlist.Key.StartsWith("C"))
                    {
                        FinalTakeSurveyObject.customquestion.Add(inserttoqlist.Value);
                    }

                }

                TempData["GrabSimpleQuestionList"] = FinalTakeSurveyObject.question;
                TempData["GrabCustomQuestionList"] = FinalTakeSurveyObject.customquestion;
            }
            return View(FinalTakeSurveyObject);
        }

        [HttpPost, ActionName("TakeSurveyFinal")]
        public ActionResult TakeSurveyFinalConfirm(TakeSurveyFinalModel mod, string SaveSurvey, string SubmitSurvey)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Document it = (Document)TempData["Item"];
            string FinalPostID = Convert.ToString(TempData["FinalPostID"]);
            string FinalPostResponseToken = Convert.ToString(TempData["FinalPostResponseToken"]);

            string tablename = "Respondent";
            if (!string.IsNullOrWhiteSpace(SaveSurvey))

            {
                mod.AnswerOptions = new List<SelectListItem>
                {
                 new SelectListItem() { Value = "Strongly Agree", Text = "Strongly Agree" },
                new SelectListItem() { Value = "Agree", Text = "Agree" },
                new SelectListItem() { Value = "Disagree", Text = "Disagree" },
                new SelectListItem() { Value = "Strongly Disagree", Text = "Strongly Disagree" }

            };


                mod.question = (List<string>)TempData["GrabSimpleQuestionList"];
                mod.customquestion = (List<string>)TempData["GrabCustomQuestionList"];

                //    foreach (var item in mod.question)
                for (int i = 0; i < mod.question.Count(); i++)
                {
                    mod.NormalQuestionsObject.Add(new NormalQuestions { qs = mod.question[i], ans = mod.answer[i] });

                }
                foreach (var itts in it)
                {
                    foreach (var itt in mod.NormalQuestionsObject)
                    {
                        if (itts.Value == itt.qs)
                        {
                            SWEN_DynamoUtilityClass.ParticipantUpdateRespondent(FinalPostID, FinalPostResponseToken, itts.Key, itt.ans);
                            var x = itts.Key;
                        }
                    }
                }
                for (int i = 0; i < mod.customquestion.Count(); i++)
                {
                    mod.CustomQuestionsClassObject.Add(new CustomQuestions { cques = mod.customquestion[i], canswer = mod.customanswer[i] });

                }
                foreach (var itts in it)
                {
                    foreach (var CustomItem in mod.CustomQuestionsClassObject)
                    {
                        if (itts.Value == CustomItem.cques)
                        {
                            SWEN_DynamoUtilityClass.ParticipantUpdateRespondent(FinalPostID, FinalPostResponseToken, itts.Key, CustomItem.canswer);
                            var x = itts.Key;
                        }
                    }
                }



            }
            if (!string.IsNullOrWhiteSpace(SubmitSurvey))
            {
                mod.AnswerOptions = new List<SelectListItem>
                {
                new SelectListItem() { Value = "Ok", Text = "Ok" },
                new SelectListItem() { Value = "Cool", Text = "Cool" },
                new SelectListItem() { Value = "This", Text = "This" },
                new SelectListItem() { Value = "That", Text = "That" }

            };


                mod.question = (List<string>)TempData["GrabSimpleQuestionList"];
                mod.customquestion = (List<string>)TempData["GrabCustomQuestionList"];

                //    foreach (var item in mod.question)
                for (int i = 0; i < mod.question.Count(); i++)
                {
                    mod.NormalQuestionsObject.Add(new NormalQuestions { qs = mod.question[i], ans = mod.answer[i] });

                }
                foreach (var itts in it)
                {
                    foreach (var itt in mod.NormalQuestionsObject)
                    {
                        if (itts.Value == itt.qs)
                        {
                            SWEN_DynamoUtilityClass.ParticipantUpdateRespondent(FinalPostID, FinalPostResponseToken, itts.Key, itt.ans);
                            var x = itts.Key;
                        }
                    }
                }
                for (int i = 0; i < mod.customquestion.Count(); i++)
                {
                    mod.CustomQuestionsClassObject.Add(new CustomQuestions { cques = mod.customquestion[i], canswer = mod.customanswer[i] });

                }
                foreach (var itts in it)
                {
                    foreach (var CustomItem in mod.CustomQuestionsClassObject)
                    {
                        if (itts.Value == CustomItem.cques)
                        {
                            SWEN_DynamoUtilityClass.ParticipantUpdateRespondent(FinalPostID, FinalPostResponseToken, itts.Key, CustomItem.canswer);
                            var x = itts.Key;
                        }
                    }
                }
                SWEN_DynamoUtilityClass.SetSurveyCompleteFlag(FinalPostID, FinalPostResponseToken, "true");//SurveyComplete
                return RedirectToAction("SurveySubmissionAck");
            }
                return View(mod);
        }

        public ActionResult SurveySubmissionAck()
        {
            return View();
        }
    }
}



