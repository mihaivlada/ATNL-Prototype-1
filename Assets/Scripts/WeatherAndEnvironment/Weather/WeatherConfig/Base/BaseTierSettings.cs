using Assets.Scripts.WeatherAndEnvironment.Weather.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.Base
{
    [Serializable]
    public class BaseTierSettings
    {
        [field: SerializeField] public WeatherTierType TierType { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public int Weight { get; set; }
        [field: SerializeField] public float TemperatureDropPerSecond { get; set; }

        public BaseTierSettings(WeatherTierType tierType, string name, int weight, float temperatureDropPerSecond)
        {
            TierType = tierType;
            Name = name;
            Weight = weight;
            TemperatureDropPerSecond = temperatureDropPerSecond;
        }
    }
}
