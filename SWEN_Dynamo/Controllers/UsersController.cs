using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWEN_Dynamo.App_Start;
using System.Web.Mvc;
using SWEN_Dynamo.Models;
using Amazon.DynamoDBv2;
using Amazon.KeyManagementService;
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


        public ActionResult Create(UserModel model)
        {
            model.USID = Helper.GenerateUSID();
            TempData["AutoUSID"] = model.USID;
            model.RolesOptions = new List<SelectListItem>
                {
                 new SelectListItem() { Value = "1", Text = "Outreach Admin" },
                new SelectListItem() { Value = "2", Text = "National SWE Comittee Member" },
                new SelectListItem() { Value = "3", Text = "Regional SWE Comittee Member" },
                new SelectListItem() { Value = "4", Text = "Chapter SWE Comittee Member" },
                new SelectListItem() { Value = "5", Text = "SWE Member Volunteer", Selected = true }

            };

            model.RegionOptions = new List<SelectListItem>
            {
                    new SelectListItem { Value = "AL", Text = "Alabama" },
                    new SelectListItem { Value = "AK", Text = "Alaska" },
                    new SelectListItem { Value = "AZ", Text = "Arizona" },
                    new SelectListItem { Value = "AR", Text = "Arkansas" },
                    new SelectListItem { Value = "CA", Text = "California" },
                    new SelectListItem { Value = "CO", Text = "Colorado" },
                    new SelectListItem { Value = "CT", Text = "Connecticut" },
                    new SelectListItem { Value = "DE", Text = "Delaware" },
                    new SelectListItem { Value = "FL", Text = "Florida" },
                    new SelectListItem { Value = "GA", Text = "Georgia" },
                    new SelectListItem { Value = "HI", Text = "Hawaii" },
                    new SelectListItem { Value = "ID", Text = "Idaho" },
                    new SelectListItem { Value = "IL", Text = "Illinois" },
                    new SelectListItem { Value = "IN", Text = "Indiana" },
                    new SelectListItem { Value = "IA", Text = "Iowa" },
                    new SelectListItem { Value = "KS", Text = "Kansas" },
                    new SelectListItem { Value = "KY", Text = "Kentucky" },
                    new SelectListItem { Value = "LA", Text = "Louisiana" },
                    new SelectListItem { Value = "ME", Text = "Maine" },
                    new SelectListItem { Value = "MD", Text = "Maryland" },
                    new SelectListItem { Value = "MA", Text = "Massachusetts" },
                    new SelectListItem { Value = "MI", Text = "Michigan" },
                    new SelectListItem { Value = "MN", Text = "Minnesota" },
                    new SelectListItem { Value = "MS", Text = "Mississippi" },
                    new SelectListItem { Value = "MO", Text = "Missouri" },
                    new SelectListItem { Value = "MT", Text = "Montana" },
                    new SelectListItem { Value = "NC", Text = "North Carolina" },
                    new SelectListItem { Value = "ND", Text = "North Dakota" },
                    new SelectListItem { Value = "NE", Text = "Nebraska" },
                    new SelectListItem { Value = "NV", Text = "Nevada" },
                    new SelectListItem { Value = "NH", Text = "New Hampshire" },
                    new SelectListItem { Value = "NJ", Text = "New Jersey" },
                    new SelectListItem { Value = "NM", Text = "New Mexico" },
                    new SelectListItem { Value = "NY", Text = "New York" },
                    new SelectListItem { Value = "OH", Text = "Ohio" },
                    new SelectListItem { Value = "OK", Text = "Oklahoma" },
                    new SelectListItem { Value = "OR", Text = "Oregon" },
                    new SelectListItem { Value = "PA", Text = "Pennsylvania" },
                    new SelectListItem { Value = "RI", Text = "Rhode Island" },
                    new SelectListItem { Value = "SC", Text = "South Carolina" },
                    new SelectListItem { Value = "SD", Text = "South Dakota" },
                    new SelectListItem { Value = "TN", Text = "Tennessee" },
                    new SelectListItem { Value = "TX", Text = "Texas" },
                    new SelectListItem { Value = "UT", Text = "Utah" },
                    new SelectListItem { Value = "VT", Text = "Vermont" },
                    new SelectListItem { Value = "VA", Text = "Virginia" },
                    new SelectListItem { Value = "WA", Text = "Washington" },
                    new SelectListItem { Value = "WV", Text = "West Virginia" },
                    new SelectListItem { Value = "WI", Text = "Wisconsin" },
                    new SelectListItem { Value = "WY", Text = "Wyoming" }
                };
            return View(model);

        }
        [HttpPost, ActionName("Create")]
        public ActionResult CreateConfirmed(UserModel model)
        {
            model.RolesOptions = new List<SelectListItem>
                {
                 new SelectListItem() { Value = "1", Text = "Outreach Admin" },
                new SelectListItem() { Value = "2", Text = "National SWE Comittee Member" },
                new SelectListItem() { Value = "3", Text = "Regional SWE Comittee Member" },
                new SelectListItem() { Value = "4", Text = "Chapter SWE Comittee Member" },
                new SelectListItem() { Value = "5", Text = "SWE Member Volunteer" }

            };
            model.RegionOptions = new List<SelectListItem>
            {
                    new SelectListItem { Value = "AL", Text = "Alabama" },
                    new SelectListItem { Value = "AK", Text = "Alaska" },
                    new SelectListItem { Value = "AZ", Text = "Arizona" },
                    new SelectListItem { Value = "AR", Text = "Arkansas" },
                    new SelectListItem { Value = "CA", Text = "California" },
                    new SelectListItem { Value = "CO", Text = "Colorado" },
                    new SelectListItem { Value = "CT", Text = "Connecticut" },
                    new SelectListItem { Value = "DE", Text = "Delaware" },
                    new SelectListItem { Value = "FL", Text = "Florida" },
                    new SelectListItem { Value = "GA", Text = "Georgia" },
                    new SelectListItem { Value = "HI", Text = "Hawaii" },
                    new SelectListItem { Value = "ID", Text = "Idaho" },
                    new SelectListItem { Value = "IL", Text = "Illinois" },
                    new SelectListItem { Value = "IN", Text = "Indiana" },
                    new SelectListItem { Value = "IA", Text = "Iowa" },
                    new SelectListItem { Value = "KS", Text = "Kansas" },
                    new SelectListItem { Value = "KY", Text = "Kentucky" },
                    new SelectListItem { Value = "LA", Text = "Louisiana" },
                    new SelectListItem { Value = "ME", Text = "Maine" },
                    new SelectListItem { Value = "MD", Text = "Maryland" },
                    new SelectListItem { Value = "MA", Text = "Massachusetts" },
                    new SelectListItem { Value = "MI", Text = "Michigan" },
                    new SelectListItem { Value = "MN", Text = "Minnesota" },
                    new SelectListItem { Value = "MS", Text = "Mississippi" },
                    new SelectListItem { Value = "MO", Text = "Missouri" },
                    new SelectListItem { Value = "MT", Text = "Montana" },
                    new SelectListItem { Value = "NC", Text = "North Carolina" },
                    new SelectListItem { Value = "ND", Text = "North Dakota" },
                    new SelectListItem { Value = "NE", Text = "Nebraska" },
                    new SelectListItem { Value = "NV", Text = "Nevada" },
                    new SelectListItem { Value = "NH", Text = "New Hampshire" },
                    new SelectListItem { Value = "NJ", Text = "New Jersey" },
                    new SelectListItem { Value = "NM", Text = "New Mexico" },
                    new SelectListItem { Value = "NY", Text = "New York" },
                    new SelectListItem { Value = "OH", Text = "Ohio" },
                    new SelectListItem { Value = "OK", Text = "Oklahoma" },
                    new SelectListItem { Value = "OR", Text = "Oregon" },
                    new SelectListItem { Value = "PA", Text = "Pennsylvania" },
                    new SelectListItem { Value = "RI", Text = "Rhode Island" },
                    new SelectListItem { Value = "SC", Text = "South Carolina" },
                    new SelectListItem { Value = "SD", Text = "South Dakota" },
                    new SelectListItem { Value = "TN", Text = "Tennessee" },
                    new SelectListItem { Value = "TX", Text = "Texas" },
                    new SelectListItem { Value = "UT", Text = "Utah" },
                    new SelectListItem { Value = "VT", Text = "Vermont" },
                    new SelectListItem { Value = "VA", Text = "Virginia" },
                    new SelectListItem { Value = "WA", Text = "Washington" },
                    new SelectListItem { Value = "WV", Text = "West Virginia" },
                    new SelectListItem { Value = "WI", Text = "Wisconsin" },
                    new SelectListItem { Value = "WY", Text = "Wyoming" }
                };

            var keyNew = Helper.GeneratePassword(10);
            var pass = Helper.EncodePassword(model.Password, keyNew);
            model.Password = pass;
            model.Vcode = keyNew;
            model.Datecreated = System.DateTime.Now;
            model.Datemodified = System.DateTime.Now;
            model.USID = Convert.ToInt64(TempData["AutoUSID"]);
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string Region;
            if (model.RID == 1 || model.RID ==2)
            {
                
               Region="USA";
            }
            else
            {
                Region = model.Region;
            }

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
          { "Password", new AttributeValue { S = pass }},
          { "Vcode", new AttributeValue { S = keyNew }},
          { "RID", new AttributeValue { N = Convert.ToString(model.RID) }},
          { "Region", new AttributeValue { S = Region }},
          { "Phone", new AttributeValue { N = Convert.ToString(model.Phone) }},
          { "IsLoginActive", new AttributeValue { S = Convert.ToString(model.IsLoginActive) } }
                    }
            };
              client.PutItem(request);
            return View(model);

        }

        // GET: Users
        static List<UserModel> people = new List<UserModel>();
        public ActionResult UsersList(UserModel usermodelobject)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.Clear();
            //}
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.GreaterThan, 0);
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "USID", "RID", "FirstName", "LastName", "Email", "Phone", "Datecreated", "Datemodified", "Region", "IsLoginActive" }
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();

            do
            {
                people.Clear();
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {

                    people.Add(new UserModel()
                    {
                        USID = Convert.ToInt64(document["USID"]),
                        RID = Convert.ToInt32(document["RID"]),
                        Email = document["Email"],
                        Datecreated = Convert.ToDateTime(document["Datecreated"]),
                        Datemodified = Convert.ToDateTime(document["Datemodified"]),
                        Firstname = document["FirstName"],
                        Lastname = document["LastName"],
                        Phone = document["Phone"],
                        Region = document["Region"],
                        IsLoginActive = Convert.ToBoolean(document["IsLoginActive"])
                    });

                }
            } while (!search.IsDone);

            return View(people);

        }

        public ActionResult Edit(long? id, UserModel mod)
        {
          
                AmazonDynamoDBClient client = new AmazonDynamoDBClient();
                Table table = Table.LoadTable(client, "User");
                ScanFilter scanFilter = new ScanFilter();
                string tablename = "User";
             
                scanFilter.AddCondition("USID", ScanOperator.Equal, Convert.ToInt64(id));
                ScanOperationConfig config = new ScanOperationConfig()
                {
                    Filter = scanFilter,
                    Select = SelectValues.AllAttributes,
                   
                };
                Search search = table.Scan(config);
                List<Document> documentList = new List<Document>();
                do
                {
                    

                    documentList = search.GetNextSet();
                    if (documentList != null)
                    {
                        foreach (var document in documentList)
                        {

                            mod.USID = Convert.ToInt64(document["USID"]);
                            mod.Firstname = document["FirstName"];
                            mod.Lastname = document["LastName"];
                            mod.Email = document["Email"];
                            mod.Datemodified = Convert.ToDateTime(document["Datemodified"]);
                            mod.Datecreated = Convert.ToDateTime(document["Datecreated"]);
                            mod.Region =  document["Region"];
                            mod.RID = Convert.ToInt32(document["RID"]);
                            mod.Phone = document["Phone"];
                            mod.Password = null ;
                            mod.IsLoginActive = Convert.ToBoolean(document["IsLoginActive"]);
                          //  mod.SelectedRole = Convert.ToString(mod.RID);
                        
                        }
                    }
               
                mod.RolesOptions = new List<SelectListItem>
                {
                   
                 new SelectListItem() {Value = "1", Text = "Outreach Admin" },
                new SelectListItem() { Value = "2", Text = "National SWE Comittee Member" },
                new SelectListItem() { Value = "3", Text = "Regional SWE Comittee Member" },
                new SelectListItem() { Value = "4", Text = "Chapter SWE Comittee Member" },
                new SelectListItem() { Value = "5", Text = "SWE Member Volunteer", Selected = true }

            };
            
            } while (!search.IsDone);
                
     
            return View(mod);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(long? id, UserModel mod)
        {
            mod.RolesOptions = new List<SelectListItem>
                {
                 new SelectListItem() { Value = "1", Text = "Outreach Admin" },
                new SelectListItem() { Value = "2", Text = "National SWE Comittee Member" },
                new SelectListItem() { Value = "3", Text = "Regional SWE Comittee Member" },
                new SelectListItem() { Value = "4", Text = "Chapter SWE Comittee Member" },
                new SelectListItem() { Value = "5", Text = "SWE Member Volunteer", Selected = true }

            };

            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            string tablename = "User";
            mod.Datemodified = System.DateTime.Now;
            scanFilter.AddCondition("USID", ScanOperator.Equal, id);

            ScanOperationConfig config = new ScanOperationConfig()

            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,
                //AttributesToGet = new List<string> { "SuriveyID", "Email", "Objective1", "Question1" }
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();
            do
            {


                documentList = search.GetNextSet();
                if (documentList != null)
                {
                    foreach (var document in documentList)
                    {
                        TempData["VCodeKeyFromDB"] = document["Vcode"];
                        TempData["PasswordFromDB"] = document["Password"];
                    }
                }
            } while (!search.IsDone);

            string VCodeKeyFromDB = Convert.ToString(TempData["VCodeKeyFromDB"]);
            string PasswordFromDB = Convert.ToString(TempData["PasswordFromDB"]);
           
                                                                        
            if (mod.Password == null )                                 
            {
               mod.Vcode = VCodeKeyFromDB;                              
                mod.Password = PasswordFromDB;                         
              
            }
            else                                
            {
                var TransactPass = mod.Password;
                mod.Password = Helper.EncodePassword(TransactPass, VCodeKeyFromDB);
                if (Convert.ToString(mod.Password) == Convert.ToString(PasswordFromDB))
                {
                    mod.Vcode = VCodeKeyFromDB;
                    mod.Password = PasswordFromDB;
                }
                else
                {
                    mod.Vcode = Helper.GeneratePassword(10);
                    mod.Password = Helper.EncodePassword(mod.Password, mod.Vcode);
                }
            }
                                                                               
                                                                               
            var request = new UpdateItemRequest
            {
                TableName = tablename,
                Key = new Dictionary<string, AttributeValue>() { { "USID", new AttributeValue { N = Convert.ToString(id) } }
                     },
                ExpressionAttributeNames = new Dictionary<string, string>()
    {
        {"#FN", "FirstName"},
        { "#LN","LastName"},
        { "#EM", "Email"},
        { "#RI","RID"},
        { "#RE","Region"},
         { "#PH", "Phone"},
        { "#DM","Datemodified" },
        { "#PA","Password" },
        { "#VC", "Vcode" },
        { "#ILAC", "IsLoginActive" }

    },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
    {
        {":FN",new AttributeValue { S = mod.Firstname}},
        { ":LN",new AttributeValue {S=mod.Lastname } },
        {":EM",new AttributeValue { S = mod.Email}},
        { ":PH",new AttributeValue {N = Convert.ToString(mod.Phone) } },
        {":RE",new AttributeValue { S = mod.Region}},
        { ":RI",new AttributeValue {N = Convert.ToString(mod.RID) } },
     
         {":DM",new AttributeValue { S = Convert.ToString(mod.Datemodified)}},
         {":PA", new AttributeValue { S = mod.Password} },
         {":VC", new AttributeValue { S = mod.Vcode} },
         { ":ILAC", new AttributeValue { S = Convert.ToString(mod.IsLoginActive)} }

         },
                UpdateExpression = "SET #FN = :FN, #LN = :LN, #PH = :PH, #RE = :RE, #RI = :RI,#EM = :EM,#DM = :DM, #PA = :PA, #VC = :VC, #ILAC = :ILAC"
            };


                     


                        var res = client.UpdateItem(request);
                        if (res != null)
                        {
                            client.UpdateItem(request);
                        }

    


            return View(mod);
        }

        public ActionResult Details(long? id)
        {
            UserModel det = new UserModel();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.Equal, Convert.ToInt64(id));
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();
            do
            {
                // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    det.USID = Convert.ToInt64(document["USID"]);
                    det.Firstname = document["FirstName"];
                    det.Lastname = document["LastName"];
                    det.Email = document["Email"];
                    det.Region = document["Region"];
                    det.RID = Convert.ToInt32(document["RID"]);
                    det.Phone = document["Phone"];
                    det.Datemodified = Convert.ToDateTime(document["Datemodified"]);
                    det.Datecreated = Convert.ToDateTime(document["Datecreated"]);
                    det.IsLoginActive = Convert.ToBoolean(document["IsLoginActive"]);
                }
            } while (!search.IsDone);
            return View(det);
        }


        public ActionResult Delete(long? id, UserModel det, string action)
        {
            //      UserModel del = new UserModel();
        //    UserModel det = new UserModel();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("USID", ScanOperator.Equal, Convert.ToInt64(id));
            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.AllAttributes,
            };
            Search search = table.Scan(config);
            List<Document> documentList = new List<Document>();
            do
            {
                // Fetch the number value from objective in doc list. Map number value to local value and iterate the loop for that number of objectives
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    det.USID = Convert.ToInt64(document["USID"]);
                    det.Firstname = document["FirstName"];
                    det.Lastname = document["LastName"];
                    det.Email = document["Email"];
                    det.Region = document["Region"];
                    det.RID = Convert.ToInt32(document["RID"]);
                    det.Phone = document["Phone"];
                    det.Datemodified = Convert.ToDateTime(document["Datemodified"]);
                    det.Datecreated = Convert.ToDateTime(document["Datecreated"]);
                    det.IsLoginActive = Convert.ToBoolean(document["IsLoginActive"]);
                }
            } while (!search.IsDone);
            

            if (action == "Back to List")
            {
                RedirectToAction("UsersList");
            }

            return View(det);

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleteconfirmed(long? id, UserModel del, string action)

        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            string tableName = "User";
            //if (ModelState.IsValid && HtmlHelper.Equals)

            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "USID", new AttributeValue { N = Convert.ToString(id) } } },
            };
            var response = client.DeleteItem(request);
            return View(del);
        }
    }
}










