using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField] private float dayDuration = 7f;   // 7 minutes
    [SerializeField] private float nightDuration = 6f; // 6 minutes

    private float timeOfDay; // 0 to 1, where 0.5 is midnight, 0 is sunrise, 0.25 is noon, 0.75 is sunset

    void Start()
    {
        timeOfDay = 0.25f;
        dayDuration = dayDuration * 60f;
        nightDuration = nightDuration * 60f;
    }

    // Update is called once per frame
    void Update()
    {
        RotateFigureEight();
    }

    void RotateFigureEight()
    {
        bool isDay = timeOfDay >= 0f && timeOfDay < 0.5f;

        float duration = isDay ? dayDuration : nightDuration;

        timeOfDay += Time.deltaTime / duration;

        if (timeOfDay >= 1f)

            timeOfDay -= 1f;

        float t = timeOfDay * Mathf.PI;

        float pitch = Mathf.Sin(t) * 45f;
        float yaw = Mathf.Sin(t * 2f) * 60f;

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
