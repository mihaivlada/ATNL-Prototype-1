using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public sealed class LightningStormWeatherSettings : WeatherStateSettings
    {
        public LightningStormWeatherSettings() : base(4f, 0.15f, new FloatRange(90f, 180f))
        {
        }

        [field: SerializeField] public FloatRange StrikeInterval { get; private set; } = new FloatRange(3f, 10f);
        [field: SerializeField, Min(0f)] public float StrikeAreaRadius { get; private set; } = 20f;
        [field: SerializeField, Min(0f)] public float Damage { get; private set; } = 40f;
        [field: SerializeField, Min(0f)] public float HitRadius { get; private set; } = 2f;
        [field: SerializeField, Range(0f, 1f)] public float LightningFiendSpawnChance { get; private set; } = 0.1f;
        [field: SerializeField, Min(0f)] public float MarkedDamageBonus { get; private set; } = 0.2f;
    }
}
