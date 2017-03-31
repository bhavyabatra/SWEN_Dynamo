using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWEN_Dynamo.Models
{
    public class StudentSurveyModel
    {
        [Display(Name = "What are yor doing")]
        public bool Objective1 { get; set; } = false;

        [Display(Name = "Question for objective 1")]
        public bool O1_Q1 { get; set; } = false;

        [Display(Name = "Question for objective 2")]
        public bool O1_Q2 { get; set; } = false;

        [Display(Name = "Where are you living ?")]
        public bool Objective2 { get; set; } = false;

        [Display(Name = "Where were you born ?")]
        public bool Objective3 { get; set; } = false;

        public int OID { get; set; } = 0;

        public int QID { get; set; } = 0;

        public string SurveyID { get; set; } = "Null";

        public string SurveyofType { get; set; } = "NUll";

        public string EventName { get; set; } = "Null";

        public string CustomQuestion1 { get; set; } = "Null";

        public string CustomQuestion2 { get; set; } = "Null";

        
    }

    public class ParentSurveyModel
    {
        [Display(Name = "You are a parent.")]
        public bool O1 { get; set; }

        [Display(Name = "Are You interested in SWE ?")]
        public bool O1_Q1 { get; set; } = false;

        [Display(Name = "Are You interested in Surveys ?")]
        public bool O1_Q2 { get; set; } = false;

        [Display(Name = "How many Children ?")]
        public bool Objective2 { get; set; } = false;

        [Display(Name = "Age of children ?")]
        public bool Objective3 { get; set; } = false;

        public int OID { get; set; } = 0;

        public int QID { get; set; } = 0;

        public string SurveyID { get; set; } = "Null";

        public string SurveyofType { get; set; } = "NUll";
    }

    public class DeploySurveyStart
    {
        public string SurveyID { get; set; } = "ABC";


        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken0 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken1 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken2 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken3 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken4 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken5 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken6 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken7 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken8 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken9 { get; set; } = "444@44444.com";

        public string SurveyType { get; set; } = "None";

        public List<DeploySurveyStart> DeploySurvey { get; set; }

    }

    public class TakeSurvey
    {
      
      //  [HiddenInput(DisplayValue = false)]
       [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken { get; set; }

   }
    public class TakeSurveyStepTwo
    {
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken { get; set; }

        [Display(Name = "Survey ID")]
        public string TakeSurveyID { get; set; }

        [Display(Name = "Survey Conducted For Event ")]
        public string EventName { get; set; }

        [Display(Name = "Your Survey Status")]
        public string SurveyStatus { get; set; }



    }

    public class TakeSurveyFinalModel
    {
        public List<string> question = new List<string>();
        public List<NormalQuestions> NormalQuestionsObject = new List<NormalQuestions>();
        public List<CustomQuestions> CustomQuestionsClassObject = new List<CustomQuestions>();
        public IEnumerable<SelectListItem> AnswerOptions { get; set; }
        public List<string> answer { get; set; }
        public List<string> customanswer { get; set; }
        public TakeSurveyFinalModel tm;
        public List<string> customquestion = new List<string>();

    }

    public class NormalQuestions

    {
        public string qs { get; set; }
        public string ans { get; set; }

    }
    public class CustomQuestions

    {
        public string cques { get; set; }
        public string canswer { get; set; }

    }

   
  

}