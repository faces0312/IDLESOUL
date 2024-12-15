using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonDDOL<ObjectPoolManager>
{
    private Dictionary<string, List<ObjectPool>> poolDict;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        poolDict = new Dictionary<string, List<ObjectPool>>();
        ObjectPoolAllClear();
        Debug.Log("ObjectPoolManager Init �Ϸ�!!");
    }

    /// <summary>
    /// ������Ʈ Ǯ ��ųʸ� ������ Ǯ ����Ʈ�� ��ȯ
    /// </summary>
    /// <param name="id">��ųʸ� key</param>
    /// <returns>List<Pool></returns>
    public List<ObjectPool> GetPoolList(string id)
    {
        return poolDict[id];
    }
    /// <summary>
    /// ������Ʈ Ǯ ��ųʸ� ���� Ǯ ����Ʈ���� Ǯ�� ��ȯ
    /// </summary>
    /// <param name="dictId">��ųʸ� key</param>
    /// <param name="poolId">������Ʈ Ǯ ID</param>
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
    /// ��ųʸ�[id]�� Ǯ�� �߰�, ���ٸ� ����
    /// </summary>
    /// <param name="id">��ųʸ� Ű</param>
    /// <param name="pool">�߰��� ������Ʈ Ǯ</param>
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

    private void ObjectPoolAllClear()
    {
        foreach(KeyValuePair<string, List<ObjectPool>> pool in poolDict)
        {
            pool.Value.Clear();
        }
    }

    /* 
    /// <summary>
    /// ������Ʈ Ǯ ��ųʸ� ������ Ǯ ����Ʈ�� ��ȯ
    /// </summary>
    /// <param name="id">��ųʸ� key</param>
    /// <returns>List<Pool></returns>
    public List<ObjectPool> GetPoolList(int id)
    {
        return poolDict[id];
    }


    /// <summary>
    /// ������Ʈ Ǯ ��ųʸ� ���� Ǯ ����Ʈ���� Ǯ�� ��ȯ
    /// </summary>
    /// <param name="dictId">��ųʸ� key</param>
    /// <param name="poolId">������Ʈ Ǯ ID</param>
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
    /// ��ųʸ�[id]�� Ǯ�� �߰�, �ִٸ� ����, ���ٸ� ����
    /// </summary>
    /// <param name="dictId">��ųʸ� Ű</param>
    /// <param name="pool">�߰��� ������Ʈ Ǯ</param>
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
