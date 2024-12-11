using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulSquadSlot : MonoBehaviour
{
    [SerializeField] private SoulInventory soulInventory;

    private Image thumbnail;
    private Button button;

    public Soul soul;
    public int index;
    public string soulName;

    private void Awake()
    {
        thumbnail = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickSlot);
    }

    private void OnClickSlot()
    {
        soulInventory.SoulSquadSlot = this;
        UIManager.Instance.ShowUI("SoulInventory");
    }
}
