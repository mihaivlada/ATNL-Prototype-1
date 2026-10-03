using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;
using System.Collections.Generic;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States
{
    public interface IWeatherState
    {
        WeatherEffects Effects { get; }
        WindInfo Wind { get; }
        IEnumerable<WeatherEnemyType> SpawnedEnemies { get; }

        void Enter();
        void Tick(float deltaTime);
        void Exit();
    }
}
