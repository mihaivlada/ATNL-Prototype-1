using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fill;          // Image Type = Filled, Fill Method = Horizontal
    [SerializeField] private float smoothSpeed = 5f;

    private float target = 1f;

    public void SetHealth(int current, int max)
    {
        target = max > 0 ? (float)current / max : 0f;
    }

    private void Update()
    {
        fill.fillAmount = Mathf.MoveTowards(fill.fillAmount, target, smoothSpeed * Time.deltaTime);
    }
}
