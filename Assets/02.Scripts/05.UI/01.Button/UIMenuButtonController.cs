using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        enterDungeon.onClick.AddListener(() =>
        {
            if(selectDungeon.activeSelf == false)
            {
                selectDungeon.SetActive(true);
            }
        });
        achievement.onClick.AddListener(() =>
        {
            if(achievementObject.activeSelf == false)
            {
                achievementObject.SetActive(true);
            }
        });
        config.onClick.AddListener(() =>
        {
            Debug.LogAssertion("설정 창 오픈"); //설정 창 생기면 추가
        });
        exitGame.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

}
