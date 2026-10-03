using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public sealed class GreatFrostWeatherSettings : WeatherStateSettings
    {
        public GreatFrostWeatherSettings() : base(2f, 0f, new FloatRange(60f, 120f))
        {
        }

        [field: SerializeField, Min(0f)] public float TemperatureDropPerSecond { get; private set; } = 2f;
        [field: SerializeField] public float AttackSpeedModifier { get; private set; } = -0.15f;
        [field: SerializeField] public float NourishmentDepletionModifier { get; private set; } = 0.25f;
        [field: SerializeField] public float FatigueDepletionModifier { get; private set; } = 0.25f;
    }
}
