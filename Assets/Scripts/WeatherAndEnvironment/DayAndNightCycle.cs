using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField] private float dayDurationInSeconds;
    [SerializeField] private float nightDurationInSeconds;
    [SerializeField] private float lightIntensityTransitionTimeInSeconds;
    // between 0 and 1
    [SerializeField] private float dayProgress;

    private Light light;

    private bool isDay;

    private const int startingAngleOffsetX = 75;
    private const int startingAngleOffsetY = 75;

    private const float startingAngleX = Mathf.PI / 8;
    private const float rotationAmplitudeX = 3 * Mathf.PI / 4;

    private const float startingAngleY = 3 * Mathf.PI / 2;
    private const float rotationAmplitudeY = Mathf.PI;

    void Start()
    {
        dayDurationInSeconds = 8f * 60;
        nightDurationInSeconds = 7f * 60;
        lightIntensityTransitionTimeInSeconds = 10f / dayDurationInSeconds;

        // start of the day
        dayProgress = 0;
        isDay = true;

        // sped up for testing
        // remove in the future
        //dayDurationInSeconds /= 40;
        //nightDurationInSeconds /= 80;

        light = GetComponent<Light>();
    }

    void LateUpdate()
    {
        if (isDay) 
        {
            dayProgress += Time.deltaTime / dayDurationInSeconds;

            light.intensity = Mathf.Clamp01(
                Mathf.Min(
                    dayProgress / lightIntensityTransitionTimeInSeconds,
                    (1f - dayProgress) / lightIntensityTransitionTimeInSeconds
                )
            );
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
            startingAngleOffsetX * Mathf.Sin(startingAngleX + (rotationAmplitudeX * dayProgress)), 
            startingAngleOffsetY * Mathf.Sin(startingAngleY + (rotationAmplitudeY * dayProgress)), 
            0);
    }

    IEnumerator WaitForNightTime()
    {
        yield return new WaitForSeconds(nightDurationInSeconds);
        TurnLightOn();
    }

    void TurnLightOn()
    {
        dayProgress = 0;
        isDay = true;
        light.enabled = true;
    }
}
