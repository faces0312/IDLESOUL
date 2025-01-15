using Enums;
using System;
using System.Linq;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//스테이지 맵과 병합 예정
public class Stage : MonoBehaviour
{
    private Button click;
    private TextMeshProUGUI text;
    private Image image;
    int chapter;
    int stage;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        click = GetComponent<Button>();
        image = GetComponent<Image>();
        image.color = Color.gray;
    }

    private void OnEnable()
    {
        Unlock();
    }

    private void SendData()
    {
        StageManager.Instance.StartSelectStage(chapter, stage);
        GameManager.Instance.NextStage();
        Debug.Log($"{chapter} - {stage}");
    }

    public void SetData(int idx)
    {
        chapter = ((idx) / 10) + 1;
        stage = idx % 10 + 1;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{chapter} - {stage}");
        Unlock();
        text.text = sb.ToString();
        if ((DataManager.Instance.StageDB.ItemsList[stage - 1].StageName == StageNameType.Casthle))
        {
            this.image.sprite = Resources.Load<Sprite>("Sprite/Stage/Castle");
        }
        else this.image.sprite = Resources.Load<Sprite>("Sprite/Stage/Forest");
    }

    private void Unlock()
    {
        click.onClick.RemoveListener(SendData);
        if (this.stage <= DataManager.Instance.StageDB.GetByKey(StageManager.Instance.CurStageID).StageNum && 
            this.chapter <= GameManager.Instance.player.UserData.ClearStageCycle)
        {
            image.color = Color.white;
            click.onClick.AddListener(SendData);
        }
        else if(this.chapter < GameManager.Instance.player.UserData.ClearStageCycle)
        {
            image.color = Color.white;
            click.onClick.AddListener(SendData);
        }
        else
        {
            image.color = Color.gray;
            click.onClick.RemoveListener(SendData);
        }
        return;
    }

}