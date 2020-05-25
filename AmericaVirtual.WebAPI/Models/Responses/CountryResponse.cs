using AmericaVirtual.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmericaVirtual.WebAPI.Models.Responses
{
    public class CountryResponse
    {
        public List<Country> countries { get; set; }
        public string errors { get; set; }
    }
}