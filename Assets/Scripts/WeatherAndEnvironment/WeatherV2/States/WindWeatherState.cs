using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public sealed class WindWeatherState : WeatherState
    {
        private readonly WindWeatherSettings settings;
        private readonly WindTierSettings tierSettings;
        private readonly WindInfo wind;
        private float timeUntilNextGust;

        public WindWeatherState(int tier, WindTierSettings tierSettings, WindWeatherSettings settings, float coldTemperatureDropPerSecond)
            : base(WeatherType.Wind, tier, new WeatherEffects(temperatureDropPerSecond: coldTemperatureDropPerSecond))
        {
            this.settings = settings;
            this.tierSettings = tierSettings;
            Direction = GetRandomHorizontalDirection();
            wind = tierSettings.ComesInGusts
                ? new WindInfo(Direction, 0f, 0f, true)
                : new WindInfo(Direction, tierSettings.WithWindSpeedModifier, tierSettings.AgainstWindSpeedModifier, false);
        }

        public Vector3 Direction { get; }
        public override WindInfo Wind => wind;

        public override void Enter()
        {
            timeUntilNextGust = settings.GustInterval.GetRandom();
        }

        public override void Tick(float deltaTime)
        {
            if (!tierSettings.ComesInGusts)
            {
                return;
            }

            timeUntilNextGust -= deltaTime;

            if (timeUntilNextGust > 0f)
            {
                return;
            }

            timeUntilNextGust += settings.GustInterval.GetRandom();
            WeatherEvents.RaiseWindGust(CreateGust());
        }

        private WindGustInfo CreateGust()
        {
            return new WindGustInfo(
                Direction,
                settings.GustDuration,
                settings.GustTemperatureDrop,
                tierSettings.WithWindSpeedModifier,
                tierSettings.AgainstWindSpeedModifier);
        }

        private static Vector3 GetRandomHorizontalDirection()
        {
            return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * Vector3.forward;
        }
    }
}
