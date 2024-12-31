using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneDataManager : SingletonDDOL<SceneDataManager>
{
    public string NextScene;
    public float Modifier;
    public float MainStageModifier;

    protected override void Awake()
    {
        base.Awake();
        MainStageModifier = 1;
    }   

    public void LoadScene(string nextScene)
    {
        NextScene = nextScene;
        SceneManager.LoadScene("LoadingScene");
        SoundManager.Instance.ChangeBGMForScene("LoadingScene");
    }

}