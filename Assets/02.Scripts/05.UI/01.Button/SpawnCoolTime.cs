using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoolTime : MonoBehaviour
{
    public event Action OnUpdateCoolTime;

    [SerializeField] SkillButton defaultBtn;
    [SerializeField] SkillButton ultimateBtn;

    public bool IsSpawn { get; set; }

    public void StartCoolTime()
    {
        OnUpdateCoolTime?.Invoke();
        int soulIndex = GameManager.Instance.player.PlayerSouls.SpawnIndex;
        defaultBtn.CurSoulIndex = soulIndex;
        ultimateBtn.CurSoulIndex = soulIndex;
    }
}
