using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public AchieveData AData;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI aName;
    [SerializeField] private TextMeshProUGUI aDescription;

    private void Start()
    {
        if(AData != null)
        {
            aName.text = AData.Name;
            aDescription.text = AData.Description;
        }
    }

    public void SetContent(AchieveDB data)
    {
        this.AData = new AchieveData(data);
        aName.text = AData.Name;
        aDescription.text = AData.Description;
        if (AData.isClear == true)
        {
            icon.sprite = Resources.Load<Sprite>(data.iconPath);
        }
        else icon.sprite = null;
    }

    public void SetContent(AchieveData data)
    {
        this.AData = data;
        aName.text = AData.Name;
        aDescription.text = AData.Description;
        if (AData.isClear == true)
        {
            icon.sprite = Resources.Load<Sprite>(data.iconPath);
        }
        else icon.sprite = null;
    }

    public void UpdateContent()
    {
        aName.text = AData.Name;
        aDescription.text = AData.Description;
        if (AData.isClear == true)
        {
            icon.sprite = Resources.Load<Sprite>(AData.iconPath);
        }
        else icon.sprite = null;
    }
}