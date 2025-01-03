using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneDataManager : SingletonDDOL<SceneDataManager>
{
    public string NextScene;
    public float Modifier;


    protected override void Awake()
    {
        base.Awake();
    }   

    public void LoadGameCheck(bool LoadDataCheck)
    {
        GameManager.Instance.LoadGame = LoadDataCheck;
    }

    public void LoadScene(string nextScene)
    {
        NextScene = nextScene;
        SceneManager.LoadScene("LoadingScene");
        SoundManager.Instance.ChangeBGMForScene("LoadingScene");
    }

    public void SaveSceneData()
    {
        //Todo : 현재 씬 값 저장, 로드 시 Stage값이 저장 값과 같다면 modifier에 추가로 곱하지 않음
    }

}