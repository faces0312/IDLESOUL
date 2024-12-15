using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulInventoryView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject invenPanel;
    [SerializeField] private Image thumbnail;

    public Sprite sprite;

    public void Initialize()
    {
        
    }

    public void ShowUI()
    {
        invenPanel.SetActive(true);
    }

    public void HideUI()
    {
        invenPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        // TODO : 클릭 시 썸네일 이미지 변경
        // thumbnail.sprite = sprite;

        //Debug.LogAssertion("소울 인벤토리 UI 업데이트");
    }
}
