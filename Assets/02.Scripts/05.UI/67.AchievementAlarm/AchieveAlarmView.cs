using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchieveAlarmView : MonoBehaviour, IUIBase
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI aName;

    public void Initialize()
    {
        //if (AData != null)
        //{
        //    icon.sprite = Resources.Load<Sprite>(AData.iconPath);
        //    aName.text = AData.Name;
        //}
    }
    public void SetContent(AchieveData data)
    {
        if (data.iconPath != "")
        {
            icon.sprite = Resources.Load<Sprite>(data.iconPath);
        }
        aName.text = data.Name;
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
    }
}