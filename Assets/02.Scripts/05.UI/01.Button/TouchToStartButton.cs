using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TouchToStartButton : MonoBehaviour
{
    [SerializeField] private Button DataDeleteButton;
    [SerializeField] private Button LoadStartButton;

    [SerializeField] private GameObject DataDeleteAlarmPopUp;

    private JsonController jsonController = new JsonController();

    private void Awake()
    {
        DataDeleteButton.onClick.AddListener(DataDelete);
        DataDeleteButton.onClick.AddListener(() => UITween.OnClickEffect(DataDeleteButton.gameObject));

        LoadStartButton.onClick.AddListener(LoadStartGame);
        LoadStartButton.onClick.AddListener(() => UITween.OnClickEffect(LoadStartButton.gameObject));

        UITween.ShowUI(DataDeleteButton.gameObject);
        UITween.ShowUI(LoadStartButton.gameObject);
    }

    public void DataDelete()
    {
        if (jsonController.CheckJsonData(Const.JsonUserDataPath))
        {
            jsonController.DeleteJsonData(Const.JsonUserDataPath);
            OpenDataDeletePopUp();
            Invoke("ClodseDataDeletePopUp", 3.0f);
        }
    }
    public void LoadStartGame()
    {
        SceneDataManager.Instance.LoadScene("GameScene_SMS");
    }

    public void OpenDataDeletePopUp()
    {
        UITween.ShowUI(DataDeleteAlarmPopUp);
    }
    public void ClodseDataDeletePopUp()
    {
        UITween.HideUI(DataDeleteAlarmPopUp);
    }
}
