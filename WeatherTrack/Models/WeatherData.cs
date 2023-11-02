using Newtonsoft.Json;
using SkiaSharp.Extended.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTrack.Models
{
    public class WeatherData
    {
        [JsonProperty("coord")]
        public Coord Coordinates { get; set; }
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("main")]
        public MainData MainData { get; set; }
        public Wind Wind { get; set; }
        public Rain Rain { get; set; }
        public Snow Snow { get; set; }
        public Clouds Clouds { get; set; }
        public Sys Sys { get; set; }
        public string Base { get; set; }
        public int Visibility { get; set; }
        public int Dt { get; set; }
        public int Timezone { get; set; }
        [JsonProperty("id")]
        public int CityId { get; set; }
        [JsonProperty("name")]
        public string CityName { get; set; }
        public int Cod { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; } = "weather01d.png";

        public SKFileLottieImageSource IconImage => new() { File = $"weather{Icon}.json" };
    }

    public class Coord
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }

    public class MainData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        public int Humidity { get; set; }

        public int Pressure { get; set; }
        [JsonProperty("sea_level")]
        public int PressureSeaLevel { get; set; }
        [JsonProperty("grnd_level")]
        public int PressureGroundLevel { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        public int CloudinessPercentage { get; set; }
    }

    public class Rain
    {
        [JsonProperty("1h")]
        public double RainVolume1h { get; set; }
        [JsonProperty("3h")]
        public double RainVolume3h { get; set; }
    }

    public class Snow
    {
        [JsonProperty("1h")]
        public double SnowVolume1h { get; set; }
        [JsonProperty("3h")]
        public double SnowVolume3h { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        [JsonProperty("deg")]
        public int Direction { get; set; }
        public double Gust { get; set; }

        public string Metric => Preferences.Default.Get("units", "metric") == "imperial" ? "mph" : "m/s";
    }

    public class Sys
    {
        public int Type { get; set; }
        public int Id { get; set; }
        [JsonProperty("country")]
        public string CountryCode { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }

        public string SunriseDateTime
        {
            get
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(Sunrise);
                DateTime dateTime = dateTimeOffset.UtcDateTime;
                return dateTime.ToString("HH:mm");
            }
        } 
        public string SunsetDateTime
        {
            get
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(Sunset);
                DateTime dateTime = dateTimeOffset.UtcDateTime;
                return dateTime.ToString("HH:mm");
            }
        }
    }
}
