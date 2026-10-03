using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public sealed class ClearWeatherSettings : WeatherStateSettings
    {
        public ClearWeatherSettings() : base(50f, 0f, new FloatRange(120f, 300f))
        {
        }

        [field: SerializeField, Min(0f)] public float TemperatureDropPerSecond { get; private set; } = 1f / 10f;
    }
}
