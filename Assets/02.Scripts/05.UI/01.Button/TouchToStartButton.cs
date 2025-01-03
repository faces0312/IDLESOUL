using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TouchToStartButton : MonoBehaviour
{
    [SerializeField] private Button NewStartButton;
    [SerializeField] private Button LoadStartButton;

    [SerializeField] private TextMeshProUGUI StartButton;
    [SerializeField] private TextMeshProUGUI LoadButton;

    private void Awake()
    {
        NewStartButton.onClick.AddListener(NewStartGame);
        NewStartButton.onClick.AddListener(() => UITween.OnClickEffect(NewStartButton.gameObject));

        LoadStartButton.onClick.AddListener(LoadStartGame);
        NewStartButton.onClick.AddListener(() => UITween.OnClickEffect(LoadStartButton.gameObject));

        UITween.ShowUI(NewStartButton.gameObject);
        UITween.ShowUI(LoadStartButton.gameObject);
    }

    public void NewStartGame()
    {
       SceneDataManager.Instance.LoadGameCheck(false);
       SceneDataManager.Instance.LoadScene("GameScene_SMS");
    }
    public void LoadStartGame()
    {
        SceneDataManager.Instance.LoadGameCheck(true);
        SceneDataManager.Instance.LoadScene("GameScene_SMS");
    }


}
