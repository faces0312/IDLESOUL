using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Enums;
using static UnityEditor.Experimental.GraphView.GraphView;


public class GameManager : SingletonDDOL<GameManager>
{
    //�÷��̾� ����
    private Player _player;
    public Player player
    {
        get { return _player; }
        set { _player = value; }
    }
    //�÷��̾� �̵�
    public PlayerController playerController;
    public JoyStick joyStick;
    public SkillButton skillButton;
    public SpawnSoulButton spawnSoul;

    public CameraController cameraController;

    public int score;//����
    public bool IsBoss;//���� Boss�� �ʵ忡 �ִ����� üũ�ϴ� ���� 
    public bool isTryBoss;//������ Ʈ���� ������ �ִ���
    GameObject gameOverPage;
    private bool isGameOver = false;

    public event Action OnEventBossSummon;
    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

    public bool LoadGame;

    //���� �ʿ� Ȱ��ȭ�Ǿ� �ִ� �� ����Ʈ
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
        Debug.Log("GameManager ���� �Ϸ�!!");
    }

    [ContextMenu("GameClear")]
    //������ ü���� 0�� �Ǹ� ȣ��
    public void GameClear()
    {
        GainExp();
        enemies.Clear();
        //�̺�Ʈ ����� ����
        //GameManager.Instance.OnGameClearEvent += ����Ŭ������������ ������ �� ����
        isTryBoss = false;
        IsBoss = false;
        OnGameClearEvent?.Invoke();
        Debug.Log("���� Ŭ����!!");

        _player.PlayerSFX.PlayClipSFXOneShot((SoundType)UnityEngine.Random.Range(4, 6));

        StageManager.Instance.StageProgressModel.CurCountDataClear();

        _player.UserData.curStageID += 1;
        StageManager.Instance.StageSelect(_player.UserData.curStageID);//���� Stage�� �̵�

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
        //_player.transform.position = Vector3.up; //�÷��̾� ��ġ �ʱ�ȭ

        //StageManager �ʱ�ȭ
        StageManager.Instance.Init();
        UIManager.Instance.ShowUI<UIStageLabelController>();

        //EnemyManager ��ȯ �����
        EnemyManager.Instance.EnemySpawnStop();

        ObjectPoolManager.Instance.ObjectPoolAllReturn(Const.ENEMY_POOL_KEY);
        ObjectPoolManager.Instance.ObjectPoolAllReturn(Const.ENEMY_EFFECT_POOL_KEY);
        ObjectPoolManager.Instance.ObjectPoolAllReturn(Const.ENEMY_BOSS_POOL_KEY);

        if (player.BaseHpSystem.IsDead)
        {
            player.Respwan();
            //Controller(FSM ����)
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

    //���� �������� Ȥ�� ���� ���������� 
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

    //�÷��̾��� ü���� 0�� �Ǹ� ȣ��
    public void GameOver()
    {
        foreach (GameObject enemyTmp in enemies)
        {
            if (enemyTmp != null)
            {
                enemyTmp.SetActive(false);
            }
        }
        //�̺�Ʈ ����� ����
        //GameManager.Instance.OnGameOverEvent += ���������������� ������ �� ����
        OnGameOverEvent?.Invoke();
        SetGameOverFlag(true);

        //Utils.fader.FadeTo(0f, 1f, 2.0f).OnComplete(Utils.fader.Release);
        gameOverPage = Instantiate(Resources.Load<GameObject>("Prefabs/UI/GameOverPage"), UIManager.Instance.popupCanvas);
        StageManager.Instance.StageProgressModel.CurCountDataClear();

        IsBoss = false;
        //isTryBoss = false; // MVP ���Ŀ� ���� Ʈ���� ��ư ���� �����ؾߵ�
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
