using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ItemStatusView : MonoBehaviour, IUIBase
{
    public StringBuilder stringBuilder = new StringBuilder();

    [SerializeField] private TextMeshProUGUI curUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI maxUpgradeLeveText;
    [SerializeField] private TextMeshProUGUI UpgradeCostText;
    [SerializeField] private TextMeshProUGUI itemPassiveEffectText;
    [SerializeField] private TextMeshProUGUI itemEquipEffectText;
    [SerializeField] private TextMeshProUGUI itemStackLevel;
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

        PassiveItemStatPrint(selectedItem);
        ActiveItemStatPrint(selectedItem);

        ItemIcon.sprite = Resources.Load<Sprite>(selectedItem.ItemData.IconPath);
    }

    private void PassiveItemStatPrint(Item selectedItem)
    {
        stringBuilder.Clear();

        if (selectedItem.PassiveStat.maxHealth > 0)
        {
            stringBuilder.Append($"HP + {selectedItem.PassiveStat.maxHealth} \n");
        }

        if (selectedItem.PassiveStat.atk > 0)
        {
            stringBuilder.Append($"공격력 + {selectedItem.PassiveStat.atk} \n");
        }

        if (selectedItem.PassiveStat.def > 0)
        {
            stringBuilder.Append($"방어력 + {selectedItem.PassiveStat.def} \n");
        }

        if (selectedItem.PassiveStat.critChance > 0)
        {
            stringBuilder.Append($"크리티컬 + {selectedItem.PassiveStat.critChance}% \n");
        }

        if (selectedItem.PassiveStat.critDamage > 0)
        {
            stringBuilder.Append($"크리티컬 데미지 + {selectedItem.PassiveStat.critDamage}% ");
        }

        itemPassiveEffectText.text = stringBuilder.ToString();
    }

    private void ActiveItemStatPrint(Item selectedItem)
    {
        stringBuilder.Clear();

        if (selectedItem.ItemStat.maxHealth > 0)
        {
            stringBuilder.Append($"HP + {selectedItem.ItemStat.maxHealth} \n");
        }

        if (selectedItem.ItemStat.atk > 0)
        {
            stringBuilder.Append($"공격력 + {selectedItem.ItemStat.atk} \n");
        }

        if (selectedItem.ItemStat.def > 0)
        {
            stringBuilder.Append($"방어력 + {selectedItem.ItemStat.def} \n");
        }

        if (selectedItem.ItemStat.critChance > 0)
        {
            stringBuilder.Append($"크리티컬 + {selectedItem.ItemStat.critChance}% \n");
        }

        if (selectedItem.ItemStat.critDamage > 0)
        {
            stringBuilder.Append($"크리티컬 데미지 + {selectedItem.ItemStat.critDamage}% ");
        }

        itemEquipEffectText.text = stringBuilder.ToString();
    }

    public void UpdateUI()
    {
    }


}
