using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDataManager : SingletonDDOL<SceneDataManager>
{
    public string NextScene;
    public int Stage;
    public float Modifier;


    public void LoadScene(string nextScene)
    {
        NextScene = nextScene;
        SceneManager.LoadScene("LoadingScene");
    }
}