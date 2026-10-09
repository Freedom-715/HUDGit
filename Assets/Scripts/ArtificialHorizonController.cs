using UnityEngine;
using UnityEngine.InputSystem;

public class ArtificialHorizonController : MonoBehaviour
{
    [Header("Horizon")]
    public RectTransform horizonContent;

    public TMPro.TMP_Text rollValue;

    [Header("Settings")]
    public float pitch = 0f;
    public float pixelsPerDegree = 4f;
    public float roll = 0f;

    [Header("Keyboard Control")]
    public float pitchSpeed = 20f;
    public float rollSpeed = 60f;

    void Update()
    {
        // PITCH — W / S
        if (Keyboard.current.wKey.isPressed)
        {
            pitch += pitchSpeed * Time.deltaTime;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            pitch -= pitchSpeed * Time.deltaTime;
        }

        // ROLL — A / D
        // A — против часовой стрелки
        if (Keyboard.current.aKey.isPressed)
        {
            roll -= rollSpeed * Time.deltaTime;
        }

        // D — по часовой стрелке
        if (Keyboard.current.dKey.isPressed)
        {
            roll += rollSpeed * Time.deltaTime;
        }

        // LIMITS
        pitch = Mathf.Clamp(pitch, -30f, 30f);
        roll = Mathf.Clamp(roll, -45f, 45f);

        // MOVE HORIZON
        float y = -pitch * pixelsPerDegree;
        horizonContent.anchoredPosition = new Vector2(0f, y);

        // ROTATE HORIZON
        horizonContent.localRotation = Quaternion.Euler(0f, 0f, roll);

        // ROLL VALUE
        if (rollValue != null)
        {
            rollValue.text = Mathf.RoundToInt(roll) + "°";
        }
    }
}