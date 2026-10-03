using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public sealed class GreatFrostWeatherState : WeatherState
    {
        public GreatFrostWeatherState(GreatFrostWeatherSettings settings)
            : base(WeatherType.GreatFrost, NoTier, new WeatherEffects(
                temperatureDropPerSecond: settings.TemperatureDropPerSecond,
                attackSpeedModifier: settings.AttackSpeedModifier,
                nourishmentDepletionModifier: settings.NourishmentDepletionModifier,
                fatigueDepletionModifier: settings.FatigueDepletionModifier))
        {
        }
    }
}
