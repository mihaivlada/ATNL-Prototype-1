using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events
{
    public static class WeatherEvents
    {
        public static event Action<WeatherSnapshot> OnWeatherChanged;
        public static event Action<float> OnTemperatureDropped;
        public static event Action<float> OnMovementSpeedModifierChanged;
        public static event Action<float> OnAttackSpeedModifierChanged;
        public static event Action<float> OnNourishmentDepletionModifierChanged;
        public static event Action<float> OnFatigueDepletionModifierChanged;
        public static event Action<float> OnVisibilityReductionChanged;
        public static event Action<WindInfo> OnWindChanged;
        public static event Action<WindGustInfo> OnWindGust;
        public static event Action<LightningStrikeInfo> OnLightningStrike;
        public static event Action<WeatherEnemyType, bool> OnEnemySpawningChanged;

        internal static void RaiseWeatherChanged(WeatherSnapshot snapshot) => OnWeatherChanged?.Invoke(snapshot);
        internal static void RaiseTemperatureDropped(float amount) => OnTemperatureDropped?.Invoke(amount);
        internal static void RaiseMovementSpeedModifierChanged(float modifier) => OnMovementSpeedModifierChanged?.Invoke(modifier);
        internal static void RaiseAttackSpeedModifierChanged(float modifier) => OnAttackSpeedModifierChanged?.Invoke(modifier);
        internal static void RaiseNourishmentDepletionModifierChanged(float modifier) => OnNourishmentDepletionModifierChanged?.Invoke(modifier);
        internal static void RaiseFatigueDepletionModifierChanged(float modifier) => OnFatigueDepletionModifierChanged?.Invoke(modifier);
        internal static void RaiseVisibilityReductionChanged(float reduction) => OnVisibilityReductionChanged?.Invoke(reduction);
        internal static void RaiseWindChanged(WindInfo wind) => OnWindChanged?.Invoke(wind);
        internal static void RaiseWindGust(WindGustInfo gust) => OnWindGust?.Invoke(gust);
        internal static void RaiseLightningStrike(LightningStrikeInfo strike) => OnLightningStrike?.Invoke(strike);
        internal static void RaiseEnemySpawningChanged(WeatherEnemyType enemy, bool isAllowed) => OnEnemySpawningChanged?.Invoke(enemy, isAllowed);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ClearSubscribersOnPlayModeStart()
        {
            OnWeatherChanged = null;
            OnTemperatureDropped = null;
            OnMovementSpeedModifierChanged = null;
            OnAttackSpeedModifierChanged = null;
            OnNourishmentDepletionModifierChanged = null;
            OnFatigueDepletionModifierChanged = null;
            OnVisibilityReductionChanged = null;
            OnWindChanged = null;
            OnWindGust = null;
            OnLightningStrike = null;
            OnEnemySpawningChanged = null;
        }
    }
}
