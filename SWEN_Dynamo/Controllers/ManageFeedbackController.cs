using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEN_Dynamo.Models;
using SWEN_Dynamo.Controllers;
using SWEN_Dynamo.App_Start;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;

namespace SWEN_Dynamo.Controllers
{
    public class ManageFeedbackController : Controller
    {
        // GET: ManageFeedback

        public ActionResult FeedbackFirst(long? id, ManageFeedbacks MF)
        {
            string Date = System.DateTime.Today.ToString("MM/dd/yyyy");
            MF.RegionOptions = new List<SelectListItem>
            {
                    new SelectListItem { Value = "AL", Text = "Alabama" },
                    new SelectListItem { Value = "AK", Text = "Alaska" },
                    new SelectListItem { Value = "AZ", Text = "Arizona" },
                    new SelectListItem { Value = "AR", Text = "Arkansas" },
                    new SelectListItem { Value = "CA", Text = "California" },
                    new SelectListItem { Value = "CO", Text = "Colorado" },
                    new SelectListItem { Value = "CT", Text = "Connecticut" },
                    new SelectListItem { Value = "DE", Text = "Delaware" },
                    new SelectListItem { Value = "FL", Text = "Florida" },
                    new SelectListItem { Value = "GA", Text = "Georgia" },
                    new SelectListItem { Value = "HI", Text = "Hawaii" },
                    new SelectListItem { Value = "ID", Text = "Idaho" },
                    new SelectListItem { Value = "IL", Text = "Illinois" },
                    new SelectListItem { Value = "IN", Text = "Indiana" },
                    new SelectListItem { Value = "IA", Text = "Iowa" },
                    new SelectListItem { Value = "KS", Text = "Kansas" },
                    new SelectListItem { Value = "KY", Text = "Kentucky" },
                    new SelectListItem { Value = "LA", Text = "Louisiana" },
                    new SelectListItem { Value = "ME", Text = "Maine" },
                    new SelectListItem { Value = "MD", Text = "Maryland" },
                    new SelectListItem { Value = "MA", Text = "Massachusetts" },
                    new SelectListItem { Value = "MI", Text = "Michigan" },
                    new SelectListItem { Value = "MN", Text = "Minnesota" },
                    new SelectListItem { Value = "MS", Text = "Mississippi" },
                    new SelectListItem { Value = "MO", Text = "Missouri" },
                    new SelectListItem { Value = "MT", Text = "Montana" },
                    new SelectListItem { Value = "NC", Text = "North Carolina" },
                    new SelectListItem { Value = "ND", Text = "North Dakota" },
                    new SelectListItem { Value = "NE", Text = "Nebraska" },
                    new SelectListItem { Value = "NV", Text = "Nevada" },
                    new SelectListItem { Value = "NH", Text = "New Hampshire" },
                    new SelectListItem { Value = "NJ", Text = "New Jersey" },
                    new SelectListItem { Value = "NM", Text = "New Mexico" },
                    new SelectListItem { Value = "NY", Text = "New York" },
                    new SelectListItem { Value = "OH", Text = "Ohio" },
                    new SelectListItem { Value = "OK", Text = "Oklahoma" },
                    new SelectListItem { Value = "OR", Text = "Oregon" },
                    new SelectListItem { Value = "PA", Text = "Pennsylvania" },
                    new SelectListItem { Value = "RI", Text = "Rhode Island" },
                    new SelectListItem { Value = "SC", Text = "South Carolina" },
                    new SelectListItem { Value = "SD", Text = "South Dakota" },
                    new SelectListItem { Value = "TN", Text = "Tennessee" },
                    new SelectListItem { Value = "TX", Text = "Texas" },
                    new SelectListItem { Value = "UT", Text = "Utah" },
                    new SelectListItem { Value = "VT", Text = "Vermont" },
                    new SelectListItem { Value = "VA", Text = "Virginia" },
                    new SelectListItem { Value = "WA", Text = "Washington" },
                    new SelectListItem { Value = "WV", Text = "West Virginia" },
                    new SelectListItem { Value = "WI", Text = "Wisconsin" },
                    new SelectListItem { Value = "WY", Text = "Wyoming" }
                };
            MF.FeedbackID = Helper.GenerateFDID();
            TempData["FeedbackID"] = MF.FeedbackID;
            MF.FLS = new List<Models.FeedbackFor>();
            MF.USID = Convert.ToString(id);
            TempData["USID"] = MF.USID;
            MF.DateofEvent = Date;
            MF.EmailID = SWEN_DynamoUtilityClass.FetchEmailfromUSID(Convert.ToInt64(MF.USID));
            MF.UserName = SWEN_DynamoUtilityClass.FetchNamefromUSID(Convert.ToInt64(MF.USID));
            TempData["UserName"] = MF.UserName;
            MF.FLS = SWEN_DynamoUtilityClass.FeedbackListPageFirst(Convert.ToInt64(MF.USID));
            MF.RID = SWEN_DynamoUtilityClass.FetchRIDfromUSID(Convert.ToInt64(MF.USID));
            TempData["RID"] = MF.RID;
            MF.ZipCode = Convert.ToString(SWEN_DynamoUtilityClass.FetchZipCodefromUSID(Convert.ToInt64(MF.USID)));
            TempData["Zip"] = MF.ZipCode;
            MF.Region = Convert.ToString(SWEN_DynamoUtilityClass.FetchRegionfromUSID(Convert.ToInt64(MF.USID)));
            TempData["Region"] = MF.Region;
            if (MF.FLS.Count() > 0)
            {
                MF.GetOnlineSurveyData = true;
                foreach (var feed in MF.FLS)
                {
                    MF.EventName = feed.EventName;
                    MF.SurveyID = feed.SurveyID;
                    MF.SurveyType = feed.SurveyType;
                }
            }
            else
            {
                MF.GetOnlineSurveyData = false; 
            }
            //   MF.XYZ = "11";
           
            return View(MF);
        }
        [HttpPost, ActionName("FeedbackFirst")]
        public ActionResult FeedbackFirstConfirmed(int? id, ManageFeedbacks MF, string Proceed)
        {
            if (!string.IsNullOrWhiteSpace(Proceed))
            {
                long USID = Convert.ToInt64(TempData["USID"]);
                string FeedbackID = Convert.ToString(TempData["FeedbackID"]);
                SWEN_DynamoUtilityClass.PushFeedIDs(Convert.ToString(USID), FeedbackID);
                MF.RegionOptions = new List<SelectListItem>
            {
                    new SelectListItem { Value = "AL", Text = "Alabama" },
                    new SelectListItem { Value = "AK", Text = "Alaska" },
                    new SelectListItem { Value = "AZ", Text = "Arizona" },
                    new SelectListItem { Value = "AR", Text = "Arkansas" },
                    new SelectListItem { Value = "CA", Text = "California" },
                    new SelectListItem { Value = "CO", Text = "Colorado" },
                    new SelectListItem { Value = "CT", Text = "Connecticut" },
                    new SelectListItem { Value = "DE", Text = "Delaware" },
                    new SelectListItem { Value = "FL", Text = "Florida" },
                    new SelectListItem { Value = "GA", Text = "Georgia" },
                    new SelectListItem { Value = "HI", Text = "Hawaii" },
                    new SelectListItem { Value = "ID", Text = "Idaho" },
                    new SelectListItem { Value = "IL", Text = "Illinois" },
                    new SelectListItem { Value = "IN", Text = "Indiana" },
                    new SelectListItem { Value = "IA", Text = "Iowa" },
                    new SelectListItem { Value = "KS", Text = "Kansas" },
                    new SelectListItem { Value = "KY", Text = "Kentucky" },
                    new SelectListItem { Value = "LA", Text = "Louisiana" },
                    new SelectListItem { Value = "ME", Text = "Maine" },
                    new SelectListItem { Value = "MD", Text = "Maryland" },
                    new SelectListItem { Value = "MA", Text = "Massachusetts" },
                    new SelectListItem { Value = "MI", Text = "Michigan" },
                    new SelectListItem { Value = "MN", Text = "Minnesota" },
                    new SelectListItem { Value = "MS", Text = "Mississippi" },
                    new SelectListItem { Value = "MO", Text = "Missouri" },
                    new SelectListItem { Value = "MT", Text = "Montana" },
                    new SelectListItem { Value = "NC", Text = "North Carolina" },
                    new SelectListItem { Value = "ND", Text = "North Dakota" },
                    new SelectListItem { Value = "NE", Text = "Nebraska" },
                    new SelectListItem { Value = "NV", Text = "Nevada" },
                    new SelectListItem { Value = "NH", Text = "New Hampshire" },
                    new SelectListItem { Value = "NJ", Text = "New Jersey" },
                    new SelectListItem { Value = "NM", Text = "New Mexico" },
                    new SelectListItem { Value = "NY", Text = "New York" },
                    new SelectListItem { Value = "OH", Text = "Ohio" },
                    new SelectListItem { Value = "OK", Text = "Oklahoma" },
                    new SelectListItem { Value = "OR", Text = "Oregon" },
                    new SelectListItem { Value = "PA", Text = "Pennsylvania" },
                    new SelectListItem { Value = "RI", Text = "Rhode Island" },
                    new SelectListItem { Value = "SC", Text = "South Carolina" },
                    new SelectListItem { Value = "SD", Text = "South Dakota" },
                    new SelectListItem { Value = "TN", Text = "Tennessee" },
                    new SelectListItem { Value = "TX", Text = "Texas" },
                    new SelectListItem { Value = "UT", Text = "Utah" },
                    new SelectListItem { Value = "VT", Text = "Vermont" },
                    new SelectListItem { Value = "VA", Text = "Virginia" },
                    new SelectListItem { Value = "WA", Text = "Washington" },
                    new SelectListItem { Value = "WV", Text = "West Virginia" },
                    new SelectListItem { Value = "WI", Text = "Wisconsin" },
                    new SelectListItem { Value = "WY", Text = "Wyoming" }
                };
                MF.UserName = Convert.ToString(TempData["UserName"]);
            string Region = null;
                string FetchSurveyID = MF.SurveyIDValueHolder;
                int TOQ1 = 0;
                int TOQ2 = 0;
                int TOQ7 = 0;
                if (MF.GetOnlineSurveyData == true)
                {
                      TOQ1 = SWEN_DynamoUtilityClass.CountFromRespondent(FetchSurveyID, "O1_Q1_A");
                     TOQ2 = SWEN_DynamoUtilityClass.CountFromRespondent(FetchSurveyID, "O1_Q2_A");
                     TOQ7 = SWEN_DynamoUtilityClass.CountFromRespondent(FetchSurveyID, "O7_Q1_A");
                }
                    string TempRegion = Convert.ToString(TempData["Region"]) ;
            string Date = System.DateTime.Today.ToString("MM/dd/yyyy");
                string ZipCode = null;
            string TempZip = Convert.ToString(TempData["Zip"]);
           
            int RID = Convert.ToInt32(TempData["RID"]);
            if(RID == 1 || RID == 2)
            {
                Region = MF.Region;
                ZipCode = MF.ZipCode;
            }
            if (RID == 3)
            {
                Region = TempRegion;
                ZipCode = MF.ZipCode;
            }
            if (RID == 4 || RID == 5)
            {
                Region = TempRegion;
                ZipCode = TempZip;
            }
            // Record Region
         
              
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();

            var request = new UpdateItemRequest
            {
                TableName = "SurveysFeedback",
                Key = new Dictionary<string, AttributeValue>() { { "USID", new AttributeValue { S = Convert.ToString(USID) } },
                                                                 { "FeedbackID", new AttributeValue{ S = FeedbackID} }
                     },
                ExpressionAttributeNames = new Dictionary<string, string>()

                 {
                    { "#RID", "RID"},
                    { "#AB", "A_Boys"},
                    { "#AG", "A_Girls"},
                    { "#NB", "N_Boys"},
                    { "#NG", "N_Girls"},
                    { "#Grade", "A Grade"},
                     { "#NS", "NSWE"},
                    { "#NO", "NOV"},
                    { "#RE", "Region"},
                    { "#ZP", "ZipCode"},
                    { "#Date", "Date of Submission"},
                    { "#TOQ1", "O1_Q1_A"},
                    { "#TOQ2", "O1_Q2_A"},
                  

                    },

                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
    {
        
                    { ":RID", new AttributeValue { S = Convert.ToString(RID) }},
                    { ":AB", new AttributeValue { S = Convert.ToString(MF.AOB) }},
                    { ":AG", new AttributeValue { S = Convert.ToString(MF.AOG) }},
                    { ":NB", new AttributeValue { S = Convert.ToString(MF.NOB) }},
                    { ":NG", new AttributeValue { S = Convert.ToString(MF.NOG) }},
                    { ":NS", new AttributeValue { S = Convert.ToString(MF.NumSWEV) }},
                    { ":NO", new AttributeValue { S = Convert.ToString(MF.NumOV) }},
                    { ":Grade", new AttributeValue { S = Convert.ToString(TOQ7) }},
                    { ":RE", new AttributeValue { S = Region }},
                    { ":ZP", new AttributeValue { S = ZipCode }},
                    { ":Date", new AttributeValue { S = Date }},
                    { ":TOQ1", new AttributeValue { S = Convert.ToString(TOQ1) }},
                    { ":TOQ2", new AttributeValue { S = Convert.ToString(TOQ2) }}
         },
                UpdateExpression = "SET #RID = :RID, #AB = :AB, #AG = :AG, #NB = :NB, #NG = :NG, #NS = :NS, #NO = :NO, #Grade = :Grade, #RE = :RE, #ZP = :ZP, #Date = :Date, #TOQ1 = :TOQ1, #TOQ2 = :TOQ2"

            };
            var res = client.UpdateItem(request);
            if (res != null)
            {
                client.UpdateItem(request);
            }
        }
        
            }

            return View("FeedbackAck");
        }
        public ActionResult Infographics(long? id, ManageFeedbacks model)
        {

            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "SurveysFeedback");


            model.USID = Convert.ToString(id);
            model.RID = SWEN_DynamoUtilityClass.FetchRIDfromUSID(Convert.ToInt64(model.USID));
            TempData["RID"] = model.RID;
            model.Region = SWEN_DynamoUtilityClass.FetchRegionfromUSID(Convert.ToInt64(model.USID));
            TempData["Region"] = model.Region;
            model.ZipCode = Convert.ToString(SWEN_DynamoUtilityClass.FetchZipCodefromUSID(Convert.ToInt64(model.USID)));
            TempData["Zip"] = model.ZipCode;
            ScanFilter scanFilter = new ScanFilter();
            if (model.RID == 1 || model.RID == 2)
            {
                scanFilter.AddCondition("USID", ScanOperator.IsNotNull);
            }
            if (model.RID == 3)
            {
                scanFilter.AddCondition("Region", ScanOperator.Equal, model.Region);
            }
            if (model.RID == 4 || model.RID ==5)
            {
                scanFilter.AddCondition("Region", ScanOperator.Equal, model.Region);
                scanFilter.AddCondition("ZipCode", ScanOperator.Equal, model.ZipCode);
            }

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,

            };

            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>(); 
            //////////////////

           

            // List<FeedbackFor> FBF = new List<FeedbackFor>();
            // string ResponseToken = "ResponseToken";
            
            {
                do
                {
                    // people.Clear();
                    documentList = search.GetNextSet();

                    foreach (var doc in documentList)
                    {
                        if (model.RID == 1 || model.RID == 2)
                        {
                            model.Infolist.Add(new ManageFeedbacks() { Region = doc["Region"], ZipCode = doc["ZipCode"], DateofEvent = doc["Date of Submission"], FeedbackID = doc["FeedbackID"] });
                        }
                        else if (model.RID == 3)
                        {
                            model.Infolist.Add(new ManageFeedbacks() { Region = model.Region, ZipCode = doc["ZipCode"], DateofEvent = doc["Date of Submission"], FeedbackID = doc["FeedbackID"] });
                        }

                       else  if (model.RID == 4 || model.RID == 5)
                        {
                            model.Infolist.Add(new ManageFeedbacks() { Region = model.Region, ZipCode = model.ZipCode, DateofEvent = doc["Date of Submission"], FeedbackID = doc["FeedbackID"] });

                        }

                        //if (SWEN_DynamoUtilityClass.CountFromRespondent(doc["SurveyID"]) > 0)
                        //{
                        //    //USID = Convert.ToInt32(doc["USID"]);
                        //    FBF.Add(new FeedbackFor() { SurveyID = doc["SurveyID"], SurveyType = doc["SurveyType"], EventName = doc["EventName"] });
                        //}
                        //else
                        //{
                        //    FBF.Add(new FeedbackFor() { SurveyID = "None", SurveyType = "None", EventName = "None" });
                        //}
                    }

                } while (!search.IsDone);


            }
          //  return FBF;

            return View(model);
        }


        public ActionResult Agree_Disagree(string id, ManageFeedbacks mod)
        {
            // retrieve date 
            // o1 number //
            //o2 number
            mod.FeedbackID = id;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "SurveysFeedback");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("FeedbackID", ScanOperator.Equal, mod.FeedbackID);
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,

            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();
            {
                do
                {
                    // people.Clear();
                    documentList = search.GetNextSet();

                    foreach (var doc in documentList)
                    {
                        mod.Agree_List.Add(new Agree_Disagree { Q1 = 23/* Convert.ToInt32(doc["O1_Q1_A"])*/, Q2 = 22/*Convert.ToInt32(doc["O1_Q2_A"])*/});
                    }
                } while (!search.IsDone);
            } 

            var output = JsonConvert.SerializeObject(mod.Agree_List);
            ViewData["Check"] = output;
            return View(mod);
        }





             public ActionResult A_Grade(long? id)
        {

            return View();
        }
             public ActionResult Number(long? id)
        {

            return View();
        }
             public ActionResult Age(long? id)
        {

            return View();
        }

    }
}