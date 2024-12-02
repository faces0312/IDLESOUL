using System;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    private StageDB stageData;
    private Button click;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        click = GetComponent<Button>();

        click.onClick.AddListener(SendData);
    }

    private void SendData()
    {
        //���� ���� �� �������� �����Ϳ� ���� ������ ����
    }

    public void SetData(StageDB stageData)
    {
        this.stageData = stageData;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine(stageData.stageName);
        sb.AppendLine(stageData.StageNum.ToString() + stageData.ChapterNum.ToString());

        text.text = sb.ToString();
    }
}