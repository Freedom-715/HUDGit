using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionPointSelector : MonoBehaviour
{
    [Header("Mission Points")]
    public Button pointA;
    public Button pointC;
    public Button pointB;

    [Header("Mission UI")]
    public TMP_Text missionStatus;
    public Button confirmMissionButton;

    [Header("Mission Parameters")]
    public MissionParametersController missionParameters;

    [Header("UI Trajectory Line")]
    public MissionUILineController uiLineController;

    [Header("Point Colors")]
    public Color normalColor = Color.white;
    public Color selectedColor = Color.cyan;

    private int selectedPoints = 0;
    private bool missionConfirmed = false;

    public bool IsMissionReady => selectedPoints == 3;
    public bool IsMissionConfirmed => missionConfirmed;

    private void Start()
    {
        if (pointA != null)
            pointA.onClick.AddListener(SelectA);

        if (pointC != null)
            pointC.onClick.AddListener(SelectC);

        if (pointB != null)
            pointB.onClick.AddListener(SelectB);

        if (confirmMissionButton != null)
        {
            confirmMissionButton.onClick.AddListener(ConfirmMission);
            confirmMissionButton.interactable = false;
        }

        ResetPointColors();
        UpdateTrajectory();
        UpdateStatus();
    }

    private void SelectA()
    {
        if (missionConfirmed)
            return;

        if (selectedPoints != 0)
        {
            ShowStatus("Сначала выберите маршрут A → C → B");
            return;
        }

        selectedPoints = 1;

        SetPointColor(pointA, selectedColor);

        UpdateTrajectory();
        UpdateStatus();

        Debug.Log("Выбрана точка A");
    }

    private void SelectC()
    {
        if (missionConfirmed)
            return;

        if (selectedPoints != 1)
        {
            ShowStatus("Сначала выберите точку A");
            return;
        }

        selectedPoints = 2;

        SetPointColor(pointC, selectedColor);

        UpdateTrajectory();
        UpdateStatus();

        Debug.Log("Выбрана точка C");
    }

    private void SelectB()
    {
        if (missionConfirmed)
            return;

        if (selectedPoints != 2)
        {
            ShowStatus("Сначала выберите точку C");
            return;
        }

        selectedPoints = 3;

        SetPointColor(pointB, selectedColor);

        UpdateTrajectory();
        UpdateStatus();

        Debug.Log("Выбрана точка B");
    }

    private void UpdateTrajectory()
    {
        // Линия поверх UI-карты
        if (uiLineController != null)
            uiLineController.SetSelectedPoints(selectedPoints);
    }

    public void ConfirmMission()
    {
        if (missionConfirmed || !IsMissionReady)
            return;

        missionConfirmed = true;

        ShowStatus("МИССИЯ ПОДТВЕРЖДЕНА");

        if (confirmMissionButton != null)
            confirmMissionButton.interactable = false;

        if (pointA != null)
            pointA.interactable = false;

        if (pointC != null)
            pointC.interactable = false;

        if (pointB != null)
            pointB.interactable = false;

        if (missionParameters != null)
            missionParameters.ShowMissionParameters();

        Debug.Log("Миссия подтверждена");
    }

    private void UpdateStatus()
    {
        if (selectedPoints == 0)
        {
            ShowStatus("ВЫБЕРИТЕ ТОЧКУ А");
        }
        else if (selectedPoints == 1)
        {
            ShowStatus("ТОЧКА А ВЫБРАНА — ВЫБЕРИТЕ С");
        }
        else if (selectedPoints == 2)
        {
            ShowStatus("А → С ВЫБРАНЫ — ВЫБЕРИТЕ B");
        }
        else if (selectedPoints >= 3)
        {
            selectedPoints = 3;
            ShowStatus("МИССИЯ ГОТОВА");

            if (confirmMissionButton != null)
            {
                confirmMissionButton.gameObject.SetActive(true);
                confirmMissionButton.interactable = true;
            }

            Debug.Log("Все точки выбраны. Кнопка подтверждения миссии активирована.");
        }
    }

    private void ShowStatus(string message)
    {
        if (missionStatus != null)
            missionStatus.text = message;
    }

    private void SetPointColor(Button button, Color color)
    {
        if (button == null)
            return;

        Image image = button.GetComponent<Image>();

        if (image != null)
            image.color = color;
    }

    private void ResetPointColors()
    {
        SetPointColor(pointA, normalColor);
        SetPointColor(pointC, normalColor);
        SetPointColor(pointB, normalColor);
    }

    public void ResetMission()
    {
        missionConfirmed = false;
        selectedPoints = 0;

        if (pointA != null)
            pointA.interactable = true;

        if (pointC != null)
            pointC.interactable = true;

        if (pointB != null)
            pointB.interactable = true;

        if (confirmMissionButton != null)
            confirmMissionButton.interactable = false;

        ResetPointColors();

        UpdateTrajectory();
        UpdateStatus();
    }

    private void OnDestroy()
    {
        if (pointA != null)
            pointA.onClick.RemoveListener(SelectA);

        if (pointC != null)
            pointC.onClick.RemoveListener(SelectC);

        if (pointB != null)
            pointB.onClick.RemoveListener(SelectB);

        if (confirmMissionButton != null)
            confirmMissionButton.onClick.RemoveListener(ConfirmMission);
    }
}