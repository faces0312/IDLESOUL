using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    private static string nextScene;
    public GameObject gauge;
    public TextMeshProUGUI percent;
    public Image LoadingSceneImage;
    private Sprite[] Sprites;
    private readonly WaitForSeconds wait1s = new WaitForSeconds(1);

    private void Awake()
    {
        nextScene = SceneDataManager.Instance.NextScene;
        Sprites = Resources.LoadAll<Sprite>("Sprite/LoadingSceneSprite");
    }

    private void OnEnable()
    {
        gauge.transform.DOScaleX(0, 0f);
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
        gauge.transform.DOScaleX(0, 0f);
        while (!loading.isDone)
        {
            yield return null;
            gauge.transform.DOScaleX(loading.progress, 0.3f); 
            percent.text = (gauge.transform.localScale.x * 100).ToString() + " %";
            if (loading.isDone)
            {
                Debug.Log("¿Ï·á!");
            }
            if (loading.progress >= 0.9f)
            {
                gauge.transform.DOScaleX(1f, 1f);
                percent.text = "Load Complete!";
                yield return wait1s;
                loading.allowSceneActivation = true;
                yield break;
            }
        }
    }
}