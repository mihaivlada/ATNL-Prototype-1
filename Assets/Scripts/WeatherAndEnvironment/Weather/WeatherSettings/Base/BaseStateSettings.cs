using Assets.Scripts.Utilities;
using Assets.Scripts.WeatherAndEnvironment.Weather.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.WeatherSettings.Base
{
    [Serializable]
    public class BaseStateSettings
    {
        [field: SerializeField] public WeatherType Type { get; set; }
        [field: SerializeField] public int Weight { get; set; }
        [field: SerializeField] public FloatRange Duration { get; set; }
        [field: SerializeField, Range(0f, 1f)] public float CombinationChance { get; set; }
        [field: SerializeField] public WeatherType[] AllowedCombinations { get; set; }
        [field: SerializeField] public BaseTierSettings[] AllTiers { get; set; }

        public BaseStateSettings(WeatherType type, int weight, FloatRange duration, float combinationChance, WeatherType[] allowedCombinations, BaseTierSettings[] allTiers)
        {
            Type = type;
            Weight = weight;
            Duration = duration;
            CombinationChance = combinationChance;
            AllowedCombinations = allowedCombinations;
            AllTiers = allTiers;
        }
    }
}
