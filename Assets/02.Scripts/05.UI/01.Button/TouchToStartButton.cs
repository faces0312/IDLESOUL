using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchToStartButton : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private string LoadScene;

    private void Awake()
    {
        StartButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(LoadScene);
    }
}
