using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public sealed class WindWeatherSettings : WeatherStateSettings
    {
        public WindWeatherSettings() : base(10f, 0.35f, new FloatRange(90f, 240f))
        {
        }

        [field: SerializeField] public WindTierSettings[] Tiers { get; private set; } = new[]
        {
            new WindTierSettings("Gusts of Wind", 50f, 0, 0.05f, -0.05f, true),
            new WindTierSettings("Continuous Wind", 35f, 1, 0.05f, -0.05f, false),
            new WindTierSettings("Heavy Wind", 15f, 2, 0.10f, -0.15f, false)
        };

        [field: SerializeField] public FloatRange GustInterval { get; private set; } = new FloatRange(5f, 12f);
        [field: SerializeField, Min(0f)] public float GustDuration { get; private set; } = 2f;
        [field: SerializeField, Min(0f)] public float GustTemperatureDrop { get; private set; } = 2f;
    }

    [Serializable]
    public sealed class WindTierSettings : WeatherTierSettings
    {
        public WindTierSettings(
            string name,
            float weight,
            int coldTier,
            float withWindSpeedModifier,
            float againstWindSpeedModifier,
            bool comesInGusts) : base(name, weight)
        {
            ColdTier = coldTier;
            WithWindSpeedModifier = withWindSpeedModifier;
            AgainstWindSpeedModifier = againstWindSpeedModifier;
            ComesInGusts = comesInGusts;
        }

        [field: SerializeField, Range(0, 3)] public int ColdTier { get; private set; }
        [field: SerializeField] public float WithWindSpeedModifier { get; private set; }
        [field: SerializeField] public float AgainstWindSpeedModifier { get; private set; }
        [field: SerializeField] public bool ComesInGusts { get; private set; }
    }
}
