using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuButtonController : MonoBehaviour
{
    //�ӽ÷� �ν����� â���� ���� ��
    //�̺�Ʈ �Ŵ����� UIä���� ����ų� UIManager�� ���� �ʿ�
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
            Debug.LogAssertion("���� â ����"); //���� â ����� �߰�
        });
        exitGame.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

}
