using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInfo : UIBase<PlayerInfoModel,PlayerInfoView, PlayerInfoController>
{
    public override void Start()
    {
        model = new PlayerInfoModel();
        base.Start();

        gameObject.SetActive(false);
    }

    //playerInfoPanel�� ��ư Inspctor�� event�� �����Ǿ����� 
    //amount�� Level�� �ǹ�
    public void HpLevelUp(int amount)
    {
        model.HpLevelUp(amount);
    }
    //playerInfoPanel�� ��ư Inspctor�� event�� �����Ǿ����� 
    //amount�� Level�� �ǹ�
    public void AtkLevelUp(int amount)
    {
        model.AtkLevelUp(amount);
    }
    //playerInfoPanel�� ��ư Inspctor�� event�� �����Ǿ����� 
    //amount�� Level�� �ǹ�
    public void DefLevelUp(int amount)
    {
        model.DefLevelUp(amount);
    }
    //playerInfoPanel�� ��ư Inspctor�� event�� �����Ǿ����� 
    //amount�� Level�� �ǹ�
    public void ReduceDmgLevelUp(int amount)
    {
        model.ReduceDmgLevelUp(amount);
    }
    //playerInfoPanel�� ��ư Inspctor�� event�� �����Ǿ����� 
    //amount�� Level�� �ǹ�
    public void CritChanceLevelUp(int amount)
    {
        model.CritChanceLevelUp(amount);
    }
    //playerInfoPanel�� ��ư Inspctor�� event�� �����Ǿ����� 
    //amount�� Level�� �ǹ�
    public void CritDmgLevelUp(int amount)
    {
        model.CritDmgLevelUp(amount);
    }
}
