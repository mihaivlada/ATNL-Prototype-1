using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public sealed class SnowWeatherSettings : WeatherStateSettings
    {
        public SnowWeatherSettings() : base(8f, 0.3f, new FloatRange(120f, 240f))
        {
        }

        [field: SerializeField] public SnowTierSettings[] Tiers { get; private set; } = new[]
        {
            new SnowTierSettings("Tier 1", 70f, 1, -0.05f, 0.25f, 0.25f),
            new SnowTierSettings("Tier 2", 30f, 1, -0.15f, 0.35f, 0.35f)
        };
    }

    [Serializable]
    public sealed class SnowTierSettings : WeatherTierSettings
    {
        public SnowTierSettings(
            string name,
            float weight,
            int coldTier,
            float movementSpeedModifier,
            float nourishmentDepletionModifier,
            float fatigueDepletionModifier) : base(name, weight)
        {
            ColdTier = coldTier;
            MovementSpeedModifier = movementSpeedModifier;
            NourishmentDepletionModifier = nourishmentDepletionModifier;
            FatigueDepletionModifier = fatigueDepletionModifier;
        }

        [field: SerializeField, Range(0, 3)] public int ColdTier { get; private set; }
        [field: SerializeField] public float MovementSpeedModifier { get; private set; }
        [field: SerializeField] public float NourishmentDepletionModifier { get; private set; }
        [field: SerializeField] public float FatigueDepletionModifier { get; private set; }
    }
}
