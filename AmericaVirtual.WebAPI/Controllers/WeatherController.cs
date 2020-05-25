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
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AmericaVirtual.WebAPI.Controllers
{
    public class WeatherController : ApiController
    {
        public dynamic GetActiveCitiesByCountry(int idCountry) //[FromBody]CityRequest cityRequest
        {
            try
            {
                CityRepository cityRepository = new CityRepository();
                var cities = cityRepository.GetActiveCitiesByCountry(idCountry); //cityRequest.idCountry

                if (cities != null)
                {
                    Logger.Instance.WriteInLog(LogType.INFO, "cities successfully obtained");

                    var data = cities.Select(city => new CityResponse
                    {
                        id = city.Id,
                        city = city.City,
                        active = city.Active,
                        errors = ""
                    }).ToList();

                    return JArray.Parse(JsonConvert.SerializeObject(data, Formatting.Indented));
                }
                else
                {
                    Logger.Instance.WriteInLog(LogType.WARNING, "something wrong ocurred while getting the cities");
                    return JObject.Parse(JsonConvert.SerializeObject(new CityResponse
                    {
                        id = -1,
                        city = "",
                        active = -1,
                        errors = "Could not get the cities"
                    }));
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteInLog(LogType.ERROR, "An error ocurred while getting the cities", null, ex.Message);
                return JObject.Parse(JsonConvert.SerializeObject(new CityResponse
                {
                    id = -1,
                    city = "",
                    active = -1,
                    errors = ex.Message.ToString()
                }));
            }
        }
        public dynamic GetActiveCountries()
        {
            try
            {
                CountryRepository countryRepository = new CountryRepository();
                var countries = countryRepository.GetActiveCountries();

                if(countries != null)
                {
                    Logger.Instance.WriteInLog(LogType.INFO, "countries successfully obtained");

                    var data = countries.Select(country => new CountryResponse
                    {
                        id = country.Id,
                        name = country.Name,
                        active = country.Active
                    }).ToList();

                    return JArray.Parse(JsonConvert.SerializeObject(data, Formatting.Indented));
                }
                else
                {
                    Logger.Instance.WriteInLog(LogType.WARNING, "something wrong ocurred while getting the countries");
                    return JObject.Parse(JsonConvert.SerializeObject(new CountryResponse
                    {
                        id = -1,
                        name = "",
                        active = -1,
                        errors = "Could not get the countries"
                    }));
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteInLog(LogType.ERROR, "An error ocurred while getting the countries", null, ex.Message);
                return JObject.Parse(JsonConvert.SerializeObject(new CountryResponse
                {
                    id = -1,
                    name = "",
                    active = -1,
                    errors = ex.Message.ToString()
                }));
            }
        }

        List<Users> GetActiveUsers()
        {
            UserRepository userRepository = new UserRepository();
            var users = userRepository.GetActiveUsers();
            return users;
        }

        public dynamic ValidateUserLogin([FromBody]LoginRequest loginRequest)
        {
            try
            {
                List<Users> users = GetActiveUsers();

                Users user = users.Find(x => x.Username.ToLower().Trim() == loginRequest.username.ToLower().Trim() && x.Password.ToLower().Trim() == loginRequest.password.ToLower().Trim());

                if (user != null)
                {
                    Logger.Instance.WriteInLog(LogType.INFO, "User validated successfully", user.Username);
                    return JObject.Parse(JsonConvert.SerializeObject(new LoginResponse { result = true,errors = string.Empty }));
                }
                else
                {
                    Logger.Instance.WriteInLog(LogType.WARNING, "User not found while validating", loginRequest.username);
                    return JObject.Parse(JsonConvert.SerializeObject(new LoginResponse { result = false, errors = "User not found" }));
                }                    
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteInLog(LogType.ERROR, "An error ocurred while validating user", loginRequest.username, ex.Message);
                return JObject.Parse(JsonConvert.SerializeObject(new LoginResponse { result = false, errors = ex.ToString() }));
            }            
        }
    }
}
