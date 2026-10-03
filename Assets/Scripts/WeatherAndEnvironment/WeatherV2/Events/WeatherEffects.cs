namespace Assets.Scripts.WeatherAndEnvironment.WeatherV2.Events
{
    public readonly struct WeatherEffects
    {
        public static readonly WeatherEffects None = default;

        public WeatherEffects(
            float temperatureDropPerSecond = 0f,
            float movementSpeedModifier = 0f,
            float attackSpeedModifier = 0f,
            float nourishmentDepletionModifier = 0f,
            float fatigueDepletionModifier = 0f,
            float visibilityReduction = 0f)
        {
            TemperatureDropPerSecond = temperatureDropPerSecond;
            MovementSpeedModifier = movementSpeedModifier;
            AttackSpeedModifier = attackSpeedModifier;
            NourishmentDepletionModifier = nourishmentDepletionModifier;
            FatigueDepletionModifier = fatigueDepletionModifier;
            VisibilityReduction = visibilityReduction;
        }

        public float TemperatureDropPerSecond { get; }
        public float MovementSpeedModifier { get; }
        public float AttackSpeedModifier { get; }
        public float NourishmentDepletionModifier { get; }
        public float FatigueDepletionModifier { get; }
        public float VisibilityReduction { get; }

        public static WeatherEffects operator +(WeatherEffects left, WeatherEffects right)
        {
            return new WeatherEffects(
                left.TemperatureDropPerSecond + right.TemperatureDropPerSecond,
                left.MovementSpeedModifier + right.MovementSpeedModifier,
                left.AttackSpeedModifier + right.AttackSpeedModifier,
                left.NourishmentDepletionModifier + right.NourishmentDepletionModifier,
                left.FatigueDepletionModifier + right.FatigueDepletionModifier,
                left.VisibilityReduction + right.VisibilityReduction);
        }
    }
}
