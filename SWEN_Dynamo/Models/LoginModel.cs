﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN_Dynamo.Models
{
    public class LoginModel
    {

        public int USID { get; set; }

        public string Password { get; set; } = "P@33w0rd";

        public string Vcode { get; set; } = "01001010";

        public int RID { get; set; }

        public string Email { get; set; } = "Default@s.com";
    }
}