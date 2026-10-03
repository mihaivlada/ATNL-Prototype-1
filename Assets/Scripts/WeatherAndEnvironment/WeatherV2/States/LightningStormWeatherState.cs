using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public sealed class LightningStormWeatherState : WeatherState
    {
        private readonly LightningStormWeatherSettings settings;
        private readonly Func<Vector3> getStrikeAreaCenter;
        private float timeUntilNextStrike;

        public LightningStormWeatherState(LightningStormWeatherSettings settings, Func<Vector3> getStrikeAreaCenter)
            : base(WeatherType.LightningStorm, NoTier, WeatherEffects.None)
        {
            this.settings = settings;
            this.getStrikeAreaCenter = getStrikeAreaCenter;
        }

        public override void Enter()
        {
            timeUntilNextStrike = settings.StrikeInterval.GetRandom();
        }

        public override void Tick(float deltaTime)
        {
            timeUntilNextStrike -= deltaTime;

            if (timeUntilNextStrike > 0f)
            {
                return;
            }

            timeUntilNextStrike += settings.StrikeInterval.GetRandom();
            WeatherEvents.RaiseLightningStrike(CreateStrike());
        }

        private LightningStrikeInfo CreateStrike()
        {
            Vector2 offset = Random.insideUnitCircle * settings.StrikeAreaRadius;
            Vector3 position = getStrikeAreaCenter() + new Vector3(offset.x, 0f, offset.y);
            bool spawnsLightningFiend = Random.value < settings.LightningFiendSpawnChance;

            return new LightningStrikeInfo(
                position,
                settings.Damage,
                settings.HitRadius,
                spawnsLightningFiend,
                settings.MarkedDamageBonus);
        }
    }
}
