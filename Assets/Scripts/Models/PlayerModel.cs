using UnityEngine;

public class PlayerModel
{
    public string Name { get; set; }

    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = Mathf.Clamp(value, 0, 100); }
    }

    public PlayerModel(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    public void Heal(int life)
    {
        Health += life;
        if (Health > 100)
        {
            Health = 100;
        }
    }
}
