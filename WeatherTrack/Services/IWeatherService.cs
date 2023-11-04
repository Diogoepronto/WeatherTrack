using WeatherTrack.Models;

namespace WeatherTrack.Services
{
    public interface IWeatherService
    {
        Task<CurrentWeather> GetCurrentWeatherAsync(string query);
        Task<ForecastList> GetForecastAsync(string query);
    }
}