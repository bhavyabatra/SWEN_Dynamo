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

           MF.SectionN = new List<SelectListItem>

            {

                new SelectListItem { Value = "1", Text = "1" },
 new SelectListItem { Value = "2", Text = "2" },
 new SelectListItem { Value = "3", Text = "3" },
 new SelectListItem { Value = "4", Text = "4" },
 new SelectListItem { Value = "5", Text = "5" },
 new SelectListItem { Value = "6", Text = "6" },
 new SelectListItem { Value = "7", Text = "7" },
 new SelectListItem { Value = "8", Text = "8" },
 new SelectListItem { Value = "9", Text = "9" },
 new SelectListItem { Value = "10", Text = "10" },
 new SelectListItem { Value = "11", Text = "11" },
 new SelectListItem { Value = "12", Text = "12" },
 new SelectListItem { Value = "13", Text = "13" },
 new SelectListItem { Value = "14", Text = "14" },
 new SelectListItem { Value = "15", Text = "15" },
 new SelectListItem { Value = "16", Text = "16" },
 new SelectListItem { Value = "17", Text = "17" },
 new SelectListItem { Value = "18", Text = "18" },
 new SelectListItem { Value = "19", Text = "19" },
 new SelectListItem { Value = "20", Text = "20" },
 new SelectListItem { Value = "21", Text = "21" },
 new SelectListItem { Value = "22", Text = "22" },
 new SelectListItem { Value = "23", Text = "23" },
 new SelectListItem { Value = "24", Text = "24" },
 new SelectListItem { Value = "25", Text = "25" },
 new SelectListItem { Value = "26", Text = "26" },
 new SelectListItem { Value = "27", Text = "27" },
 new SelectListItem { Value = "28", Text = "28" },
 new SelectListItem { Value = "29", Text = "29" },
 new SelectListItem { Value = "30", Text = "30" },
 new SelectListItem { Value = "31", Text = "31" },
 new SelectListItem { Value = "32", Text = "32" },
 new SelectListItem { Value = "33", Text = "33" },
 new SelectListItem { Value = "34", Text = "34" },
 new SelectListItem { Value = "35", Text = "35" },
 new SelectListItem { Value = "36", Text = "36" },
 new SelectListItem { Value = "37", Text = "37" },
 new SelectListItem { Value = "38", Text = "38" },
 new SelectListItem { Value = "39", Text = "39" },
 new SelectListItem { Value = "40", Text = "40" },
 new SelectListItem { Value = "41", Text = "41" },
 new SelectListItem { Value = "42", Text = "42" },
 new SelectListItem { Value = "43", Text = "43" },
 new SelectListItem { Value = "44", Text = "44" },
 new SelectListItem { Value = "45", Text = "45" },
 new SelectListItem { Value = "46", Text = "46" },
 new SelectListItem { Value = "47", Text = "47" },
 new SelectListItem { Value = "48", Text = "48" },
 new SelectListItem { Value = "49", Text = "49" },
 new SelectListItem { Value = "50", Text = "50" },
 new SelectListItem { Value = "51", Text = "51" },
 new SelectListItem { Value = "52", Text = "52" },
 new SelectListItem { Value = "53", Text = "53" },
 new SelectListItem { Value = "54", Text = "54" },
 new SelectListItem { Value = "55", Text = "55" },
 new SelectListItem { Value = "56", Text = "56" },
 new SelectListItem { Value = "57", Text = "57" },
 new SelectListItem { Value = "58", Text = "58" },
 new SelectListItem { Value = "59", Text = "59" },
 new SelectListItem { Value = "60", Text = "60" },
 new SelectListItem { Value = "61", Text = "61" },
 new SelectListItem { Value = "62", Text = "62" },
 new SelectListItem { Value = "63", Text = "63" },
 new SelectListItem { Value = "64", Text = "64" },
 new SelectListItem { Value = "65", Text = "65" },
 new SelectListItem { Value = "66", Text = "66" },
 new SelectListItem { Value = "67", Text = "67" },
 new SelectListItem { Value = "68", Text = "68" },
 new SelectListItem { Value = "69", Text = "69" },
 new SelectListItem { Value = "70", Text = "70" },
 new SelectListItem { Value = "71", Text = "71" },
 new SelectListItem { Value = "72", Text = "72" },
 new SelectListItem { Value = "73", Text = "73" },
 new SelectListItem { Value = "74", Text = "74" },
 new SelectListItem { Value = "75", Text = "75" },
 new SelectListItem { Value = "76", Text = "76" },
 new SelectListItem { Value = "77", Text = "77" },
 new SelectListItem { Value = "78", Text = "78" },
 new SelectListItem { Value = "79", Text = "79" },
 new SelectListItem { Value = "80", Text = "80" },
 new SelectListItem { Value = "81", Text = "81" },
 new SelectListItem { Value = "82", Text = "82" },
 new SelectListItem { Value = "83", Text = "83" },
 new SelectListItem { Value = "84", Text = "84" },
 new SelectListItem { Value = "85", Text = "85" },
 new SelectListItem { Value = "86", Text = "86" },
 new SelectListItem { Value = "87", Text = "87" },
 new SelectListItem { Value = "88", Text = "88" },
 new SelectListItem { Value = "89", Text = "89" },
 new SelectListItem { Value = "90", Text = "90" },
 new SelectListItem { Value = "91", Text = "91" },
 new SelectListItem { Value = "92", Text = "92" },
 new SelectListItem { Value = "93", Text = "93" },
 new SelectListItem { Value = "94", Text = "94" },
 new SelectListItem { Value = "95", Text = "95" },
 new SelectListItem { Value = "96", Text = "96" },
 new SelectListItem { Value = "97", Text = "97" },
 new SelectListItem { Value = "98", Text = "98" },
 new SelectListItem { Value = "99", Text = "99" },
 new SelectListItem { Value = "100", Text = "100" },
 new SelectListItem { Value = "101", Text = "101" },
 new SelectListItem { Value = "102", Text = "102" },
 new SelectListItem { Value = "103", Text = "103" },
 new SelectListItem { Value = "104", Text = "104" },
 new SelectListItem { Value = "105", Text = "105" },
 new SelectListItem { Value = "106", Text = "106" },
 new SelectListItem { Value = "107", Text = "107" },
 new SelectListItem { Value = "108", Text = "108" },
 new SelectListItem { Value = "109", Text = "109" },
 new SelectListItem { Value = "110", Text = "110" },
 new SelectListItem { Value = "111", Text = "111" },
 new SelectListItem { Value = "112", Text = "112" },
 new SelectListItem { Value = "113", Text = "113" },
 new SelectListItem { Value = "114", Text = "114" },
 new SelectListItem { Value = "115", Text = "115" },
 new SelectListItem { Value = "116", Text = "116" },
 new SelectListItem { Value = "117", Text = "117" },
 new SelectListItem { Value = "118", Text = "118" },
 new SelectListItem { Value = "119", Text = "119" },
 new SelectListItem { Value = "120", Text = "120" },
 new SelectListItem { Value = "121", Text = "121" },
 new SelectListItem { Value = "122", Text = "122" },
 new SelectListItem { Value = "123", Text = "123" },
 new SelectListItem { Value = "124", Text = "124" },
 new SelectListItem { Value = "125", Text = "125" },
 new SelectListItem { Value = "126", Text = "126" },
 new SelectListItem { Value = "127", Text = "127" },
 new SelectListItem { Value = "128", Text = "128" },
 new SelectListItem { Value = "129", Text = "129" },
 new SelectListItem { Value = "130", Text = "130" },
                                                
        }; 
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

            string SecL = Convert.ToString(MF.SectionL);
            //   MF.XYZ = "11";

            return View(MF);
        }
        [HttpPost, ActionName("FeedbackFirst")]
        public ActionResult FeedbackFirstConfirmed(int? id, ManageFeedbacks MF, string Proceed)
        {
            if (!string.IsNullOrWhiteSpace(Proceed))
            {
                string SecL = Convert.ToString(MF.SectionL);
                //for (int i = 1; i <= 130; i++)
                //{
                //    MF.SectionN.Add(i);
                //}

                MF.SectionN = new List<SelectListItem>

            {

                new SelectListItem { Value = "1", Text = "1" },
 new SelectListItem { Value = "2", Text = "2" },
 new SelectListItem { Value = "3", Text = "3" },
 new SelectListItem { Value = "4", Text = "4" },
 new SelectListItem { Value = "5", Text = "5" },
 new SelectListItem { Value = "6", Text = "6" },
 new SelectListItem { Value = "7", Text = "7" },
 new SelectListItem { Value = "8", Text = "8" },
 new SelectListItem { Value = "9", Text = "9" },
 new SelectListItem { Value = "10", Text = "10" },
 new SelectListItem { Value = "11", Text = "11" },
 new SelectListItem { Value = "12", Text = "12" },
 new SelectListItem { Value = "13", Text = "13" },
 new SelectListItem { Value = "14", Text = "14" },
 new SelectListItem { Value = "15", Text = "15" },
 new SelectListItem { Value = "16", Text = "16" },
 new SelectListItem { Value = "17", Text = "17" },
 new SelectListItem { Value = "18", Text = "18" },
 new SelectListItem { Value = "19", Text = "19" },
 new SelectListItem { Value = "20", Text = "20" },
 new SelectListItem { Value = "21", Text = "21" },
 new SelectListItem { Value = "22", Text = "22" },
 new SelectListItem { Value = "23", Text = "23" },
 new SelectListItem { Value = "24", Text = "24" },
 new SelectListItem { Value = "25", Text = "25" },
 new SelectListItem { Value = "26", Text = "26" },
 new SelectListItem { Value = "27", Text = "27" },
 new SelectListItem { Value = "28", Text = "28" },
 new SelectListItem { Value = "29", Text = "29" },
 new SelectListItem { Value = "30", Text = "30" },
 new SelectListItem { Value = "31", Text = "31" },
 new SelectListItem { Value = "32", Text = "32" },
 new SelectListItem { Value = "33", Text = "33" },
 new SelectListItem { Value = "34", Text = "34" },
 new SelectListItem { Value = "35", Text = "35" },
 new SelectListItem { Value = "36", Text = "36" },
 new SelectListItem { Value = "37", Text = "37" },
 new SelectListItem { Value = "38", Text = "38" },
 new SelectListItem { Value = "39", Text = "39" },
 new SelectListItem { Value = "40", Text = "40" },
 new SelectListItem { Value = "41", Text = "41" },
 new SelectListItem { Value = "42", Text = "42" },
 new SelectListItem { Value = "43", Text = "43" },
 new SelectListItem { Value = "44", Text = "44" },
 new SelectListItem { Value = "45", Text = "45" },
 new SelectListItem { Value = "46", Text = "46" },
 new SelectListItem { Value = "47", Text = "47" },
 new SelectListItem { Value = "48", Text = "48" },
 new SelectListItem { Value = "49", Text = "49" },
 new SelectListItem { Value = "50", Text = "50" },
 new SelectListItem { Value = "51", Text = "51" },
 new SelectListItem { Value = "52", Text = "52" },
 new SelectListItem { Value = "53", Text = "53" },
 new SelectListItem { Value = "54", Text = "54" },
 new SelectListItem { Value = "55", Text = "55" },
 new SelectListItem { Value = "56", Text = "56" },
 new SelectListItem { Value = "57", Text = "57" },
 new SelectListItem { Value = "58", Text = "58" },
 new SelectListItem { Value = "59", Text = "59" },
 new SelectListItem { Value = "60", Text = "60" },
 new SelectListItem { Value = "61", Text = "61" },
 new SelectListItem { Value = "62", Text = "62" },
 new SelectListItem { Value = "63", Text = "63" },
 new SelectListItem { Value = "64", Text = "64" },
 new SelectListItem { Value = "65", Text = "65" },
 new SelectListItem { Value = "66", Text = "66" },
 new SelectListItem { Value = "67", Text = "67" },
 new SelectListItem { Value = "68", Text = "68" },
 new SelectListItem { Value = "69", Text = "69" },
 new SelectListItem { Value = "70", Text = "70" },
 new SelectListItem { Value = "71", Text = "71" },
 new SelectListItem { Value = "72", Text = "72" },
 new SelectListItem { Value = "73", Text = "73" },
 new SelectListItem { Value = "74", Text = "74" },
 new SelectListItem { Value = "75", Text = "75" },
 new SelectListItem { Value = "76", Text = "76" },
 new SelectListItem { Value = "77", Text = "77" },
 new SelectListItem { Value = "78", Text = "78" },
 new SelectListItem { Value = "79", Text = "79" },
 new SelectListItem { Value = "80", Text = "80" },
 new SelectListItem { Value = "81", Text = "81" },
 new SelectListItem { Value = "82", Text = "82" },
 new SelectListItem { Value = "83", Text = "83" },
 new SelectListItem { Value = "84", Text = "84" },
 new SelectListItem { Value = "85", Text = "85" },
 new SelectListItem { Value = "86", Text = "86" },
 new SelectListItem { Value = "87", Text = "87" },
 new SelectListItem { Value = "88", Text = "88" },
 new SelectListItem { Value = "89", Text = "89" },
 new SelectListItem { Value = "90", Text = "90" },
 new SelectListItem { Value = "91", Text = "91" },
 new SelectListItem { Value = "92", Text = "92" },
 new SelectListItem { Value = "93", Text = "93" },
 new SelectListItem { Value = "94", Text = "94" },
 new SelectListItem { Value = "95", Text = "95" },
 new SelectListItem { Value = "96", Text = "96" },
 new SelectListItem { Value = "97", Text = "97" },
 new SelectListItem { Value = "98", Text = "98" },
 new SelectListItem { Value = "99", Text = "99" },
 new SelectListItem { Value = "100", Text = "100" },
 new SelectListItem { Value = "101", Text = "101" },
 new SelectListItem { Value = "102", Text = "102" },
 new SelectListItem { Value = "103", Text = "103" },
 new SelectListItem { Value = "104", Text = "104" },
 new SelectListItem { Value = "105", Text = "105" },
 new SelectListItem { Value = "106", Text = "106" },
 new SelectListItem { Value = "107", Text = "107" },
 new SelectListItem { Value = "108", Text = "108" },
 new SelectListItem { Value = "109", Text = "109" },
 new SelectListItem { Value = "110", Text = "110" },
 new SelectListItem { Value = "111", Text = "111" },
 new SelectListItem { Value = "112", Text = "112" },
 new SelectListItem { Value = "113", Text = "113" },
 new SelectListItem { Value = "114", Text = "114" },
 new SelectListItem { Value = "115", Text = "115" },
 new SelectListItem { Value = "116", Text = "116" },
 new SelectListItem { Value = "117", Text = "117" },
 new SelectListItem { Value = "118", Text = "118" },
 new SelectListItem { Value = "119", Text = "119" },
 new SelectListItem { Value = "120", Text = "120" },
 new SelectListItem { Value = "121", Text = "121" },
 new SelectListItem { Value = "122", Text = "122" },
 new SelectListItem { Value = "123", Text = "123" },
 new SelectListItem { Value = "124", Text = "124" },
 new SelectListItem { Value = "125", Text = "125" },
 new SelectListItem { Value = "126", Text = "126" },
 new SelectListItem { Value = "127", Text = "127" },
 new SelectListItem { Value = "128", Text = "128" },
 new SelectListItem { Value = "129", Text = "129" },
 new SelectListItem { Value = "130", Text = "130" },

        };

                MF.MALCode = SecL + MF.SectionNum;
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
                    { "#MAL", "MAL Code"}


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
                    { ":TOQ2", new AttributeValue { S = Convert.ToString(TOQ2) }},
                    { ":MAL", new AttributeValue { S = Convert.ToString(MF.MALCode) }}
         },
                UpdateExpression = "SET #RID = :RID, #AB = :AB, #AG = :AG, #NB = :NB, #NG = :NG, #NS = :NS, #NO = :NO, #Grade = :Grade, #RE = :RE, #ZP = :ZP, #Date = :Date, #TOQ1 = :TOQ1, #TOQ2 = :TOQ2, #MAL = :MAL"

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
                            model.Infolist.Add(new ManageFeedbacks() { Region = doc["Region"], ZipCode = doc["ZipCode"], DateofEvent = doc["Date of Submission"], FeedbackID = doc["FeedbackID"], MALCode = doc["MAL Code"] });
                        }
                        else if (model.RID == 3)
                        {
                            model.Infolist.Add(new ManageFeedbacks() { Region = model.Region, ZipCode = doc["ZipCode"], DateofEvent = doc["Date of Submission"], FeedbackID = doc["FeedbackID"], MALCode = doc["MAL Code"] });
                        }

                       else  if (model.RID == 4 || model.RID == 5)
                        {
                            model.Infolist.Add(new ManageFeedbacks() { Region = model.Region, ZipCode = model.ZipCode, DateofEvent = doc["Date of Submission"], FeedbackID = doc["FeedbackID"], MALCode = doc["MAL Code"] });

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
                        mod.Agree_List.Add(new Agree_Disagree { Q1 = Convert.ToInt32(doc["O1_Q1_A"]), Q2 = Convert.ToInt32(doc["O1_Q2_A"])});

                     
                    }
                } while (!search.IsDone);
                
            }

            mod.Ex = JsonConvert.SerializeObject(mod.Agree_List);
            mod.Ag_Dis = Json(mod.Agree_List);
             return View(mod);
        }





             public ActionResult A_Grade(string id, ManageFeedbacks model)
        {
            model.FeedbackID = id;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "SurveysFeedback");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("FeedbackID", ScanOperator.Equal, model.FeedbackID);
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
                        model.A_G.Add(new A_Grade { Grade_A = Convert.ToInt32(doc["A Grade"]) });
                    }
                } while (!search.IsDone);

            }

            model.Ex = JsonConvert.SerializeObject(model.A_G);
          //  model.A_G = Json(model.A_G);
            return View(model);
           // return View();
        }
             public ActionResult Number(string id, ManageFeedbacks mod)
        {

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
                        mod.Agree_List.Add(new Agree_Disagree { Q1 = Convert.ToInt32(doc["N_Boys"]), Q2 = Convert.ToInt32(doc["N_Girls"]) });


                    }
                } while (!search.IsDone);

            }

            mod.Ex = JsonConvert.SerializeObject(mod.Agree_List);
            mod.Ag_Dis = Json(mod.Agree_List);
            return View(mod);

           
        }
             public ActionResult Age(string id, ManageFeedbacks mod)
        {

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
                        mod.Agree_List.Add(new Agree_Disagree { Q1 = Convert.ToInt32(doc["A_Boys"]), Q2 = Convert.ToInt32(doc["A_Girls"]) });


                    }
                } while (!search.IsDone);

            }

            mod.Ex = JsonConvert.SerializeObject(mod.Agree_List);
         //   mod.Ag_Dis = Json(mod.Agree_List);
            return View(mod);

           // return View();
        }

    }
}