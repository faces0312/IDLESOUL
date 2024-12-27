using Enums;
using System.ComponentModel;

public class ItemEvent : IEventObject
{
    public int Key;
    public ShopAction ShopAction;
    public ShopType ShopType;
    

    public int Getkey()
    {
        return this.Key;
    }

    public ItemEvent SetEvent(int key)
    {
        this.Key = key;
        return this;
    }

    public ItemEvent SetEvent(int key, ShopAction action, ShopType type)
    {
        this.Key = key;
        this.ShopAction = action;
        this.ShopType = type;
        return this;
    }
}