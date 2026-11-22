

using UnityEngine;

namespace Assets.Scripts.WeatherAndEnvironment.Weather.Models
{
    public class ActiveWindModel
    {
        public bool HasWind { get; set; }
        public Vector3 Direction { get; set; }
        public bool ComesInGusts { get; set; }

        public ActiveWindModel(bool hasWind, Vector3 direction, bool comesInGusts)
        {
            HasWind = hasWind;
            Direction = direction;
            ComesInGusts = comesInGusts;
        }
    }
}
