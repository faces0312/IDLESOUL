using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICurGainKeyCountView : MonoBehaviour, IUIBase
{
    [SerializeField] private TextMeshProUGUI curGainKeyText;
    public void Initialize()
    {

    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
        UpdateUI();
    }

    public void UpdateUI()
    {
        curGainKeyText.text = " X "+ GameManager.Instance.player.UserData.DungeonKey.ToString();
    }
}
