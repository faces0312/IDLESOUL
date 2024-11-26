using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static string nextScene;

    private void Awake()
    {
        StartCoroutine(CoLoading());
    }

    IEnumerator CoLoading()
    {
        yield return null;
        AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
        loading.allowSceneActivation = false;
        while (loading.isDone)
        {
            yield return null;
            if(loading.progress == 1)
            {
                loading.allowSceneActivation=true;
                yield break;
            }
        }
    }
}