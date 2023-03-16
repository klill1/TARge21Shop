using System.Text.Json.Serialization;

namespace TARge21Shop.Core.Dto.WeatherDtos
{
    public class OpenWeatherDto
    {
        [JsonPropertyName("main")]
        public Mains Main { get; set; }

        [JsonPropertyName("wind")]
        public Winds Wind { get; set; }

        [JsonPropertyName("weather")]
        public List<Weathers> Weather { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        public class Weathers
        {
            [JsonPropertyName("description")]
            public string Description { get; set; }
        }

        public class Mains
        {
            [JsonPropertyName("temp")]
            public double Temp { get; set; }

            [JsonPropertyName("feels_like")]
            public double Feels_Like { get; set; }

            [JsonPropertyName("pressure")]
            public int Pressure { get; set; }

            [JsonPropertyName("humidity")]
            public int Humidity { get; set; }
        }

        public class Winds
        {
            [JsonPropertyName("speed")]
            public double Speed { get; set; }
        }
    }
}
