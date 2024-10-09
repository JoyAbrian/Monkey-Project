using UnityEngine;

[RequireComponent(typeof(Light))]
public class AnomalyLightController : MonoBehaviour
{
    private Light lightComponent;
    private float anomalyIntensity = 0.5f;  // Intensitas saat anomali terjadi

    private void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    // Mengatur intensitas saat anomali terjadi
    public void TriggerAnomaly()
    {
        lightComponent.intensity = anomalyIntensity;
    }

    // Mengembalikan intensitas ke keadaan awal
    public void ResetToInitialIntensity(float initialIntensity)
    {
        lightComponent.intensity = initialIntensity;
    }

    // Mengambil intensitas awal
    public float GetIntensity()
    {
        return lightComponent.intensity;
    }
}
