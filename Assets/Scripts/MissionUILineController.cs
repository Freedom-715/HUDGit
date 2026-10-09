
using UnityEngine;
using UnityEngine.UI;

public class MissionUILineController : MonoBehaviour
{
    [Header("UI точки маршрута")]
    public RectTransform pointA;
    public RectTransform pointC;
    public RectTransform pointB;

    [Header("Объект линии")]
    public RectTransform lineContainer;

    [Header("Настройки линии")]
    public Color lineColor = new Color(0f, 0.65f, 1f, 1f);
    public float lineWidth = 5f;
    [Range(0f, 0.5f)]
    public float arcHeight = 0.12f;
    [Range(10, 100)]
    public int segmentsPerPart = 30;

    private RectTransform[] segments;
    private Image[] segmentImages;
    private int selectedPoints;

    private void Awake()
    {
        if (lineContainer == null)
            lineContainer = GetComponent<RectTransform>();

        int count = segmentsPerPart * 2;
        segments = new RectTransform[count];
        segmentImages = new Image[count];

        for (int i = 0; i < count; i++)
        {
            GameObject segment = new GameObject(
                "LineSegment_" + i,
                typeof(RectTransform),
                typeof(CanvasRenderer),
                typeof(Image)
            );

            segment.transform.SetParent(lineContainer, false);

            RectTransform rect = segment.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0f, 0.5f);
            rect.sizeDelta = new Vector2(0f, lineWidth);

            Image image = segment.GetComponent<Image>();
            image.color = lineColor;
            image.raycastTarget = false;

            segments[i] = rect;
            segmentImages[i] = image;
            segment.SetActive(false);
        }
    }

    public void SetSelectedPoints(int count)
    {
        selectedPoints = Mathf.Clamp(count, 0, 3);
        Redraw();
    }

    public void HideLine()
    {
        selectedPoints = 0;
        Redraw();
    }

    private void LateUpdate()
    {
        Redraw();
    }

    private void Redraw()
    {
        if (lineContainer == null ||
            pointA == null || pointC == null || pointB == null)
        {
            HideSegments();
            return;
        }

        HideSegments();

        if (selectedPoints < 2)
            return;

        Vector2 a = GetLocalPoint(pointA);
        Vector2 c = GetLocalPoint(pointC);
        Vector2 b = GetLocalPoint(pointB);

        int index = 0;

        DrawArc(a, c, ref index);

        if (selectedPoints >= 3)
            DrawArc(c, b, ref index);
    }

    private Vector2 GetLocalPoint(RectTransform point)
    {
        Vector3 worldPosition = point.TransformPoint(point.rect.center);
        Vector3 localPosition =
            lineContainer.InverseTransformPoint(worldPosition);

        return new Vector2(localPosition.x, localPosition.y);
    }

    private void DrawArc(Vector2 start, Vector2 end, ref int index)
    {
        Vector2 previous = start;
        float height = lineContainer.rect.height * arcHeight;

        for (int i = 1; i <= segmentsPerPart; i++)
        {
            float t = (float)i / segmentsPerPart;

            Vector2 current = Vector2.Lerp(start, end, t);
            current.y += Mathf.Sin(t * Mathf.PI) * height;

            if (index < segments.Length)
            {
                SetSegment(index, previous, current);
                index++;
            }

            previous = current;
        }
    }

    private void SetSegment(int index, Vector2 start, Vector2 end)
    {
        RectTransform rect = segments[index];

        Vector2 difference = end - start;
        float length = difference.magnitude;

        rect.anchoredPosition = start;
        rect.sizeDelta = new Vector2(length, lineWidth);

        float angle = Mathf.Atan2(
            difference.y, difference.x
        ) * Mathf.Rad2Deg;

        rect.localRotation = Quaternion.Euler(0f, 0f, angle);
        segmentImages[index].color = lineColor;
        rect.gameObject.SetActive(true);
    }

    private void HideSegments()
    {
        if (segments == null)
            return;

        foreach (RectTransform segment in segments)
        {
            if (segment != null)
                segment.gameObject.SetActive(false);
        }
    }
}