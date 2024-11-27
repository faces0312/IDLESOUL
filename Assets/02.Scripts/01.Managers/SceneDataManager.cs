using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDataManager : SingletonDDOL<SceneDataManager>
{
    public string NextScene;
    private StageData TestStageData;

    public void LoadScene(string nextScene)
    {
        NextScene = nextScene;
        SceneManager.LoadScene("LoadingScene");
    }

    public StageData GetData()
    {
        return TestStageData;
    }
    public void SetData(StageData data)
    {
        TestStageData = data;
    }
}

public struct StageData
{
    public Dictionary<Enums.Stage, List<bool>> Clear;
    public int StageLevel;
    public int StageCount;
    public float Modifier;
    public GameObject Prefab;
}