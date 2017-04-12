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
            TempData["FeedbackID"] = MF.FeedbackID;
            MF.FLS = new List<Models.FeedbackFor>();
            MF.USID = Convert.ToString(id);
            TempData["USID"] = MF.USID;
            MF.DateofEvent = Date;
            MF.EmailID = SWEN_DynamoUtilityClass.FetchEmailfromUSID(Convert.ToInt64(MF.USID));
            MF.UserName = SWEN_DynamoUtilityClass.FetchNamefromUSID(Convert.ToInt64(MF.USID));
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
            string Region = null;
            string TempRegion = Convert.ToString(TempData["Region"]) ;
            string Date = System.DateTime.Today.ToString("MM/dd/yyyy");
                string ZipCode = null;
            string TempZip = Convert.ToString(TempData["Zip"]);
            long USID = Convert.ToInt64(TempData["USID"]);
            string FeedbackID = Convert.ToString(TempData["FeedbackID"]);
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
                    { "#RE", "Region"},
                    { "#ZP", "ZipCode"},
                    { "#Date", "Date of Submission"},
                    { "#Total", "Number of Responses"},

                    },

                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
    {
        
        { ":RID", new AttributeValue { S = Convert.ToString(RID) }},
                    { ":AB", new AttributeValue { S = Convert.ToString(RID) }},
                    { ":AG", new AttributeValue { S = Convert.ToString(RID) }},
                    { ":NB", new AttributeValue { S = Convert.ToString(RID) }},
                    { ":NG", new AttributeValue { S = Convert.ToString(RID) }},
                    { ":Grade", new AttributeValue { S = Convert.ToString(RID) }},
                    { ":RE", new AttributeValue { S = Region }},
                    { ":ZP", new AttributeValue { S = ZipCode }},
                    { ":Date", new AttributeValue { S = Date }},
                    { ":Total", new AttributeValue { S = Convert.ToString(RID) }},
         },
                UpdateExpression = "SET #FN = :FN, #EN = :EN"

            };
            var res = client.UpdateItem(request);
            if (res != null)
            {
                client.UpdateItem(request);
            }
        }
        string FetchSurveyID = MF.SurveyIDValueHolder;
            }

            return View();
        }
        public ActionResult Infographics(ManageFeedbacks model)
        {
            return View();
        }


    }
}