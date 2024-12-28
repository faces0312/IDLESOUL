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
        //ToDoCode : ��� ���� 
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

    public void PrintData(Item selectedItem)
    {
        curUpgradeLevelText.text = $"{selectedItem.UpgradeLevel}";
        maxUpgradeLeveText.text = $"{selectedItem.UpgradeLevelMax}";
        UpgradeCostText.text = $"{selectedItem.stack} / {selectedItem.UpgradeStackCount}"; 

        itemPassiveEffectText.text = 
            $"���ݷ� + {selectedItem.ItemStat.atk} " +
            $"���� + {selectedItem.ItemStat.def} " +
            $"HP + {selectedItem.ItemStat.maxHealth} \n" +
            $"ũ��Ƽ�� + {selectedItem.ItemStat.critChance}% " +
            $"ũ��Ƽ�� ������ + {selectedItem.ItemStat.critDamage}% ";

        itemEquipEffectText.text =
        $"���ݷ� + {selectedItem.ItemStat.atk} " +
            $"���� + {selectedItem.ItemStat.def} " +
            $"HP + {selectedItem.ItemStat.maxHealth} \n" +
            $"ũ��Ƽ�� + {selectedItem.ItemStat.critChance}% " +
            $"ũ��Ƽ�� ������ + {selectedItem.ItemStat.critDamage}% ";
        ItemIcon.sprite = Resources.Load<Sprite>(selectedItem.ItemData.IconPath);
    }

    public void UpdateUI()
    {
    }


}
