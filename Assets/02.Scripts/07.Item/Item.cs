using System;


public class Item : BaseItem
{
    public bool IsGain; //�÷��̾��� ������ ù ��������
    public bool equip; //�÷��̾��� ������ ���� ����
    public int stack; //������ ���� ���� 

    public override void Initialize(ItemDB data)
    {
        base.Initialize(data);

        //ToDo : �÷��̾� ���������͸� �����Ͽ� �����Ҽ� �ְ� �ؾߵ�
        IsGain = false;
        stack = 0;
    }


}
