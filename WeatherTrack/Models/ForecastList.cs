using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTrack.Models
{
    public class ForecastList
    {
        public string Cod { get; set; }
        public int Message { get; set; }
        [JsonProperty("cnt")]
        public int QtyOfForecastsFetched { get; set; }

        [JsonProperty("list")]
        public List<ForecastData> ForecastDataList { get; set; } = new List<ForecastData>();

        public bool FetchSuccessful { get; set; } = true;
    }

    public class ForecastData
    {
        [JsonProperty("dt")]
        public int ForecastDateTime { get; set; } = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        [JsonProperty("main")]
        public MainData MainData { get; set; } = new();

        public List<WeatherDescription> Weather { get; set; } = new();

        [JsonProperty("pop")]
        public double ProbabilityOfPrecipitation { get; set; }

        public string ForecastDayOfMonth
        {
            get
            {
                return ConvertToDateTime(ForecastDateTime).ToString("dd/MM");
            }
        }

        public string ForecastDayOfWeek
        {
            get
            {
                return ConvertToDateTime(ForecastDateTime).ToString("ddd");
            }
        }

        public string ForecastHour
        {
            get
            {
                return ConvertToDateTime(ForecastDateTime).ToString("HH:mm");
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
