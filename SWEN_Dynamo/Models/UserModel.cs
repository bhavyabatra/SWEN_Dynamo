using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN_Dynamo.Models
{
    public class UserModel
    {
        public int USID { get; set; }
        public int RID { get; set; }
        public int SA { get; set; } //SurveyAdmin
        public int RA { get; set; } //ReportAdmin
        public int FA { get; set; } //FeedbackAdmin
        public double Phone { get; set; }
        public DateTime Datecreated { get; set; }
        public DateTime Datemodified { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public string Password { get; set; }
        public string Vcode { get; set; }

    }
}