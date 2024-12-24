using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public string nextScene;

    public Image gauge;
    public TextMeshProUGUI percent;

    private void Start()
    {
        gauge.fillAmount = 0;
        nextScene = SceneDataManager.Instance.NextScene;
        if (nextScene == "")
        {
            Debug.LogAssertion("로딩씬 이후로 진행 할 씬이 없습니다.");
        }
        else StartCoroutine(LoadingAsync(nextScene));
    }

    IEnumerator LoadingAsync(string sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
        loading.allowSceneActivation = false;

        while (!loading.isDone)
        {
            gauge.fillAmount = loading.progress;
            percent.text = (loading.progress * 100).ToString();
            if (loading.progress >= 0.9f)
            {
                gauge.fillAmount = 1.0f;
                percent.text = "Load Complete!";
                yield return Wait.Wait1s;
                loading.allowSceneActivation = true;
            }
            yield return null;
        }
        yield return null;
    }
}