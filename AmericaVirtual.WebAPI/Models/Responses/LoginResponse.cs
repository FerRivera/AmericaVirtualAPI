using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmericaVirtual.WebAPI.Models.Responses
{
    public class LoginResponse
    {
        public bool result { get; set; }
        public string errors { get; set; }
    }
}