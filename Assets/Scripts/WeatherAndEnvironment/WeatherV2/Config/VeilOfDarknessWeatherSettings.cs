using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public sealed class VeilOfDarknessWeatherSettings : WeatherStateSettings
    {
        public VeilOfDarknessWeatherSettings() : base(2f, 0.15f, new FloatRange(120f, 240f))
        {
        }

        [field: SerializeField, Min(1f)] public float LongNightChanceMultiplier { get; private set; } = 3f;
    }
}
