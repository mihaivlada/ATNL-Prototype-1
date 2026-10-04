using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.States.Base;
using System.Collections.Generic;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public sealed class FogWeatherState : WeatherState
    {
        private static readonly WeatherEnemyType[] FogDwellers = { WeatherEnemyType.FogDweller };

        private readonly bool spawnsFogDwellers;

        public FogWeatherState(int tier, FogTierSettings settings)
            : base(WeatherType.Fog, tier, new WeatherEffects(visibilityReduction: settings.VisibilityReduction))
        {
            spawnsFogDwellers = settings.SpawnsFogDwellers;
        }

        public override IEnumerable<WeatherEnemyType> SpawnedEnemies => spawnsFogDwellers ? FogDwellers : NoEnemies;
    }
}
