using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events
{
    public readonly struct ActiveWeather
    {
        public ActiveWeather(WeatherType type, int tier)
        {
            Type = type;
            Tier = tier;
        }

        public WeatherType Type { get; }
        public int Tier { get; }
    }

    public sealed class WeatherSnapshot
    {
        public WeatherSnapshot(IReadOnlyList<ActiveWeather> weathers, float duration, WeatherEffects effects, WindInfo wind)
        {
            Weathers = weathers;
            Duration = duration;
            Effects = effects;
            Wind = wind;
        }

        public IReadOnlyList<ActiveWeather> Weathers { get; }
        public float Duration { get; }
        public WeatherEffects Effects { get; }
        public WindInfo Wind { get; }

        public bool Contains(WeatherType type)
        {
            foreach (ActiveWeather weather in Weathers)
            {
                if (weather.Type == type)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
