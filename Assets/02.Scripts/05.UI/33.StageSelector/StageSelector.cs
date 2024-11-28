using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelector : MonoBehaviour
{
    private ObjectPool pool;
    [SerializeField] private GameObject stageButton;
    [SerializeField] private Transform content;
    private readonly string POOL_DICT_KEY = "StageSelector";
    private readonly string POOL_KEY = "StageSelectors";

    private void OnEnable()
    {
        if(ObjectPoolManager.Instance.GetPool(POOL_DICT_KEY, POOL_KEY) == null)
        {
            MakePool();
        }
        pool = ObjectPoolManager.Instance.GetPool(POOL_DICT_KEY, POOL_KEY);
    }

    private void MakePool()
    {
        ObjectPool pool = new ObjectPool(POOL_KEY, 5, stageButton, content);
        ObjectPoolManager.Instance.AddPool(POOL_DICT_KEY, pool);

    }

    
}
