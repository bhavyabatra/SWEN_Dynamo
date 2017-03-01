using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SWEN_Dynamo.Models
{
    public class StudentSurveyModel : SurveyModel
    {
        [Display(Name = "What are yor doing")]
        public bool Objective1 { get; set; }

        [Display(Name = "Question for objective 1")]
        public bool Question1 { get; set; } = false;

        [Display(Name = "Question for objective 2")]
        public bool Question2 { get; set; } = false;

        [Display(Name = "Where are you living ?")]
        public bool Objective2 { get; set; } = false;

        [Display(Name = "Where were you born ?")]
        public bool Objective3 { get; set; } = false;

        public int OID { get; set; } = 0;

        public int QID { get; set; } = 0;

        string id { get; set; } = "xx";
     }
}