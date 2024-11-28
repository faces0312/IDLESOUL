using System;
using System.Collections.Generic;
using UnityEngine;

public class GachaView : MonoBehaviour, UIBase
{
    [SerializeField] private GameObject gachaPanel;
    private testSoul testSoul;

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