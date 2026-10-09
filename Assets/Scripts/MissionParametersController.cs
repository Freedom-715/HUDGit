using UnityEngine;
using TMPro;

public class MissionParametersController : MonoBehaviour
{
    [Header("Mission Parameters")]
    public TMP_Text targetValue;
    public TMP_Text apogeeValue;
    public TMP_Text flightTimeValue;
    public TMP_Text rangeValue;

    [Header("Mission Points")]
    public Transform pointA;
    public Transform pointC;
    public Transform pointB;

    void Start()
    {
        ResetParameters();
    }

    public void ShowMissionParameters()
    {
        // Route
        if (targetValue != null)
        {
            targetValue.text = "A → C → B";
        }

        // Temporary values.
        // Later they can be replaced with real rocket telemetry.
        if (apogeeValue != null)
        {
            apogeeValue.text = "—";
        }

        if (flightTimeValue != null)
        {
            flightTimeValue.text = "—";
        }

        if (rangeValue != null)
        {
            float distance = CalculateDistance();

            rangeValue.text = distance.ToString("0") + " m";
        }
    }

    void ResetParameters()
    {
        if (targetValue != null)
            targetValue.text = "—";

        if (apogeeValue != null)
            apogeeValue.text = "—";

        if (flightTimeValue != null)
            flightTimeValue.text = "—";

        if (rangeValue != null)
            rangeValue.text = "—";
    }

    float CalculateDistance()
    {
        if (pointA == null || pointC == null || pointB == null)
            return 0f;

        float distanceAC = Vector3.Distance(
            pointA.position,
            pointC.position
        );

        float distanceCB = Vector3.Distance(
            pointC.position,
            pointB.position
        );

        return distanceAC + distanceCB;
    }
}