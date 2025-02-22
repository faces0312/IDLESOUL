using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Enums;


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
    public bool isGameOver = false;
    public bool isCutScene = false;

    public event Action OnEventBossSummon;
    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;
    public event Action OnBossDieEvent;

    //현재 맵에 활성화되어 있는 적 리스트
    public List<GameObject> enemies = new List<GameObject>();
    
    public int curDropItemCount = 0;  //현재 맵에 있는 아이템의 갯수
    public bool LoadData; //현재 게임이 불러온 게임인지 체크하는 변수 

    public bool isGoldDungeon;
    GameObject dungeonEndingPage;

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

        if (StageManager.Instance.Chapter > _player.UserData.BestStageChapter)
        {
            _player.UserData.BestStageChapter = StageManager.Instance.Chapter;
        }
        if (StageManager.Instance.Stage > _player.UserData.BestStageNum)
        {
            _player.UserData.BestStageNum = StageManager.Instance.Stage;
        }
       //Utils.StartFadeOut();
        Invoke("NextStage", 3.0f);
    }

    public void NextStage()
    {
        playerController.isStunned = false;
        if (isGameOver == true) EventManager.Instance.Publish<AchieveEvent>(Channel.Achievement, new AchieveEvent(AchievementType.Kill, ActionType.Player, 1));
        SetGameOverFlag(false);
        //SceneManager.LoadScene("GameScene_SMS");
        Destroy(gameOverPage);

        _player.UserData.ClearStageCycle = StageManager.Instance.Chapter;
        _player.UserData.curStageID = StageManager.Instance.CurStageID;
        UIManager.Instance.ShowUI<UIStageProgressBarController>();
        DataManager.Instance.SaveUserData(_player.UserData);
        //_player.transform.position = Vector3.up; //플레이어 위치 초기화

        //StageManager 초기화
        StageManager.Instance.Init(true);
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
            player.targetSearch.TargetClear();
            player.playerStateMachine.ChangeState(player.playerStateMachine.IdleState);
        }

        if (isTryBoss == true)
        {
            UIManager.Instance.HideUI<UIStageProgressBarController>();
            UIManager.Instance.tryBoss.SetActive(true);
        }
        else
        {
            UIManager.Instance.tryBoss.SetActive(false);
        }

        joyStick.ResumeAutoModeAfterStun();

        enemies.Clear();
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
        

        OnGameOverEvent?.Invoke();
        SetGameOverFlag(true);
        OnBossDieEvent?.Invoke();

        UIManager.Instance.AllHidePopUpUI();
        //Utils.fader.FadeTo(0f, 1f, 2.0f).OnComplete(Utils.fader.Release);
        gameOverPage = Instantiate(Resources.Load<GameObject>("Prefabs/UI/GameOverPage"), UIManager.Instance.popupCanvas);
        StageManager.Instance.StageProgressModel.CurCountDataClear();

        IsBoss = false;

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

    public void GoldDungeonEndilg()
    {
        dungeonEndingPage = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DungeonEndingPage"), UIManager.Instance.popupCanvas);
        _player.PlayerSFX.PlayClipSFXOneShot((SoundType)UnityEngine.Random.Range(4, 6));
        playerController.isStunned = true;
        player.playerStateMachine.ChangeState(player.playerStateMachine.IdleState);
        player.targetSearch.TargetClear();
        Invoke("GoldDungeonSetting", 3f);
    }

    public bool GoldDungeon()
    {
        if (!isGoldDungeon && _player.UserData.DungeonKey >= 1)
        {
            //플레이어가 소지한 던전 입장 재료 소모
            _player.UserData.DungeonKey--;
            UIManager.Instance.ShowUI<UICurGainKeyCountController>();

            //게임 진척도 UI 비활성화
            UIManager.Instance.HideUI<UIStageProgressBarController>();
            UIManager.Instance.tryBoss.SetActive(false); //보스 트라이 버튼 UI 비활성화

            GoldDungeonSetting();

            return true;
        }
        else
        {
            //ToDoCode : 던전 입장재료가 부족하다는 UI 출력 
            return false;
        }
        
    }

    private void GoldDungeonSetting()
    {
        if (dungeonEndingPage != null)
            Destroy(dungeonEndingPage);
        isGoldDungeon = !isGoldDungeon;
        player.BaseHpSystem.IsDead = true;
        NextStage();
    }

    public void OnDestroy()
    {
        _player.UserData.UsersAchieveData.Clear();

        //게임이 종료될때, 즉 게임매니저가 파괴돌때 해당 게임의 업적 데이터를 Json파일에 저장하는 메서드 
        foreach (KeyValuePair<AchievementType,List<AchieveData>> achieveList in AchievementManager.Instance.achievements )
        {
            foreach(AchieveData achieve  in achieveList.Value)
            {
                UserAchieveData data = new UserAchieveData(achieve);
                _player.UserData.UsersAchieveData.Add(data);
            }

            DataManager.Instance.SaveUserData(_player.UserData);
        }
    }
}
public static class Wait
{
    public readonly static WaitForSeconds Wait1s = new WaitForSeconds(1);
    public readonly static WaitForSeconds Wait3s = new WaitForSeconds(3);
    public readonly static WaitForSeconds Wait5s = new WaitForSeconds(5);
}
