using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public AchieveData AData;
    [SerializeField] private Image icon;
    [SerializeField] private Button receive;
    [SerializeField] private TextMeshProUGUI aName;
    [SerializeField] private TextMeshProUGUI aDescription;

    private void Start()
    {
        receive.gameObject.SetActive(false);
        if(AData != null)
        {
            icon.sprite = Resources.Load<Sprite>(AData.iconPath);
            aName.text = AData.Name;
            aDescription.text = AData.Description;
        }
    }

    public void SetContent(AchieveDB data)
    {
        AData = new AchieveData(data);
        icon.sprite = Resources.Load<Sprite>(AData.iconPath);
        aName.text = AData.Name;
        aDescription.text = AData.Description;
        if (AData.isClear == true && receive.onClick == null)
        {
            receive.gameObject.SetActive(true);
            receive.onClick.AddListener(Prize);
        }
    }

    public void SetContent(AchieveData data)
    {
        AData = new AchieveData(data);
        icon.sprite = Resources.Load<Sprite>(AData.iconPath);
        aName.text = AData.Name;
        aDescription.text = AData.Description;
        if (AData.isClear == true && receive.onClick == null)
        {
            receive.gameObject.SetActive(true);
            receive.onClick.AddListener(Prize);
        }
    }

    private void Prize()
    {
        receive.gameObject.SetActive(false);
    }
}