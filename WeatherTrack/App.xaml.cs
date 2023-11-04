using WeatherTrack.Models;
using WeatherTrack.ViewModels;

namespace WeatherTrack
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}