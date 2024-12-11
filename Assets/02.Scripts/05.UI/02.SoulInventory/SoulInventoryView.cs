using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulInventoryView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject invenPanel;
    
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
        Debug.LogAssertion("�ҿ� �κ��丮 UI ������Ʈ");
    }
}
