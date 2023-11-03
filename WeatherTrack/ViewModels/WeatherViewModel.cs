using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkiaSharp.Extended.UI.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTrack.Models;
using WeatherTrack.Services;

namespace WeatherTrack.ViewModels;

public partial class WeatherViewModel : BaseViewModel
{
    private readonly WeatherService _weatherService;
    private readonly IConnectivity _connectivity;
    private readonly IGeolocation _geolocation;

    [ObservableProperty]
    bool _isRefreshing;

    [ObservableProperty]
    CurrentWeather _weather;

    [ObservableProperty]
    ForecastList _forecast;

    [ObservableProperty]
    string _pageTitle = "Weather Track";

    public WeatherViewModel(WeatherService weatherService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Weather Track";
        _weatherService = weatherService;
        _connectivity = connectivity;
        _geolocation = geolocation;
    }

    [RelayCommand]
    async Task GetWeatherAsync()
    {
        if (IsBusy) return;

        try
        {
            // Check internet connection
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet issue", "Please, check your internet connection and try again", "OK");
                return;
            }

            IsBusy = true;

            // Get user's location
            var geolocation = await _geolocation.GetLocationAsync(
                new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

            if (geolocation == null)
            {
                geolocation = await _geolocation.GetLastKnownLocationAsync();
            }

            if (geolocation == null)
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong when trying to get your location. Please, check if GPS is enabled and try again.", "OK");
                return;
            }

            // Generate the query to fetch data from the API
            var units = Preferences.Default.Get("units", "metric");

            var query =
                $"lat={geolocation.Latitude}" +
                $"&lon={geolocation.Longitude}" +
                $"&units={units}";

            Weather = await _weatherService.GetCurrentWeatherAsync(query);

            Forecast = await _weatherService.GetForecastAsync(query);

            PageTitle = $"{Weather.CityName}, {Weather.Sys.CountryCode}";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Something went wrong when trying to get the weather data.", "OK");
            throw;
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
}
