using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Gauge Arcs")]
    public HUDArcGauge speedArc;
    public HUDArcGauge altitudeArc;
    public HUDArcGauge verticalSpeedArc;
    public HUDArcGauge gForceArc;

    [Header("Speed")]
    public TMP_Text speedValue;

    [Header("Altitude")]
    public TMP_Text altitudeValue;

    [Header("Vertical Speed")]
    public TMP_Text verticalSpeedValue;

    [Header("Fuel")]
    public TMP_Text fuelValue;
    public UnityEngine.UI.Slider fuelBar;

    [Header("Fuel Warning")]
    public GameObject fuelWarning;

    [Header("G-Force")]
    public TMP_Text gForceValue;

    [Header("G-Force Warning")]
    public Image gForceWarning;

    // Эти методы будут использоваться,
    // когда ИТ-1 передаст нам телеметрию.

    public void SetSpeed(float speed)
    {
        if (speedValue != null)
            speedValue.text = Mathf.RoundToInt(speed).ToString();

        if (speedArc != null)
            speedArc.SetValue(speed);
    }

    public void SetAltitude(float altitude)
    {
        if (altitudeValue != null)
            altitudeValue.text = Mathf.RoundToInt(altitude).ToString("N0");

        if (altitudeArc != null)
            altitudeArc.SetValue(altitude);
    }

    public void SetVerticalSpeed(float verticalSpeed)
    {
        if (verticalSpeedValue != null)
        {
            verticalSpeedValue.text = verticalSpeed >= 0
                ? "+" + verticalSpeed.ToString("0")
                : verticalSpeed.ToString("0");
        }

        if (verticalSpeedArc != null)
            verticalSpeedArc.SetValue(Mathf.Abs(verticalSpeed));
    }

    public void SetFuel(float fuel)
    {
        fuel = Mathf.Clamp(fuel, 0f, 100f);

        if (fuelValue != null)
            fuelValue.text = Mathf.RoundToInt(fuel).ToString();

        if (fuelBar != null)
            fuelBar.value = fuel;

        if (fuelWarning != null)
            fuelWarning.SetActive(fuel <= 20f);
    }

    public void SetGForce(float gForce)
    {
        gForce = Mathf.Clamp(gForce, 1f, 6f);

        if (gForceValue != null)
        {
            gForceValue.text = Mathf.RoundToInt(gForce).ToString();
        }

        if (gForceArc != null)
        {
            gForceArc.SetValue(gForce);
        }

        if (gForceWarning != null)
        {
            gForceWarning.gameObject.SetActive(gForce >= 5f);
        }
    }
}