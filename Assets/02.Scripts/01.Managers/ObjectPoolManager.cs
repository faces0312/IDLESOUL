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
    /// 딕셔너리[id]에 풀을 추가, 없다면 생성
    /// </summary>
    /// <param name="id">딕셔너리 키</param>
    /// <param name="pool">추가할 오브젝트 풀</param>
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
    /// 오브젝트 풀의 Queue<GameObject>를 반환
    /// </summary>
    /// <returns>Queue<GameObject></returns>
    public Queue<GameObject> GetPool()
    {
        return Pool;
    }

    /// <summary>
    /// 풀 내부의 게임 오브젝트를 반환 후 다시 큐에 추가
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject GetObject()
    {
        GameObject obj = Pool.Dequeue();
        Pool.Enqueue(obj);
        return obj;
    }

    /// <summary>
    /// 풀 내부의 게임 오브젝트를 반환, 큐에 추가하지 않음
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
    /// 큐의 첫 번째 오브젝트를 SetActive(true) 상태로 만듦
    /// </summary>
    public void SpawnFromPool()
    {
        GameObject obj = Pool.Dequeue();
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }

    /// <summary>
    /// 큐의 첫 번째 오브젝트를 Vector2 pos으로 이동 후 SetActive(true) 상태로 만듦
    /// </summary>
    public void SpawnFromPool(Vector2 pos)
    {
        GameObject obj = Pool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }

    /// <summary>
    /// 큐의 첫 번째 오브젝트를 Vector3 pos으로 이동 후 SetActive(true) 상태로 만듦
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