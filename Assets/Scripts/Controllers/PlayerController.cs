using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerView view;
    private PlayerRepository _playerRepository;
    private PlayerModel model;
    private Vector2 input;
    private PlayerModel _playerData = new PlayerModel("Harkyl", 100);

    void Awake()
    {
        _playerRepository = new PlayerRepository();
    }

    void Start()
    {
        //_playerData = _saveManager.Load().PlayerData;
        model = new PlayerModel(_playerData.Name, _playerData.Health);

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
        _playerRepository.Save(_playerData);
    }
}
