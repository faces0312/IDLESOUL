//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//internal class
//: MonoBehaviour
//{
//    public AchieveData AData;
//    [SerializeField] private Image icon;
//    [SerializeField] private TextMeshProUGUI aName;

//    private void Start()
//    {
//        if (AData != null)
//        {
//            icon.sprite = Resources.Load<Sprite>(AData.iconPath);
//            aName.text = AData.Name;
//        }

//        this.gameObject.SetActive(false);
//    }

//    public void SetContent(AchieveDB data)
//    {
//        AData = new AchieveData(data);
//        icon.sprite = Resources.Load<Sprite>(AData.iconPath);
//        aName.text = AData.Name;
//    }

//    public void SetContent(AchieveData data)
//    {
//        AData = new AchieveData(data);
//        icon.sprite = Resources.Load<Sprite>(AData.iconPath);
//        aName.text = AData.Name;
//    }
//}