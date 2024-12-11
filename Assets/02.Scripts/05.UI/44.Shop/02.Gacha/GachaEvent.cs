using Enums;

public class GachaEvent : IEventObject
{
    public GachaType type;
    public int count;


    public GachaEvent SetEvent(GachaType type, int count = 1)
    {
        this.type = type;
        this.count = count;
        return this;
    }
}