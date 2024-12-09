using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : SingletonDDOL<GameManager>
{
    //�÷��̾� ����
    public Player _player;
    public Player player
    {
        get { return _player; }
        set { _player = value; }
    }

    public CinemachineVirtualCamera virtualCamera;

    public int score;//����
    public bool IsBoss;//���� Boss�� �ʵ忡 �ִ����� üũ�ϴ� ���� 
    public bool isTryBoss;//������ Ʈ���� ������ �ִ���

    public UIStageProgressBarModel StageProgressModel;

    public event Action OnEventBossSummon;
    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

    //������Ʈ Ǯ�� ����
    //public ObjectPool objectPool;
    //���� �ʿ� Ȱ��ȭ�Ǿ� �ִ� �� ����Ʈ
    public List<GameObject> enemies = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        var fader = Instantiate(Resources.Load<Fader>("Prefabs/UI/UIFade"));
        fader.FadeTo(1f, 0f, 0.3f).OnComplete(fader.Release);

        //TodoCode : StageDB���� �����Ͱ��� �޾ƿ;ߵ�
        //BossTriggerEnemySlayerCount = 100;
    }

    private void Start()
    {
        //StageDB���� �ܺε����� ȣ���Ͽ� �ʱ�ȭ�ϱ�
        StageProgressModel.Initialize(10);
        
        
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

    //������ ü���� 0�� �Ǹ� ȣ��
    public void GameClear()
    {
        //�̺�Ʈ ����� ����
        //GameManager.Instance.OnGameClearEvent += ����Ŭ������������ ������ �� ����
        isTryBoss = false;
        OnGameClearEvent?.Invoke();
        Debug.Log("���� Ŭ����!!");
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

    
    //�÷��̾��� ü���� 0�� �Ǹ� ȣ��
    public void GameOver()
    {
        //�̺�Ʈ ����� ����
        //GameManager.Instance.OnGameOverEvent += ���������������� ������ �� ����
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
public static class Wait
{
    public readonly static WaitForSeconds Wait1s = new WaitForSeconds(1);
    public readonly static WaitForSeconds Wait3s = new WaitForSeconds(3);
    public readonly static WaitForSeconds Wait5s = new WaitForSeconds(5);
}
