﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Emails
    {
        public string email { get; set; }
    }
    class TakeSurvey 
        {
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
        public static void Main(string[] args)
        {
            {
                List<Emails> em = new List<Emails>();
                em.Add(new Emails() { email = "jhjj@gmail.com" });
                em.Add(new Emails() { email = "jhj@gmail.com" });
                em.Add(new Emails() { email = "jj@gmail.com" });
                em.Add(new Emails() { email = "j@gmail.com" });
                foreach (Emails mail in em)
                {
                    DeploymentStepFinal("4", mail.email);
                }


            }
            Console.Read();
        }

    }
}



