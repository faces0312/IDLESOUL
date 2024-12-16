using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
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

    public CameraController cameraController;

    public int score;//����
    public bool IsBoss;//���� Boss�� �ʵ忡 �ִ����� üũ�ϴ� ���� 
    public bool isTryBoss;//������ Ʈ���� ������ �ִ���

    public event Action OnEventBossSummon;
    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

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

        //if (!isTryBoss)
        //{
        //UIManager.Instance.ShowUI("StageProgress"); //Debug - ����� ���� UI�Ŵ��� �ʱ�ȭ�Ҷ� �� ���������
        //}

        Debug.Log("GameManager ���� �Ϸ�!!");
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

        
        StageManager.Instance.StageProgressModel.CurCountDataClear();

        int stageID = StageManager.Instance.CurStageID;
        StageManager.Instance.StageSelect(stageID + 1);//���� Stage�� �̵�

        //Utils.StartFadeOut();
        Invoke("NextStage", 3.0f);

    }

    public void NextStage()
    {
        //SceneManager.LoadScene("GameScene_SMS");

        UIManager.Instance.ShowUI("StageProgress");
        DataManager.Instance.SaveUserData(_player.UserData);
        _player.transform.position = Vector3.up; //�÷��̾� ��ġ �ʱ�ȭ

        //StageManager �ʱ�ȭ
        StageManager.Instance.Init();

        //EnemyManager �ʱ�ȭ
        EnemyManager.Instance.Init();

        
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
