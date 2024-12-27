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

    //playerInfoPanel의 버튼 Inspctor에 event로 참조되어있음 
    //amount는 Level을 의미
    public void HpLevelUp(int amount)
    {
        model.HpLevelUp(amount);
    }
    //playerInfoPanel의 버튼 Inspctor에 event로 참조되어있음 
    //amount는 Level을 의미
    public void AtkLevelUp(int amount)
    {
        model.AtkLevelUp(amount);
    }
    //playerInfoPanel의 버튼 Inspctor에 event로 참조되어있음 
    //amount는 Level을 의미
    public void DefLevelUp(int amount)
    {
        model.DefLevelUp(amount);
    }
    //playerInfoPanel의 버튼 Inspctor에 event로 참조되어있음 
    //amount는 Level을 의미
    public void ReduceDmgLevelUp(int amount)
    {
        model.ReduceDmgLevelUp(amount);
    }
    //playerInfoPanel의 버튼 Inspctor에 event로 참조되어있음 
    //amount는 Level을 의미
    public void CritChanceLevelUp(int amount)
    {
        model.CritChanceLevelUp(amount);
    }
    //playerInfoPanel의 버튼 Inspctor에 event로 참조되어있음 
    //amount는 Level을 의미
    public void CritDmgLevelUp(int amount)
    {
        model.CritDmgLevelUp(amount);
    }
}
