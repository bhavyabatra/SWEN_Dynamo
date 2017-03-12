using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace TakeSurveyUnitTest
{
    class Program
    {
        public string Items { get; set; }

        static void Main(string[] args)
        {
            var m = new List<Program>();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
             {
                Dictionary<string, AttributeValue> lastKeyEvaluated = null;
                do
                {
                  var request = new ScanRequest
                    {
                        TableName = "Respondent",
                        ExclusiveStartKey = lastKeyEvaluated,
                        ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {":val", new AttributeValue { S = "444@4.com"    }}
                },
                        FilterExpression = "ResponseToken = :val",
                        ProjectionExpression = "SurveyID"
                    };
                   var response = client.Scan(request);
                   foreach (Dictionary<string, AttributeValue> item in response.Items)
                    {
                       // Console.WriteLine("\nScanThreadTableUsePaging - printing.....");
                        m.Add( new Program() { Items = (item["SurveyID"].S) });
                    }
                    lastKeyEvaluated = response.LastEvaluatedKey;
                } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
                foreach (Program n in m)
                {
                    Console.WriteLine(n.Items);
                }
                  Console.ReadLine();
            }
        }
    }
}
