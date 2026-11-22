using Assets.Scripts.WeatherAndEnvironment.Weather.Events;
using Assets.Scripts.WeatherAndEnvironment.Weather.Models;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region blabla
    [SerializeField] private PlayerView view;
    private PlayerRepository _playerRepository;
    private PlayerModel model;
    private Vector2 input;
    private PlayerModel _playerData = new PlayerModel("Harkyl", 100, 100);

    void Awake()
    {
        _playerRepository = new PlayerRepository();
    }
    #endregion

    private void OnEnable()
    {
        WeatherEvents.OnWeatherChanged += HandleWeatherChanged;
        WeatherEvents.OnTemperatureDropped += HandleTemperatureDrop;
        WeatherEvents.OnMovementSpeedModifierChanged += HandleMovementSpeedModifierChanged;
        WeatherEvents.OnNourishmentDepletionModifierChanged += HandleNourishmentDepletionModifierChanged;
        WeatherEvents.OnFatigueDepletionModifierChanged += HandleFatigueDepletionModifierChanged;
        WeatherEvents.OnWindChanged += HandleWindChanged;
    }
    private void OnDisable()
    {
        WeatherEvents.OnWeatherChanged -= HandleWeatherChanged;
        WeatherEvents.OnTemperatureDropped -= HandleTemperatureDrop;
        WeatherEvents.OnMovementSpeedModifierChanged -= HandleMovementSpeedModifierChanged;
        WeatherEvents.OnNourishmentDepletionModifierChanged -= HandleNourishmentDepletionModifierChanged;
        WeatherEvents.OnFatigueDepletionModifierChanged -= HandleFatigueDepletionModifierChanged;
        WeatherEvents.OnWindChanged -= HandleWindChanged;
    }

    void Start()
    {
        //_playerData = _saveManager.Load().PlayerData;
        model = new PlayerModel(_playerData.Name, _playerData.Health, _playerData.Temperature);

        RefreshView();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Aici punem codul care se executa la un interval fix de timp, indiferent de framerate (de exemplu, calculele fizice)
        // Default FixedUpdate interval is 0.02 seconds (50 times per second)

        Vector3 movement = new Vector3(input.x, 0, input.y).normalized * Helper.playerSpeed * Time.fixedDeltaTime;

        view.MovePlayer(movement);
    }


    private void HandleWeatherChanged(CurrentWeatherModel currentWeather)
    {
        // Handle weather change logic here
        Debug.Log($"Weather changed to: {string.Join(", ", currentWeather.ActiveWeathers.Select(w => $"{w.State.Type} - {w.Tier.TierType}"))}");
    }

    private void HandleMovementSpeedModifierChanged(float modifier)
    {
        // Handle movement speed modifier change logic here
        Debug.Log($"Movement speed modifier changed to: {modifier}");
    }

    private void HandleNourishmentDepletionModifierChanged(float modifier)
    {
        // Handle nourishment depletion modifier change logic here
        Debug.Log($"Nourishment depletion modifier changed to: {modifier}");
    }

    private void HandleFatigueDepletionModifierChanged(float modifier)
    {
        // Handle fatigue depletion modifier change logic here
        Debug.Log($"Fatigue depletion modifier changed to: {modifier}");
    }

    private void HandleWindChanged(ActiveWindModel windInfo)
    {
        // Handle wind change logic here
        Debug.Log($"Wind changed. Direction: {windInfo.Direction}, Comes in gusts: {windInfo.ComesInGusts}");
    }

    public void DamagePlayer(int damage)
    {
        model.TakeDamage(damage);
        _playerData.Health = model.Health;

        RefreshView();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            Debug.Log("Player collided with a tree and took damage!");
            DamagePlayer(5);
        }
    }

    private void RefreshView()
    {
        view.UpdateHealth(model.Health);
        view.UpdateTemperature(model.Temperature);
        _playerRepository.Save(_playerData);
    }

    private void HandleTemperatureDrop(float temperature)
    {
        Debug.Log($"Temperature dropped by: {temperature}");
        model.DropTemperature(temperature);
        _playerData.Temperature = model.Temperature;

        RefreshView();
    }
}
