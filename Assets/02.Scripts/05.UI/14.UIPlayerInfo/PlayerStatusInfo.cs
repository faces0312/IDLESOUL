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
        gold.text = GameManager.Instance.player.UserData.Gold.ToString(); ;
        diamond.text = GameManager.Instance.player.UserData.Diamonds.ToString();
        //expBar.fillAmount = DataManager.Instance.UserData.Exp / DataManager.Instance.UserData.MaxExp;
        expBar.fillAmount = GameManager.Instance.player.UserData.Exp / (float)GameManager.Instance.player.UserData.MaxExp;
        lv.text = GameManager.Instance.player.UserData.Level.ToString();

        GameManager.Instance.OnGameClearEvent += OnUpdateStatus;
    }

    private void Update()
    {
        gold.text = GameManager.Instance.player.UserData.Gold.ToString();
        diamond.text = GameManager.Instance.player.UserData.Diamonds.ToString();
    }

    private void Kill(AchieveEvent arg)
    {
        OnUpdateStatus();
    }

    private void OnUpdateStatus()
    {
        expBar.fillAmount = GameManager.Instance.player.UserData.Exp / (float)GameManager.Instance.player.UserData.MaxExp;
        lv.text = GameManager.Instance.player.UserData.Level.ToString();
    }
}