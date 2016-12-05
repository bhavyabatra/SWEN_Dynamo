using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace SWEN_Dynamo.Models
{
    public class ResponseModel
    {

            [DynamoDBHashKey]
            public int SurveyID { get; set; } 

            [DynamoDBRangeKey]
            public string Email { get; set; }

            public string O1 { get; set; }

            public string Q1 { get; set; }

        public string O1_Q1_A { get; set; } = " ";
    }
}
