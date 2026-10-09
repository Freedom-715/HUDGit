using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [Header("Pause UI")]
    public GameObject pauseUI;
    public Button resumeButton;
    public Button missionButton;

    [Header("Mission UI")]
    public GameObject missionUI;

    private bool isPaused = false;

    void Start()
    {
        isPaused = false;

        if (pauseUI != null)
            pauseUI.SetActive(false);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (missionButton != null)
            missionButton.onClick.AddListener(OpenMission);
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;

        if (pauseUI != null)
            pauseUI.SetActive(true);

        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        isPaused = false;

        if (pauseUI != null)
            pauseUI.SetActive(false);

        Time.timeScale = 1f;
    }

    void OpenMission()
    {
        isPaused = false;

        if (pauseUI != null)
            pauseUI.SetActive(false);

        Time.timeScale = 1f;

        if (missionUI != null)
            missionUI.SetActive(true);
    }
}