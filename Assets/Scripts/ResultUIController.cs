using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUIController : MonoBehaviour
{
    [Header("UI")]
    public GameObject resultUI;
    public TMP_Text resultTitle;

    [Header("Buttons")]
    public Button restartButton;
    public Button exitButton;

    void Start()
    {
        if (resultUI != null)
            resultUI.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartMission);

        if (exitButton != null)
            exitButton.onClick.AddListener(CloseResult);
    }

    public void ShowVictory()
    {
        if (resultUI != null)
            resultUI.SetActive(true);

        if (resultTitle != null)
            resultTitle.text = "MISSION COMPLETE";

        Time.timeScale = 0f;
    }

    public void ShowCrash()
    {
        if (resultUI != null)
            resultUI.SetActive(true);

        if (resultTitle != null)
            resultTitle.text = "MISSION FAILED";

        Time.timeScale = 0f;
    }

    void RestartMission()
    {
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    void CloseResult()
    {
        Time.timeScale = 1f;

        if (resultUI != null)
            resultUI.SetActive(false);
    }
}