using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SWEN_Dynamo.Models
{
    public class SurveyModel 
    {
        public long ParentID { get; set; } 

        public string SurveyID { get; set; } = "Was";

        public string EventName { get; set; } = "Was";

        public SurveyType SurveyType { get; set; } 

        public int CreatedBy { get; set; } = 000000;

        public string CustomQuestion1 { get; set; } = "Null";

        public string CustomQuestion2 { get; set; } = "Null";

        public List<SurveyModel> SurveyModelList = new List<SurveyModel>();

        [Display(Name = "User ID")]
        public long USID { get; set; }

        [Display(Name = "Survey Type")]
        public string ST { get; set; }

        
    }

    public enum SurveyType
    {
        Student,
        Parent,
        Adult,
       
    }
}