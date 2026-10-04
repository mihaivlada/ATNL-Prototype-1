using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.States.Base;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.States.CombinedWeatherState
{
    public sealed class CombinedWeatherState : IWeatherState
    {
        private readonly WeatherState[] states;

        public CombinedWeatherState(IEnumerable<WeatherState> states, float duration)
        {
            this.states = states.ToArray();
            Duration = duration;
            RemainingTime = duration;
            Effects = this.states.Aggregate(WeatherEffects.None, (total, state) => total + state.Effects);
            Wind = this.states.Select(state => state.Wind).FirstOrDefault(wind => wind.HasWind);
            SpawnedEnemies = this.states.SelectMany(state => state.SpawnedEnemies).Distinct().ToArray();
        }

        public float Duration { get; }
        public float RemainingTime { get; private set; }
        public bool IsFinished => RemainingTime <= 0f;
        public WeatherEffects Effects { get; }
        public WindInfo Wind { get; }
        public IEnumerable<WeatherEnemyType> SpawnedEnemies { get; }

        public void Enter()
        {
            foreach (WeatherState state in states)
            {
                state.Enter();
            }
        }

        public void Tick(float deltaTime)
        {
            RemainingTime -= deltaTime;

            foreach (WeatherState state in states)
            {
                state.Tick(deltaTime);
            }
        }

        public void Exit()
        {
            foreach (WeatherState state in states)
            {
                state.Exit();
            }
        }

        public WeatherSnapshot CreateSnapshot()
        {
            ActiveWeather[] weathers = states.Select(state => new ActiveWeather(state.Type, state.Tier)).ToArray();
            return new WeatherSnapshot(weathers, Duration, Effects, Wind);
        }
    }
}
