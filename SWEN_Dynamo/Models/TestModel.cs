using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWEN_Dynamo.Models
{
    public class TestModel
    {
        public List<ques> q = new List<ques>();
       // public List<Test> tq ;
       // public string questions { get; set; }

        //public SelectList answeroptions { get; set; }

        public string answer { get; set; } = "null";
     //   public string Answer { get; set; }
        public IEnumerable<SelectListItem> AnswerOptions { get; set; }
        //public TestModel()
        //{
        //    AnswerOptions = new List<SelectListItem>();
       
        //}
        //public string ans { get; set; } = "Agree";

        //public string Answers { get; set; }

        //public List<TestModel> tm;
    }
    //public class Test
    //{
    //    public List<Test> testquestions { get; set; }
    //    public Test()
    //    {
    //         testquestions = new List<Test>();
    //    }
    //    //public string testquestions { get; set; } = "check";
    //}
    // public List<TestModel> tm = new List<TestModel>();



}
    
public class ques
{
    public string quest { get; set; } = "Wu";
}