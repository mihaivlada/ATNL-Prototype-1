using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.States.Base;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public sealed class SnowWeatherState : WeatherState
    {
        public SnowWeatherState(int tier, SnowTierSettings settings, float coldTemperatureDropPerSecond)
            : base(WeatherType.Snow, tier, new WeatherEffects(
                temperatureDropPerSecond: coldTemperatureDropPerSecond,
                movementSpeedModifier: settings.MovementSpeedModifier,
                nourishmentDepletionModifier: settings.NourishmentDepletionModifier,
                fatigueDepletionModifier: settings.FatigueDepletionModifier))
        {
        }
    }
}
