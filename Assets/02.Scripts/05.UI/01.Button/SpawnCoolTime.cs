using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoolTime : MonoBehaviour
{
    public event Action OnUpdateCoolTime;

    public bool IsSpawn { get; set; }

    public void StartCoolTime()
    {
        OnUpdateCoolTime?.Invoke();
    }
}
