using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : SingletonDDOL<CoroutineHelper>
{ 
    public void StartCoroutineHelper(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void StopCoroutineHelper(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }
}
