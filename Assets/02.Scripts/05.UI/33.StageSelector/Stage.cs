using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public string StageName;
    private StageDB stageData;
    private Button click;

    private void Start()
    {
        click = GetComponent<Button>();
        click.onClick.AddListener(SendData);
    }

    private void SendData()
    {
        //Todo : 해당 스테이지 데이터 전송 후 게임씬 이동?
    }
}