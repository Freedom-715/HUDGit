using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ControlModeController : MonoBehaviour
{
    [Header("Mode Text")]
    public GameObject autoPilotText;
    public GameObject manualText;

    [Header("Colors")]
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.gray;

    private bool autoPilot = true;

    void Start()
    {
        UpdateMode();
    }

    void Update()
    {
        // Переключение режима по клавише TAB
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            autoPilot = !autoPilot;
            UpdateMode();
        }
    }

    void UpdateMode()
    {
        if (autoPilotText != null)
        {
            TMP_Text text = autoPilotText.GetComponent<TMP_Text>();

            if (text != null)
            {
                text.color = autoPilot ? activeColor : inactiveColor;
            }
        }

        if (manualText != null)
        {
            TMP_Text text = manualText.GetComponent<TMP_Text>();

            if (text != null)
            {
                text.color = autoPilot ? inactiveColor : activeColor;
            }
        }

        Debug.Log(autoPilot ? "MODE: AUTO PILOT" : "MODE: MANUAL CONTROL");
    }
}