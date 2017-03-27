using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using SWEN_Dynamo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWEN_Dynamo.App_Start;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DataModel;
using System.Web.Helpers;

namespace SWEN_Dynamo.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(LoginModel model)
        {
            return View();
        }
        [HttpPost, ActionName("Index")]
        public ActionResult IndexConfirmed(LoginModel model)
        {
           
            var CheckWithUSIDandEmail = model.CheckWithUSIDandEmail;
            var PasswordFromUser = model.Password;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table table = Table.LoadTable(client, "User");
            ScanFilter firstscanFilter = new ScanFilter();
            ScanFilter secondscanFilter = new ScanFilter();
            bool x;


            // int x = (Convert.ToInt32(CheckWithUSIDandEmail) );
            if (!CheckWithUSIDandEmail.Contains("@"))
            {
                firstscanFilter.AddCondition("USID", ScanOperator.Equal, Convert.ToInt32(CheckWithUSIDandEmail));
                x = true;
                ScanOperationConfig firstconfig = new ScanOperationConfig()
                {
                    Filter = firstscanFilter,
                    Select = SelectValues.AllAttributes,
                };
                Search firstsearch = table.Scan(firstconfig);
                List<Document> firstdocumentList = new List<Document>();
                do
                {
                    firstdocumentList = firstsearch.GetNextSet();

                    var CountFromFirstList = firstdocumentList.Count();

                    if (CountFromFirstList > 0)
                    {
                        // var x = CountFromFirstList;
                        foreach (var FirstListElement in firstdocumentList)
                        {
                            TempData["USID"] = FirstListElement["USID"];
                            TempData["PasswordFromDB"] = FirstListElement["Password"];
                            TempData["VCodeFromDB"] = FirstListElement["Vcode"];
                            TempData["RIDFromDB"] = FirstListElement["RID"];
                        }
                    }


                } while (!firstsearch.IsDone);
            }
            else if (CheckWithUSIDandEmail.Contains("@"))
            {
                secondscanFilter.AddCondition("Email", ScanOperator.Equal, Convert.ToString(CheckWithUSIDandEmail));
                x = false;
                ScanOperationConfig secondconfig = new ScanOperationConfig()
                {
                    Filter = secondscanFilter,
                    Select = SelectValues.AllAttributes,
                };

                Search secondsearch = table.Scan(secondconfig);

                List<Document> seconddocumentList = new List<Document>();


                do
                {
                    seconddocumentList = secondsearch.GetNextSet();

                    var CountFromSecondList = seconddocumentList.Count();


                    //var x = CountFromSecondList;
                    foreach (var SecondListElement in seconddocumentList)
                    {
                        TempData["Email"] = SecondListElement["Email"];
                        TempData["PasswordFromDB"] = SecondListElement["Password"];
                        TempData["VCodeFromDB"] = SecondListElement["Vcode"];
                        TempData["RIDFromDB"] = SecondListElement["RID"];
                    }



                } while (!secondsearch.IsDone);
            }
            if (TempData["USID"] != null)
            {
                string USID = Convert.ToString(TempData["USID"]);
                string Password = Convert.ToString(TempData["PasswordFromDB"]);
                var VCode = TempData["VCodeFromDB"];
                string RID = Convert.ToString(TempData["RIDFromDB"]);
                var CheckPassword = Helper.EncodePassword(PasswordFromUser, Convert.ToString(VCode));

                if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "1")
                {
                    model.USID = Convert.ToInt32(USID);
                    return View("OutreachAdmin", model);
                }
                else if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "2")
                {
                    return View("NationalLevelMember");
                }
                else if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "3")
                {
                    return View("RegionalLevelMember");
                }
                else if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "4")
                {
                    return View("ChapterLevelMember");
                }
                else if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "5")
                {
                    return View("SWEVolunteer");
                }
            }
            else
            {
                string Email = Convert.ToString(TempData["Email"]);
                string Password = Convert.ToString(TempData["PasswordFromDB"]);
                var VCode = TempData["VCodeFromDB"];
                string RID = Convert.ToString(TempData["RIDFromDB"]);
                var CheckPassword = Helper.EncodePassword(PasswordFromUser, Convert.ToString(VCode));

                if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "1")
                {
                    model.USID = Convert.ToInt32(SWEN_DynamoUtilityClass.FetchUSIDfromEmail(Email));
                    return View("OutreachAdmin",model);
                }
                else if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "2")
                {
                    return View("NationalLevelMember");
                }
                else if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "3")
                {
                    return View("RegionalLevelMember");
                }
                else if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "4")
                {
                    return View("ChapterLevelMember");
                }
                else if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "5")
                {
                    return View("SWEVolunteer");
                }
            }


            return View(model);


        }

        public ActionResult OutreachAdmin(LoginModel m,string action)
        {
            
            return View("OutreachAdmin");

        }
    }
}