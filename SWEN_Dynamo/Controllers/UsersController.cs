using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEN_Dynamo.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.MachineLearning.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using System.Collections.Concurrent;
using Amazon.Runtime.Internal.Transform;
using Amazon.SecurityToken;

namespace SWEN_Dynamo.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index(UserModel model)
        {

            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
          
            string tablename = "User";
                   var request = new PutItemRequest
                {
                    TableName = tablename,
                    Item = new Dictionary<string, AttributeValue>()
      {
          { "USID", new AttributeValue { N = Convert.ToString(model.USID) } },
          { "Email", new AttributeValue { S = model.Email }},
          { "Datecreated", new AttributeValue { S = Convert.ToString(model.Datecreated) }},
          { "Datemodified", new AttributeValue { S = Convert.ToString(model.Datemodified) }},
          { "FirstName", new AttributeValue { S = model.Firstname }},
          { "LastName", new AttributeValue { S = model.Lastname }},
          { "Password", new AttributeValue { S = model.Password }},
          { "Vcode", new AttributeValue { S = model.Vcode }},
          { "RID", new AttributeValue { N = Convert.ToString(model.RID) }},
          { "RA", new AttributeValue { N = Convert.ToString(model.RA) }},
          { "FA", new AttributeValue { N = Convert.ToString(model.FA) }},
          { "SA", new AttributeValue { N = Convert.ToString(model.SA) }},
          { "Region", new AttributeValue { S = model.Region }},
          { "Phone", new AttributeValue { N = Convert.ToString(model.Phone) }},
                    }
                };
                client.PutItem(request);
                return View();

            }
        } } 
        
    




//        var request = new PutItemRequest
//            {
//                TableName = tablename,
//                 Key = new Dictionary<string, AttributeValue>()
//                { { "USID", new AttributeValue { N = Convert.ToString(model.USID)  } } },

//            Item = new Dictionary<string, AttributeValue>()
//      {
         
//          { "Email", new AttributeValue { S = model.Email }},
//          { "Datecreated", new AttributeValue { S = Convert.ToString(model.Datecreated) }},
//          { "Datemodified", new AttributeValue { S = Convert.ToString(model.Datemodified) }},
//          { "FirstName", new AttributeValue { S = model.Firstname }},
//          { "LastName", new AttributeValue { S = model.Lastname }},
//          { "Password", new AttributeValue { S = model.Password }},
//          { "Vcode", new AttributeValue { S = model.Vcode }},
//          { "RID", new AttributeValue { N = Convert.ToString(model.RID) }},
//          { "RA", new AttributeValue { N = Convert.ToString(model.RA) }},
//          { "FA", new AttributeValue { N = Convert.ToString(model.FA) }},
//          { "SA", new AttributeValue { N = Convert.ToString(model.SA) }},
//          { "Region", new AttributeValue { S = model.Region }},
//          { "Phone", new AttributeValue { N = Convert.ToString(model.Phone) }},
//      }
//            };
//            client.PutItem(request);
//            return View();
//        }
//    }
//}