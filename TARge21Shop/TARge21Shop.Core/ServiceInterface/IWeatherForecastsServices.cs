using TARge21Shop.Core.Dto;
using TARge21Shop.Core.Dto.WeatherDtos;

namespace TARge21Shop.Core.ServiceInterface
{
    public interface IWeatherForecastsServices
    {
        Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto);
        Task<OpenWeatherResultDto> WeatherDetailsForOpenWeather(OpenWeatherResultDto dto);
    }
}
