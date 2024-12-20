using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestInventoryModel : UIModel
{
    public event Action OnInventoryChanged;
    private List<string> items = new List<string>();

    public void AddItem(string item)
    {
        items.Add(item);
        Debug.LogAssertion($"<{item}>획득!");
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(string item)
    {
        if(items.Remove(item))
        {
            Debug.LogAssertion($"<{item}>파괴...");
            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.LogAssertion($"<{item}>는 존재하지 않습니다.");
        }
    }
}
