using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class ObjectPoolManager : SingletonDDOL<ObjectPoolManager>
{
    private Dictionary<string, List<ObjectPool>> poolDict;

    protected override void Awake()
    {
        base.Awake();
        poolDict = new Dictionary<string, List<ObjectPool>>();
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
    public ObjectPool GetPool(string dictId, string poolId)
    {
        for (int i = 0; i < poolDict[dictId].Count; i++)
        {
            if(poolDict[dictId][i].Id == poolId)
                return poolDict[dictId][i];
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
}
