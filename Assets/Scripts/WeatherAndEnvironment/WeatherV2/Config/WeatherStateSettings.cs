using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public abstract class WeatherStateSettings
    {
        protected WeatherStateSettings(float weight, float combineChance, FloatRange duration)
        {
            Weight = weight;
            CombineChance = combineChance;
            Duration = duration;
        }

        [field: SerializeField, Min(0f)] public float Weight { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float CombineChance { get; private set; }
        [field: SerializeField] public FloatRange Duration { get; private set; }
    }

    [Serializable]
    public abstract class WeatherTierSettings
    {
        protected WeatherTierSettings(string name, float weight)
        {
            Name = name;
            Weight = weight;
        }

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField, Min(0f)] public float Weight { get; private set; }
    }
}
