using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTrack.Models
{
    public class Settings
    {
        public string UnitsOfMeasurement { get; set; } = Preferences.Default.Get(nameof(UnitsOfMeasurement), "metric");
        public bool UseCurrentLocation { get; set; } = Preferences.Default.Get(nameof(UseCurrentLocation), true);
        public string SelectedLocation { get; set; } = Preferences.Default.Get(nameof(SelectedLocation), string.Empty);
        public bool SettingsChanged { get; set; } = false;
    }
}
