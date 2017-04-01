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
                firstscanFilter.AddCondition("USID", ScanOperator.Equal, Convert.ToInt64(CheckWithUSIDandEmail));
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
                            TempData["IsLoginActive"] = FirstListElement["IsLoginActive"];
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
                        TempData["IsLoginActive"] = SecondListElement["IsLoginActive"];
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
                string IsLoginActive = Convert.ToString(TempData["IsLoginActive"]);

                if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "1" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(USID);
                    return View("OutreachAdmin", model);
                }
                else if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "2" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(USID);
                    return View("NationalLevelMember", model);
                }
                else if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "3" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(USID);
                    return View("RegionalLevelMember", model);
                }
                else if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "4" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(USID);
                    return View("ChapterLevelMember", model);
                }
                else if (USID == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "5" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(USID);
                    return View("SWEVolunteer", model);
                }
            }
            else
            {
                string Email = Convert.ToString(TempData["Email"]);
                string Password = Convert.ToString(TempData["PasswordFromDB"]);
                var VCode = TempData["VCodeFromDB"];
                string RID = Convert.ToString(TempData["RIDFromDB"]);
                var CheckPassword = Helper.EncodePassword(PasswordFromUser, Convert.ToString(VCode));
                string IsLoginActive = Convert.ToString(TempData["IsLoginActive"]);

                if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "1" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(SWEN_DynamoUtilityClass.FetchUSIDfromEmail(Email));
                    return View("OutreachAdmin",model);
                }
                else if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "2" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(SWEN_DynamoUtilityClass.FetchUSIDfromEmail(Email));
                    return View("NationalLevelMember", model);
                }
                else if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "3" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(SWEN_DynamoUtilityClass.FetchUSIDfromEmail(Email));
                    return View("RegionalLevelMember", model);
                }
                else if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "4" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(SWEN_DynamoUtilityClass.FetchUSIDfromEmail(Email));
                    return View("ChapterLevelMember", model);
                }
                else if (Email == Convert.ToString(model.CheckWithUSIDandEmail) && CheckPassword == Password && RID == "5" && IsLoginActive == "True")
                {
                    model.USID = Convert.ToInt64(SWEN_DynamoUtilityClass.FetchUSIDfromEmail(Email));
                    return View("SWEVolunteer", model);
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