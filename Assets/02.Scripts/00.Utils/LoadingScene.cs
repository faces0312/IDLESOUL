using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    //private static string nextScene;
    public string nextScene;

    public Image gauge;
    public TextMeshProUGUI percent;
    public Image LoadingSceneImage;
    private Sprite[] Sprites;
    private readonly WaitForSeconds wait1s = new WaitForSeconds(1);

    private Coroutine LoadNextScene;

    private void Awake()
    {
        nextScene = SceneDataManager.Instance.NextScene;
        //nextScene = "GameScene_SMS"; //Test 목적
        Sprites = Resources.LoadAll<Sprite>("Sprite/LoadingSceneSprite");
    }

    private void OnEnable()
    {
        gauge.fillAmount = 0;
        nextScene = SceneDataManager.Instance.NextScene;
        LoadNextScene = StartCoroutine(CoLoading());

        if (nextScene == "")
        {
            Debug.LogAssertion("로딩씬 이후로 진행 할 씬이 없습니다.");
        }
    }

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator CoLoading()
    {
        while (true)
        {
            yield return null;
            AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
            loading.allowSceneActivation = true;
            gauge.fillAmount = 0;
            while (!loading.isDone)
            {
                gauge.fillAmount = loading.progress;
                percent.text = (gauge.fillAmount * 100).ToString() + " %";
                if (loading.progress >= 0.9f)
                {
                    gauge.fillAmount = 1.0f;
                    percent.text = "Load Complete!";
                    yield return wait1s;
                    loading.allowSceneActivation = true;
                    yield break;
                }
                yield return null;
            }
            StopCoroutine(LoadNextScene);
        }
        
    }
}