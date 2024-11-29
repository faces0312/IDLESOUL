using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    UserData userData;

    private void Start()
    {
        //Model(UserData) 세팅
        userData = DataManager.Instance.UserData;
        base.statHandler.InitializeStats(StatType.Player); //Player 데이터 세팅

        statHandler.currentStat = userData.Status;
    }

    public override void TakeDamage(float damage)
    {

    }

    public override void TakeKnockBack(Vector3 direction, float force)
    {

    }

    public override void Attack()
    {

    }

    public override void Move()
    {

    }

}
