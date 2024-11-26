using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDDOL<GameManager>
{

    //플레이어 접근
    /*public Player _player;
    public Player player
    {
        get { return _player; }
        set { _player = value; }
    }*/
    public int score;//점수
    public bool isTryBoss;//보스를 트라이 한적이 있는지


    public event Action OnGameOverEvent;
    public event Action OnGameClearEvent;

    //오브젝트 풀에 접근
    public ObjectPool objectPool;
    //현재 맵에 활성화되어 있는 적 리스트
    public List<GameObject> enemies = new List<GameObject>();

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

        //_player = null;
        objectPool = null;
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
    }

    public void Test()
    {
        ClearManager();
        SceneManager.LoadScene("TestHS");
    }
}
