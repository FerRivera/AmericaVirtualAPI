using AmericaVirtual.WebAPI.Persistance.Classes;
using AmericaVirtual.WebAPI.Persistance.EntityModel;
using AmericaVirtual.WebAPI.Persistance.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmericaVirtual.WebAPI.Controllers
{
    public class WeatherController : ApiController
    {
        public string GetUsers()
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                var users = JsonConvert.SerializeObject(userRepository.GetUsers());
                return users;
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        [HttpGet]
        public bool ValidateUserLogin(string username, string password)
        {
            try
            {
                string usersJson = GetUsers();

                List<Users> users = JsonConvert.DeserializeObject<List<Users>>(usersJson);

                Users user = users.Find(x => x.Username.ToLower().Trim() == username.ToLower().Trim() && x.Password.ToLower().Trim() == password.ToLower().Trim());

                if (user != null)
                {
                    Logger.Instance.WriteInfoInLog(user.Username, "User validated successfully");
                    return true;
                }
                else
                {
                    Logger.Instance.WriteWarningInLog(user.Username, "User not found while validating");
                    return false;
                }                    
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteErrorInLog(username, "An error ocurred while validating user", ex.Message);
                throw;
            }
            
        }
    }
}
