using Assets.Scripts.ConfigClasses;
using Assets.Scripts.Utilities;
using Assets.Scripts.WeatherAndEnvironment.Weather.Enums;
using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.Base;
using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.StatesSettings
{
    [Serializable]
    public class WindWeatherSettings : BaseStateSettings
    {
        public static WeatherStateConfig WindConfig { get; private set; } = WeatherSystemConfig.Weathers[WeatherType.Wind.ToString()];

        public WindWeatherSettings() : base(
            WeatherType.Wind,
            WindConfig.Weight,
            new FloatRange(WindConfig.Duration.Min, WindConfig.Duration.Max),
            WindConfig.CombinationChance,
            new WeatherType[] { WeatherType.Cold, WeatherType.Snow },
            new WindTierSettings[]
            {
                new WindTierSettings(
                    WeatherTierType.Tier1,
                    WindConfig.Tiers[0].Name,
                    WindConfig.Tiers[0].Weight,
                    WindConfig.Tiers[0].TemperatureDropPerSecond,
                    WindConfig.Tiers[0].WithWindMovementSpeedModifier ?? throw new NullReferenceException("WithWindMovementSpeedModifier is null"),
                    WindConfig.Tiers[0].AgainstWindMovementSpeedModifier ?? throw new NullReferenceException("AgainstWindMovementSpeedModifier is null"),
                    WindConfig.Tiers[0].ComesInGusts ?? throw new NullReferenceException("ComesInGusts is null")),
                new WindTierSettings(
                    WeatherTierType.Tier2,
                    WindConfig.Tiers[1].Name,
                    WindConfig.Tiers[1].Weight,
                    WindConfig.Tiers[1].TemperatureDropPerSecond,
                    WindConfig.Tiers[1].WithWindMovementSpeedModifier ?? throw new NullReferenceException("WithWindMovementSpeedModifier is null"),
                    WindConfig.Tiers[1].AgainstWindMovementSpeedModifier ?? throw new NullReferenceException("AgainstWindMovementSpeedModifier is null"),
                    WindConfig.Tiers[1].ComesInGusts ?? throw new NullReferenceException("ComesInGusts is null")),
                new WindTierSettings(
                    WeatherTierType.Tier3,
                    WindConfig.Tiers[2].Name,
                    WindConfig.Tiers[2].Weight,
                    WindConfig.Tiers[2].TemperatureDropPerSecond,
                    WindConfig.Tiers[2].WithWindMovementSpeedModifier ?? throw new NullReferenceException("WithWindMovementSpeedModifier is null"),
                    WindConfig.Tiers[2].AgainstWindMovementSpeedModifier ?? throw new NullReferenceException("AgainstWindMovementSpeedModifier is null"),
                    WindConfig.Tiers[2].ComesInGusts ?? throw new NullReferenceException("ComesInGusts is null"))
            }
        )
        {

        }
    }

    [Serializable]
    public class WindTierSettings : BaseTierSettings
    {
        [field: SerializeField] public float WithWindMovementSpeedModifier { get; set; }
        [field: SerializeField] public float AgainstWindMovementSpeedModifier { get; set; }
        [field: SerializeField] public bool ComesInGusts { get; set; }

        public WindTierSettings(
            WeatherTierType tierType,
            string name,
            int weight,
            float temperatureDropPerSecond,
            float withWindMovementSpeedModifier,
            float againstWindMovementSpeedModifier,
            bool comesInGusts)
            : base(tierType, name, weight, temperatureDropPerSecond)
        {
            WithWindMovementSpeedModifier = withWindMovementSpeedModifier;
            AgainstWindMovementSpeedModifier = againstWindMovementSpeedModifier;
            ComesInGusts = comesInGusts;
        }
    }
}
