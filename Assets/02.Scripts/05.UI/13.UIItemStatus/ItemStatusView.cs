using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemStatusView : MonoBehaviour, IUIBase
{
    [SerializeField] private TextMeshProUGUI curUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI maxUpgradeLeveText;
    [SerializeField] private TextMeshProUGUI UpgradeCostText;
    [SerializeField] private TextMeshProUGUI itemPassiveEffectText;
    [SerializeField] private TextMeshProUGUI itemEquipEffectText;
    [SerializeField] private Image ItemIcon;
    [SerializeField] private Button UpgradeBtn;
    [SerializeField] private Button EquipBtn;
    [SerializeField] private Button DisEquipBtn;

    public Button EquipButton { get => EquipBtn; }
    public Button DisEquipButton { get => DisEquipBtn; }
    public Button UpgradeButton { get => UpgradeBtn; }

    public void Initialize()
    {
        //UpgradeBtn.onClick.AddListener(() => Debug.Log("������ ��ȭ"));
        //EquipBtn.onClick.AddListener(() => Debug.Log("������ ����"));
        //DisEquipBtn.onClick.AddListener(() => Debug.Log("������ ��������"));
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void PrintData(ItemDB data)
    {
        curUpgradeLevelText.text = "0";
        maxUpgradeLeveText.text = "100";
        UpgradeCostText.text = "100";

        itemPassiveEffectText.text = 
            $"���ݷ� + {data.Attack} " +
            $"���� + {data.Defence} " +
            $"HP + {data.Health} \n" +
            $"ũ��Ƽ�� + {data.CritChance}% " +
            $"ũ��Ƽ�� ������ + {data.CritDamage}% ";

        itemEquipEffectText.text =
            $"���ݷ� + {data.Attack} " +
            $"���� + {data.Defence} " +
            $"HP + {data.Health} \n" +
            $"ũ��Ƽ�� + {data.CritChance}% " +
            $"ũ��Ƽ�� ������ + {data.CritDamage}% ";
        ItemIcon.sprite = Resources.Load<Sprite>(data.IconPath);
    }

    public void UpdateUI()
    {
    }


}
