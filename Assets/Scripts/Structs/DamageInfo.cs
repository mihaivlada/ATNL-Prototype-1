using ATNL.Enums;
using UnityEngine;

namespace ATNL.Structs
{
    public readonly struct DamageInfo
    {
        public readonly int Amount;
        public readonly DamageType Type;
        public readonly GameObject Source;

        public DamageInfo(int amount, DamageType type, GameObject source = null)
        {
            Amount = amount;
            Type = type;
            Source = source;
        }
    }
}
