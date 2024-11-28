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

    public void LoadScene(string nextScene)
    {
        NextScene = nextScene;
        SceneManager.LoadScene("LoadingScene");
    }
}