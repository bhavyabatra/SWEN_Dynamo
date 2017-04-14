using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWEN_Dynamo.Models
{
    public class ManageFeedbacks
    {
            [Display(Name = "User ID : ")]
            public string USID { get; set; }

            [Display(Name = "Welcome ")]
            public string UserName { get; set; }

            [Display(Name = "Email : ")]
            public string EmailID { get; set; }

            public string SurveyID { get; set; }

            [Display(Name = "Region ")]
            public string Region { get; set; }

        [Display(Name = "Either you dont have any Online Survey OR Any of your Online Survey do not have any response yet. ")]
        public string No_Survey_Text { get; set; }

        public IEnumerable<SelectListItem> RegionOptions { get; set; }

        public int RID { get; set; }

        [Display(Name = "Feedback for ZipCode* ")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Invalid Zip Code")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Please Enter numerical value only")]
        public string ZipCode { get; set; }

        public string SurveyType { get; set; }

        [Display(Name = "Feedback ID : ")]
        public string FeedbackID { get; set; }

        [Display(Name = "Include Online Survey Response in Feedback ")]
        public bool CheckToOnlineTable { get; set; }

        public string SurveyIDValueHolder { get; set; } 

            [Display(Name = "Link Online Survey Report for this Feedback")]
           public bool GetOnlineSurveyData { get; set; }
            
            public string EventName { get; set; }

            public List<FeedbackFor> FLS { get; set; }

        [Display(Name = "Number of Girls ")]
        public int NOG { get; set; }
        [Display(Name = "Number of Boys ")]
        public int NOB { get; set; }
        [Display(Name = "Average age of Girls ")]
        public int AOG { get; set; }
        [Display(Name = "Average of Boys")]
        public int AOB { get; set; }
        [Display(Name = "Number of SWE Volunteer ")]
        public int NumSWEV { get; set; }
        [Display(Name = "Number of Other Volunteers ")]
        public int NumOV { get; set; }
        [Display(Name = "Date of Feedback ")]
        public string DateofEvent { get; set; }
        [Display(Name = "Type of Event ")]
        public string EventType { get; set; }

        [Display(Name = "Before/After Event Infographics ")]
        public string I_Com { get; set; }
        [Display(Name = "A-Grade from Parents Infographics ")]
        public string I_Grade { get; set; }
        [Display(Name = "Number of Boys/Girls Infographics ")]
        public string I_Number { get; set; }
        [Display(Name = "Age of Boys/Girls Infographics ")]
        public string I_Age { get; set; }


        public List<ManageFeedbacks> Infolist = new List<ManageFeedbacks>();

        public List<Agree_Disagree> Agree_List = new List<Agree_Disagree>();


    }
    public class FeedbackFor
    {
        public string SurveyType { get; set; }
        public string SurveyID { get; set; }
        public string EventName { get; set; }

    }

    public class Agree_Disagree
    {
        public int Q1 { get; set; }
        public int Q2 { get; set; }
        

    }

}