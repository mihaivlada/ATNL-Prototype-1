using System;
using ATNL.ScriptableObjects;
using ATNL.Structs;
using UnityEngine;

public class PlayerModel
{
    public string Name { get; set; }
    public int MaxHealth { get; }
    public int Health { get; private set; }
    public bool IsDead => Health <= 0;

    public Stat Stamina { get; } = new Stat(100f);
    public Stat Fatigue { get; } = new Stat(100f);
    public Stat Hunger { get; } = new Stat(100f);

    public int PoulticeCount { get; private set; }

    /// <summary>Raised whenever the poultice count changes.</summary>
    public event Action<int> PoulticeCountChanged;

    public void AddPoultice(int amount = 1)
    {
        if (amount <= 0) return;
        PoulticeCount += amount;
        PoulticeCountChanged?.Invoke(PoulticeCount);
    }

    /// <summary>Consumes one poultice. Returns false if none are available or the player is dead.</summary>
    public bool TryConsumePoultice()
    {
        if (IsDead || PoulticeCount <= 0) return false;
        PoulticeCount--;
        PoulticeCountChanged?.Invoke(PoulticeCount);
        return true;
    }

    /// <summary>Raised whenever health changes: (current, max).</summary>
    public event Action<int, int> HealthChanged;

    /// <summary>Raised once, when health reaches 0.</summary>
    public event Action Died;

    public PlayerModel(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    public void TakeDamage(DamageInfo info, ArmorData armor = null)
    {
        if (IsDead) return;

        int final = armor != null ? armor.Mitigate(info) : info.Amount;
        SetHealth(Health - final);
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        SetHealth(Health + amount);
    }

    private void SetHealth(int value)
    {
        Health = Mathf.Clamp(value, 0, MaxHealth);
        HealthChanged?.Invoke(Health, MaxHealth);

        // Callers return early when already dead, so this only fires on the alive -> dead transition.
        if (Health == 0)
            Died?.Invoke();
    }
}
