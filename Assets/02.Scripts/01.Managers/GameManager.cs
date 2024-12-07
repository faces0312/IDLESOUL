using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : SingletonDDOL<GameManager>
{
    //플레이어 접근
    public Player _player;
    public Player player
    {
        get { return _player; }
        set { _player = value; }
    }

    public CinemachineVirtualCamera virtualCamera;

    public int score;//점수
    public bool isTryBoss;//보스를 트라이 한적이 있는지

    public UIStageProgressBarModel StageProgressModel;

    public event Action OnEventBossSummon;
    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

    //오브젝트 풀에 접근
    //public ObjectPool objectPool;
    //현재 맵에 활성화되어 있는 적 리스트
    public List<GameObject> enemies = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        var fader = Instantiate(Resources.Load<Fader>("Prefabs/UI/UIFade"));
        fader.FadeTo(1f, 0f, 0.3f).OnComplete(fader.Release);

        //TodoCode : StageDB에서 데이터값을 받아와야됨
        //BossTriggerEnemySlayerCount = 100;
    }

    private void Start()
    {
        //StageDB에서 외부데이터 호출하여 초기화하기
        StageProgressModel.Initialize(1000);
        
        
        if (!isTryBoss)
        {
            UIManager.Instance.ShowUI("StageProgress");
        }
    }

    //
    public void ToggleFollowTarget(Transform newFollowTr)
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = newFollowTr;
            virtualCamera.LookAt = newFollowTr;

            Invoke("ResetFollowTarget", 3.0f);
        }
    }

    public void ResetFollowTarget()
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = _player.CamarePivot.transform;
            virtualCamera.LookAt = _player.transform;
        }
    }

    private void Update()
    {

    }

    //보스의 체력이 0이 되면 호출
    public void GameClear()
    {
        //이벤트 등록을 통해
        //GameManager.Instance.OnGameClearEvent += 게임클리어페이지를 선언할 수 있음
        isTryBoss = false;
        OnGameClearEvent?.Invoke();
    }

    //다음 스테이지 혹은 현재 스테이지에 
    public void ClearManager()
    {
        score = 0;
        //CurEnemySlayerCount = 0;

        //_player = null;
        //objectPool = null;
        enemies.Clear();

        OnGameClearEvent = null;
        OnGameOverEvent = null;
    }

    
    //플레이어의 체력이 0이 되면 호출
    public void GameOver()
    {
        //이벤트 등록을 통해
        //GameManager.Instance.OnGameOverEvent += 게임종료페이지를 선언할 수 있음
        OnGameOverEvent?.Invoke();

        var fader = Instantiate(Resources.Load<Fader>("Prefabs/UI/UIFade"));
        fader.FadeTo(0f, 1f, 2.0f).OnComplete(fader.Release);

    }

    public void Test()
    {
        ClearManager();
        SceneManager.LoadScene("TestHS");
    }


}
