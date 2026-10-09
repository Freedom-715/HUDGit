using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionViewController : MonoBehaviour
{
    [Header("Views")]
    public GameObject pointsView;
    public GameObject trajectoryView;
    public GameObject parametersView;
    public GameObject layersView;
    public GameObject hotkeysView;

    [Header("Buttons")]
    public Button pointsButton;
    public Button trajectoryButton;
    public Button parametersButton;
    public Button layersButton;
    public Button hotkeysButton;

    [Header("Button Texts")]
    public TMP_Text pointsText;
    public TMP_Text trajectoryText;
    public TMP_Text hotkeysText;
    public TMPro.TMP_Text layersText;

    [Header("Button Colors")]
    public Color activeColor = new Color(0f, 1f, 1f, 1f);
    public Color inactiveColor = Color.gray;

    [Header("Map")]
    public GameObject mapArea;

    [Header("Mission Points")]
    public GameObject pointAObject;
    public GameObject pointBObject;
    public GameObject pointCObject;

    [Header("Map Mode")]
    public MissionMapModeController mapModeController;

    private void Start()
    {
        if (pointsButton != null)
            pointsButton.onClick.AddListener(ShowPoints);

        if (trajectoryButton != null)
            trajectoryButton.onClick.AddListener(ShowTrajectory);

        if (parametersButton != null)
            parametersButton.onClick.AddListener(ShowParameters);

        if (layersButton != null)
            layersButton.onClick.AddListener(ShowLayers);

        if (hotkeysButton != null)
            hotkeysButton.onClick.AddListener(ShowHotkeys);

        ShowPoints();
    }

    private void HideAllViews()
    {
        if (pointsView != null)
            pointsView.SetActive(false);

        if (trajectoryView != null)
            trajectoryView.SetActive(false);

        if (parametersView != null)
            parametersView.SetActive(false);

        if (layersView != null)
            layersView.SetActive(false);

        if (hotkeysView != null)
            hotkeysView.SetActive(false);
    }

    private void SetMapAndPointsVisible(bool visible)
    {
        if (mapArea != null)
            mapArea.SetActive(visible);

        if (pointAObject != null)
            pointAObject.SetActive(visible);

        if (pointBObject != null)
            pointBObject.SetActive(visible);

        if (pointCObject != null)
            pointCObject.SetActive(visible);
    }



    private void UpdateTabColors(string activeTab)
    {
        SetTabColor(pointsText, activeTab == "Points");
        SetTabColor(trajectoryText, activeTab == "Trajectory");
        SetTabColor(hotkeysText, activeTab == "Hotkeys");
        SetTabColor(layersText, activeTab == "Layers");
    }

    private void SetTabColor(TMPro.TMP_Text text, bool isActive)
    {
        if (text == null)
            return;

        text.color = isActive ? activeColor : inactiveColor;
    }

    private void SetTextColor(TMPro.TMP_Text label, bool isActive)
    {
        if (label == null)
        {
            Debug.LogWarning("Не назначен текст одной из вкладок!");
            return;
        }

        label.color = isActive ? activeColor : inactiveColor;

        // Обновляем текст немедленно
        label.ForceMeshUpdate();
    }

    private void SetTabColor(Button button, TMP_Text label, bool isActive)
    {
        Color targetColor = isActive ? activeColor : inactiveColor;

        if (label != null)
        {
            label.color = targetColor;

            // Обновляем отображение текста
            label.SetVerticesDirty();
            label.SetAllDirty();
        }

        if (button != null)
        {
            button.transition = Selectable.Transition.None;
        }
    }

    public void ShowPoints()
    {
        HideAllViews();
        SetMapAndPointsVisible(true);

        if (pointsView != null)
            pointsView.SetActive(true);

        UpdateTabColors("Points");

        if (mapModeController != null)
            mapModeController.ShowPlanning();
    }

    public void ShowTrajectory()
    {
        HideAllViews();
        SetMapAndPointsVisible(true);

        if (trajectoryView != null)
            trajectoryView.SetActive(true);

        UpdateTabColors("Trajectory");

        if (mapModeController != null)
            mapModeController.ShowTrajectory();
        Debug.Log("ShowTrajectory: переключаем цвета вкладок");
        UpdateTabColors("Trajectory");
    }

    public void ShowHotkeys()
    {
        HideAllViews();
        SetMapAndPointsVisible(false);

        if (hotkeysView != null)
            hotkeysView.SetActive(true);

        UpdateTabColors("Hotkeys");

        if (mapModeController != null)
            mapModeController.HideMapView();
        Debug.Log("ShowHotkeys: переключаем цвета вкладок");
        UpdateTabColors("Hotkeys");
    }

    public void ShowParameters()
    {
        HideAllViews();
        SetMapAndPointsVisible(false);

        if (parametersView != null)
            parametersView.SetActive(true);

        UpdateTabColors("");

        if (mapModeController != null)
            mapModeController.HideMapView();
    }

    public void ShowLayers()
    {
        HideAllViews();
        SetMapAndPointsVisible(false);

        if (layersView != null)
            layersView.SetActive(true);

        UpdateTabColors("Layers");

        if (mapModeController != null)
            mapModeController.HideMapView();
    }
}