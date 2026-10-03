using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public sealed class BlizzardWeatherSettings : WeatherStateSettings
    {
        public BlizzardWeatherSettings() : base(2f, 0f, new FloatRange(60f, 120f))
        {
        }

        [field: SerializeField, Min(0f)] public float TemperatureDropPerSecond { get; private set; } = 3f / 4f;
        [field: SerializeField] public float MovementSpeedModifier { get; private set; } = -0.25f;
        [field: SerializeField, Range(0f, 1f)] public float VisibilityReduction { get; private set; } = 0.75f;
    }
}
