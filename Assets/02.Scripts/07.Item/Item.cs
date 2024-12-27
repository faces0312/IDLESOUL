using System;


public class Item : BaseItem
{
    public bool IsGain; //플레이어의 아이템 첫 소지여부
    public bool equip; //플레이어의 아이템 장착 여부
    public int stack; //아이템 소지 갯수 

    public override void Initialize(ItemDB data)
    {
        base.Initialize(data);

        //ToDo : 플레이어 유저데이터를 참고하여 갱신할수 있게 해야됨
        IsGain = false;
        stack = 0;
    }


}
