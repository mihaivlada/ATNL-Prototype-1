using Assets.Scripts.Utilities;
using Assets.Scripts.WeatherAndEnvironment.Weather.Enums;
using Assets.Scripts.WeatherAndEnvironment.Weather.Models;
using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig;
using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.StatesSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.Selector
{
    public class WeatherSelector
    {
        private readonly WeatherProfileSO _profile;

        public WeatherSelector(WeatherProfileSO profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile), "WeatherProfileSO cannot be null.");
            }
            _profile = profile;
        }

        public CurrentWeatherModel GetCombinedWeatherStates()
        {
            var activeWeathers = new List<CurrentWeatherStateModel>();

            var firstState = GetRandomState();
            activeWeathers.Add(firstState);

            var duration = UnityEngine.Random.Range(
                firstState.State.Duration.Min,
                firstState.State.Duration.Max
            );

            AddCombinations(firstState, firstState, activeWeathers);

            // add wind info
            var windInfo = GetRandomWindInfo(activeWeathers);

            return new CurrentWeatherModel(activeWeathers, duration, windInfo);
        }

        #region PickingRandomWeatherStateLogic
        private CurrentWeatherStateModel GetRandomState()
        {
            var selectedState = WeightedRandom.Pick(_profile.AllStates, s => s.Weight);

            if (selectedState.AllTiers is null || selectedState.AllTiers.Count() == 0)
            {
                throw new InvalidOperationException("Selected weather state has no available tiers.");
            }

            var selectedTier = WeightedRandom.Pick(selectedState.AllTiers, t => t.Weight);

            return new CurrentWeatherStateModel(selectedState, selectedTier);
        }

        private void AddCombinations(CurrentWeatherStateModel baseState, CurrentWeatherStateModel currentState, List<CurrentWeatherStateModel> activeWeathers)
        {
            if (UnityEngine.Random.value < currentState.State.CombinationChance)
            {
                var newState = GetRandomStateFrom(currentState.State.AllowedCombinations);

                if (newState.State == baseState.State)
                    return;

                if (activeWeathers.Exists(x => x.State == newState.State))
                    return;

                activeWeathers.Add(newState);
                AddCombinations(baseState, newState, activeWeathers);
            }
        }

        private CurrentWeatherStateModel GetRandomStateFrom(IReadOnlyList<WeatherType> availableTypes)
        {
            var availableStates = availableTypes
                .Select(_profile.GetSettings)
                .ToList();

            var selectedState = WeightedRandom.Pick(availableStates, s => s.Weight);
            var selectedTier = selectedState.AllTiers != null
                && selectedState.AllTiers.Count() > 0
                    ? WeightedRandom.Pick(selectedState.AllTiers, t => t.Weight)
                    : null;

            return new CurrentWeatherStateModel(selectedState, selectedTier);
        }
        #endregion

        #region PickingWindInfoLogic
        public ActiveWindModel GetRandomWindInfo(List<CurrentWeatherStateModel> activeWeathers)
        {
            var windState = activeWeathers.FirstOrDefault(w => w.State.Type == WeatherType.Wind)?.State as WindWeatherSettings;
            var windTier = activeWeathers.FirstOrDefault(w => w.State.Type == WeatherType.Wind)?.Tier as WindTierSettings;

            if (windState == null || windTier == null)
            {
                // Default wind model
                return new ActiveWindModel(false, Vector3.zero, false);
            }

            var windDirection = new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                0,
                UnityEngine.Random.Range(-1f, 1f)
            ).normalized;

            return new ActiveWindModel(true, windDirection, windTier.ComesInGusts);
        }
        #endregion
    }
}