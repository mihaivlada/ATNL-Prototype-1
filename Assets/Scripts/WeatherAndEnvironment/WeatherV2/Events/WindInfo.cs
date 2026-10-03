using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events
{
    public readonly struct WindInfo
    {
        public static readonly WindInfo None = default;

        public WindInfo(Vector3 direction, float withWindSpeedModifier, float againstWindSpeedModifier, bool comesInGusts)
        {
            Direction = direction;
            WithWindSpeedModifier = withWindSpeedModifier;
            AgainstWindSpeedModifier = againstWindSpeedModifier;
            ComesInGusts = comesInGusts;
        }

        public bool HasWind => Direction != Vector3.zero;
        public Vector3 Direction { get; }
        public float WithWindSpeedModifier { get; }
        public float AgainstWindSpeedModifier { get; }
        public bool ComesInGusts { get; }
    }
}
