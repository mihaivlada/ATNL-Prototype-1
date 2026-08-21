using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField] private float dayDuration;
    [SerializeField] private float nightDuration;

    private Light light;

    // between 0 and 1
    [SerializeField] private float dayProgress;

    private bool isDay;

    private const int sunAngleYAxis = 75;
    private const int sunAngleXAxis = 75;

    void Start()
    {
        dayDuration = 8f * 60;
        nightDuration = 7f * 60;

        // start of the day
        dayProgress = 0;
        isDay = true;

        // sped up for testing
        // remove in the future
        //dayDuration /= 80;
        //nightDuration /= 80;

        light = GetComponent<Light>();
    }

    void LateUpdate()
    {
        if (isDay) 
        {
            dayProgress += Time.deltaTime / dayDuration;
        }

        // end of the day, resets back to start
        if (dayProgress >= 1)
        {
            dayProgress = 0;
            isDay = false;
            light.enabled = false;
            StartCoroutine(WaitForNightTime());
        }

        transform.rotation = Quaternion.Euler(
            sunAngleXAxis * Mathf.Sin(Mathf.PI * dayProgress), 
            sunAngleYAxis * Mathf.Sin((3 * Mathf.PI / 2) + (Mathf.PI * dayProgress)), 
            0);
    }

    IEnumerator WaitForNightTime()
    {
        yield return new WaitForSeconds(nightDuration);
        TurnLightOn();
    }

    void TurnLightOn()
    {
        dayProgress = 0;
        isDay = true;
        light.enabled = true;
    }
}
