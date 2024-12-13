using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class GameManager : SingletonDDOL<GameManager>
{
    //�÷��̾� ����
    private Player _player;
    public Player player
    {
        get { return _player; }
        set { _player = value; }
    }

    public CameraController cameraController;

    public int score;//����
    public bool IsBoss;//���� Boss�� �ʵ忡 �ִ����� üũ�ϴ� ���� 
    public bool isTryBoss;//������ Ʈ���� ������ �ִ���

    [Header("StageData")]
    //UserData�� ������ �ʵ�
    public int curStageNum;
    public int curChapterNum;

    public UIStageProgressBarModel StageProgressModel;

    public event Action OnEventBossSummon;
    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

    //���� �ʿ� Ȱ��ȭ�Ǿ� �ִ� �� ����Ʈ
    public List<GameObject> enemies = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        _player = Resources.Load<Player>("Prefabs/Player/Player_Origin");
        cameraController = Resources.Load<CameraController>("Prefabs/Managers/CameraController");
    }

    public void Init()
    {
        Instantiate(_player);

        if (cameraController == null)
        {
            cameraController = GetComponentInChildren<CameraController>();
        }

        //StageDB���� �ܺε����� ȣ���Ͽ� �ʱ�ȭ�ϱ�
        //StageProgressModel.Initialize(30);//Debug - ����� ���� UI�Ŵ��� �ʱ�ȭ�Ҷ� �� ���������
        cameraController.Initialize(_player.CamarePivot.transform ,_player.transform);
        _player.enabled = true;

        Utils.fader.FadeTo(1f, 0f, 0.3f).OnComplete(Utils.fader.Release);

        //if (!isTryBoss)
        //{
        //UIManager.Instance.ShowUI("StageProgress"); //Debug - ����� ���� UI�Ŵ��� �ʱ�ȭ�Ҷ� �� ���������
        //}
    }

    [ContextMenu("GameClear")]
    //������ ü���� 0�� �Ǹ� ȣ��
    public void GameClear()
    {
        enemies.Clear();
        //�̺�Ʈ ����� ����
        //GameManager.Instance.OnGameClearEvent += ����Ŭ������������ ������ �� ����
        isTryBoss = false;
        IsBoss = false;
        OnGameClearEvent?.Invoke();
        Debug.Log("���� Ŭ����!!");

        StageProgressModel.CurCountDataClear();

        //Utils.StartFadeOut();
        Invoke("NextStage", 3.0f);
       
    }

    public void NextStage()
    {
        SceneManager.LoadScene("GameScene_SMS");
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
    //�÷��̾��� ü���� 0�� �Ǹ� ȣ��
    public void GameOver()
    {
        enemies.Clear();
        //�̺�Ʈ ����� ����
        //GameManager.Instance.OnGameOverEvent += ���������������� ������ �� ����
        OnGameOverEvent?.Invoke();

        Utils.fader.FadeTo(0f, 1f, 2.0f).OnComplete(Utils.fader.Release);

        StageProgressModel.CurCountDataClear();

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
