using Spine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaGrid : MonoBehaviour
{
    [SerializeField] private GameObject gachaBase;
    [SerializeField] private Button confirm;
    private Sprite None;
    private GachaSlot[] slots;

    private void Start()
    {
        slots = GetComponentsInChildren<GachaSlot>();
        None = Resources.Load<Sprite>("Sprite/SoulSprite/SoulIcon/None");
        confirm.onClick.AddListener(() =>
        {
            gachaBase.SetActive(false);
        });
        this.gameObject.SetActive(false);
    }

    public void SetSlots(List<IGachableDB> dbs)
    {
        foreach (GachaSlot slot in slots)
        {
            slot.key = 0;
            slot.icon.sprite = None;
            slot.gameObject.SetActive(false);
        }
        for (int i = 0; i < dbs.Count; i++)
        {
            slots[i].gameObject.SetActive(true);

            if (dbs[i].GetType() == typeof(ItemDB))
                slots[i].SetContentItem(dbs[i].GetKey());

            else if (dbs[i].GetType() == typeof(SoulDB))
                slots[i].SetContentSoul(dbs[i].GetKey());

            else
            {
                slots[i].key = 0;
                slots[i].icon.sprite = None;
            }
        }
    }
}
