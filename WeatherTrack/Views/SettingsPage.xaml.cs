namespace WeatherTrack.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

		var units = Preferences.Default.Get("units", "metric");

		foreach(var item in unitsRbtnGroup)
		{
			if(item is RadioButton radioButton && (string)radioButton.Value == units)
				radioButton.IsChecked = true;
		}
    }

	public void OnUnitChanged(object sender, EventArgs e)
	{
		var button = sender as RadioButton;
		Preferences.Default.Set("units", button.Value.ToString());
    }
}