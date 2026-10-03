using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public abstract class WeatherState : IWeatherState
    {
        public const int NoTier = 0;

        protected static readonly WeatherEnemyType[] NoEnemies = Array.Empty<WeatherEnemyType>();

        protected WeatherState(WeatherType type, int tier, WeatherEffects effects)
        {
            Type = type;
            Tier = tier;
            Effects = effects;
        }

        public WeatherType Type { get; }
        public int Tier { get; }
        public WeatherEffects Effects { get; }
        public virtual WindInfo Wind => WindInfo.None;
        public virtual IEnumerable<WeatherEnemyType> SpawnedEnemies => NoEnemies;

        public virtual void Enter()
        {
        }

        public virtual void Tick(float deltaTime)
        {
        }

        public virtual void Exit()
        {
        }
    }
}
