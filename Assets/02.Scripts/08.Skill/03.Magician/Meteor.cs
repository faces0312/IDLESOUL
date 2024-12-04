using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private BigInteger value;
    private float range;

    public void InitSettings(BigInteger value, float range)
    {
        this.value = value;
        this.range = range;
    }

    private void OnDestroy()
    {
        MoveDestroy moveDestroy = GetComponent<MoveDestroy>();
       
        if(moveDestroy.MakedObject.TryGetComponent(out MeteorHit hit))
        {
            hit.InitSettings(value, range);
        }
    }
}
