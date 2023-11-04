using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTrack.Models;
using WeatherTrack.Services;

namespace WeatherTrack.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _unitsOfMeasurement = Preferences.Default.Get("UnitsOfMeasurement", "metric");

        [ObservableProperty]
        bool _useCurrentLocation = Preferences.Default.Get("UseCurrentLocation", true);

        [ObservableProperty]
        string _selectedLocation = Preferences.Default.Get("SelectedLocation", string.Empty);

        [ObservableProperty]
        bool _settingsChanged = Preferences.Default.Get("SettingsChanged", false);

        public SettingsViewModel()
        {
            
        }
    }
}
