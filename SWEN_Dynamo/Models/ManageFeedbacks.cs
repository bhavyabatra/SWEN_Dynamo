using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SWEN_Dynamo.Models
{
    public class ManageFeedbacks
    {
            [Display(Name = "Your User ID is : ")]
            public string USID { get; set; }

            [Display(Name = "Welcome ")]
            public string UserName { get; set; }

            [Display(Name = "You registered Email : ")]
            public string EmailID { get; set; }

            public string SurveyID { get; set; }

            public string SurveyType { get; set; }

            public string FeedbackID { get; set; }

        [Display(Name = "Include Online Survey Response in Feedback ")]
        public bool CheckToOnlineTable { get; set; }

        public string XYZ { get; set; } 

            [Display(Name = "Link Online Survey Report for this Feedback")]
            bool GetOnlineSurveyData { get; set; }
            
            public string EventName { get; set; }

            public List<FeedbackFor> FLS { get; set; }

        [Display(Name = "Number of Girls ")]
        public int NOG { get; set; }
        [Display(Name = "Number of Boys ")]
        public int NOB { get; set; }
        [Display(Name = "What is average age of Girls ")]
        public int AOG { get; set; }
        [Display(Name = "What is average of boys")]
        public int AOB { get; set; }
        [Display(Name = "Number of SWE Volunteer ")]
        public int NumSWEV { get; set; }
        [Display(Name = "Number of Other Volunteers ")]
        public int NumOV { get; set; }
        [Display(Name = "Enter Date of Event ")]
        public DateTime DateofEvent { get; set; }
        [Display(Name = "Type of Event ")]
        public string EventType { get; set; }


    }
    public class FeedbackFor
    {
        public string SurveyType { get; set; }
        public string SurveyID { get; set; }
        public string EventName { get; set; }

    }

}