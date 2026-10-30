using Assets.Scripts.WeatherAndEnvironment.Weather.Models;
using System;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.Events
{
    public static class WeatherEvents
    {
        public static event Action<CurrentWeatherModel> OnWeatherChanged;
        public static event Action<float> OnTemperatureDropped;
        public static event Action<float> OnMovementSpeedModifierChanged;
        public static event Action<float> OnNourishmentDepletionModifierChanged;
        public static event Action<float> OnFatigueDepletionModifierChanged;
        public static event Action<ActiveWindModel> OnWindChanged;

        public static void RaiseWeatherChanged(CurrentWeatherModel currentWeather) => OnWeatherChanged?.Invoke(currentWeather);
        public static void RaiseTemperatureDropped(float temperatureDrop) => OnTemperatureDropped?.Invoke(temperatureDrop);
        public static void RaiseMovementSpeedModifierChanged(float modifier) => OnMovementSpeedModifierChanged?.Invoke(modifier);
        public static void RaiseNourishmentDepletionModifierChanged(float modifier) => OnNourishmentDepletionModifierChanged?.Invoke(modifier);
        public static void RaiseFatigueDepletionModifierChanged(float modifier) => OnFatigueDepletionModifierChanged?.Invoke(modifier);
        public static void RaiseWindChanged(ActiveWindModel windInfo) => OnWindChanged?.Invoke(windInfo);
    }
}
