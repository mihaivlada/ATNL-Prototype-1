using Assets.Scripts.WeatherAndEnvironment.Weather.WeatherConfig.StatesSettings;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.Models
{
    public class CurrentWeatherModel
    {
        public List<CurrentWeatherStateModel> ActiveWeathers { get; set; } = new List<CurrentWeatherStateModel>();
        public float Duration { get; set; }
        public ActiveWindModel WindInfo { get; set; }
        public float TemperatureDropPerSecond { get; set; }
        public float WithWindMovementSpeedModifier { get; set; }
        public float AgainstWindMovementSpeedModifier { get; set; }
        public float MovementSpeedModifier { get; set; }
        public float NourishmentModifier { get; set; }
        public float FatigueModifier { get; set; }

        public CurrentWeatherModel(List<CurrentWeatherStateModel> activeWeathers, float duration, ActiveWindModel windInfo)
        {
            ActiveWeathers = activeWeathers;
            Duration = duration;
            WindInfo = windInfo;
            TemperatureDropPerSecond = ActiveWeathers.Sum(aw => aw.Tier?.TemperatureDropPerSecond ?? 0);

            var snowTiers = ActiveWeathers.Select(aw => aw.Tier as SnowTierSettings);
            var windTiers = ActiveWeathers.Select(aw => aw.Tier as WindTierSettings);

            WithWindMovementSpeedModifier = windTiers.Sum(wt => wt?.WithWindMovementSpeedModifier ?? 0);
            AgainstWindMovementSpeedModifier = windTiers.Sum(wt => wt?.AgainstWindMovementSpeedModifier ?? 0);
            MovementSpeedModifier = snowTiers.Sum(st => st?.MovementSpeedModifier ?? 0);
            NourishmentModifier = snowTiers.Sum(st => st?.NourishmentModifier ?? 0);
            FatigueModifier = snowTiers.Sum(st => st?.FatigueModifier ?? 0);
        }
    }
}
