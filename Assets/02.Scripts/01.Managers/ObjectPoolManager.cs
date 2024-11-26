using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
        if (poolDict[id] == null)
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

[System.Serializable]
public class ObjectPool : MonoBehaviour
{
    public string Id;
    public int Size;
    public string Path;
    public GameObject Prefab;
    public Queue<GameObject> Pool;

    public ObjectPool()
    {

    }

    public ObjectPool(string id, int size, string path)
    {
        this.Id = id;
        this.Size = size;
        this.Path = path;
    }

    private void Awake()
    {
        Pool = new Queue<GameObject>();
        Prefab = Resources.Load<GameObject>(Path);

        MakePool();
    }

    /// <summary>
    /// ������Ʈ Ǯ�� Queue<GameObject>�� ��ȯ
    /// </summary>
    /// <returns>Queue<GameObject></returns>
    public Queue<GameObject> GetPool()
    {
        return Pool;
    }

    /// <summary>
    /// Ǯ ������ ���� ������Ʈ�� ��ȯ �� �ٽ� ť�� �߰�
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject GetObject()
    {
        GameObject obj = Pool.Dequeue();
        Pool.Enqueue(obj);
        return obj;
    }

    /// <summary>
    /// Ǯ ������ ���� ������Ʈ�� ��ȯ, ť�� �߰����� ����
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject Dequeue()
    {
        return Pool.Dequeue();
    }

    public void MakePool()
    {
        Pool = new Queue<GameObject>();

        for (int i = 0; i < Size; i++)
        {
            GameObject obj = Instantiate(Prefab);
            obj.SetActive(false);
            Pool.Enqueue(obj);
        }
    }

    #region SpawnFromPool OverLoading
    /// <summary>
    /// ť�� ù ��° ������Ʈ�� SetActive(true) ���·� ����
    /// </summary>
    public void SpawnFromPool()
    {
        GameObject obj = Pool.Dequeue();
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }

    /// <summary>
    /// ť�� ù ��° ������Ʈ�� Vector2 pos���� �̵� �� SetActive(true) ���·� ����
    /// </summary>
    public void SpawnFromPool(Vector2 pos)
    {
        GameObject obj = Pool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }

    /// <summary>
    /// ť�� ù ��° ������Ʈ�� Vector3 pos���� �̵� �� SetActive(true) ���·� ����
    /// </summary>
    public void SpawnFromPool(Vector3 pos)
    {
        GameObject obj = Pool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }
    #endregion
}