using Assets.Scripts.ConfigClasses;
using Assets.Scripts.Utilities;
using Assets.Scripts.WeatherAndEnvironment.Weather.Enums;
using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherSettings.Base;
using System;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.WeatherSettings.StatesSettings
{
    [Serializable]
    public class ClearWeatherSettings : BaseStateSettings
    {
        public static WeatherStateConfig ClearConfig { get; private set; } = WeatherSystemConfig.Weathers[WeatherType.Clear.ToString()];

        public ClearWeatherSettings() : base(
            WeatherType.Clear,
            ClearConfig.Weight,
            new FloatRange(ClearConfig.Duration.Min, ClearConfig.Duration.Max),
            ClearConfig.CombinationChance,
            null,
            new ClearTierSettings[]
            {
                new ClearTierSettings(WeatherTierType.Tier1, ClearConfig.Tiers[0].Name, ClearConfig.Tiers[0].Weight, ClearConfig.Tiers[0].TemperatureDropPerSecond)
            }
        )
        {

        }
    }

    public class ClearTierSettings : BaseTierSettings
    {
        public ClearTierSettings(
            WeatherTierType tierType,
            string name,
            int weight,
            float temperatureDropPerSecond)
            : base(tierType, name, weight, temperatureDropPerSecond
        )
        {

        }
    }
}
