using UnityEngine;
using UnityEngine.UI;
using Enums;

public class ShopSlotComponent : MonoBehaviour
{
    [SerializeField] private Button button;
    public ShopSlot slot;
    private ItemEvent itemEvent;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);
    }

    public void Select()
    {
        EventManager.Instance.Publish<ItemEvent>(Channel.Shop, itemEvent.SetEvent(slot.GetItem().key));
    }
}