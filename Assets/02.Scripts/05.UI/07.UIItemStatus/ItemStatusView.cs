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
        //ToDoCode : 사용 예정 
        //UpgradeBtn.onClick.AddListener(() => Debug.Log("아이템 강화"));
        //EquipBtn.onClick.AddListener(() => Debug.Log("아이템 장착"));
        //DisEquipBtn.onClick.AddListener(() => Debug.Log("아이템 장착해지"));
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void PrintData(Item selectedItem)
    {
        curUpgradeLevelText.text = $"{selectedItem.UpgradeLevel}";
        maxUpgradeLeveText.text = $"{selectedItem.UpgradeLevelMax}";
        UpgradeCostText.text = $"{selectedItem.stack} / {selectedItem.UpgradeStackCount}"; 

        itemPassiveEffectText.text = 
            $"공격력 + {selectedItem.ItemStat.atk} " +
            $"방어력 + {selectedItem.ItemStat.def} " +
            $"HP + {selectedItem.ItemStat.maxHealth} \n" +
            $"크리티컬 + {selectedItem.ItemStat.critChance}% " +
            $"크리티컬 데미지 + {selectedItem.ItemStat.critDamage}% ";

        itemEquipEffectText.text =
        $"공격력 + {selectedItem.ItemStat.atk} " +
            $"방어력 + {selectedItem.ItemStat.def} " +
            $"HP + {selectedItem.ItemStat.maxHealth} \n" +
            $"크리티컬 + {selectedItem.ItemStat.critChance}% " +
            $"크리티컬 데미지 + {selectedItem.ItemStat.critDamage}% ";
        ItemIcon.sprite = Resources.Load<Sprite>(selectedItem.ItemData.IconPath);
    }

    public void UpdateUI()
    {
    }


}
