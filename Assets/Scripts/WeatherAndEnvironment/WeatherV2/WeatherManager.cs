using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Selection;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.States.CombinedWeatherState;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2
{
    public sealed class WeatherManager : MonoBehaviour
    {
        [SerializeField] private WeatherProfileSO profile;
        [SerializeField] private Transform lightningTarget;
        [SerializeField, Min(0.01f)] private float temperatureTickInterval = 0.1f;


        #region UI

        [Header("UI FIELDS")]
        [SerializeField] TextMeshProUGUI WeatherStates;
        [SerializeField] TextMeshProUGUI Duration;
        [SerializeField] TextMeshProUGUI RemainingTime;

        [Space]
        [Header("Effects")]
        [SerializeField] TextMeshProUGUI TemperatureDropPerSecond;
        [SerializeField] TextMeshProUGUI MovementSpeedModifier;
        [SerializeField] TextMeshProUGUI AttackSpeedModifier;
        [SerializeField] TextMeshProUGUI NourishmentDepletionModifier;
        [SerializeField] TextMeshProUGUI FatigueDepletionModifier;
        [SerializeField] TextMeshProUGUI VisibilityReduction;

        [Space]
        [Header("Wind Info")]
        [SerializeField] TextMeshProUGUI WindDirection;
        [SerializeField] TextMeshProUGUI WithWindSpeedModifier;
        [SerializeField] TextMeshProUGUI AgainstWindSpeedModifier;
        [SerializeField] TextMeshProUGUI ComesInGusts;

        [Space]
        [Header("Spawned Enemies")]
        [SerializeField] TextMeshProUGUI SpawnedEnemies;

        #endregion

        private WeatherSelector selector;
        private CombinedWeatherState currentWeather;
        private float temperatureTickTimer;

        public WeatherSnapshot CurrentWeather { get; private set; }
        public bool IsLongNight { get; private set; }

        private void Start()
        {
            if (profile == null)
            {
                Debug.LogError($"{nameof(WeatherManager)} has no {nameof(WeatherProfileSO)} assigned.", this);
                enabled = false;
                return;
            }

            selector = new WeatherSelector(profile, GetLightningAreaCenter);
            RollNextWeather();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            currentWeather.Tick(deltaTime);
            TickTemperature(deltaTime);

            // UI Updates
            RemainingTime.text = $"Remaining time: {Math.Floor(currentWeather.RemainingTime)} sec";

            if (currentWeather.IsFinished)
            {
                RollNextWeather();
            }
        }

        private void OnDestroy()
        {
            currentWeather?.Exit();
        }

        public void SetLongNight(bool isLongNight)
        {
            IsLongNight = isLongNight;
        }

        [ContextMenu("Roll Next Weather")]
        public void RollNextWeather()
        {
            if (selector == null)
            {
                return;
            }

            TransitionTo(selector.CreateNextWeather(IsLongNight));
        }

        private void TransitionTo(CombinedWeatherState nextWeather)
        {
            CombinedWeatherState previousWeather = currentWeather;
            previousWeather?.Exit();

            currentWeather = nextWeather;
            currentWeather.Enter();
            CurrentWeather = currentWeather.CreateSnapshot();

            // Update UI
            WeatherStates.text = $"Weather States: {string.Join(" + ", CurrentWeather.Weathers.Select(w => $"{w.Type.ToString()} - Tier {w.Tier}").ToList())}";
            Duration.text = $"Duration: {CurrentWeather.Duration} sec";

            // Effects
            TemperatureDropPerSecond.text = $"Temperature drops: {CurrentWeather.Effects.TemperatureDropPerSecond} times per second.";
            MovementSpeedModifier.text = $"Movement speed modifier: {CurrentWeather.Effects.MovementSpeedModifier}";
            AttackSpeedModifier.text = $"Attack speed modifier: {CurrentWeather.Effects.AttackSpeedModifier}";
            NourishmentDepletionModifier.text = $"Nourishment depletion modifier: {CurrentWeather.Effects.NourishmentDepletionModifier}";
            FatigueDepletionModifier.text = $"Fatigue depletion modifier: {CurrentWeather.Effects.FatigueDepletionModifier}";
            VisibilityReduction.text = $"Visibility reduction: {CurrentWeather.Effects.VisibilityReduction}";

            // Wind info
            WindDirection.text = $"Wind direction: {CurrentWeather.Wind.Direction}";
            WithWindSpeedModifier.text = $"Going with the wind - speed modifier: {CurrentWeather.Wind.WithWindSpeedModifier}";
            AgainstWindSpeedModifier.text = $"Going against the wind - speed modifier: {CurrentWeather.Wind.AgainstWindSpeedModifier}";
            ComesInGusts.text = $"Comes in gusts: {CurrentWeather.Wind.ComesInGusts}";

            // Spawned enemies
            var spawnedText = currentWeather.SpawnedEnemies.Any()
                ? string.Join(" and ", currentWeather.SpawnedEnemies.Select(se => se.ToString()))
                : "None";
            SpawnedEnemies.text = $"Spawned enemies: {spawnedText}";

            RaiseWeatherEvents(previousWeather);
        }

        private void RaiseWeatherEvents(CombinedWeatherState previousWeather)
        {
            WeatherEffects effects = currentWeather.Effects;

            WeatherEvents.RaiseWeatherChanged(CurrentWeather);
            WeatherEvents.RaiseMovementSpeedModifierChanged(effects.MovementSpeedModifier);
            WeatherEvents.RaiseAttackSpeedModifierChanged(effects.AttackSpeedModifier);
            WeatherEvents.RaiseNourishmentDepletionModifierChanged(effects.NourishmentDepletionModifier);
            WeatherEvents.RaiseFatigueDepletionModifierChanged(effects.FatigueDepletionModifier);
            WeatherEvents.RaiseVisibilityReductionChanged(effects.VisibilityReduction);
            WeatherEvents.RaiseWindChanged(currentWeather.Wind);
            RaiseEnemySpawningChanges(previousWeather);
        }

        private void RaiseEnemySpawningChanges(CombinedWeatherState previousWeather)
        {
            IEnumerable<WeatherEnemyType> previousEnemies = previousWeather?.SpawnedEnemies ?? Enumerable.Empty<WeatherEnemyType>();
            IEnumerable<WeatherEnemyType> currentEnemies = currentWeather.SpawnedEnemies;

            foreach (WeatherEnemyType enemy in previousEnemies.Except(currentEnemies))
            {
                WeatherEvents.RaiseEnemySpawningChanged(enemy, false);
            }

            foreach (WeatherEnemyType enemy in currentEnemies.Except(previousEnemies))
            {
                WeatherEvents.RaiseEnemySpawningChanged(enemy, true);
            }
        }

        private void TickTemperature(float deltaTime)
        {
            temperatureTickTimer += deltaTime;

            while (temperatureTickTimer >= temperatureTickInterval)
            {
                temperatureTickTimer -= temperatureTickInterval;
                WeatherEvents.RaiseTemperatureDropped(currentWeather.Effects.TemperatureDropPerSecond * temperatureTickInterval);
            }
        }

        private Vector3 GetLightningAreaCenter()
        {
            return lightningTarget != null ? lightningTarget.position : transform.position;
        }
    }
}
