using AmericaVirtual.WebAPI.Persistance.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmericaVirtual.WebAPI.Persistance.Classes
{
    public class CityRepository
    {
        public List<Cities> GetActiveCitiesByCountry(int idCountry)
        {
            using (AmericaVirtualEntities context = new AmericaVirtualEntities())
            {
                return context.Cities.Where(x => x.Active == 1 && x.Country == idCountry).ToList();
            }
        }
    }
}
