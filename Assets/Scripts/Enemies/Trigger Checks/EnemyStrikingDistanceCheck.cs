using UnityEngine;

public class EnemyStrikingDistanceCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    public Enemy _enemy;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            _enemy.SetStrkingDistanceBool(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            _enemy.SetStrkingDistanceBool(false);
        }
    }
}
