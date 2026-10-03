using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config
{
    [Serializable]
    public struct WeatherCombination
    {
        public WeatherCombination(WeatherType first, WeatherType second)
        {
            First = first;
            Second = second;
        }

        [field: SerializeField] public WeatherType First { get; private set; }
        [field: SerializeField] public WeatherType Second { get; private set; }

        public bool Matches(WeatherType first, WeatherType second)
        {
            return (First == first && Second == second) || (First == second && Second == first);
        }
    }
}
