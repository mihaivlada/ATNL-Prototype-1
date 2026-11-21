using System;
using ATNL.Enums;
using ATNL.Interfaces;
using ATNL.ScriptableObjects;
using ATNL.Structs;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private PlayerView view;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ArmorData armor; // optional

    /// <summary>Raised once when the player dies. Anything that cares (UI, audio, enemies) subscribes to this.</summary>
    public event Action Died;

    private PlayerRepository _playerRepository;
    private PlayerModel model;
    private Vector2 input;

    void Awake()
    {
        _playerRepository = new PlayerRepository();
    }

    void Start()
    {
        model = new PlayerModel("Harkyl", 100);
        model.HealthChanged += OnHealthChanged;
        model.Died += OnDied;
        model.AddPoultice(startingPoultices);

        // Show initial state
        OnHealthChanged(model.Health, model.MaxHealth);
    }

    void OnDestroy()
    {
        if (model != null)
        {
            model.HealthChanged -= OnHealthChanged;
            model.Died -= OnDied;
        }
    }

    void Update()
    {
        if (model.IsDead) return;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        HandleDebugHealthKeys();

        if (Input.GetKeyDown(KeyCode.Z))
            UsePoultice();
    }

    private const int PoulticeHealPerTick = 1;
    private const float PoulticeTickSeconds = 3f;
    private const float PoulticeDurationSeconds = 60f;

    [SerializeField] private int startingPoultices = 3;

    public void AddPoultice(int amount = 1) => model.AddPoultice(amount);

    private void UsePoultice()
    {
        if (poulticeActive) return;
        if (!model.TryConsumePoultice()) return;
        StartCoroutine(PoulticeRoutine());
    }

    private bool poulticeActive;

    private System.Collections.IEnumerator PoulticeRoutine()
    {
        poulticeActive = true;
        var wait = new WaitForSeconds(PoulticeTickSeconds);
        int ticks = Mathf.RoundToInt(PoulticeDurationSeconds / PoulticeTickSeconds);
        for (int i = 0; i < ticks; i++)
        {
            yield return wait;
            if (model.IsDead) 
            {
                poulticeActive = false;
                break;
            }
            Heal(PoulticeHealPerTick);
        }
        poulticeActive = false;
    }

    private void HandleDebugHealthKeys()
    {
        if (Input.GetKeyDown(KeyCode.X))
            TakeDamage(new DamageInfo(10, DamageType.Physical, gameObject));

        if (Input.GetKeyDown(KeyCode.Y))
            Heal(10);
    }

    private void FixedUpdate()
    {
        // Aici punem codul care se executa la un interval fix de timp, indiferent de framerate (de exemplu, calculele fizice)
        // Default FixedUpdate interval is 0.02 seconds (50 times per second)

        if (model.IsDead) return;

        Vector3 movement = new Vector3(input.x, 0, input.y).normalized * Helper.playerSpeed * Time.fixedDeltaTime;

        view.MovePlayer(movement);
    }

    public void TakeDamage(DamageInfo info)
    {
        if (model.IsDead) return;

        model.TakeDamage(info, armor);
    }

    public void Heal(int amount)
    {
        if (model.IsDead) return;

        model.Heal(amount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            Debug.Log("Player collided with a tree and took damage!");
            TakeDamage(new DamageInfo(5, DamageType.Physical, collision.gameObject));
        }
    }

    private void OnDied()
    {
        Died?.Invoke();
    }

    private void OnHealthChanged(int current, int max)
    {
        view.UpdateHealth(current);
        if (healthBar != null)
            healthBar.SetHealth(current, max);

        _playerRepository.Save(model);
    }
}
