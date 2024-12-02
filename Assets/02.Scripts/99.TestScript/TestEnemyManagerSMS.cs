using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEneTestEnemyManagerSMSmySMS : MonoBehaviour
{
    public TestEnemySMS[] testEnemys;
    int index = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            testEnemys[index].Remove();
            index++;
        }
    }
}
