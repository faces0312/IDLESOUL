using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class SoulSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SoulInventoryView sounInventoryView;
    [SerializeField] private GameObject cursorDescription;

    private Image outLine;
    private Image icon;
    private Button button;
    private TextMeshProUGUI ownStackText;
    private bool isEquip;

    public Soul soul;
    public int index;
    public string soulName;

    private float holdTime = 1f;
    private SoulInfoModel soulInfoModel;

    private Color selectedColor = new Color(0f, 248f / 255f, 159f / 255f);

    public event Action<SoulSlot> OnSlotChanged;
    public event Action OnUpdateInteractable;

    private void Awake()
    {
        outLine = transform.GetChild(0).GetComponent<Image>();
        icon = transform.GetChild(1).GetComponent<Image>();
        ownStackText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        ownStackText.text = string.Empty;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnUpdateThumbnail);
        outLine.color = Color.white;
    }

    private void OnEnable()
    {
        if (soul != null)
            icon.sprite = soul.icon;
    }

    private void Start()
    {
        if (soul != null)
            icon.sprite = soul.icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (soul == null) return;
        if (!cursorDescription.activeSelf)
            cursorDescription.SetActive(true);

        cursorDescription.GetComponent<CursorDescription>().StartProgress();
        Invoke(nameof(ShowInfo), holdTime);
        OnSlotChanged?.Invoke(this);
        OnUpdateInteractable?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (soul == null) return;

        cursorDescription.GetComponent<CursorDescription>().StopProgress();
        CancelInvoke(nameof(ShowInfo));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (soul == null) return;
       
        cursorDescription.SetActive(true);

        if (isEquip) return;

        outLine.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (soul == null) return;

        cursorDescription.SetActive(false);

        if (isEquip) return;

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

        cursorDescription.SetActive(false);
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

    public void UpdateStack()
    {
        if (soul != null)
        {
            int stack = soul.OwnStack;

            if (soul.OwnStack > 0)
                ownStackText.text = $"+{stack}";
            else
                ownStackText.text = string.Empty;
        }
    }
}
