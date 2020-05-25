using AmericaVirtual.Domain.Entities;
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
        public dynamic GetWeatherConditionsByCity(int idCity)
        {
            try
            {
                WeatherRepository weatherRepository = new WeatherRepository();

                int partFromToday = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek;

                var weatherConditionsList = weatherRepository.GetWeatherConditionsByCity(idCity, partFromToday);

                if (weatherConditionsList != null)
                {
                    Logger.Instance.WriteInLog(LogType.INFO, "Weather conditions successfully obtained");

                    WeatherConditionsResponse weatherConditionsResponse = new WeatherConditionsResponse();
                    weatherConditionsResponse.weatherConditions = new List<WeatherCondition>();

                    foreach (var weatherConditionTemp in weatherConditionsList)
                    {
                        WeatherCondition weatherCondition = new WeatherCondition();
                        weatherCondition.cityId = weatherConditionTemp.IdCity;
                        weatherCondition.dayId = weatherConditionTemp.IdDay;
                        weatherCondition.weatherId = weatherConditionTemp.IdWeather;
                        weatherCondition.dayName = weatherConditionTemp.Days.Day;
                        weatherConditionsResponse.weatherConditions.Add(weatherCondition);
                    }

                    return JObject.Parse(JsonConvert.SerializeObject(weatherConditionsResponse));
                }
                else
                {
                    Logger.Instance.WriteInLog(LogType.WARNING, "Something wrong ocurred while getting the weather conditions");
                    return JObject.Parse(JsonConvert.SerializeObject(new WeatherConditionsResponse
                    {
                        weatherConditions = new List<WeatherCondition>(),
                        errors = "Could not get the weather conditions"
                    }));
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteInLog(LogType.ERROR, "An error ocurred while getting the weather conditions", null, ex.Message);
                return JObject.Parse(JsonConvert.SerializeObject(new WeatherConditionsResponse
                {
                    weatherConditions = new List<WeatherCondition>(),
                    errors = ex.Message.ToString()
                }));
            }
        }
        public dynamic GetActiveCitiesByCountry(int idCountry)
        {
            try
            {
                CityRepository cityRepository = new CityRepository();
                var citiesList = cityRepository.GetActiveCitiesByCountry(idCountry);

                if (citiesList != null)
                {
                    Logger.Instance.WriteInLog(LogType.INFO, "Cities successfully obtained");

                    CityResponse cityResponse = new CityResponse();
                    cityResponse.cities = new List<City>();

                    foreach (var cityTemp in citiesList)
                    {
                        City city = new City();
                        city.city = cityTemp.City;
                        city.id = cityTemp.Id;
                        city.active = cityTemp.Active;
                        cityResponse.cities.Add(city);
                    }

                    return JObject.Parse(JsonConvert.SerializeObject(cityResponse));
                }
                else
                {
                    Logger.Instance.WriteInLog(LogType.WARNING, "Something wrong ocurred while getting the cities");
                    return JObject.Parse(JsonConvert.SerializeObject(new CityResponse
                    {
                        cities = new List<City>(),
                        errors = "Could not get the cities"
                    }));
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteInLog(LogType.ERROR, "An error ocurred while getting the cities", null, ex.Message);
                return JObject.Parse(JsonConvert.SerializeObject(new CityResponse
                {
                    cities = new List<City>(),
                    errors = ex.Message.ToString()
                }));
            }
        }
        public dynamic GetActiveCountries()
        {
            try
            {
                CountryRepository countryRepository = new CountryRepository();
                var countriesList = countryRepository.GetActiveCountries();

                if(countriesList != null)
                {
                    Logger.Instance.WriteInLog(LogType.INFO, "Countries successfully obtained");

                    CountryResponse countryResponse = new CountryResponse();
                    countryResponse.countries = new List<Country>();

                    foreach (var countryTemp in countriesList)
                    {
                        Country country = new Country();
                        country.name = countryTemp.Name;
                        country.id = countryTemp.Id;
                        country.active = countryTemp.Active;
                        countryResponse.countries.Add(country);
                    }

                    return JObject.Parse(JsonConvert.SerializeObject(countryResponse));
                }
                else
                {
                    Logger.Instance.WriteInLog(LogType.WARNING, "Something wrong ocurred while getting the countries");
                    return JObject.Parse(JsonConvert.SerializeObject(new CountryResponse
                    {
                        countries = new List<Country>(),
                        errors = "Could not get the countries"
                    }));
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteInLog(LogType.ERROR, "An error ocurred while getting the countries", null, ex.Message);
                return JObject.Parse(JsonConvert.SerializeObject(new CountryResponse
                {
                    countries = new List<Country>(),
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
