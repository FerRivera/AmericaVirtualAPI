using AmericaVirtual.WebAPI.Persistance.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmericaVirtual.WebAPI.Persistance.Classes
{
    public class CountryRepository
    {
        public List<Countries> GetActiveCountries()
        {
            using (AmericaVirtualEntities context = new AmericaVirtualEntities())
            {
                return context.Countries.Where(x => x.Active == 1).ToList();
            }
        }        
    }
}
