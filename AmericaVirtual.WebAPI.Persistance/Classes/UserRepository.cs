using AmericaVirtual.WebAPI.Persistance.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmericaVirtual.WebAPI.Persistance.Classes
{
    public class UserRepository
    {
        public List<Users> GetActiveUsers()
        {
            using (AmericaVirtualEntities context = new AmericaVirtualEntities())
            {
                return context.Users.Where(x => x.Active == 1).ToList();
            }
        }
    }
}
