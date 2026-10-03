using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public sealed class ColdWeatherSettings : WeatherStateSettings
    {
        public ColdWeatherSettings() : base(15f, 0.4f, new FloatRange(120f, 300f))
        {
        }

        [field: SerializeField] public ColdTierSettings[] Tiers { get; private set; } = new[]
        {
            new ColdTierSettings("Tier 1", 60f, 1f / 6f),
            new ColdTierSettings("Tier 2", 30f, 2f / 6f),
            new ColdTierSettings("Tier 3", 10f, 3f / 4f)
        };

        public float GetTemperatureDropPerSecond(int tier)
        {
            return tier >= 1 && tier <= Tiers.Length ? Tiers[tier - 1].TemperatureDropPerSecond : 0f;
        }
    }

    [Serializable]
    public sealed class ColdTierSettings : WeatherTierSettings
    {
        public ColdTierSettings(string name, float weight, float temperatureDropPerSecond) : base(name, weight)
        {
            TemperatureDropPerSecond = temperatureDropPerSecond;
        }

        [field: SerializeField, Min(0f)] public float TemperatureDropPerSecond { get; private set; }
    }
}
