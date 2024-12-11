using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    [SerializeField] private Button itemButton;
    [SerializeField] private Image itemIconImage; // UI Image ÂüÁ¶
    [SerializeField] private TextMeshProUGUI Name; //

    public void Initiliaze(Item item)
    {
        this.item = item;
        Name.text = item.ItemData.Name;
        itemIconImage.sprite = Resources.Load<Sprite>(item.ItemData.IconPath);
    }
}
