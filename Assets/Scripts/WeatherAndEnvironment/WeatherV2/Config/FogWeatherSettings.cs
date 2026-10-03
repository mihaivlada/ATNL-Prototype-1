using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public sealed class FogWeatherSettings : WeatherStateSettings
    {
        public FogWeatherSettings() : base(7f, 0f, new FloatRange(90f, 180f))
        {
        }

        [field: SerializeField] public FogTierSettings[] Tiers { get; private set; } = new[]
        {
            new FogTierSettings("Tier 1", 65f, 0.4f, false),
            new FogTierSettings("Tier 2", 35f, 0.6f, true)
        };
    }

    [Serializable]
    public sealed class FogTierSettings : WeatherTierSettings
    {
        public FogTierSettings(string name, float weight, float visibilityReduction, bool spawnsFogDwellers) : base(name, weight)
        {
            VisibilityReduction = visibilityReduction;
            SpawnsFogDwellers = spawnsFogDwellers;
        }

        [field: SerializeField, Range(0f, 1f)] public float VisibilityReduction { get; private set; }
        [field: SerializeField] public bool SpawnsFogDwellers { get; private set; }
    }
}
