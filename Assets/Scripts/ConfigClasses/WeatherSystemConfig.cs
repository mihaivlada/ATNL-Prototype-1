using Assets.Scripts.WeatherAndEnvironment.Weather.Enums;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assets.Scripts.ConfigClasses
{
    public static class WeatherSystemConfig
    {
        public static Dictionary<string, WeatherStateConfig> Weathers { get; private set; } =
            JsonSerializer.Deserialize<Dictionary<string, WeatherStateConfig>>(
                System.IO.File.ReadAllText("Assets/Scripts/weathersConfig.json"),
                new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                });
    }

    public class WeatherStateConfig
    {
        public int Weight { get; set; }
        public float CombinationChance { get; set; }
        public WeatherType[] AllowedCombinations { get; set; }
        public DurationConfig Duration { get; set; }
        public float TemperatureDropPerSecond { get; set; }
        public List<WeatherTierConfig> Tiers { get; set; }
    }

    public class DurationConfig
    {
        public float Min { get; set; }
        public float Max { get; set; }
    }

    public class WeatherTierConfig
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public float TemperatureDropPerSecond { get; set; }

        public float? MovementSpeedModifier { get; set; }
        public float? NourishmentModifier { get; set; }
        public float? FatigueModifier { get; set; }

        public float? WithWindMovementSpeedModifier { get; set; }
        public float? AgainstWindMovementSpeedModifier { get; set; }

        public bool? ComesInGusts { get; set; }
    }
}
