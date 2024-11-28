using System;
using System.Collections.Generic;
using UnityEngine;

public class GachaView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject gachaPanel;
    [SerializeField] private Sprite ResultSprite;

    private void OnEnable()
    {

    }

    public void HideUI()
    {
        gachaPanel.SetActive(false);
    }

    public void Initialize()
    {
        
    }

    public void ShowUI()
    {
        gachaPanel.SetActive(true);
    }

    public void UpdateUI()
    {
        
    }
}