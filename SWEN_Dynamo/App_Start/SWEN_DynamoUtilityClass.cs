﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using SWEN_Dynamo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SWEN_Dynamo.App_Start
{
    public static class SWEN_DynamoUtilityClass
    {
        public static int USIDvalue;
        public static void UpdateDynamoDBItem(string tablename, string ID, string objectivenumber, string dbitem)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            var request = new UpdateItemRequest
            {

                TableName = tablename,
                Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = ID } }
                                 },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#"+dbitem, dbitem},

                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":"+dbitem,new AttributeValue { S = Convert.ToString(objectivenumber) }},
                     },
                UpdateExpression = "SET #"+dbitem+" = :"+dbitem
            };
            var res = client.UpdateItem(request);
            if (res != null)
            {
                client.UpdateItem(request);
            }
        }

        public static void UpdateRespondent(string RS_SurveyID, string ResponseToken, string QuestionID, string Question)
        {
            var client = new AmazonDynamoDBClient();
            string tableName = "Respondent";
            var request = new UpdateItemRequest
            {

                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = RS_SurveyID } }, { "ResponseToken", new AttributeValue { S = ResponseToken } } },
                ExpressionAttributeNames = new Dictionary<string, string>()
    {
        {"#O", QuestionID}, {"#O_A",QuestionID+ "_A"}
    },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
    {
        {":O",new AttributeValue { S = Question}},
        {":O_A",new AttributeValue { S = " "}}

    },

                UpdateExpression = "SET #O = :O, #O_A = :O_A "
            };
            var respons = client.UpdateItem(request);

        }
        public static Document DeploymentStepFirst(string SC_SurveyID)
        {
            var client = new AmazonDynamoDBClient();
            var SCtable = Table.LoadTable(client, "SurveyCatalog");
            var item = SCtable.GetItem(SC_SurveyID);
            return item;
        }
        public static void DeploymentStepSecond(string RS_SurveyID, string ResponseToken)
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "Respondent");
            string tableName = "Respondent";
            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = new Dictionary<string, AttributeValue>()
              {
                  { "SurveyID", new AttributeValue { S = RS_SurveyID } },
                  { "ResponseToken", new AttributeValue { S = ResponseToken } }
              }
            };
            client.PutItem(request);
        }

        public static string DeploymentStepThird(string QDBQID, string QDBSurveyType)
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "QuestionsDB");
            var QuestionItem = table.GetItem(QDBQID, QDBSurveyType);
            string Question = QuestionItem["Question"];
            return Question;

        }
        public static string DeploymentStepThirdCustom(string SCID, string CQ)
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "SurveyCatalog");
            var QuestionItem = table.GetItem(SCID);
            string Question = QuestionItem[CQ];
            return Question;
        }
        public static void DeploymentStepFive(string FinalSurveyID, string FinalResponseToken, string O_Q)
        {
            Document t = DeploymentStepFirst(FinalSurveyID); // Got the Survey complete survey information in t.

            string ST = t["SurveyType"];
            string S_O_Q = O_Q;
            if (S_O_Q.StartsWith("O"))
            {
                bool OQ = Convert.ToBoolean(t[S_O_Q]);
                if (OQ == true)
                {
                    string Question = DeploymentStepThird(S_O_Q, ST);
                    UpdateRespondent(FinalSurveyID, FinalResponseToken, S_O_Q, Question);
                    Console.WriteLine(Question);
                }
            }
            if (S_O_Q.StartsWith("CQ"))
            {
                if (t[S_O_Q] != "Null")
                {
                    string Question = DeploymentStepThirdCustom(FinalSurveyID, S_O_Q);
                    UpdateRespondent(FinalSurveyID, FinalResponseToken, S_O_Q, Question);
                    Console.WriteLine(Question);
                }
            }



        }
        public static void DeploymentStepSixth(string FinalSurveyID, string FinalResponseToken)
        {
            string[] PickQuestion = new string[3] { "O1_Q1", "O1_Q2", "CQ1" };
            for (int i = 0; i < 3; i++)
            {
                DeploymentStepFive(FinalSurveyID, FinalResponseToken, PickQuestion[i]);
            }

        }
        public static void DeploymentStepFinal(string FinalSurveyID, string FinalResponseToken)
        {
            DeploymentStepSecond(FinalSurveyID, FinalResponseToken);
            DeploymentStepSixth(FinalSurveyID, FinalResponseToken);
        }
        public static void ParticipantUpdateRespondent(string SurveyID, string ResToken, string keytoupdate, string answervalue)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            
                        var request = new UpdateItemRequest
                        {
                            TableName = "Respondent",
                            Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = SurveyID } }, {"ResponseToken", new AttributeValue{ S = ResToken } }
                     },
                            ExpressionAttributeNames = new Dictionary<string, string>()

                 {
                    { "#FN", keytoupdate+"_A"}
                    },
                            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
    {
        {":FN",new AttributeValue { S = answervalue }},
         },
                            UpdateExpression = "SET #FN = :FN"

                        };
                        var res = client.UpdateItem(request);
                        if (res != null)
                        {
                            client.UpdateItem(request);
                        }
                    }
        public static void SetSurveyCompleteFlag(string SurveyID, string ResToken, string answervalue)
        {
             {
                AmazonDynamoDBClient client = new AmazonDynamoDBClient();

                var request = new UpdateItemRequest
                {
                    TableName = "Respondent",
                    Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = SurveyID } }, {"ResponseToken", new AttributeValue{ S = ResToken } }
                     },
                    ExpressionAttributeNames = new Dictionary<string, string>()

                 {
                    { "#FN", "SurveyComplete"}
                    },
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
    {
        {":FN",new AttributeValue { S = answervalue }},
         },
                    UpdateExpression = "SET #FN = :FN"

                };
                var res = client.UpdateItem(request);
                if (res != null)
                {
                    client.UpdateItem(request);
                }
            }
        }
        public static long FetchUSIDfromEmail (string Email)
        {
            long USID ;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("Email", ScanOperator.Equal, Email);

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "USID" }
            };
            Search search = table.Scan(config);
            List<long> USIDList = new List<long>();
            List<Document> documentList = new List<Document>();
            do
            {
                // people.Clear();
                documentList = search.GetNextSet();
                Console.WriteLine(documentList);
                foreach (var doc in documentList)
                {
                    USID = Convert.ToInt64(doc["USID"]);
                    USIDList.Add(USID);
                 
                  
                }

            } while (!search.IsDone);
            return USIDList[0];


        }
        public static string FetchEmailfromUSID(long USID)
        {
            string Email;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.Equal, USID);

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "Email" }
            };
            Search search = table.Scan(config);
            List<string> EmailList = new List<string>();
            List<Document> documentList = new List<Document>();
            do
            {
                // people.Clear();
                documentList = search.GetNextSet();
                Console.WriteLine(documentList);
                foreach (var doc in documentList)
                {
                    Email = Convert.ToString(doc["Email"]);
                    EmailList.Add(Email);
                    
                }

            } while (!search.IsDone);
            return EmailList[0];


        }
        public static string FetchNamefromUSID(long USID)
        {
            string First;
            string Last;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.Equal, USID);

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "FirstName", "LastName" }
            };
            Search search = table.Scan(config);
            List<string> NameList = new List<string>();
            List<Document> documentList = new List<Document>();
            do
            {
                // people.Clear();
                documentList = search.GetNextSet();
                Console.WriteLine(documentList);
                foreach (var doc in documentList)
                {
                    First = Convert.ToString(doc["FirstName"]);
                    Last = Convert.ToString(doc["LastName"]);
                    NameList.Add(First + " " + Last);

                }

            } while (!search.IsDone);
            return NameList[0];


        }
        public static List<FeedbackFor> FeedbackListPageFirst(long USID)
        {

            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "SurveyCatalog");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.Equal, USID);

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "EventName", "SurveyID", "SurveyType" }
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();
            List<FeedbackFor> FBF = new List<FeedbackFor>();


            do
            {
                // people.Clear();
                documentList = search.GetNextSet();

                foreach (var doc in documentList)
                {
                    //USID = Convert.ToInt32(doc["USID"]);
                    FBF.Add(new FeedbackFor() { SurveyID = doc["SurveyID"], SurveyType = doc["SurveyType"], EventName = doc["EventName"] });

                }

            } while (!search.IsDone);

            return FBF;


        }

    }

    //public class FeedbackFor
    //{
    //    public string SurveyType { get; set; }
    //    public string SurveyID { get; set; }
    //    public string EventName { get; set; }

    //}
}