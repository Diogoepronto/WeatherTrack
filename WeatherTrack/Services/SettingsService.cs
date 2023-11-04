using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTrack.Models;

namespace WeatherTrack.Services
{
    public class SettingsService : ISettingsService
    {
        public Task<T> GetSettings<T>(string key, T defaultValue)
        {
            var result = Preferences.Default.Get<T>(key, defaultValue);

            return Task.FromResult(result);
        }
        public Task SaveSettings<T>(string key, T value)
        {
            Preferences.Default.Set<T>(key, value);

            return Task.CompletedTask;
        }
    }
}
