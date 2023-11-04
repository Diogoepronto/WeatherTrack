using WeatherTrack.ViewModels;

namespace WeatherTrack.Views;

public partial class SettingsPage : ContentPage
{
    private string _units = Preferences.Default.Get("UnitsOfMeasurement", "metric");
    private bool _useLocation = Preferences.Default.Get("UseCurrentLocation", true);
    private string _selectedLocation = Preferences.Default.Get("SelectedLocation", string.Empty);

    public SettingsPage()
    {
        InitializeComponent();

        foreach (var item in unitsRbtnGroup)
        {
            if (item is RadioButton radioButton && (string)radioButton.Value == _units)
                radioButton.IsChecked = true;
        }

        useLocationSwitch.IsToggled = _useLocation;

        if (!_useLocation)
        {
            selectedLocationSearch.IsVisible = true;
            selectedLocationSearch.Placeholder = _selectedLocation == string.Empty ? "Select a city" : _selectedLocation;
        }
    }

    public void OnUnitChanged(object sender, EventArgs e)
    {
        var radioButton = sender as RadioButton;
        Preferences.Default.Set("UnitsOfMeasurement", radioButton.Value.ToString());
    }

    public void OnSwitchChanged(object sender, EventArgs e)
    {
        var currentLocationSwitch = sender as Switch;
        Preferences.Default.Set("UseCurrentLocation", currentLocationSwitch.IsToggled);

        if (currentLocationSwitch.IsToggled)
        {
            selectedLocationSearch.IsVisible = false;
        }
        else
        {
            selectedLocationSearch.IsVisible = true;
            Preferences.Default.Set("SelectedLocation", string.Empty);
            selectedLocationSearch.Text = string.Empty;
            selectedLocationSearch.Placeholder = "Select a city";
        }
    }

    public void OnCityTextChanged(object sender, EventArgs e)
    {
        var searchBar = sender as Entry;
        Preferences.Default.Set("SelectedLocation", searchBar.Text);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _units = Preferences.Default.Get("UnitsOfMeasurement", "metric");
        _useLocation = Preferences.Default.Get("UseCurrentLocation", true);
        _selectedLocation = Preferences.Default.Get("SelectedLocation", string.Empty);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        string units = Preferences.Default.Get("UnitsOfMeasurement", "metric");
        bool useLocation = Preferences.Default.Get("UseCurrentLocation", true);
        string selectedLocation = Preferences.Default.Get("SelectedLocation", string.Empty);

        if (units != _units || useLocation != _useLocation || selectedLocation != _selectedLocation)
            Preferences.Default.Set("SettingsChanged", true);
        else
            Preferences.Default.Set("SettingsChanged", false);
    }
}