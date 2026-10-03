using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events
{
    public readonly struct WindGustInfo
    {
        public WindGustInfo(
            Vector3 direction,
            float duration,
            float temperatureDrop,
            float withWindSpeedModifier,
            float againstWindSpeedModifier)
        {
            Direction = direction;
            Duration = duration;
            TemperatureDrop = temperatureDrop;
            WithWindSpeedModifier = withWindSpeedModifier;
            AgainstWindSpeedModifier = againstWindSpeedModifier;
        }

        public Vector3 Direction { get; }
        public float Duration { get; }
        public float TemperatureDrop { get; }
        public float WithWindSpeedModifier { get; }
        public float AgainstWindSpeedModifier { get; }
    }
}
