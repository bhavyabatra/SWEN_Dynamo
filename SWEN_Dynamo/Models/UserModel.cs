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
        public long USID { get; set; }

        [Display(Name = "SWE Membership Role")]
        public int RID { get; set; }

           
        [StringLength(10, MinimumLength = 10, ErrorMessage =  "Invalid phone number")]
        [Phone]
        [RegularExpression(@"^[1-9]+[0-9]{9}$", ErrorMessage = "Please Enter valid number only")]
        [Display(Name = "Phone Number without Country Code")]
        public string Phone { get; set; } 

        public DateTime Datecreated { get; set; } 

        public DateTime Datemodified { get; set; }

        [Display(Name = "First Name*")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name*")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string Lastname { get; set; } 

        [Display(Name = "Email address*")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote("EmailExist", "Users", HttpMethod = "POST", ErrorMessage = "Email id already exists. Please use a different Email Id.")]
        public string Email { get; set; } 
     
        [Display(Name = "Region of Operation* ")]
        [Required(ErrorMessage = "Region of Operation is required")]
        public string Region { get; set; }

        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password should be between 8 to 15 characters")]
       // [Required(ErrorMessage = "Password is required")]
        // [Required(AllowEmptyStrings = true)]
        // [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.Password)]
        [MembershipPassword(MinRequiredNonAlphanumericCharacters = 1, MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).")]
        public string Password { get; set; }

        public string Vcode { get; set; } = "01001010";

        [Display(Name = "Region of Operation* ")]
        public string HiddenText { get; set; } = "United States of America";

        public IEnumerable<SelectListItem> RolesOptions { get; set; }

        public IEnumerable<SelectListItem> RegionOptions { get; set; }

        [Display(Name = "Activate Login for this user")]
        public bool IsLoginActive { get; set; }

        public string FullName { get; set; }

        // public string SelectedRole { get; set; }

    }
}