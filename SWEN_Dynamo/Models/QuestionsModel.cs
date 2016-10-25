using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SWEN_Dynamo.Models
{
    public class QuestionsModel
    {
        // Manage Questions Database 
        // Include Options 
    // Option 1.2.3.4/5
    // Add in View and then bind to model. 
        [Required(ErrorMessage = "SurveyType is required")]
        //  [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string SurveyType { get; set; } = "Default";

        public int OID { get; set; }

        [Required(ErrorMessage = "Obective is required")]
     //  [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Objective { get; set; } = "Default";

        public int QID { get; set; }

       [Required(ErrorMessage = "Question is required")]
      //  [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Question { get; set; } = "Default";


    }
}