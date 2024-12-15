using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class SoulSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private SoulInventoryView sounInventoryView;
    private Image icon;
    private Button button;

    public Soul soul;
    public int index;
    public string soulName;

    private float holdTime = 1f;
    private SoulInfoModel soulInfoModel;

    private void Awake()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnUpdateThumbnail);
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

        // Debug.Log($"소울 이름 : {soulInfoModel.soul.soulName}");
    }

    public void OnUpdateThumbnail()
    {
        if (soul == null) return;

        GameManager.Instance.player.PlayerSouls.SoulInventory.SoulSlot = this;
        sounInventoryView.Sprite = soul.icon;
        sounInventoryView.UpdateUI();
    }
}
