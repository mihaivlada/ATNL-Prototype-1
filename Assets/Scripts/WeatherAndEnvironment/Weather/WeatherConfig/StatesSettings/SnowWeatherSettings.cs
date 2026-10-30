using Assets.Scripts.ConfigClasses;
using Assets.Scripts.Utilities;
using Assets.Scripts.WeatherAndEnvironment.Weather.Enums;
using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.Base;
using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.StatesSettings
{
    [Serializable]
    public class SnowWeatherSettings : BaseStateSettings
    {
        public static WeatherStateConfig SnowConfig { get; private set; } = WeatherSystemConfig.Weathers[WeatherType.Snow.ToString()];

        public SnowWeatherSettings() : base(
            WeatherType.Snow,
            SnowConfig.Weight,
            new FloatRange(SnowConfig.Duration.Min, SnowConfig.Duration.Max),
            SnowConfig.CombinationChance,
            new WeatherType[] { WeatherType.Wind, WeatherType.Cold },
            new SnowTierSettings[]
            {
                new SnowTierSettings(
                    WeatherTierType.Tier1,
                    SnowConfig.Tiers[0].Name,
                    SnowConfig.Tiers[0].Weight,
                    SnowConfig.Tiers[0].TemperatureDropPerSecond,
                    SnowConfig.Tiers[0].MovementSpeedModifier ?? throw new NullReferenceException("MovementSpeedModifier is null"),
                    SnowConfig.Tiers[0].NourishmentModifier ?? throw new NullReferenceException("NourishmentModifier is null"),
                    SnowConfig.Tiers[0].FatigueModifier ?? throw new NullReferenceException("FatigueModifier is null")),
                new SnowTierSettings(
                    WeatherTierType.Tier2,
                    SnowConfig.Tiers[1].Name,
                    SnowConfig.Tiers[1].Weight,
                    SnowConfig.Tiers[1].TemperatureDropPerSecond,
                    SnowConfig.Tiers[1].MovementSpeedModifier ?? throw new NullReferenceException("MovementSpeedModifier is null"),
                    SnowConfig.Tiers[1].NourishmentModifier ?? throw new NullReferenceException("NourishmentModifier is null"),
                    SnowConfig.Tiers[1].FatigueModifier ?? throw new NullReferenceException("FatigueModifier is null")),
            }
        )
        {

        }
    }

    [Serializable]
    public class SnowTierSettings : BaseTierSettings
    {
        [field: SerializeField] public float MovementSpeedModifier { get; set; }
        [field: SerializeField] public float NourishmentModifier { get; set; }
        [field: SerializeField] public float FatigueModifier { get; set; }

        public SnowTierSettings(
            WeatherTierType tierType,
            string name,
            int weight,
            float temperatureDropPerSecond,
            float movementSpeedModifier,
            float nourishmentModifier,
            float fatigueModifier)
            : base(tierType, name, weight, temperatureDropPerSecond)
        {
            MovementSpeedModifier = movementSpeedModifier;
            NourishmentModifier = nourishmentModifier;
            FatigueModifier = fatigueModifier;
        }
    }
}
