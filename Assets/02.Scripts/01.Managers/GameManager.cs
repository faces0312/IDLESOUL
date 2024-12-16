using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;


public class GameManager : SingletonDDOL<GameManager>
{
    //플레이어 접근
    private Player _player;
    public Player player
    {
        get { return _player; }
        set { _player = value; }
    }

    public CameraController cameraController;

    public int score;//점수
    public bool IsBoss;//현재 Boss가 필드에 있는지를 체크하는 변수 
    public bool isTryBoss;//보스를 트라이 한적이 있는지

    public event Action OnEventBossSummon;
    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

    //현재 맵에 활성화되어 있는 적 리스트
    public List<GameObject> enemies = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        if (_player == null)
        {
            _player = Instantiate(Resources.Load<Player>("Prefabs/Player/Player_Origin"));
            _player.enabled = true;
        }

        if(cameraController == null)
        {
            cameraController = Instantiate(Resources.Load<CameraController>("Prefabs/Managers/CameraController"));
            cameraController.Initialize(_player.CamarePivot.transform, _player.transform);
        }
      


        //Utils.fader.FadeTo(1f, 0f, 0.3f).OnComplete(Utils.fader.Release);

        //if (!isTryBoss)
        //{
        //UIManager.Instance.ShowUI("StageProgress"); //Debug - 까먹지 말고 UI매니저 초기화할때 꼭 집어넣을것
        //}

        Debug.Log("GameManager 세팅 완료!!");
    }

    [ContextMenu("GameClear")]
    //보스의 체력이 0이 되면 호출
    public void GameClear()
    {
        enemies.Clear();
        //이벤트 등록을 통해
        //GameManager.Instance.OnGameClearEvent += 게임클리어페이지를 선언할 수 있음
        isTryBoss = false;
        IsBoss = false;
        OnGameClearEvent?.Invoke();
        Debug.Log("게임 클리어!!");

        
        StageManager.Instance.StageProgressModel.CurCountDataClear();

        int stageID = StageManager.Instance.CurStageID;
        StageManager.Instance.StageSelect(stageID + 1);//다음 Stage로 이동

        //Utils.StartFadeOut();
        Invoke("NextStage", 3.0f);

    }

    public void NextStage()
    {
        //SceneManager.LoadScene("GameScene_SMS");

        UIManager.Instance.ShowUI("StageProgress");
        DataManager.Instance.SaveUserData(_player.UserData);
        _player.transform.position = Vector3.up; //플레이어 위치 초기화

        //StageManager 초기화
        StageManager.Instance.Init();

        //EnemyManager 초기화
        EnemyManager.Instance.Init();

        
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

    [ContextMenu("GameOver")]
    //플레이어의 체력이 0이 되면 호출
    public void GameOver()
    {
        enemies.Clear();
        //이벤트 등록을 통해
        //GameManager.Instance.OnGameOverEvent += 게임종료페이지를 선언할 수 있음
        OnGameOverEvent?.Invoke();

        //Utils.fader.FadeTo(0f, 1f, 2.0f).OnComplete(Utils.fader.Release);

        StageManager.Instance.StageProgressModel.CurCountDataClear();


        IsBoss = false;
        Invoke("NextStage", 2.0f);
    }

    public void Test()
    {
        ClearManager();
        SceneManager.LoadScene("TestHS");
    }
}
public static class Wait
{
    public readonly static WaitForSeconds Wait1s = new WaitForSeconds(1);
    public readonly static WaitForSeconds Wait3s = new WaitForSeconds(3);
    public readonly static WaitForSeconds Wait5s = new WaitForSeconds(5);
}
