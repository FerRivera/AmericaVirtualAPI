using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmericaVirtual.WebAPI.Models.Responses
{
    public class CityResponse
    {
        public int id { get; set; }
        public string city { get; set; }
        public int active { get; set; }
        public string errors { get; set; }
    }
}