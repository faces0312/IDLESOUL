using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RecycleScrollX : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject scrollview;
    [SerializeField] private GameObject content;
    private float pastPos;

    private float prefabWidth;
    private float prefabHeight;
    public float leftMargin;
    public float contentSpace;

    public int rectCnt; //ÄÜÅÙÃ÷ ³»ºÎÀÇ ÃÖ´ë °¹¼ö
    public int showCnt; //ÇÑ ÆäÀÌÁö¿¡ º¸¿©Áú °¹¼ö

    private int prevIdx = 0;
    private int totalCnt;

    public List<GameObject> objs = new List<GameObject>();
    public List<float> rectPositions = new List<float>();

    private void Start()
    {
        pastPos = 0;

        prefabWidth = prefab.GetComponent<RectTransform>().sizeDelta.x;
        prefabHeight = prefab.GetComponent<RectTransform>().sizeDelta.y;

        float contentX = (prefabWidth + contentSpace) * rectCnt + leftMargin;
        content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(contentX - scrollview.GetComponent<RectTransform>().sizeDelta.x, content.GetComponent<RectTransform>().sizeDelta.y);
        Debug.Log(content.GetComponent<RectTransform>().sizeDelta);

        totalCnt = showCnt + 2;
        for (int i = 0; i < rectCnt; i++)
        {
            float xPos = i * (prefabWidth + contentSpace) + leftMargin;
            rectPositions.Add(xPos);

            if (i < totalCnt)
            {
                GameObject obj = content.transform.GetChild(i).gameObject;
                obj.GetComponent<RectTransform>().localPosition
                    = new Vector3(xPos, obj.GetComponent<RectTransform>().localPosition.y);

                SetContent(obj, i.ToString());
                objs.Add(obj);
            }
        }
        scrollview.GetComponent<ScrollRect>().onValueChanged.AddListener(GetPosition);
    }

    private void SetContent(GameObject obj, string data)
    {
        obj.GetComponentInChildren<TextMeshProUGUI>().text = data;
    }


    private void GetPosition(Vector2 delta)
    {
        int pageCnt = rectCnt - showCnt; //15 - 5 = 10
        int pageOffset = pageCnt - 2; // 8
        int curPage = pageCnt - (int)Mathf.Round(delta.x * pageCnt);

        float deltaX = pastPos - delta.x;

        if (curPage == 0) return;

        if (deltaX == 0)
        {
        }
        else if (deltaX < 0)
        {
            pastPos = delta.x;

            if (curPage <= pageOffset)
            {
                int temp = pageOffset - curPage;
                for (int i = prevIdx; i < temp + 1; i++)
                {
                    int on = rectCnt - pageOffset + i;

                    Debug.Log($"on {on},curPage {curPage},previdx {prevIdx},i {i}");

                    int idx = i % totalCnt;
                    objs[idx].GetComponent<RectTransform>().localPosition
                        = new Vector3(rectPositions[on], objs[idx].GetComponent<RectTransform>().localPosition.y);
                    SetContent(objs[idx], on.ToString());
                }
                prevIdx = temp;
            }
        }
        else
        {
            pastPos = delta.x;

            if (curPage <= pageOffset + 1)
            {
                int temp = pageOffset + 1 - curPage;
                for (int i = prevIdx; i >= temp; i--)
                {
                    int on = rectCnt - pageOffset + i;

                    Debug.Log($"on {on},curPage {curPage},previdx {prevIdx},i {i}");

                    int idx = i % totalCnt;
                    objs[idx].GetComponent<RectTransform>().localPosition
                        = new Vector3(rectPositions[on - (showCnt + 2)], objs[idx].GetComponent<RectTransform>().localPosition.y);
                    SetContent(objs[idx], (on - (showCnt + 2)).ToString());
                }
                prevIdx = temp;
            }
        }
    }
    private void AddContent(int count)
    {
        float contentX = (prefabWidth + contentSpace) * count;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x + contentX, content.GetComponent<RectTransform>().sizeDelta.y);

        rectCnt += count;

        int passCnt = rectPositions.Count;
        for (int i = passCnt; i < passCnt + count; i++)
        {
            float xPos = -i * (prefabWidth + contentSpace);
            rectPositions.Add(xPos);
        }
    }
}