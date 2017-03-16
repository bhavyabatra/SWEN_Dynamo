using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWEN_Dynamo.Models
{
    public class TestModel
    {
        public string test { get; set; } = "Strongly Agree";

     //   public string Answer { get; set; }
        public IEnumerable<SelectListItem> AnswerOptions { get; set; }
        public string ans { get; set; } = "Agree";

        public string Answers;
    }

    // public List<TestModel> tm = new List<TestModel>();

  

}
    
