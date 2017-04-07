using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWEN_Dynamo.Models
{
    public class StudentSurveyModel
    {
        [Display(Name = "Develop students understanding of what engineers do.")]
        public bool Objective1 { get; set; } = false;

        [Display(Name = "Before this event, I knew what an engineer did.")]
        public bool O1_Q1 { get; set; } = false;
        [Display(Name = "After this event, I know what an engineer does.")]
        public bool O1_Q2 { get; set; } = false;
        [Display(Name = "Engineers are innovative.(They come up with new ideas and inventions.)")]
        public bool O1_Q3 { get; set; } = false;
        [Display(Name = "Engineers are creative.")]
        public bool O1_Q4 { get; set; } = false;
        [Display(Name = "Engineers do work that is hands-on.")]
        public bool O1_Q5 { get; set; } = false;
        [Display(Name = "Engineers work in many different kinds of career fields.")]
        public bool O1_Q6 { get; set; } = false;

        [Display(Name = "Change negative attitudes about engineering careers.")]
        public bool Objective2 { get; set; } = false;

        [Display(Name = "Before this event, I was interested in becoming an engineer.")]
        public bool O2_Q1 { get; set; } = false;
        [Display(Name = "After this event, I am interested in becoming an engineer.")]
        public bool O2_Q2 { get; set; } = false;
        [Display(Name = "I know my friends would support my interest in engineering.")]
        public bool O2_Q3 { get; set; } = false;
        [Display(Name = "Engineers do work that is fun.")]
        public bool O2_Q4 { get; set; } = false;
        [Display(Name = "Engineers do work that allows them to help their community and/or society.")]
        public bool O2_Q5 { get; set; } = false;
        [Display(Name = "Engineers work in many different kinds of career fields.")]
        public bool O2_Q6 { get; set; } = false;

        [Display(Name = "Help students draw connections between their interests/passions and engineering.")]
        public bool Objective3 { get; set; } = false;

        [Display(Name = "Before this event, I was interested in becoming an engineer.")]
        public bool O3_Q1 { get; set; } = false;

        [Display(Name = "Build students’ self-confidence skills as they relate to engineering.")]
        public bool Objective4 { get; set; } = false;

        [Display(Name = "My confidence in problem-solving is improved.")]
        public bool O4_Q1 { get; set; } = false;
        [Display(Name = "My confidence in building and designing things is improved.")]
        public bool O4_Q2 { get; set; } = false;

        [Display(Name = "Build students’ critical thinking skills as they relate to engineering.")]
        public bool Objective5 { get; set; } = false;

        [Display(Name = "My ability to brainstrom solutions to problems is improved.")]
        public bool O5_Q1 { get; set; } = false;
        [Display(Name = "My ability to think of many different possible ways to solve a problem is improved.")]
        public bool O5_Q2 { get; set; } = false;
        [Display(Name = "My ability to design process (brainstrom, design, build, test, redesign) is improved.")]
        public bool O5_Q3 { get; set; } = false;


        [Display(Name = "Enable students to meet and network with engineering role models.")]
        public bool Objective6 { get; set; } = false;

        [Display(Name = "I worked with a mentor/role model who was helpful and easy to talk to. ")]
        public bool O6_Q1 { get; set; } = false;

        [Display(Name = "Help students see that engineering is a viable career for women.")]
        public bool Objective7 { get; set; } = false;

        [Display(Name = "Engineering is a good career choice for women.")]
        public bool O7_Q1 { get; set; } = false;

        [Display(Name = "Enable students to identify what the next steps to becoming an engineer are.")]
        public bool Objective8 { get; set; } = false;

        [Display(Name = "I know how to find out more about engineering if I want to.")]
        public bool O8_Q1 { get; set; } = false;

        [Display(Name = "Open Ended Questions")]
        public bool Objective9 { get; set; } = false;

                [Display(Name = "Do you consider today’s event as an A-Grade event ? [Strongly Agree = A Grade, Strongly Disagree = D Grade]" )]
                public bool O9_Q1 { get; set; } = false;
                [Display(Name = "Did you like most of the part about the event today? ")]
                public bool O9_Q2 { get; set; } = false;
                [Display(Name = "If you were in charge, would you like to change this event?")]
                public bool O9_Q3 { get; set; } = false;
                [Display(Name = "Would you recommend that other kids participate in events like this?")]
                public bool O9_Q4 { get; set; } = false;

        [Display(Name = "Demographic Questions")]
        public bool Objective10 { get; set; } = false;

                [Display(Name = "I am (Gender):")]
                public bool O10_Q1 { get; set; } = false;
                [Display(Name = "How old are you? ")]
                public bool O10_Q2 { get; set; } = false;
                [Display(Name = "With what races or ethnicities do you most identify?")]
                public bool O10_Q3 { get; set; } = false;
                [Display(Name = "What grade are you going to?")]
                public bool O10_Q4 { get; set; } = false;

        [Display(Name = "Custom Questions")]
        public bool Objective11 { get; set; } = false;

                [Display(Name = "First Question")]
                public string CustomQuestion1 { get; set; } = "Null";
                [Display(Name = "Second Question")]
                public string CustomQuestion2 { get; set; } = "Null";
                [Display(Name = "Third Question")]
                public string CustomQuestion3 { get; set; } = "Null";
                [Display(Name = "Fourth Question")]
                public string CustomQuestion4 { get; set; } = "Null";
                [Display(Name = "Fifth Question")]
                public string CustomQuestion5 { get; set; } = "Null";


        public int OID { get; set; } = 0;

        public int QID { get; set; } = 0;

        public string SurveyID { get; set; } = "Null";

        public string SurveyofType { get; set; } = "NUll";

        public string EventName { get; set; } = "Null";

      
        
    }

    public class ParentSurveyModel
    {
        [Display(Name = "You are a parent.")]
        public bool O1 { get; set; }

        [Display(Name = "Are You interested in SWE ?")]
        public bool O1_Q1 { get; set; } = false;

        [Display(Name = "Are You interested in Surveys ?")]
        public bool O1_Q2 { get; set; } = false;

        [Display(Name = "How many Children ?")]
        public bool Objective2 { get; set; } = false;

        [Display(Name = "Age of children ?")]
        public bool Objective3 { get; set; } = false;

        public int OID { get; set; } = 0;

        public int QID { get; set; } = 0;

        public string SurveyID { get; set; } = "Null";

        public string SurveyofType { get; set; } = "NUll";
    }

    public class DeploySurveyStart
    {
        public string SurveyID { get; set; } = "ABC";


        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken0 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken1 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken2 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken3 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken4 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken5 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken6 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken7 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken8 { get; set; } = "444@44444.com";
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken9 { get; set; } = "444@44444.com";

        public string SurveyType { get; set; } = "None";

        public List<DeploySurveyStart> DeploySurvey { get; set; }

    }

    public class TakeSurvey
    {
      
      //  [HiddenInput(DisplayValue = false)]
       [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken { get; set; }

   }
    public class TakeSurveyStepTwo
    {
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ResponseToken { get; set; }

        [Display(Name = "Survey ID")]
        public string TakeSurveyID { get; set; }

        [Display(Name = "Survey Conducted For Event ")]
        public string EventName { get; set; }

        [Display(Name = "Your Survey Status")]
        public string SurveyStatus { get; set; }



    }

    public class TakeSurveyFinalModel
    {
        public List<string> question = new List<string>();
        public List<NormalQuestions> NormalQuestionsObject = new List<NormalQuestions>();
        public List<CustomQuestions> CustomQuestionsClassObject = new List<CustomQuestions>();
        public IEnumerable<SelectListItem> AnswerOptions { get; set; }
        public List<string> answer { get; set; }
        public List<string> customanswer { get; set; }
        public TakeSurveyFinalModel tm;
        public List<string> customquestion = new List<string>();

    }

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

   
  

}