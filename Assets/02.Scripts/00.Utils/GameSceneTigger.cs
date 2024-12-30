using System;
using UnityEngine;

/*
 호출 시점 정리

씬데이터매니저(-,보류)

TitleSceneTrigger
데이터매니저 (1) DDOL
사운드매니저 (2) DDOL

GameSceneTrigger
오브젝트풀매니저(1) DDOL
게임매니저(Player 내부 포함)(2)  DDOL
Enemy매니저(3)
이벤트매니저 (4) DDOL
도전과제매니저 (5) DDOL
UI매니저 (6) DDOL

DDOL : DontDestroyOnLoad
 */

public class GameSceneTigger : MonoBehaviour
{
    private void Start()
    {
        //TitleSceneTrigger로 이동 예정
        DataManager.Instance.Init();
        SoundManager.Instance.ChangeBGMForScene("GameScene_SMS");
        //SoundManager.Instance.Init();
        //

        //ObjectPoolManager 초기화 및 Scene에서 필요한 오브젝트 풀링 세팅
        ObjectPoolManager.Instance.Init();

        //GameManager 초기화 및
        //GameManager 내부에서 Player와 CameraController(미니맵컨트롤러,가상카메라) 초기화 진행
        GameManager.Instance.Init();

        //StageManager 초기화
        StageManager.Instance.Init();

        //EnemyManager 초기화
        EnemyManager.Instance.Init();

        //EventManager 초기화
        EventManager.Instance.Init();

        //AchievementManager 초기화
        AchievementManager.Instance.Init();

        //UIManager 초기화
        UIManager.Instance.Init();
    }
}