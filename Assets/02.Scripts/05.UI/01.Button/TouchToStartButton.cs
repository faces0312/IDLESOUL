using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchToStartButton : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private string LoadScene;
    [SerializeField] private Image TitleImg;

    private void Awake()
    {
        StartButton.onClick.AddListener(StartGame);

        StartCoroutine(StartSceneEffect());
    }

    public void StartGame()
    {
        SceneDataManager.Instance.LoadScene("GameScene_SMS");
    }

    IEnumerator StartSceneEffect()
    {
        TitleImg.material.SetFloat("_FadeAmount", 1.0f);

        float time = TitleImg.material.GetFloat("_FadeAmount");
        while (time >= -1)
        {
            time -= Time.deltaTime;
            TitleImg.material.SetFloat("_FadeAmount", time);

            yield return new WaitForSeconds(0.01f);
        }

    }
}
