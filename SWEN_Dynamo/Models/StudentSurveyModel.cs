using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SWEN_Dynamo.Models
{
    public class StudentSurveyModel
    {
        [Display(Name = "What are yor doing")]
        public bool Objective1 { get; set; }

        [Display(Name = "Question for objective 1")]
        public bool Question1 { get; set; } = false;

        [Display(Name = "Question for objective 2")]
        public bool Question2 { get; set; } = false;

        public string Objective2 { get; set; } = "What you wanna do ? ";
        public string Objective3 { get; set; } = "Shall We Start ?";
  
        public int OID { get; set;  }
        public int QID { get; set; } 
            
     }
}