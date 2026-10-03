using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events
{
    public readonly struct LightningStrikeInfo
    {
        public LightningStrikeInfo(
            Vector3 position,
            float damage,
            float hitRadius,
            bool spawnsLightningFiend,
            float markedDamageBonus)
        {
            Position = position;
            Damage = damage;
            HitRadius = hitRadius;
            SpawnsLightningFiend = spawnsLightningFiend;
            MarkedDamageBonus = markedDamageBonus;
        }

        public Vector3 Position { get; }
        public float Damage { get; }
        public float HitRadius { get; }
        public bool SpawnsLightningFiend { get; }
        public float MarkedDamageBonus { get; }
    }
}
