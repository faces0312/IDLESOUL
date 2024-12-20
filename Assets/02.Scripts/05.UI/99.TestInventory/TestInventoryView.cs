using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestInventoryView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject invenPanel;
    [SerializeField] private TextMeshProUGUI goldText;

    public void Initialize()
    {
        goldText.text = "0";
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
        Debug.LogAssertion("인벤토리 UI 업데이트");
    }
}
