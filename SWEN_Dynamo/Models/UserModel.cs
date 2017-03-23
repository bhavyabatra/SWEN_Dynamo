using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Amazon.DynamoDBv2.DataModel;
using System.Web.Mvc;

namespace SWEN_Dynamo.Models
{
    public class UserModel
    {
        [DynamoDBHashKey]
        public int USID { get; set; }

        public int RID { get; set; }

        public int SA { get; set; } //SurveyAdmin

        public int RA { get; set; } //ReportAdmin

        public int FA { get; set; } //FeedbackAdmin

        
        [StringLength(10, MinimumLength = 10, ErrorMessage =  "Invalid phone number, Do not enter country code if you are doing so")]
        [Required(ErrorMessage = "Phone is required")]
        [Phone]
        public string Phone { get; set; } = "0000000000";

        public DateTime Datecreated { get; set; } 

        public DateTime Datemodified { get; set; } 

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Firstname { get; set; } = "Default";
    
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string Lastname { get; set; } = "Default";

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = "Default@s.com";

        
        [Required(ErrorMessage = "Region is required")]
        public string Region { get; set; }

        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password should be between 8 to 15 characters")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MembershipPassword(  MinRequiredNonAlphanumericCharacters = 1, MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).")]
        public string Password { get; set; } = "P@33w0rd";

        public string Vcode { get; set; } = "01001010";

        public IEnumerable<SelectListItem> RolesOptions { get; set; }

    }
}