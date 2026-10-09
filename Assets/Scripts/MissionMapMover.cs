using UnityEngine;
using System.Collections;

public class MissionMapMover : MonoBehaviour
{
    [Header("Map")]
    public RectTransform mapArea;

    [Header("Map - Trajectory Position")]
    public float targetMapPosX = 0f;
    public float targetMapPosY = -250f;

    [Header("Map - Trajectory Size")]
    public float targetMapWidth = 600f;
    public float targetMapHeight = 180f;


    [Header("Point A")]
    public RectTransform pointA;

    public float targetAPosX = -200f;
    public float targetAPosY = -250f;

    public float targetAWidth = 50f;
    public float targetAHeight = 50f;


    [Header("Point B")]
    public RectTransform pointB;

    public float targetBPosX = 200f;
    public float targetBPosY = -250f;

    public float targetBWidth = 50f;
    public float targetBHeight = 50f;


    [Header("Point C")]
    public RectTransform pointC;

    public float targetCPosX = 0f;
    public float targetCPosY = -250f;

    public float targetCWidth = 50f;
    public float targetCHeight = 50f;


    [Header("Animation")]
    public float moveDuration = 0.6f;

    private Coroutine moveCoroutine;


    public void MoveToTrajectory()
    {
        if (mapArea == null)
        {
            Debug.LogWarning("MapArea не подключён.");
            return;
        }

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveAll());
    }


    IEnumerator MoveAll()
    {
        // Начальные значения карты
        Vector2 mapStartPos = mapArea.anchoredPosition;
        Vector2 mapStartSize = mapArea.sizeDelta;

        // Начальные значения A
        Vector2 aStartPos = Vector2.zero;
        Vector2 aStartSize = Vector2.zero;

        if (pointA != null)
        {
            aStartPos = pointA.anchoredPosition;
            aStartSize = pointA.sizeDelta;
        }

        // Начальные значения B
        Vector2 bStartPos = Vector2.zero;
        Vector2 bStartSize = Vector2.zero;

        if (pointB != null)
        {
            bStartPos = pointB.anchoredPosition;
            bStartSize = pointB.sizeDelta;
        }

        // Начальные значения C
        Vector2 cStartPos = Vector2.zero;
        Vector2 cStartSize = Vector2.zero;

        if (pointC != null)
        {
            cStartPos = pointC.anchoredPosition;
            cStartSize = pointC.sizeDelta;
        }


        // Конечные значения карты
        Vector2 mapTargetPos = new Vector2(
            targetMapPosX,
            targetMapPosY
        );

        Vector2 mapTargetSize = new Vector2(
            targetMapWidth,
            targetMapHeight
        );


        // Конечные значения A
        Vector2 aTargetPos = new Vector2(
            targetAPosX,
            targetAPosY
        );

        Vector2 aTargetSize = new Vector2(
            targetAWidth,
            targetAHeight
        );


        // Конечные значения B
        Vector2 bTargetPos = new Vector2(
            targetBPosX,
            targetBPosY
        );

        Vector2 bTargetSize = new Vector2(
            targetBWidth,
            targetBHeight
        );


        // Конечные значения C
        Vector2 cTargetPos = new Vector2(
            targetCPosX,
            targetCPosY
        );

        Vector2 cTargetSize = new Vector2(
            targetCWidth,
            targetCHeight
        );


        float time = 0f;


        while (time < moveDuration)
        {
            time += Time.unscaledDeltaTime;

            float t = time / moveDuration;

            t = Mathf.SmoothStep(0f, 1f, t);


            // MAP
            mapArea.anchoredPosition =
                Vector2.Lerp(
                    mapStartPos,
                    mapTargetPos,
                    t
                );

            mapArea.sizeDelta =
                Vector2.Lerp(
                    mapStartSize,
                    mapTargetSize,
                    t
                );


            // A
            if (pointA != null)
            {
                pointA.anchoredPosition =
                    Vector2.Lerp(
                        aStartPos,
                        aTargetPos,
                        t
                    );

                pointA.sizeDelta =
                    Vector2.Lerp(
                        aStartSize,
                        aTargetSize,
                        t
                    );
            }


            // B
            if (pointB != null)
            {
                pointB.anchoredPosition =
                    Vector2.Lerp(
                        bStartPos,
                        bTargetPos,
                        t
                    );

                pointB.sizeDelta =
                    Vector2.Lerp(
                        bStartSize,
                        bTargetSize,
                        t
                    );
            }


            // C
            if (pointC != null)
            {
                pointC.anchoredPosition =
                    Vector2.Lerp(
                        cStartPos,
                        cTargetPos,
                        t
                    );

                pointC.sizeDelta =
                    Vector2.Lerp(
                        cStartSize,
                        cTargetSize,
                        t
                    );
            }


            yield return null;
        }


        // Устанавливаем точные конечные значения

        mapArea.anchoredPosition = mapTargetPos;
        mapArea.sizeDelta = mapTargetSize;


        if (pointA != null)
        {
            pointA.anchoredPosition = aTargetPos;
            pointA.sizeDelta = aTargetSize;
        }


        if (pointB != null)
        {
            pointB.anchoredPosition = bTargetPos;
            pointB.sizeDelta = bTargetSize;
        }


        if (pointC != null)
        {
            pointC.anchoredPosition = cTargetPos;
            pointC.sizeDelta = cTargetSize;
        }
    }
}