using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class UIPlayerHPDisplayModel : UIModel
{
    private BigInteger curHealth;
    private BigInteger maxHealth;

    public BigInteger CurHealth { get => curHealth; }
    public BigInteger MaxHealth { get => maxHealth; }

    public void Init()
    {
        curHealth = GameManager.Instance.player.StatHandler.CurrentStat.health;
        maxHealth = GameManager.Instance.player.StatHandler.CurrentStat.maxHealth; 
    }

    public void Update()
    {
        curHealth = GameManager.Instance.player.StatHandler.CurrentStat.health;
        maxHealth = GameManager.Instance.player.StatHandler.CurrentStat.maxHealth;
    }    
}
