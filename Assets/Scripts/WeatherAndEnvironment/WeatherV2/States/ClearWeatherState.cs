using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public sealed class ClearWeatherState : WeatherState
    {
        public ClearWeatherState(ClearWeatherSettings settings)
            : base(WeatherType.Clear, NoTier, new WeatherEffects(temperatureDropPerSecond: settings.TemperatureDropPerSecond))
        {
        }
    }
}
