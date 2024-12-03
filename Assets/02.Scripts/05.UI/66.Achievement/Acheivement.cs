using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private Button Receive;
    public UnityEvent OnEvent;
    //Todo : 데이터를 어디서 가져올것인가
    //Todo : 적을 일정 수 처치, 재화를 일정 수 획득 등의 목표설정
    //달성 시 실행되게끔 설계

    public void Set(Sprite sprite)
    {
        Icon.sprite = sprite;
    }

    



}