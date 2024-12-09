using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class ObjectPoolManager : SingletonDDOL<ObjectPoolManager>
{
    private Dictionary<string, List<ObjectPool>> poolDict;
    //private Dictionary<int, List<ObjectPool>> poolDict;

    protected override void Awake()
    {
        base.Awake();
        poolDict = new Dictionary<string, List<ObjectPool>>();
    }

   
    /// <summary>
    /// 오브젝트 풀 딕셔너리 내부의 풀 리스트를 반환
    /// </summary>
    /// <param name="id">딕셔너리 key</param>
    /// <returns>List<Pool></returns>
    public List<ObjectPool> GetPoolList(string id)
    {
        return poolDict[id];
    }
    /// <summary>
    /// 오브젝트 풀 딕셔너리 내부 풀 리스트에서 풀을 반환
    /// </summary>
    /// <param name="dictId">딕셔너리 key</param>
    /// <param name="poolId">오브젝트 풀 ID</param>
    /// <returns>ObjectPool</returns>
    public ObjectPool GetPool(string dictId, int poolId)
    {
        if (poolDict.ContainsKey(dictId))
        {
            for (int i = 0; i < poolDict[dictId].Count; i++)
            {
                if (poolDict[dictId][i].Id == poolId)
                    return poolDict[dictId][i];
            }
        }
        return null;
    }
    /// <summary>
    /// 딕셔너리[id]에 풀을 추가, 없다면 생성
    /// </summary>
    /// <param name="id">딕셔너리 키</param>
    /// <param name="pool">추가할 오브젝트 풀</param>
    public void AddPool(string id, ObjectPool pool)
    {
        if (!poolDict.ContainsKey(id))
        {
            List<ObjectPool> objPool = new List<ObjectPool>();
            objPool.Add(pool);
            poolDict.Add(id, objPool);
        }
        else
        {
            poolDict[id].Add(pool);
        }
    }

    /* 
    /// <summary>
    /// 오브젝트 풀 딕셔너리 내부의 풀 리스트를 반환
    /// </summary>
    /// <param name="id">딕셔너리 key</param>
    /// <returns>List<Pool></returns>
    public List<ObjectPool> GetPoolList(int id)
    {
        return poolDict[id];
    }


    /// <summary>
    /// 오브젝트 풀 딕셔너리 내부 풀 리스트에서 풀을 반환
    /// </summary>
    /// <param name="dictId">딕셔너리 key</param>
    /// <param name="poolId">오브젝트 풀 ID</param>
    /// <returns>ObjectPool</returns>
    public ObjectPool GetPool(int dictId, int poolId)
    {
        for (int i = 0; i < poolDict[dictId].Count; i++)
        {
            if (poolDict[dictId][i].Id == poolId)
                return poolDict[dictId][i];
        }
        return null;
    }

    /// <summary>
    /// 딕셔너리[id]에 풀을 추가, 있다면 갱신, 없다면 생성
    /// </summary>
    /// <param name="dictId">딕셔너리 키</param>
    /// <param name="pool">추가할 오브젝트 풀</param>
    public void AddPool(int id, ObjectPool pool)
    {
        if (!poolDict.ContainsKey(dictId))
        {
            List<ObjectPool> objPool = new List<ObjectPool>();
            objPool.Add(pool);
            poolDict.Add(dictId, objPool);
        }
        else if(poolDict[dictId].Contains(pool))
        {
            poolDict[dictId].Remove(pool);
            poolDict[dictId].Add(pool);
        }
        else
        {
            poolDict[dictId].Add(pool);
        }
    }
    */
}
