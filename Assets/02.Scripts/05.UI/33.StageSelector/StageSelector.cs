using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelector : MonoBehaviour
{
    ObjectPool pool;


    private void Awake()
    {
        
    }

    private void MakePool()
    {
        pool = new ObjectPool();
    }
}
