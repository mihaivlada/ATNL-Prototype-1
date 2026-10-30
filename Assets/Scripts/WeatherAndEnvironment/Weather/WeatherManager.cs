using Assets.Scripts.WeatherAndEnvironment.Weather.Events;
using Assets.Scripts.WeatherAndEnvironment.Weather.Models;
using Assets.Scripts.WeatherAndEnvironment.Weather.Selector;
using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.Weather
{
    public class WeatherManager : MonoBehaviour
    {
        [SerializeField] private WeatherProfileSO profile;

        #region UI

        [Header("UI FIELDS")]
        [SerializeField] TextMeshProUGUI WeatherStates;
        [SerializeField] TextMeshProUGUI Duration;

        [Space]
        [Header("Effects")]
        [SerializeField] TextMeshProUGUI TemperatureDropPerSecond;
        [SerializeField] TextMeshProUGUI MovementSpeedModifier;
        [SerializeField] TextMeshProUGUI NourishmentDepletionModifier;
        [SerializeField] TextMeshProUGUI FatigueDepletionModifier;

        [Space]
        [Header("Wind Info")]
        [SerializeField] TextMeshProUGUI WindDirection;
        [SerializeField] TextMeshProUGUI WithWindSpeedModifier;
        [SerializeField] TextMeshProUGUI AgainstWindSpeedModifier;
        [SerializeField] TextMeshProUGUI ComesInGusts;

        #endregion

        public CurrentWeatherModel CurrentWeatherModel { get; set; }
        private WeatherSelector _selector;
        private Coroutine _weatherCycleCoroutine;
        private Coroutine _weatherEffectsCoroutine;


        private void Awake()
        {
            if (profile == null)
            {
                Debug.LogError($"{nameof(WeatherManager)} has no {nameof(WeatherProfileSO)} assigned.", this);
                enabled = false;
                return;
            }
            _selector = new WeatherSelector(profile);
        }

        private void OnEnable()
        {
            _weatherCycleCoroutine = StartCoroutine(WeatherCycle());
            _weatherEffectsCoroutine = StartCoroutine(WeatherEffects());
        }

        private void OnDisable()
        {
            if (_weatherCycleCoroutine != null)
            {
                StopCoroutine(_weatherCycleCoroutine);
                _weatherCycleCoroutine = null;
            }
            if (_weatherEffectsCoroutine != null)
            {
                StopCoroutine(_weatherEffectsCoroutine);
                _weatherEffectsCoroutine = null;
            }
        }

        private void DisplayWeatherInfo()
        {
            // Update UI
            WeatherStates.text = $"Weather States: {string.Join(" + ", CurrentWeatherModel.ActiveWeathers.Select(w => w.Tier == null ? w.State.Type.ToString() + "; " : $"{w.State.Type} - Tier {w.Tier.TierType}; "))}";
            Duration.text = $"Duration: {CurrentWeatherModel.Duration} sec";

            // Effects
            TemperatureDropPerSecond.text = $"Temperature drops: {CurrentWeatherModel.TemperatureDropPerSecond} times per second.";
            MovementSpeedModifier.text = $"Movement speed modifier: {CurrentWeatherModel.MovementSpeedModifier}";
            NourishmentDepletionModifier.text = $"Nourishment depletion modifier: {CurrentWeatherModel.NourishmentModifier}";
            FatigueDepletionModifier.text = $"Fatigue depletion modifier: {CurrentWeatherModel.FatigueModifier}";

            // Wind info
            WindDirection.text = $"Wind direction: {CurrentWeatherModel.WindInfo.Direction}";
            WithWindSpeedModifier.text = $"Going with the wind - speed modifier: {CurrentWeatherModel.WithWindMovementSpeedModifier}";
            AgainstWindSpeedModifier.text = $"Going against the wind - speed modifier: {CurrentWeatherModel.AgainstWindMovementSpeedModifier}";
            ComesInGusts.text = $"Comes in gusts: {CurrentWeatherModel.WindInfo.ComesInGusts}";
        }

        #region Coroutines
        private IEnumerator WeatherCycle()
        {
            while (true)
            {
                CurrentWeatherModel = _selector.GetCombinedWeatherStates();

                DisplayWeatherInfo();

                WeatherEvents.RaiseWeatherChanged(CurrentWeatherModel);

                yield return new WaitForSeconds(CurrentWeatherModel.Duration);
            }
        }

        private IEnumerator WeatherEffects()
        {
            var interval = Helper.weatherModifiersInterval;

            while (true)
            {
                yield return new WaitForSeconds(interval);
                WeatherEvents.RaiseTemperatureDropped(CurrentWeatherModel.TemperatureDropPerSecond * interval);
                WeatherEvents.RaiseMovementSpeedModifierChanged(CurrentWeatherModel.MovementSpeedModifier * interval);
                WeatherEvents.RaiseNourishmentDepletionModifierChanged(CurrentWeatherModel.NourishmentModifier * interval);
                WeatherEvents.RaiseFatigueDepletionModifierChanged(CurrentWeatherModel.FatigueModifier * interval);
                WeatherEvents.RaiseWindChanged(CurrentWeatherModel.WindInfo);
            }
        }
        #endregion
    }
}
