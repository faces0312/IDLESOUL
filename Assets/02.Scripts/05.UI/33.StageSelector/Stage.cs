using TMPro;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int ID;
    //public StageData stageData;

    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Level;
    
    private string stageName;
    private string stageLevel;
    private bool isOpen;

    private void OnEnable()
    {
        //for(int i = 0; i < DataManager.Instance.) Data�� ID ��ȸ�Ͽ� ��ġ �� �ش� ������ ���� �ҷ���
    }

    public void SetStageText()
    {
        Name.text = stageName;
        Level.text = stageLevel;
    }

    public void TouchStage()
    {
        if(isOpen == true)
        {
            //SceneDataManager.Instance.StageData = this.stageData;

        }
    }

    public void MakeIsOpenTrue()
    {
        this.isOpen = true;
    }
}