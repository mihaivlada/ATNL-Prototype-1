using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public sealed class ColdWeatherState : WeatherState
    {
        public ColdWeatherState(int tier, ColdTierSettings settings)
            : base(WeatherType.Cold, tier, new WeatherEffects(temperatureDropPerSecond: settings.TemperatureDropPerSecond))
        {
        }
    }
}
