using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private Button Receive;
    public UnityEvent OnEvent;
    //Todo : �����͸� ��� �����ð��ΰ�
    //Todo : ���� ���� �� óġ, ��ȭ�� ���� �� ȹ�� ���� ��ǥ����
    //�޼� �� ����ǰԲ� ����

    public void Set(Sprite sprite)
    {
        Icon.sprite = sprite;
    }

    



}