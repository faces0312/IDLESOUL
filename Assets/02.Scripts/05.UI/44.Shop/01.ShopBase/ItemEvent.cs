using Enums;

public class ItemEvent : IEventObject
{
    public int Key;
    public ShopAction ShopAction;

    public ItemEvent(int key)
    {
        this.Key = key;
    }

    public ItemEvent(int key, ShopAction action)
    {
        this.Key = key;
        this.ShopAction = action;
    }

    public int Getkey()
    {
        return this.Key;
    }

    public ItemEvent SetEvent(int key)
    {
        this.Key = key;
        return this;
    }

    public ItemEvent SetEvent(int key, ShopAction action)
    {
        this.Key = key;
        this.ShopAction = action;
        return this;
    }
}