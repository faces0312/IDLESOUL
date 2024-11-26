using UnityEngine.SceneManagement;

public class SceneDataManager : Singleton<SceneDataManager>
{
    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}