using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public sealed class VeilOfDarknessWeatherState : WeatherState
    {
        private readonly FogWeatherState fog;
        private readonly WeatherEnemyType[] spawnedEnemies;

        public VeilOfDarknessWeatherState(FogWeatherState fog)
            : base(WeatherType.VeilOfDarkness, NoTier, fog.Effects)
        {
            this.fog = fog;
            spawnedEnemies = fog.SpawnedEnemies.Append(WeatherEnemyType.IesserFeihdhar).ToArray();
        }

        public int FogTier => fog.Tier;
        public override IEnumerable<WeatherEnemyType> SpawnedEnemies => spawnedEnemies;

        public override void Enter() => fog.Enter();
        public override void Tick(float deltaTime) => fog.Tick(deltaTime);
        public override void Exit() => fog.Exit();
    }
}
