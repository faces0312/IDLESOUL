using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class SoulSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SoulInventoryView sounInventoryView;
    private Image outLine;
    private Image icon;
    private Button button;
    private bool isEquip;

    public Soul soul;
    public int index;
    public string soulName;

    private float holdTime = 1f;
    private SoulInfoModel soulInfoModel;

    private Color selectedColor = new Color(0f, 248f / 255f, 159f / 255f);

    private void Awake()
    {
        outLine = transform.GetChild(0).GetComponent<Image>();
        icon = transform.GetChild(1).GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnUpdateThumbnail);
        outLine.color = Color.white;
    }

    private void Start()
    {
        if (soul != null)
            icon.sprite = soul.icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Invoke(nameof(ShowInfo), holdTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke(nameof(ShowInfo));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (soul == null || isEquip) return;

        outLine.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (soul == null || isEquip) return;

        outLine.enabled = false;
    }

    private void ShowInfo()
    {
        OnUpdateThumbnail();
        if (soulInfoModel == null)
        {
            SoulInfoController controller = UIManager.Instance.GetController("SoulInfo") as SoulInfoController;
            soulInfoModel = controller.SoulInfoModel;
        }
        soulInfoModel.soul = soul;
        UIManager.Instance.ShowUI("SoulInfo");
    }

    public void OnUpdateThumbnail()
    {
        if (soul == null) return;

        GameManager.Instance.player.PlayerSouls.SoulInventory.SoulSlot = this;
        sounInventoryView.Sprite = soul.icon;
        sounInventoryView.UpdateUI();
    }

    public void EquipSlot()
    {
        isEquip = true;
        outLine.color = selectedColor;
        outLine.enabled = true;
    }

    public void UnEquipSlot()
    {
        isEquip = false;
        outLine.color = Color.white;
        outLine.enabled = false;
    }
}
