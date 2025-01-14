using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public int rectCnt; //콘텐츠 내부의 최대 갯수
    public int showCnt; //한 페이지에 보여질 갯수 예시 오브젝트는 이 수보다 2개 많게 미리 만들어놔야함

    private int prevIdx = 0;
    private int totalCnt;

    public List<GameObject> objs = new List<GameObject>();
    public List<float> rectPositions = new List<float>();

    public Action<GameObject, int> SetContent;

    public void Init()
    {
        pastPos = 0;

        prefabWidth = prefab.GetComponent<RectTransform>().sizeDelta.x;
        prefabHeight = prefab.GetComponent<RectTransform>().sizeDelta.y;

        float contentX = (prefabWidth + contentSpace) * rectCnt + leftMargin;
        content.GetComponent<RectTransform>().sizeDelta
            = new Vector2(contentX - scrollview.GetComponent<RectTransform>().sizeDelta.x, content.GetComponent<RectTransform>().sizeDelta.y);

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

                SetContent?.Invoke(obj, i);
                objs.Add(obj);
            }
        }
        scrollview.GetComponent<ScrollRect>().onValueChanged.AddListener(GetPosition);
    }


    private void GetPosition(Vector2 delta)
    {
        int pageCnt = rectCnt - showCnt;
        int pageOffset = pageCnt - 2;
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

                    int idx = i % totalCnt;
                    objs[idx].GetComponent<RectTransform>().localPosition
                        = new Vector3(rectPositions[on], objs[idx].GetComponent<RectTransform>().localPosition.y);
                    SetContent?.Invoke(objs[idx], on);
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

                    int idx = i % totalCnt;
                    objs[idx].GetComponent<RectTransform>().localPosition
                        = new Vector3(rectPositions[on - (showCnt + 2)], objs[idx].GetComponent<RectTransform>().localPosition.y);
                    SetContent?.Invoke(objs[idx], (on - (showCnt + 2)));
                }
                prevIdx = temp;
            }
        }
    }
}