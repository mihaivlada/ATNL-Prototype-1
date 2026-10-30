using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI temperatureText;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayer(Vector3 movement)
    {
        rb.MovePosition(rb.position + movement);
    }

    public void UpdateHealth(int health)
    {
        healthText.text = $"HP: {health}";
    }

    public void UpdateTemperature(float temperature)
    {
        temperatureText.text = $"Temp: {temperature}";
    }
}
