using System;


public class Item : BaseItem
{
    public bool IsGain; //�÷��̾��� ������ ù ��������
    public bool equip; //�÷��̾��� ������ ���� ����
    public int stack; //������ ���� ���� 

    public int UpgradeLevel; // ������ ���� 
    public int UpgradeLevelMax; //������ ��ȭ �ִ� ����
    public int UpgradeStackCount; //������ ��ȭ ���� �ʿ�� 
    public int UpgradeStatIncreaseRatio; //������ ��ȭ�� ���� ������
    public int UpgradeCostIncreaseRatio; //������ ��ȭ ���� ������
    public override void Initialize(ItemDB data)
    {
        base.Initialize(data);

        //ToDo : �÷��̾� ���������͸� �����Ͽ� �����Ҽ� �ְ� �ؾߵ�
        IsGain = false;
        stack = 0;
        UpgradeLevel = 1;
        UpgradeLevelMax = 10;
        UpgradeStackCount = 1;
        UpgradeStatIncreaseRatio = 2;
        UpgradeCostIncreaseRatio = 3;
    }


}
