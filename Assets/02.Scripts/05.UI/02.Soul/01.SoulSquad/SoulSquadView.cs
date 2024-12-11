using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulSquadView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject squadPanel;

    public void Initialize()
    {
        
    }

    public void ShowUI()
    {
        squadPanel.SetActive(true);
    }

    public void HideUI()
    {
        squadPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        Debug.LogAssertion("�ҿ� ������ UI ������Ʈ");
    }
}
