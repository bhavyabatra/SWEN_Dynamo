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

namespace SWEN_Dynamo.Controllers
{
    public class ManageFeedbackController : Controller
    {
        // GET: ManageFeedback

        public ActionResult FeedbackFirst(int? id, ManageFeedbacks MF)
        {

            MF.FLS = new List<Models.FeedbackFor>();
            MF.USID = Convert.ToString(id);
            MF.EmailID = SWEN_DynamoUtilityClass.FetchEmailfromUSID(Convert.ToInt32(MF.USID));
            MF.UserName = SWEN_DynamoUtilityClass.FetchNamefromUSID(Convert.ToInt32(MF.USID));
            MF.FLS = SWEN_DynamoUtilityClass.FeedbackListPageFirst(Convert.ToInt32(MF.USID));
            foreach (var feed in MF.FLS)
            {
                MF.EventName = feed.EventName;
                MF.SurveyID = feed.SurveyID;
                MF.SurveyType = feed.SurveyType;
            }
            //   MF.XYZ = "11";
            return View(MF);
        }
        [HttpPost, ActionName("FeedbackFirst")]
        public ActionResult FeedbackFirstConfirmed(int? id, ManageFeedbacks MF, string Proceed)
        {
            if (!string.IsNullOrWhiteSpace(Proceed))
            {
                string x = MF.XYZ;

            }
            return View();
        }



    }
}