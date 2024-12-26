using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorDescription : MonoBehaviour
{
    [SerializeField] private Image progress;
    private RectTransform cursorUI;

    private float holdTime = 1f;
    private float startTime;
    private float curTime;

    private float fillRate = 1.15f;

    private Coroutine coroutine;

    private void Awake()
    {
        cursorUI = GetComponent<RectTransform>();
    }

    private void Update()
    {
        UpdatePos();
    }

    private void UpdatePos()
    {
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x > Screen.width)
            mousePos.x = Screen.width;
        else if (mousePos.x < 0)
            mousePos.x = 0;

        if (mousePos.y > Screen.height)
            mousePos.y = Screen.height;
        else if (mousePos.y < 0)
            mousePos.y = 0;

        cursorUI.position = mousePos;
    }

    private IEnumerator CoroutineTime()
    {
        startTime = Time.time;
        curTime = Time.time;

        while (curTime - startTime < holdTime)
        {
            curTime += Time.deltaTime;
            progress.fillAmount = (curTime - startTime) * fillRate;

            yield return null;
        }

        progress.fillAmount = 1f;
    }

    public void StartProgress()
    {
        progress.fillAmount = 0f;

        coroutine = StartCoroutine(CoroutineTime());
    }

    public void StopProgress()
    {
        progress.fillAmount = 0f;

        StopCoroutine(coroutine);
    }
}
