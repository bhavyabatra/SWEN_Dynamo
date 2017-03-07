using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using SWEN_Dynamo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SWEN_Dynamo.App_Start
{
    public static class SWEN_DynamoUtilityClass
    {
        public static void UpdateDynamoDBItem(string tablename, string ID, string objectivenumber, string dbitem)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            var request = new UpdateItemRequest
            {

                TableName = tablename,
                Key = new Dictionary<string, AttributeValue>() { { "SurveyID", new AttributeValue { S = ID } }
                                 },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#"+dbitem, dbitem},

                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":"+dbitem,new AttributeValue { S = Convert.ToString(objectivenumber) }},
                     },
                UpdateExpression = "SET #"+dbitem+" = :"+dbitem
            };
            var res = client.UpdateItem(request);
            if (res != null)
            {
                client.UpdateItem(request);
            }
        }
    }
}