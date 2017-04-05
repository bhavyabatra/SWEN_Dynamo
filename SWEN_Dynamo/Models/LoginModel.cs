using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SWEN_Dynamo.Models
{
    public class LoginModel
    {
        
        [DynamoDBHashKey]
        public long USID { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; } 

        public string Vcode { get; set; } 

        public int RID { get; set; }

        [DynamoDBRangeKey]
        public string Email { get; set; }

        [Display(Name = "Enter Your USID or Email")]
        public string CheckWithUSIDandEmail { get; set; } 
 
        public bool LoginActive { get; set; } = false;
    }
}