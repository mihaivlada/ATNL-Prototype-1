using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public sealed class BlizzardWeatherState : WeatherState
    {
        public BlizzardWeatherState(BlizzardWeatherSettings settings)
            : base(WeatherType.Blizzard, NoTier, new WeatherEffects(
                temperatureDropPerSecond: settings.TemperatureDropPerSecond,
                movementSpeedModifier: settings.MovementSpeedModifier,
                visibilityReduction: settings.VisibilityReduction))
        {
        }
    }
}
