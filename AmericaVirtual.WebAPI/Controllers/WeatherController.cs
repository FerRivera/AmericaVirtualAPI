using AmericaVirtual.WebAPI.Models.Requests;
using AmericaVirtual.WebAPI.Models.Responses;
using AmericaVirtual.WebAPI.Persistance.Classes;
using AmericaVirtual.WebAPI.Persistance.EntityModel;
using AmericaVirtual.WebAPI.Persistance.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace AmericaVirtual.WebAPI.Controllers
{
    public class WeatherController : ApiController
    {
        List<Users> GetUsers()
        {
            UserRepository userRepository = new UserRepository();
            var users = userRepository.GetUsers();
            return users;
        }

        public dynamic ValidateUserLogin([FromBody]LoginRequest loginRequest)
        {
            try
            {
                List<Users> users = GetUsers();

                Users user = users.Find(x => x.Username.ToLower().Trim() == loginRequest.username.ToLower().Trim() && x.Password.ToLower().Trim() == loginRequest.password.ToLower().Trim());

                if (user != null)
                {
                    Logger.Instance.WriteInLog(user.Username,LogType.INFO, "User validated successfully");
                    return JObject.Parse(JsonConvert.SerializeObject(new LoginResponse { result = true,errors = string.Empty }));
                }
                else
                {
                    Logger.Instance.WriteInLog(loginRequest.username, LogType.WARNING, "User not found while validating");
                    return JObject.Parse(JsonConvert.SerializeObject(new LoginResponse { result = false, errors = "User not found" }));
                }                    
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteInLog(loginRequest.username, LogType.ERROR, "An error ocurred while validating user", ex.Message);
                return JObject.Parse(JsonConvert.SerializeObject(new LoginResponse { result = false, errors = ex.ToString() }));
            }
            
        }
    }
}
