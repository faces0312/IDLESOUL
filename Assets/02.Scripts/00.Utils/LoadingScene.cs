using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    private static string nextScene;
    public Slider gauge;
    public TextMeshProUGUI percent;
    private readonly WaitForSeconds wait1s = new WaitForSeconds(1);

    private void Awake()
    {
        nextScene = SceneDataManager.Instance.NextScene;
        StartCoroutine(CoLoading());
    }

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator CoLoading()
    {
        yield return null;
        AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
        loading.allowSceneActivation = true;
        while (!loading.isDone)
        {
            yield return null;
            gauge.value = loading.progress;
            percent.text = (gauge.value * 100).ToString() + " %";
            if (loading.isDone)
            {
                Debug.Log("¿Ï·á!");
            }
            if (loading.progress >= 0.9f)
            {
                gauge.value = 1f;
                percent.text = "Load Complete!";
                yield return wait1s;
                loading.allowSceneActivation = true;
                yield break;
            }
        }
    }
}