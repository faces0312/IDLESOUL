using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuButtonController : MonoBehaviour
{
    //임시로 인스펙터 창에서 관리 중
    //이벤트 매니저에 UI채널을 만들거나 UIManager로 관리 필요
    [SerializeField] private Button enterDungeon;
    [SerializeField] private GameObject selectDungeon;

    [SerializeField] private Button achievement;
    [SerializeField] private GameObject achievementObject;

    [SerializeField] private Button config;
    [SerializeField] private GameObject configObject;

    [SerializeField] private Button exitGame;
    [SerializeField] private Image fadeOutImg;

    private void Start()
    {
        enterDungeon.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowUI("dungeonSelectorController");
        });

        achievement.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowUI<AchievementController>();
        });

        config.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowUI<ConfigController>();
        });

        exitGame.onClick.AddListener(() =>
        {
            DataManager.Instance.SaveUserData(GameManager.Instance.player.UserData); //게임 종료시 데이터를 세이브함 

            fadeOutImg.gameObject.SetActive(true);

            var seq = DOTween.Sequence();

            seq.Append(fadeOutImg.DOFade(1.0f, 1.0f));
            seq.Play().SetUpdate(true);
            seq.OnComplete(() =>
            {
                SceneManager.LoadScene("Ending");
                EnemyManager.Instance.EnemySpawnStop();
                SoundManager.Instance.StopBGM();
            });

            //Application.Quit();
        });

        gameObject.SetActive(false);
    }

}
