using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusInfo : MonoBehaviour
{
    [SerializeField] private Image characterIcon;
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI diamond;
    [SerializeField] private TextMeshProUGUI lv;

    private void Start()
    {
        EventManager.Instance.Subscribe<AchieveEvent>(Enums.Channel.Achievement, Kill);
        gold.text = DataManager.Instance.UserData.Gold.ToString();
        diamond.text = DataManager.Instance.UserData.Diamonds.ToString();
        expBar.fillAmount = DataManager.Instance.UserData.Exp / DataManager.Instance.UserData.MaxExp;
        lv.text = DataManager.Instance.UserData.Level.ToString();
    }

    private void Kill(AchieveEvent arg)
    {
        gold.text = DataManager.Instance.UserData.Gold.ToString();
        diamond.text = DataManager.Instance.UserData.Diamonds.ToString();
        expBar.fillAmount = DataManager.Instance.UserData.Exp / DataManager.Instance.UserData.MaxExp;
        lv.text = DataManager.Instance.UserData.Level.ToString();
    }
}