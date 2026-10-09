using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionMapModeController : MonoBehaviour
{
    [Header("Buttons")]
    public Button pointsButton;
    public Button trajectoryButton;

    [Header("Texts")]
    public TMP_Text pointsText;
    public TMP_Text trajectoryText;

    [Header("Mission Point Selection")]
    public MissionPointSelector pointSelector;

    [Header("Trajectory")]
    public LineRenderer trajectoryLine;

    [Header("Colors")]
    public Color activeColor = Color.cyan;
    public Color inactiveColor = Color.gray;

    // Миссия подтверждена — возврат к планированию запрещён
    private bool missionLocked = false;

    void Start()
    {
        if (pointsButton != null)
            pointsButton.onClick.AddListener(ShowPlanning);

        if (trajectoryButton != null)
            trajectoryButton.onClick.AddListener(ShowTrajectory);

        ShowPlanning();
    }

    public void ShowPlanning()
    {
        // Если миссия уже подтверждена,
        // вернуться в планирование нельзя
        if (missionLocked)
        {
            Debug.Log("Планирование заблокировано после подтверждения миссии.");
            return;
        }

        if (pointSelector != null)
            pointSelector.enabled = true;

        if (trajectoryLine != null)
            trajectoryLine.enabled = false;

        UpdateColors(true);
    }

    public void ShowTrajectory()
    {
        if (pointSelector != null)
            pointSelector.enabled = false;

        if (pointSelector != null && !pointSelector.IsMissionReady)
        {
            if (trajectoryLine != null)
                trajectoryLine.enabled = false;

            UpdateColors(false);

            Debug.Log("Сначала выберите маршрут A → C → B");
            return;
        }

        if (trajectoryLine != null)
            trajectoryLine.enabled = true;

        UpdateColors(false);
    }

    // Вызывается после Mission Confirm
    public void LockPlanning()
    {
        missionLocked = true;

        // Полностью отключаем кнопку "Планирование"
        if (pointsButton != null)
        {
            pointsButton.interactable = false;
        }

        // Оставляем "Траектория" активной
        if (trajectoryButton != null)
        {
            trajectoryButton.interactable = true;
        }

        // Визуально показываем, что планирование недоступно
        if (pointsText != null)
        {
            pointsText.color = inactiveColor;
        }

        if (trajectoryText != null)
        {
            trajectoryText.color = activeColor;
        }

        Debug.Log("ПЛАНИРОВАНИЕ ЗАБЛОКИРОВАНО");
    }

    void UpdateColors(bool planning)
    {
        if (pointsText != null)
            pointsText.color = planning ? activeColor : inactiveColor;

        if (trajectoryText != null)
            trajectoryText.color = planning ? inactiveColor : activeColor;
    }
    public void HideMapView()
    {
        if (pointSelector != null)
            pointSelector.enabled = false;

        if (trajectoryLine != null)
            trajectoryLine.enabled = false;
    }
}