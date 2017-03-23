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
                    {"O1_Q1", new AttributeValue { S = "false" } },
                    {"O1_Q2", new AttributeValue { S = "false" } },
                    {"O2", new AttributeValue { S = "false" } },
                    {"O3", new AttributeValue { S = "false" } },
                    {"CQ1", new AttributeValue { S = model.CustomQuestion1} },
                    {"CQ2", new AttributeValue { S = model.CustomQuestion2} }
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
            Table table1 = Table.LoadTable(client, "SurveyCatalog");
            //Table table2 = Table.LoadTable(client, "Objectives");
            //Table table3 = Table.LoadTable(client, "Questions");
            mod.SurveyID = Convert.ToString(TempData["ID"]);
            mod.EventName = Convert.ToString(TempData["EventName"]);
            mod.SurveyofType = Convert.ToString(TempData["Type"]);

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
                    mod.O1_Q1 = Convert.ToBoolean(document["O1_Q1"]);
                    mod.O1_Q2 = Convert.ToBoolean(document["O1_Q2"]);
                    mod.Objective2 = Convert.ToBoolean(document["O2"]);
                    mod.Objective3 = Convert.ToBoolean(document["O3"]);
                    mod.CustomQuestion1 = document["CQ1"];
                    mod.CustomQuestion2 = document["CQ2"];

                }
            } while (!search.IsDone) ;
            TempData["NewID"] = mod.SurveyID;
            TempData["NewSurveyType"] = mod.SurveyofType;
            TempData["NewEventName"] = mod.EventName;


            return View(mod);
        }


        [HttpPost, ActionName("StudentSurvey")]
        public ActionResult StudentSurveyConfirmed(StudentSurveyModel mod, string SaveSurvey, string DeploySurvey)
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
                    //// Function call above is responsible for following task 
                    ////var request = new UpdateItemRequest
                    ////{

                    ////    TableName = tablename,
                    ////    Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = mod.SurveyID } }
                    ////                 },
                    ////    ExpressionAttributeNames = new Dictionary<string, string>()
                    ////{
                    ////    {"#O1", "O1"},

                    ////},
                    ////    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                    ////{
                    ////    {":O1",new AttributeValue { S = Convert.ToString(mod.Objective1) }},
                    ////     },
                    ////    UpdateExpression = "SET #O1 = :O1"
                    ////};
                    ////var res = client.UpdateItem(request);
                    ////if (res != null)
                    ////{
                    ////    client.UpdateItem(request);
                    ////}
                }
                if (mod.O1_Q1 == true || mod.O1_Q1 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q1), "O1_Q1");
                }
                if (mod.O1_Q2 == true || mod.O1_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q2), "O1_Q2");
                }
                if (mod.Objective2 == true || mod.Objective2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective2), "O2");
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

                if (mod.O1_Q2 == true || mod.O1_Q2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.O1_Q2), "O1_Q1");
                }
                if (mod.Objective2 == true || mod.Objective2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective2), "O1_Q2");
                }
                if (mod.Objective2 == true || mod.Objective2 == false)
                {
                    SWEN_DynamoUtilityClass.UpdateDynamoDBItem(tablename, mod.SurveyID, Convert.ToString(mod.Objective2), "O2");
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

                TempData["RedirectID"] = mod.SurveyID;
                TempData["RedirectType"] = mod.SurveyofType;
                TempData["RedirectEventName"] = mod.EventName;
                return RedirectToAction("DeploySurveyStart");
            }

            return View(mod);
        }

        public ActionResult ParentSurvey(ParentSurveyModel mod)
        {

            mod.SurveyID = Convert.ToString(TempData["ID"]);
            mod.SurveyofType = Convert.ToString(TempData["Type"]);
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
            string[] RT = new string[10] { mod.ResponseToken0, mod.ResponseToken1, mod.ResponseToken2, mod.ResponseToken3, mod.ResponseToken4, mod.ResponseToken5, mod.ResponseToken6, mod.ResponseToken7, mod.ResponseToken8, mod.ResponseToken9 };
            for (int i = 0; i < 10; i++)
            {
                if (!string.IsNullOrWhiteSpace(RT[i]))
                {
                    SWEN_DynamoUtilityClass.DeploymentStepFinal(mod.SurveyID, RT[i]);
                }
            }
            return View(mod);

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
                       
                                TakeSurveylist.Add(new TakeSurveyStepTwo() { TakeSurveyID = (item["SurveyID"].S), SurveyStatus = "Submitted" });
                          
                       }
                       else 
                        {
                            TakeSurveylist.Add(new TakeSurveyStepTwo() { TakeSurveyID = (item["SurveyID"].S), SurveyStatus = "Pending" });
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
            item.TryGetValue("O1_Q1", out SurveyCompleteFlag);
            if (SurveyCompleteFlag == "True")
            {
                return Redirect("SurveySubmissionAck");
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
                SWEN_DynamoUtilityClass.SetSurveyCompleteFlag(FinalPostID, FinalPostResponseToken, "true");
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



