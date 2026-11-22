using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherSettings.Base;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.Models
{
    public class CurrentWeatherStateModel
    {
        public BaseStateSettings State { get; }
        public BaseTierSettings Tier { get; }

        public CurrentWeatherStateModel(BaseStateSettings state, BaseTierSettings tier)
        {
            State = state;
            Tier = tier;
        }
    }
}
