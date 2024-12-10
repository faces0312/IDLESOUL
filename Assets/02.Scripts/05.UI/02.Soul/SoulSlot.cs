using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class SoulSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image thumbnail;
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

        Debug.Log($"소울 이름 : {soulInfoModel.soul.soulName}");
    }

    private void OnUpdateThumbnail()
    {
        // TODO : 클릭 시 썸네일 이미지 변경
        // thumbnail.sprite = 

        Debug.Log($"썸네일 변경 : {index}번");
    }
}
