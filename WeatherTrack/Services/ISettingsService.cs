using WeatherTrack.Models;

namespace WeatherTrack.Services
{
    public interface ISettingsService
    {
        Task<T> GetSettings<T>(string key, T defaultValue);
        Task SaveSettings<T>(string key, T value);
    }
}