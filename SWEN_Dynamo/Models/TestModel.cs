using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWEN_Dynamo.Models
{
    public class TestModel
    {
        public List<string> question = new List<string>();
        public List<NormalQuestions> NormalQuestionsObject = new List<NormalQuestions>();
        public List<CustomQuestions> CustomQuestionsClassObject = new List<CustomQuestions>();
        public IEnumerable<SelectListItem> AnswerOptions { get; set; }
        public List<string> answer { get; set; }
        public List<string> customanswer { get; set; }
        public TestModel tm;
        public List<string> customquestion = new List<string>();
        //       public List<TestModel> ans;
        // public List<Test> tq ;
        // public string questions { get; set; }

        //public SelectList answeroptions { get; set; }


        //   public string Answer { get; set; }
        // public IEnumerable<SelectListItem> AnswerOptions { get; set; }
        //    public List<string> answer { get; set; } 
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
    
//public class ques
//{
//    public string quest { get; set; }
   
//}

public class NormalQuestions

{
    public string qs { get; set; } 
    public string ans { get; set; }
   
}

public class CustomQuestions

{
    public string cques { get; set; }
    public string canswer { get; set; }

}

