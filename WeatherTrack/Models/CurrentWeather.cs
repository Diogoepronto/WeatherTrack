using Newtonsoft.Json;
using SkiaSharp.Extended.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTrack.Models
{
    public class CurrentWeather
    {
        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; } = new();
        
        [JsonProperty("weather")]
        public List<WeatherDescription> Weather { get; set; } = new();

        [JsonProperty("main")]
        public MainData MainData { get; set; } = new();
        public Wind Wind { get; set; } = new();
        public Rain Rain { get; set; }
        public Snow Snow { get; set; }
        public Clouds Clouds { get; set; }
        public Sys Sys { get; set; } = new();
        public string Base { get; set; }
        public int Visibility { get; set; }
        
        [JsonProperty("dt")]
        public int CurrentWeatherDateTime { get; set; }
        public int Timezone { get; set; }
        
        [JsonProperty("id")]
        public int CityId { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; }

        public int Cod { get; set; }

        public bool FetchSuccessful { get; set; } = true;
    }

    public class WeatherDescription
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; } = "Failed to load data";
        public string Icon { get; set; } = "weather01d.png";

        public string IconImage => $"weather{Icon}.svg";
        public SKFileLottieImageSource IconAnimation => new() { File = $"weather{Icon}.json" };
    }

    public class Coordinates
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }

    public class MainData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; } = 0;

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; } = 0;

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        public int Humidity { get; set; } = 0;

        public int Pressure { get; set; } = 0;

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
        public double Speed { get; set; } = 0;

        [JsonProperty("deg")]
        public int Direction { get; set; } = 0;
        public double Gust { get; set; } = 0;

        public string Metric => Preferences.Default.Get("units", "metric") == "imperial" ? "mph" : "m/s";
    }

    public class Sys
    {
        public int Type { get; set; }
        public int Id { get; set; }
        [JsonProperty("country")]
        public string CountryCode { get; set; }
        public int Sunrise { get; set; } = 0;
        public int Sunset { get; set; } = 0;

        public string SunriseDateTime
        {
            get
            {
                return ConvertToDateTime(Sunrise).ToString("HH:mm");
            }
        } 
        public string SunsetDateTime
        {
            get
            {
                return ConvertToDateTime(Sunset).ToString("HH:mm");
            }
        }

        public DateTime ConvertToDateTime(int unixTime)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime);
            DateTime dateTime = dateTimeOffset.UtcDateTime;
            return dateTime;
        }
    }
}
