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
        //Todo : �ش� �������� ������ ���� �� ���Ӿ� �̵�?
    }
}