using UnityEngine;
using UnityEngine.UI;

public class HUDArcGauge : MonoBehaviour
{
    [Header("Arc")]
    public Image arc;

    [Header("Range")]
    public float minValue = 0f;
    public float maxValue = 6f;

    [Header("Colors")]
    public Color lowColor = new Color(1f, 0.5f, 0.5f);
    public Color mediumColor = Color.red;
    public Color highColor = Color.red;

    public void SetValue(float value)
    {
        value = Mathf.Clamp(value, minValue, maxValue);

        float normalized = Mathf.InverseLerp(
            minValue,
            maxValue,
            value
        );

        if (arc != null)
        {
            arc.fillAmount = normalized;

            if (normalized < 0.5f)
            {
                arc.color = Color.Lerp(
                    lowColor,
                    mediumColor,
                    normalized * 2f
                );
            }
            else
            {
                arc.color = Color.Lerp(
                    mediumColor,
                    highColor,
                    (normalized - 0.5f) * 2f
                );
            }
        }
    }
}