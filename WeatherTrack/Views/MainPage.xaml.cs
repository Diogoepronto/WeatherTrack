using SkiaSharp.Extended.UI.Controls;
using WeatherTrack.Models;
using WeatherTrack.ViewModels;

namespace WeatherTrack;

public partial class MainPage : ContentPage
{
    WeatherViewModel _viewModel;

    public MainPage(WeatherViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetWeatherCommand.Execute(this);
    }
}