using Assets.Scripts.ConfigClasses;
using Assets.Scripts.Utilities;
using Assets.Scripts.WeatherAndEnvironment.Weather.Enums;
using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.Base;
using System;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.StatesSettings
{
    [Serializable]
    public class ColdWeatherSettings : BaseStateSettings
    {
        public static WeatherStateConfig ColdConfig { get; private set; } = WeatherSystemConfig.Weathers[WeatherType.Cold.ToString()];

        public ColdWeatherSettings() : base(
            WeatherType.Cold,
            ColdConfig.Weight,
            new FloatRange(ColdConfig.Duration.Min, ColdConfig.Duration.Max),
            ColdConfig.CombinationChance,
            new WeatherType[] { WeatherType.Wind, WeatherType.Snow },
            new ColdTierSettings[]
            {
                new ColdTierSettings(WeatherTierType.Tier1, ColdConfig.Tiers[0].Name, ColdConfig.Tiers[0].Weight, ColdConfig.Tiers[0].TemperatureDropPerSecond),
                new ColdTierSettings(WeatherTierType.Tier2, ColdConfig.Tiers[1].Name, ColdConfig.Tiers[1].Weight, ColdConfig.Tiers[1].TemperatureDropPerSecond),
                new ColdTierSettings(WeatherTierType.Tier3, ColdConfig.Tiers[2].Name, ColdConfig.Tiers[2].Weight, ColdConfig.Tiers[2].TemperatureDropPerSecond)
            }
        )
        {

        }
    }

    [Serializable]
    public class ColdTierSettings : BaseTierSettings
    {
        public ColdTierSettings(
            WeatherTierType tierType,
            string name,
            int weight,
            float temperatureDropPerSecond)
            : base(tierType, name, weight, temperatureDropPerSecond)
        {

        }
    }
}
