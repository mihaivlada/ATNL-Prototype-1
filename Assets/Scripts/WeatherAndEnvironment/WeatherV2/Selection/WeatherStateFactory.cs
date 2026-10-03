using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Config;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.Enums;
using Assets.Scripts.WeatherAndEnvironment.WeatherV2.States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Selection
{
    public sealed class WeatherStateFactory
    {
        private readonly WeatherProfileSO profile;
        private readonly Func<Vector3> getLightningAreaCenter;

        public WeatherStateFactory(WeatherProfileSO profile, Func<Vector3> getLightningAreaCenter)
        {
            this.profile = profile;
            this.getLightningAreaCenter = getLightningAreaCenter;
        }

        public WeatherState Create(WeatherType type)
        {
            return type switch
            {
                WeatherType.Clear => new ClearWeatherState(profile.Clear),
                WeatherType.Cold => CreateCold(),
                WeatherType.Snow => CreateSnow(),
                WeatherType.Wind => CreateWind(),
                WeatherType.Fog => CreateFog(),
                WeatherType.Blizzard => new BlizzardWeatherState(profile.Blizzard),
                WeatherType.LightningStorm => new LightningStormWeatherState(profile.LightningStorm, getLightningAreaCenter),
                WeatherType.GreatFrost => new GreatFrostWeatherState(profile.GreatFrost),
                WeatherType.VeilOfDarkness => new VeilOfDarknessWeatherState(CreateFog()),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private ColdWeatherState CreateCold()
        {
            int index = PickTierIndex(profile.Cold.Tiers);
            return new ColdWeatherState(index + 1, profile.Cold.Tiers[index]);
        }

        private SnowWeatherState CreateSnow()
        {
            int index = PickTierIndex(profile.Snow.Tiers);
            SnowTierSettings tier = profile.Snow.Tiers[index];
            return new SnowWeatherState(index + 1, tier, profile.Cold.GetTemperatureDropPerSecond(tier.ColdTier));
        }

        private WindWeatherState CreateWind()
        {
            int index = PickTierIndex(profile.Wind.Tiers);
            WindTierSettings tier = profile.Wind.Tiers[index];
            return new WindWeatherState(index + 1, tier, profile.Wind, profile.Cold.GetTemperatureDropPerSecond(tier.ColdTier));
        }

        private FogWeatherState CreateFog()
        {
            int index = PickTierIndex(profile.Fog.Tiers);
            return new FogWeatherState(index + 1, profile.Fog.Tiers[index]);
        }

        private static int PickTierIndex(IReadOnlyList<WeatherTierSettings> tiers)
        {
            return WeightedRandom.PickIndex(tiers.Count, index => tiers[index].Weight);
        }
    }
}
