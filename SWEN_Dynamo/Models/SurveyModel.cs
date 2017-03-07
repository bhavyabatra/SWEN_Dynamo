using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN_Dynamo.Models
{
    public class SurveyModel 
    {
        public string SurveyID { get; set; } = "Was";

        public string EventName { get; set; } = "Was";

        public SurveyType SurveyType { get; set; } 

        public int CreatedBy { get; set; } = 000000;

        
    }

    public enum SurveyType
    {
        Student,
        Parent,
        Adult,
       
    }
}