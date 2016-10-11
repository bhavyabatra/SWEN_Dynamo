using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;

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
        public string Firstname { get; set; } = "Default";
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Lastname { get; set; } = "Default";
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string Email { get; set; } = "Default";
        public string Region { get; set; } = "Default";
        [StringLength(75, MinimumLength = 2, ErrorMessage = "Specify Region ( Between 2-75 letters)")]
        public string Password { get; set; } = "P@33w0rd";
        [StringLength(15, MinimumLength = 8 , ErrorMessage = "Password should be between 8 to 15 characters")]
        public string Vcode { get; set; } = "01001010";


    }
}