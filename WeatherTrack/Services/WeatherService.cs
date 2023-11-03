using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherTrack.Models;

namespace WeatherTrack.Services
{
    public class WeatherService
    {
        HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<CurrentWeather> GetCurrentWeatherAsync(string query)
        {
            CurrentWeather currentWeather = null;

            try
            {
                var url = Constants.APIUrl + "weather?" + query + "&appid=" + Constants.APIKey;
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    currentWeather = JsonConvert.DeserializeObject<CurrentWeather>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Something went wrong when trying to get the weather data.", "OK");
                throw;
            }

            return currentWeather;
        }

        public async Task<ForecastList> GetForecastAsync(string query)
        {
            ForecastList forecastList = null;

            try
            {
                var url = Constants.APIUrl + "forecast?" + query + "&appid=" + Constants.APIKey;
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    forecastList = JsonConvert.DeserializeObject<ForecastList>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Something went wrong when trying to get the weather data.", "OK");
                throw;
            }

            return forecastList;
        }
    }
}
