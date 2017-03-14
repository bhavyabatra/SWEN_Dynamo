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
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tablename = "Respondent";
            string check = "CQ";
            Table table1 = Table.LoadTable(client, "Respondent");
            Program mod = new Program();
            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
{
    { "SurveyID", new AttributeValue { S = "444@4.com" } },
    { "ResponseToken", new AttributeValue { S = "Test Four" } }
};
            GetItemRequest request = new GetItemRequest
            {
                TableName = "Respondent",
                Key = key,
            };
  
     
            var item = table1.GetItem("Test Four", "444@4.com");
            //var result = client.GetItem(request);
         //   var item = result.Item;
           
            List<Program> tm = new List<Program>();
            // Issue request
            // foreach (var kvp in itme.)
            {
                if (item.Contains("O1_Q3"))
                {
                    tm.Add(new Program { Items = item["ResponseToken"] });
                    tm.Add(new Program { Items = item["CQ1"] });
                    tm.Add(new Program { Items = item["O1_Q1"] });
                    tm.Add(new Program { Items = item["O1_Q2"] });
                }
                else
                {
                    Console.WriteLine("Does not exist");
                }
            }
            foreach (var x in tm)
            {
                Console.WriteLine(x.Items);
            }
                Console.ReadLine();
            }
        }
    }

