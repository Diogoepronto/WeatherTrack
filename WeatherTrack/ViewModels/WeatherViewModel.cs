using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Devices.Sensors;
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
    private readonly IWeatherService _weatherService;
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


    public WeatherViewModel(IWeatherService weatherService, IConnectivity connectivity, IGeolocation geolocation)
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
            string unitsOfMeasurement = Preferences.Default.Get("UnitsOfMeasurement", "metric");
            bool useCurrentLocation = Preferences.Default.Get("UseCurrentLocation", true);
            string selectedLocation = Preferences.Default.Get("SelectedLocation", string.Empty);

            // Check internet connection
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet issue", "Please, check your internet connection and try again", "OK");
                return;
            }

            IsBusy = true;

            // Get user's location
            var query = $"units={unitsOfMeasurement}";

            if (useCurrentLocation)
            {
                bool tryAgain = true;
                while (tryAgain)
                {
                    try
                    {
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

                        query +=
                            $"&lat={geolocation.Latitude}" +
                            $"&lon={geolocation.Longitude}";

                        tryAgain = false;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        tryAgain = await Shell.Current.DisplayAlert("Permission Required", "To provide accurate weather information, we need access to your location. Would you like to try again?", "Yes", "No");

                        if (!tryAgain)
                        {
                            Preferences.Default.Set("UseCurrentLocation", false);
                            await Shell.Current.GoToAsync("//SettingsPage");
                        }
                    }
                }                
            }
            else
            {
                query += $"&q={selectedLocation}";
            }

            Weather = Task.Run(async () => await _weatherService.GetCurrentWeatherAsync(query)).Result;

            Forecast = Task.Run(async () => await _weatherService.GetForecastAsync(query)).Result;

            if(!Weather.FetchSuccessful)
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong when trying to get the weather data.", "OK");
                await Shell.Current.GoToAsync("//SettingsPage");
            }

            PageTitle = $"{Weather.CityName}, {Weather.Sys.CountryCode}";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Something went wrong when trying to get the weather data.", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
}
