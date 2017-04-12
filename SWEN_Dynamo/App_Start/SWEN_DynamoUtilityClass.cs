using Amazon.DynamoDBv2;
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

        public static int FetchRIDfromUSID(long USID)
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "User");
            var RIDItem = table.GetItem(USID);
            int RID = Convert.ToInt32(RIDItem["RID"]);
            return RID;
        }

        public static int FetchZipCodefromUSID(long USID)
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "User");
            var ZipCodeItem = table.GetItem(USID);
            int ZipCode = Convert.ToInt32(ZipCodeItem["ZipCode"]);
            return ZipCode;
        }

        public static string FetchRegionfromUSID(long USID)
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "User");
            var RegionItem = table.GetItem(USID);
            string Region = RegionItem["Region"];
            return Region;
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
                   // Console.WriteLine(Question);
                }
            }
            if (S_O_Q.StartsWith("CQ"))
            {
                if (t[S_O_Q] != "Null")
                {
                    string Question = DeploymentStepThirdCustom(FinalSurveyID, S_O_Q);
                    UpdateRespondent(FinalSurveyID, FinalResponseToken, S_O_Q, Question);
                    //Console.WriteLine(Question);
                }
            }



        }
        public static void DeploymentStepSixth(string FinalSurveyID, string FinalResponseToken)
        {
            string[] PickQuestion = new string[40]
            {   "O1_Q1", "O1_Q2","O1_Q3","O1_Q4", "O1_Q5", "O1_Q6",
                "O2_Q1", "O2_Q2","O2_Q3","O2_Q4", "O2_Q5", "O2_Q6",
                "O3_Q1",
                "O4_Q1","O4_Q2",
               "O5_Q1", "O5_Q2","O5_Q3",
               "O6_Q1",
               "O7_Q1","O7_Q2","O7_Q3","O7_Q4","O7_Q5",
               "O8_Q1","O8_Q2","O8_Q3",
               "O9_Q1", "O9_Q2","O9_Q3","O9_Q4",
               "O10_Q1", "O10_Q2","O10_Q3","O10_Q4",
                "CQ1",
            "CQ2",
            "CQ3",
            "CQ4",
            "CQ5" };
            for (int i = 0; i < 40; i++)
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
                    { "#FN", keytoupdate+"_A"},
                    { "#EN", "EventName"},

                    },

                            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
    {
        {":FN",new AttributeValue { S = answervalue }},
        {":EN",new AttributeValue { S = SWEN_DynamoUtilityClass.FetchEventNameFromSID(SurveyID) }}
         },
                            UpdateExpression = "SET #FN = :FN, #EN = :EN"

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
               // Console.WriteLine(documentList);
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
            string ResponseToken = "ResponseToken";

            do
            {
                // people.Clear();
                documentList = search.GetNextSet();

                foreach (var doc in documentList)
                {
                    if(SWEN_DynamoUtilityClass.CountFromRespondent(doc["SurveyID"]) > 0)
                        {
                        //USID = Convert.ToInt32(doc["USID"]);
                        FBF.Add(new FeedbackFor() { SurveyID = doc["SurveyID"], SurveyType = doc["SurveyType"], EventName = doc["EventName"] });
                    }
                    //else
                    //{
                    //    FBF.Add(new FeedbackFor() { SurveyID = "None", SurveyType = "None", EventName = "None" });
                    //}
                }

            } while (!search.IsDone);

            return FBF;


        }
        public static bool CheckEmailWRTUSID (long USID, string Email)
        {

            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.GreaterThan, 0);
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "USID" }
            };
            Search search = table.Scan(config);
            List<long> USIDS = new List<long>();
            List<Document> documentList = new List<Document>();
            do
            {
                // people.Clear();
                documentList = search.GetNextSet();
                foreach(var d in documentList)
                {
                    USIDS.Add(Convert.ToInt64(d["USID"]));
                }
            } while (!search.IsDone);
            if (USIDS.Contains(USID))
            {
                var EmailItem = table.GetItem(USID);
                string CheckEmail = EmailItem["Email"];
                if (CheckEmail == Email)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public static int CheckRIDwithUSID (long USID)
        {
             
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "User");
            var RIDItem = table.GetItem(Convert.ToInt64(USID));
            int RID = Convert.ToInt32(RIDItem["RID"]);
            return RID;
        
    }

        public static string SurveyEditorNavigation(string SID)
        {

            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "SurveyCatalog");
            var SurveyItem = table.GetItem(Convert.ToString(SID));
            string Stype = Convert.ToString(SurveyItem["SurveyType"]);
            return Stype;

        }
        public static string FetchEventNameFromSID (string SID)
        {
            string EventName;
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "SurveyCatalog");
            var SurveyItem = table.GetItem(Convert.ToString(SID));
            //if ()
            //{
            //    EventName = "Expired Event";
                
            //}
            //else
            //{
                EventName = Convert.ToString(SurveyItem["EventName"]);
           // }
                return EventName;

        }

        public static long FetchUSIDFromSurveyID(string SID)
        {

            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "SurveyCatalog");
            var SurveyItem = table.GetItem(Convert.ToString(SID));
            long USID = Convert.ToInt64(SurveyItem["USID"]);
            return USID;

        }


        ///

        public static int CountFromRespondent(string SID, string KeyName = "ResponseToken")
        {
            int number_of_keys = 0;
            string Total;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "Respondent");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("SurveyID", ScanOperator.Equal, SID);
            scanFilter.AddCondition("SurveyComplete", ScanOperator.Equal, "true");
            if (KeyName == "O1_Q1_A" || KeyName == "O1_Q2_A" || KeyName == "O7_Q1_A")
            {
                scanFilter.AddCondition(KeyName, ScanOperator.Equal, "Strongly Agree");
                scanFilter.AddCondition(KeyName, ScanOperator.Equal, "Agree");
            }
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { KeyName }
            };
            Search search = table.Scan(config);
            List<string> ReskeyList = new List<string>();
            List<Document> documentList = new List<Document>();
            do
            {
                // people.Clear();
                documentList = search.GetNextSet();
                Console.WriteLine(documentList);
                foreach (var doc in documentList)
                {
                    Total = Convert.ToString(doc[KeyName]);
                    ReskeyList.Add(Total);
                }
            } while (!search.IsDone);

            number_of_keys = ReskeyList.Count();
            return number_of_keys;


        }

    }

  
}




///////////////////////


//public static string ScanKeysForExistence(string nameoftable, string attributename, string attributevalue)
//{
//    string Email;
//    AmazonDynamoDBClient client = new AmazonDynamoDBClient();
//    Table table = Table.LoadTable(client, nameoftable);
//    ScanFilter scanFilter = new ScanFilter();
    
//    scanFilter.AddCondition(attributename, ScanOperator.Contains, attributevalue);

//    ScanOperationConfig config = new ScanOperationConfig()
//    {
//        Filter = scanFilter,
//        Select = SelectValues.AllAttributes,
//    };
//    Search search = table.Scan(config);
//   // List<string> EmailList = new List<string>();
//    List<Document> documentList = new List<Document>();
//    do
//    {
//        // people.Clear();
//        documentList = search.GetNextSet();
//        // Console.WriteLine(documentList);
       

//    } while (!search.IsDone);
//   if(documentList.)


//}