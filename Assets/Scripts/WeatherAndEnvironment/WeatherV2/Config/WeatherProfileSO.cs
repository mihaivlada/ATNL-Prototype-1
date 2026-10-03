using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [CreateAssetMenu(fileName = "WeatherProfile", menuName = "WeatherV2/Weather Profile")]
    public sealed class WeatherProfileSO : ScriptableObject
    {
        [field: SerializeField] public ClearWeatherSettings Clear { get; private set; } = new ClearWeatherSettings();
        [field: SerializeField] public ColdWeatherSettings Cold { get; private set; } = new ColdWeatherSettings();
        [field: SerializeField] public SnowWeatherSettings Snow { get; private set; } = new SnowWeatherSettings();
        [field: SerializeField] public WindWeatherSettings Wind { get; private set; } = new WindWeatherSettings();
        [field: SerializeField] public FogWeatherSettings Fog { get; private set; } = new FogWeatherSettings();
        [field: SerializeField] public BlizzardWeatherSettings Blizzard { get; private set; } = new BlizzardWeatherSettings();
        [field: SerializeField] public LightningStormWeatherSettings LightningStorm { get; private set; } = new LightningStormWeatherSettings();
        [field: SerializeField] public GreatFrostWeatherSettings GreatFrost { get; private set; } = new GreatFrostWeatherSettings();
        [field: SerializeField] public VeilOfDarknessWeatherSettings VeilOfDarkness { get; private set; } = new VeilOfDarknessWeatherSettings();

        [field: SerializeField] public WeatherCombination[] Combinations { get; private set; } = new[]
        {
            new WeatherCombination(WeatherType.Cold, WeatherType.Wind),
            new WeatherCombination(WeatherType.Cold, WeatherType.Snow),
            new WeatherCombination(WeatherType.Cold, WeatherType.LightningStorm),
            new WeatherCombination(WeatherType.Cold, WeatherType.VeilOfDarkness),
            new WeatherCombination(WeatherType.Snow, WeatherType.Wind),
            new WeatherCombination(WeatherType.Wind, WeatherType.LightningStorm)
        };

        public WeatherStateSettings GetSettings(WeatherType type)
        {
            return type switch
            {
                WeatherType.Clear => Clear,
                WeatherType.Cold => Cold,
                WeatherType.Snow => Snow,
                WeatherType.Wind => Wind,
                WeatherType.Fog => Fog,
                WeatherType.Blizzard => Blizzard,
                WeatherType.LightningStorm => LightningStorm,
                WeatherType.GreatFrost => GreatFrost,
                WeatherType.VeilOfDarkness => VeilOfDarkness,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public bool CanCombine(WeatherType first, WeatherType second)
        {
            foreach (WeatherCombination combination in Combinations)
            {
                if (combination.Matches(first, second))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
