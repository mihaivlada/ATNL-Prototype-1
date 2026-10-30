using Assets.Scripts.WeatherAndEnvironment.Weather.Enums;
using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.Base;
using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.StatesSettings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig
{
    [CreateAssetMenu(fileName = "WeatherProfile", menuName = "Weather/Weather Profile")]
    public class WeatherProfileSO : ScriptableObject
    {
        [field: SerializeField] public ClearWeatherSettings Clear { get; private set; }
        [field: SerializeField] public ColdWeatherSettings Cold { get; private set; }
        [field: SerializeField] public SnowWeatherSettings Snow { get; private set; }
        [field: SerializeField] public WindWeatherSettings Wind { get; private set; }

        public IReadOnlyList<BaseStateSettings> AllStates => new BaseStateSettings[] { Clear, Cold, Snow, Wind };

        public BaseStateSettings GetSettings(WeatherType type)
        {
            return type switch
            {
                WeatherType.Clear => Clear,
                WeatherType.Cold => Cold,
                WeatherType.Snow => Snow,
                WeatherType.Wind => Wind,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        private void OnEnable()
        {
        }
    }
}
