
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{ 
    UserData userData; //
    float invisibleTime; //
    StatHandler statHandler; //스텟 핸들러

    public void Initialize(UserData userData)
    {
        this.userData = userData;

        //statHandler.InitializeStats(StatType.Player);
    }
}
