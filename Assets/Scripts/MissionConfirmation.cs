using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionConfirmation : MonoBehaviour
{
    [Header("UI")]
    public Button confirmButton;
    public TMP_Text missionStatus;

    [Header("Mission")]
    public MissionPointSelector pointSelector;

    [Header("Map Mode")]
    public MissionMapModeController mapModeController;

    [Header("Planning UI")]
    public GameObject missionUI;

    [Header("Map Movement")]
    public MissionMapMover mapMover;

    void Start()
    {
        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(ConfirmMission);
        }
    }

    void ConfirmMission()
    {
        // Проверяем MissionPointSelector
        if (pointSelector == null)
        {
            Debug.LogWarning("MissionPointSelector не подключён.");
            return;
        }

        // Проверяем маршрут
        if (!pointSelector.IsMissionReady)
        {
            if (missionStatus != null)
            {
                missionStatus.text = "SELECT A → C → B FIRST";
            }

            Debug.Log("Сначала выберите маршрут A → C → B.");
            return;
        }

        // Подтверждаем миссию
        pointSelector.ConfirmMission();

        // Меняем текст
        if (missionStatus != null)
        {
            missionStatus.text = "MISSION CONFIRMED";
        }

        // Отключаем кнопку
        if (confirmButton != null)
        {
            confirmButton.interactable = false;
        }

        // Переключаем карту на траекторию
        if (mapModeController != null)
        {
            mapModeController.ShowTrajectory();
        }

        if (mapMover != null)
        {
            mapMover.MoveToTrajectory();
        }

        // Скрываем кнопку Mission Complete
        if (confirmButton != null)
        {
            confirmButton.gameObject.SetActive(false);
        }

        // Скрываем MissionUI
        if (missionUI != null)
        {
            missionUI.SetActive(false);
        }

        Debug.Log("MISSION CONFIRMED → MISSION UI CLOSED → TRAJECTORY");
    }
}