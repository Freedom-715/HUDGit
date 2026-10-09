using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionUIController : MonoBehaviour
{
    [Header("Mission Points")]
    public Button pointA;
    public Button pointB;
    public Button pointC;

    [Header("Buttons")]
    public Button confirmMissionButton;
    public Button closeButton;

    [Header("Status")]
    public TMP_Text missionStatus;

    // Порядок выбранных точек
    private List<string> route = new List<string>();

    void Start()
    {
        if (pointA != null)
            pointA.onClick.AddListener(() => SelectPoint("A"));

        if (pointB != null)
            pointB.onClick.AddListener(() => SelectPoint("B"));

        if (pointC != null)
            pointC.onClick.AddListener(() => SelectPoint("C"));

        if (confirmMissionButton != null)
            confirmMissionButton.onClick.AddListener(ConfirmMission);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseMission);

        UpdateStatus();
    }

    void SelectPoint(string point)
    {
        // Не позволяем выбрать одну точку дважды
        if (route.Contains(point))
        {
            Debug.Log(point + " уже выбрана.");
            return;
        }

        // Максимум три точки
        if (route.Count >= 3)
        {
            Debug.Log("Маршрут уже заполнен.");
            return;
        }

        route.Add(point);

        Debug.Log("Выбрана точка: " + point);

        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (missionStatus != null)
        {
            if (route.Count == 0)
            {
                missionStatus.text = "SELECT MISSION POINTS";
            }
            else
            {
                missionStatus.text = "ROUTE: " + string.Join(" → ", route);
            }
        }
    }

    void ConfirmMission()
    {
        // Проверяем, что выбран полный маршрут A → C → B
        if (route.Count != 3)
        {
            Debug.Log("Выберите три точки.");
            return;
        }

        if (route[0] == "A" &&
            route[1] == "C" &&
            route[2] == "B")
        {
            Debug.Log("MISSION CONFIRMED: A → C → B");
        }
        else
        {
            Debug.Log("Неверный маршрут. Требуется: A → C → B");
        }
    }

    void CloseMission()
    {
        gameObject.SetActive(false);
    }
}