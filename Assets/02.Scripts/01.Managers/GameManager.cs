using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Enums;
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
    //플레이어 이동
    public PlayerController playerController;
    public JoyStick joyStick;
    public SkillButton skillButton;
    public SpawnSoulButton spawnSoul;

    public CameraController cameraController;

    public int score;//점수
    public bool IsBoss;//현재 Boss가 필드에 있는지를 체크하는 변수 
    public bool isTryBoss;//보스를 트라이 한적이 있는지
    GameObject gameOverPage;
    private bool isGameOver = false;

    public event Action OnEventBossSummon;
    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

    public bool LoadGame;

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
        Debug.Log("GameManager 세팅 완료!!");
    }

    [ContextMenu("GameClear")]
    //보스의 체력이 0이 되면 호출
    public void GameClear()
    {
        GainExp();
        enemies.Clear();
        //이벤트 등록을 통해
        //GameManager.Instance.OnGameClearEvent += 게임클리어페이지를 선언할 수 있음
        isTryBoss = false;
        IsBoss = false;
        OnGameClearEvent?.Invoke();
        Debug.Log("게임 클리어!!");

        _player.PlayerSFX.PlayClipSFXOneShot((SoundType)UnityEngine.Random.Range(4, 6));

        StageManager.Instance.StageProgressModel.CurCountDataClear();

        _player.UserData.curStageID += 1;
        StageManager.Instance.StageSelect(_player.UserData.curStageID);//다음 Stage로 이동

        //Utils.StartFadeOut();
        Invoke("NextStage", 3.0f);
    }

    public void NextStage()
    {
        if(isGameOver == true) EventManager.Instance.Publish<AchieveEvent>(Channel.Achievement, new AchieveEvent(AchievementType.Kill, ActionType.Player, 1));
        SetGameOverFlag(false);
        //SceneManager.LoadScene("GameScene_SMS");
        Destroy(gameOverPage);

        _player.UserData.curStageID = StageManager.Instance.CurStageID;
        UIManager.Instance.ShowUI<UIStageProgressBarController>();
        DataManager.Instance.SaveUserData(_player.UserData);
        //_player.transform.position = Vector3.up; //플레이어 위치 초기화

        //StageManager 초기화
        StageManager.Instance.Init();
        UIManager.Instance.ShowUI<UIStageLabelController>();

        //EnemyManager 소환 재실행
        EnemyManager.Instance.EnemySpawnStop();

        ObjectPoolManager.Instance.ObjectPoolAllReturn(Const.ENEMY_POOL_KEY);
        ObjectPoolManager.Instance.ObjectPoolAllReturn(Const.ENEMY_EFFECT_POOL_KEY);
        ObjectPoolManager.Instance.ObjectPoolAllReturn(Const.ENEMY_BOSS_POOL_KEY);

        if (player.BaseHpSystem.IsDead)
        {
            player.Respwan();
            //Controller(FSM 세팅)
            player.playerStateMachine.ChangeState(player.playerStateMachine.IdleState);
            player.targetSearch.TargetClear();
        }

        if(isTryBoss == true)
        {
            UIManager.Instance.HideUI<UIStageProgressBarController>();
            UIManager.Instance.tryBoss.SetActive(true);
        }
        else
        {
            UIManager.Instance.tryBoss.SetActive(false);
        }    

        EnemyManager.Instance.EnemySpawnStart();
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
    public void DebugGameOver()
    {
        _player.TakeDamage(new ScottGarland.BigInteger(9999999));
    }

    //플레이어의 체력이 0이 되면 호출
    public void GameOver()
    {
        foreach (GameObject enemyTmp in enemies)
        {
            if (enemyTmp != null)
            {
                enemyTmp.SetActive(false);
            }
        }
        //이벤트 등록을 통해
        //GameManager.Instance.OnGameOverEvent += 게임종료페이지를 선언할 수 있음
        OnGameOverEvent?.Invoke();
        SetGameOverFlag(true);

        //Utils.fader.FadeTo(0f, 1f, 2.0f).OnComplete(Utils.fader.Release);
        gameOverPage = Instantiate(Resources.Load<GameObject>("Prefabs/UI/GameOverPage"), UIManager.Instance.popupCanvas);
        StageManager.Instance.StageProgressModel.CurCountDataClear();

        IsBoss = false;
        //isTryBoss = false; // MVP 이후에 보스 트라이 버튼 따로 구현해야됨
        Invoke("NextStage", 3.0f);
    }

    public void SetGameOverFlag(bool flag)
    {
        isGameOver = flag;
        if (isGameOver)
        {
            DisableUILobbyCanvas();
        }
        else
        {
            EnableUILobbyCanvas();
        }
    }

    private void DisableUILobbyCanvas()
    {
        if (UIManager.Instance != null && UIManager.Instance.uiLobbyCanvas != null)
        {
            UIManager.Instance.uiLobbyCanvas.gameObject.SetActive(false);
        }
    }

    private void EnableUILobbyCanvas()
    {
        if (UIManager.Instance != null && UIManager.Instance.uiLobbyCanvas != null)
        {
            UIManager.Instance.uiLobbyCanvas.gameObject.SetActive(true);
        }
    }

    private void GainExp()
    {
        _player.UserData.Exp += (int)(_player.UserData.MaxExp / 10f);

        if (_player.UserData.Exp >= _player.UserData.MaxExp)
        {
            _player.UserData.Level++;
            _player.UserData.Exp = 0;
        }
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
