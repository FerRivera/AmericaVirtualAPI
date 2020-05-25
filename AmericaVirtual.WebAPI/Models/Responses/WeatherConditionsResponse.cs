using AmericaVirtual.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmericaVirtual.WebAPI.Models.Responses
{
    public class WeatherConditionsResponse
    {
        public List<WeatherCondition> weatherConditions { get; set; }
        public string errors { get; set; }
    }
}