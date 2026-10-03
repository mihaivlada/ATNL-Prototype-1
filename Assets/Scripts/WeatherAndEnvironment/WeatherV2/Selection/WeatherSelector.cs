using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.States;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Selection
{
    public sealed class WeatherSelector
    {
        private static readonly WeatherType[] AllWeatherTypes = (WeatherType[])Enum.GetValues(typeof(WeatherType));

        private readonly WeatherProfileSO profile;
        private readonly WeatherStateFactory factory;

        public WeatherSelector(WeatherProfileSO profile, Func<Vector3> getLightningAreaCenter)
        {
            this.profile = profile;
            factory = new WeatherStateFactory(profile, getLightningAreaCenter);
        }

        public CombinedWeatherState CreateNextWeather(bool isLongNight)
        {
            WeatherType primary = WeightedRandom.Pick(AllWeatherTypes, type => GetWeight(type, isLongNight));
            List<WeatherType> types = PickCombinedTypes(primary, isLongNight);
            float duration = profile.GetSettings(primary).Duration.GetRandom();

            return new CombinedWeatherState(types.Select(factory.Create), duration);
        }

        private List<WeatherType> PickCombinedTypes(WeatherType primary, bool isLongNight)
        {
            var picked = new List<WeatherType> { primary };

            foreach (WeatherType candidate in AllWeatherTypes.OrderBy(_ => Random.value))
            {
                if (CanJoin(candidate, picked) && Random.value < GetCombineChance(candidate, isLongNight))
                {
                    picked.Add(candidate);
                }
            }

            return picked;
        }

        private bool CanJoin(WeatherType candidate, List<WeatherType> picked)
        {
            return !picked.Contains(candidate) && picked.All(type => profile.CanCombine(type, candidate));
        }

        private float GetWeight(WeatherType type, bool isLongNight)
        {
            return profile.GetSettings(type).Weight * GetLongNightMultiplier(type, isLongNight);
        }

        private float GetCombineChance(WeatherType type, bool isLongNight)
        {
            return profile.GetSettings(type).CombineChance * GetLongNightMultiplier(type, isLongNight);
        }

        private float GetLongNightMultiplier(WeatherType type, bool isLongNight)
        {
            return isLongNight && type == WeatherType.VeilOfDarkness ? profile.VeilOfDarkness.LongNightChanceMultiplier : 1f;
        }
    }
}
