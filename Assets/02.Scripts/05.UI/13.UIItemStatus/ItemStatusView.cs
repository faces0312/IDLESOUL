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


    public void Initialize()
    {
        
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
            $"공격력 + {data.Attack} " +
            $"방어력 + {data.Defence} " +
            $"HP + {data.Health} \n" +
            $"크리티컬 + {data.CritChance}% " +
            $"크리티컬 데미지 + {data.CritDamage}% ";

        itemEquipEffectText.text =
            $"공격력 + {data.Attack} " +
            $"방어력 + {data.Defence} " +
            $"HP + {data.Health} \n" +
            $"크리티컬 + {data.CritChance}% " +
            $"크리티컬 데미지 + {data.CritDamage}% ";
        ItemIcon.sprite = Resources.Load<Sprite>(data.IconPath);
    }

    public void UpdateUI()
    {
        Debug.LogAssertion("아이템 정보창 UI 업데이트");
    }


}
