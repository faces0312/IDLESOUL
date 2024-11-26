using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDDOL<GameManager>
{

    //�÷��̾� ����
    /*public Player _player;
    public Player player
    {
        get { return _player; }
        set { _player = value; }
    }*/
    public int score;//����
    public bool isTryBoss;//������ Ʈ���� ������ �ִ���


    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

    //������Ʈ Ǯ�� ����
    public ObjectPool objectPool;
    //���� �ʿ� Ȱ��ȭ�Ǿ� �ִ� �� ����Ʈ
    public List<GameObject> enemies = new List<GameObject>();

    //������ ü���� 0�� �Ǹ� ȣ��
    public void GameClear()
    {
        //�̺�Ʈ ����� ����
        //GameManager.Instance.OnGameClearEvent += ����Ŭ������������ ������ �� ����
        isTryBoss = false;
        OnGameClearEvent?.Invoke();
    }

    //���� �������� Ȥ�� ���� ���������� 
    public void ClearManager()
    {
        score = 0;

        //_player = null;
        objectPool = null;
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
    }

    public void Test()
    {
        ClearManager();
        SceneManager.LoadScene("TestHS");
    }
}
