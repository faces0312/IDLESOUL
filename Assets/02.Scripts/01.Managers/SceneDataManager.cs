using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneDataManager : SingletonDDOL<SceneDataManager>
{
    public string NextScene;
    public float Modifier;
    public string NickName;


    protected override void Awake()
    {
        base.Awake();
    }   

    public void LoadScene(string nextScene)
    {
        NextScene = nextScene;
        SceneManager.LoadScene("LoadingScene");
        SoundManager.Instance.ChangeBGMForScene("LoadingScene");
    }

    public void SaveSceneData()
    {
        //Todo : ���� �� �� ����, �ε� �� Stage���� ���� ���� ���ٸ� modifier�� �߰��� ������ ����
    }
}