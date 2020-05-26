using AmericaVirtual.WebAPI.Persistance.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmericaVirtual.WebAPI.Persistance.Classes
{
    public class WeatherRepository
    {
        public List<WeatherConditions> GetWeatherConditionsByCity(int idCity, int partFromToday)
        {
            using (AmericaVirtualEntities context = new AmericaVirtualEntities())
            {
                return context.WeatherConditions.Include("Days").Include("Weathers").Where(x => x.IdCity == idCity && x.IdDay >= partFromToday).Take(5).ToList();
            }
        }
    }
}
